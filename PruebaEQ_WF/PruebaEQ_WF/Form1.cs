using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Components.RenderTree;
using PruebaEQ_WF.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.PortableExecutable;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Path = System.IO.Path;
namespace PruebaEQ_WF
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer _timer;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();

            _timer = new System.Timers.Timer(60000);
            _timer.Elapsed += async (s, ev) => await ProccesFilesAsync(); // Ejecuta el método
            _timer.AutoReset = true;
            _timer.Enabled = true;

        }
        private async Task ProccesFilesAsync()
        {
                Console.WriteLine("Revisando carpeta...");

                string folder = @"C:\PruebaEQ";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                var Files = Directory.GetFiles(folder, "*.pdf");
                List<DocKey> keys = [];
                if (Files.Length > 0)
                {
                    keys = await GetDocKeys();
                }

            foreach (var file in Files)
            {
                var splitfile = file.Split("\\");
                var namefile = "\\" + splitfile[splitfile.Length - 1];
                string ExtractedText = ExtractTextFromPdf(file);
                bool founded = false;
                string docName = "";
                foreach (var key in keys)
                {
                    if (ExtractedText.Contains(key.Key, StringComparison.OrdinalIgnoreCase))
                    {
                        founded = true;
                        docName = key.DocName+".pdf";
                        break;
                    }
                }

                string Proccessed = @"C:\PruebaEQ\OSC";
                if (!Directory.Exists(Proccessed))
                {
                    Directory.CreateDirectory(Proccessed);
                }

                string Unknown = @"C:\PruebaEQ\UNKNOWN";
                if (!Directory.Exists(Unknown))
                {
                    Directory.CreateDirectory(Unknown);
                }

                var rutaDestino = founded ? Proccessed + "\\" + docName : Unknown  + namefile;

                if (!Path.Exists(rutaDestino))
                {
                    File.Move(file, rutaDestino); 
                    LogProcces logProcces = new();
                    var splitf = rutaDestino.Split("\\");
                    var newname = founded ? rutaDestino.Split("\\")[splitf.Length - 1] : null;
                    logProcces.Status = founded ? nameof(Proccessed) : nameof(Unknown);
                    logProcces.NewFileName = newname;
                    logProcces.OriginalFileName = file.Split("\\")[splitfile.Length - 1];
                    await EnviarLogAsync(logProcces );
                }

            }
        }

        public async Task EnviarLogAsync(LogProcces log)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7209"); // Cambia al puerto correcto de tu API
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("/api/logprocces", log); // Ajusta la ruta a la de tu endpoint real

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Log enviado correctamente");
                }
                else
                {
                    Console.WriteLine("Error al enviar el log: " + response.StatusCode);
                }
            }
        }

        private static string ExtractTextFromPdf(string uriPdf)
        {
            using (PdfReader reader = new PdfReader(uriPdf))
            {
                string text = "";
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text += PdfTextExtractor.GetTextFromPage(reader, i);
                }
                return text;
            }
        }
        private async Task<List<DocKey>> GetDocKeys()
        {
            using (HttpClient client = new())
            {
                client.BaseAddress = new Uri("https://localhost:7209"); 
                HttpResponseMessage response = await client.GetAsync("/api/dockeys");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<DocKey>>();
                }
                return [];
            }
        }


    }
}
