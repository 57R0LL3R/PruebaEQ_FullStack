namespace PruebaEQ_WF
{
    // Punto de entrada de la aplicaci�n de escritorio.
    // Solo inicializa y ejecuta el formulario principal en segundo plano.

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }

}