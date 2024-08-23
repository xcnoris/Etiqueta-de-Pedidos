using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etiqueta_de_Pedidos.Metodos
{
    internal static class MetodosGerais
    {
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

        static MetodosGerais()
        {
            // Garantir que o diretório de logs exista
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
        }

        private static string GetLogFilePath(string logType)
        {
            return Path.Combine(LogDirectory, $"log-{logType}-{DateTime.Now:dd-MM-yyyy}.txt");
        }

        public static void RegistrarInicioLog(string logType)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(GetLogFilePath(logType), true))
                {
                    string logEntry = $"======================================> Inicio do Log <======================================";
                    sw.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                // Tratar exceções relacionadas ao log
                MessageBox.Show($"Erro ao registrar log: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void RegistrarLog(string logType, string mensagem)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(GetLogFilePath(logType), true))
                {
                    string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensagem}";
                    sw.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                // Tratar exceções relacionadas ao log
                MessageBox.Show($"Erro ao registrar log: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void RegistrarFinalLog(string logType)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(GetLogFilePath(logType), true))
                {
                    string logEntry = $"\n======================================>   Fim do Log  <======================================\n";
                    sw.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                // Tratar exceções relacionadas ao log
                MessageBox.Show($"Erro ao registrar log: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
