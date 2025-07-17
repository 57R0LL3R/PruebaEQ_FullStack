using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using PruebaEQ_WF.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Path = System.IO.Path;
namespace PruebaEQ_WF
{
    // Aplicación de escritorio en segundo plano (sin ventana visible).
    // Cada 60 segundos revisa una carpeta local con archivos PDF, los analiza y los clasifica.
    // Además, se conecta al backend para obtener claves, renombrar archivos y registrar logs.

    public partial class Form1 : Form
    {
        private System.Timers.Timer _timer;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();

            _timer = new System.Timers.Timer(60000);  // Cada 60 segundos
            _timer.Elapsed += async (s, ev) => await ProccesFilesAsync();
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private async Task ProccesFilesAsync()
        {
            await ValidateToken();  // Asegura que el token JWT está activo
            string folder = @"C:\PruebaEQ";

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var files = Directory.GetFiles(folder, "*.pdf");
            List<DocKey> keys = files.Length > 0 ? await GetDocKeys() : [];

            foreach (var file in files)
            {
                string text = ExtractTextFromPdf(file);
                bool found = false;
                string newName = "";

                foreach (var key in keys)
                {
                    if (text.Contains(key.Key, StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        newName = key.DocName + ".pdf";
                        break;
                    }
                }

                string targetFolder = found ? @"C:\PruebaEQ\OSC" : @"C:\PruebaEQ\UNKNOWN";
                if (!Directory.Exists(targetFolder))
                    Directory.CreateDirectory(targetFolder);

                string destPath = Path.Combine(targetFolder, Path.GetFileName(file));
                if (!File.Exists(destPath))
                {
                    File.Move(file, destPath);

                    var log = new LogProcces
                    {
                        Status = found ? "Processed" : "Unknown",
                        NewFileName = found ? newName : null,
                        OriginalFileName = Path.GetFileName(file)
                    };

                    await EnviarLogAsync(log);
                }
            }
        }

        // Extrae texto de un PDF usando iTextSharp
        private static string ExtractTextFromPdf(string uriPdf)
        {
            using var reader = new PdfReader(uriPdf);
            string text = "";
            for (int i = 1; i <= reader.NumberOfPages; i++)
                text += PdfTextExtractor.GetTextFromPage(reader, i);
            return text;
        }

        // Obtiene las claves de documentos desde el backend
        private async Task<List<DocKey>> GetDocKeys()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7209");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionUser.Token);

            var res = await client.GetAsync("/api/dockeys");
            return res.IsSuccessStatusCode
                ? await res.Content.ReadFromJsonAsync<List<DocKey>>()
                : [];
        }

        // Envia un log de proceso al backend
        public async Task EnviarLogAsync(LogProcces log)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7209");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionUser.Token);

            var res = await client.PostAsJsonAsync("/api/logprocces", log);
            Console.WriteLine(res.IsSuccessStatusCode ? "Log enviado correctamente" : $"Error: {res.StatusCode}");
        }

        // Valida que el token aún sea válido
        public static async Task ValidateToken()
        {
            if (!string.IsNullOrEmpty(SessionUser.Token))
            {
                using var httpClient = new HttpClient();
                var res = await httpClient.GetAsync("https://localhost:7209/api/logprocces");
                if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    await CreateToken();
            }
            else
            {
                await CreateToken();
            }
        }

        // Solicita un nuevo token con credenciales de servicio
        public static async Task CreateToken()
        {
            using var httpClient = new HttpClient();
            var res = await httpClient.PostAsJsonAsync("https://localhost:7209/api/auth/login",
                new User { Usuario = "Service@email.com", Contrasena = "Servicio123" });

            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadFromJsonAsync<TokenResponse>();
                SessionUser.Token = result.Token;
            }
        }
    }

}
