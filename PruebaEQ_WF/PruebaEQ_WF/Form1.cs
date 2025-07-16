using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using PruebaEQ_WF.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.PortableExecutable;
using System.Timers;
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
            try
            {
                Console.WriteLine("Revisando carpeta...");

                string carpeta = @"C:\PruebaEQ";
                var archivos = Directory.GetFiles(carpeta, "*.pdf");

                foreach (var archivo in archivos)
                {
                    string textoExtraido = ExtractTextFromPdf(archivo);
                    var claves = await ObtenerClavesDesdeApi();

                    bool encontrada = false;
                    string docName = "";

                    foreach (var clave in claves)
                    {
                        if (textoExtraido.Contains(clave.Key, StringComparison.OrdinalIgnoreCase))
                        {
                            encontrada = true;
                            docName = clave.DocName;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al procesar archivos: " + ex.Message);
            }
        }


        private static string ExtractTextFromPdf(string ruta)
        {
            using (PdfReader reader = new PdfReader(ruta))
            {
                string texto = "";
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    texto += PdfTextExtractor.GetTextFromPage(reader, i);
                }
                return texto;
            }
        }
        private async Task<List<DocKey>> ObtenerClavesDesdeApi()
        {
            using (HttpClient client = new())
            {
                client.BaseAddress = new Uri("https://localhost:7209"); 
                HttpResponseMessage response = await client.GetAsync("/api/dockey");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<DocKey>>();
                }
                return [];
            }
        }


    }
}
