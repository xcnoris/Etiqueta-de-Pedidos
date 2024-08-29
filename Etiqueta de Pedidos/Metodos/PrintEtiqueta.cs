using Etiqueta_de_Pedidos.Modelos;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace Etiqueta_de_Pedidos.Metodos
{
    internal class PrintEtiqueta
    {
        public string Mensagem;
        public bool Status;

        public PrintEtiqueta()
        {
            Status = true;
        }
        internal void ExecutarImpressao(string codigoFonteEtiqueta, DadosImpressao dadosImpressao, DadosExibirInImpressao dadosExibirImpressao)
        {
            try
            {
                // Código da etiqueta (comandos específicos da impressora)
                string codigoEtiqueta = codigoFonteEtiqueta;

                string cliente = dadosExibirImpressao.ExibirCliente + dadosImpressao.Cliente ;
                string dataCompra = dadosExibirImpressao.ExibirDataCompra + dadosImpressao.DataCompra ;
                string numTransacao = dadosExibirImpressao.ExibirNumTransacao + dadosImpressao.NumTransacao ;
                string produto = dadosExibirImpressao.ExibirProduto + dadosImpressao.Produto;
                string vendedor = dadosExibirImpressao.ExibirVendedor + dadosImpressao.Vendedor;
                string tamanho = dadosExibirImpressao.ExibirTamanho + dadosImpressao.Tamanho;
                string observacao = dadosExibirImpressao.ExibirObservacao + dadosImpressao.Observacao;

                // Realizar a substituição das variáveis no código da etiqueta
                codigoEtiqueta = codigoEtiqueta.Replace("<Cliente>", cliente);
                codigoEtiqueta = codigoEtiqueta.Replace("<DataCompra>", dataCompra);
                codigoEtiqueta = codigoEtiqueta.Replace("<NumTransacao>", numTransacao);
                codigoEtiqueta = codigoEtiqueta.Replace("<Produto>", produto);
                codigoEtiqueta = codigoEtiqueta.Replace("<Vendedor>", vendedor);
                codigoEtiqueta = codigoEtiqueta.Replace("<Tamanho>", tamanho);
                codigoEtiqueta = codigoEtiqueta.Replace("<Observacao>", observacao);
                codigoEtiqueta = codigoEtiqueta.Replace("<Vendedor>", observacao);


                // Exibir caixa de diálogo para o usuário selecionar a impressora
                PrintDialog printDialog = new PrintDialog();
                printDialog.PrinterSettings = new PrinterSettings();

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    bool result = RawPrinterHelper.SendStringToPrinter(printDialog.PrinterSettings.PrinterName, codigoEtiqueta);
                    if (result)
                    {
                        Status = true;
                        Mensagem = "Etiqueta impressa com sucesso!";
                    }
                    else
                    {
                        Status = false;
                        Mensagem = "Falha ao imprimir a etiqueta.";
                    }
                }
            }
            catch (Exception ex)
            {
                Status = false;
                Mensagem += ex.Message;
                MessageBox.Show($"Erro ao tentar imprimir: {ex.Message}");
            }
        }

        public static class RawPrinterHelper
        {

            [DllImport("winspool.Drv", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool OpenPrinter(string printerName, out IntPtr hPrinter, IntPtr pd);

            [DllImport("winspool.Drv", SetLastError = true)]
            public static extern bool ClosePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", SetLastError = true)]
            public static extern bool StartDocPrinter(IntPtr hPrinter, int level, IntPtr di);

            [DllImport("winspool.Drv", SetLastError = true)]
            public static extern bool EndDocPrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", SetLastError = true)]
            public static extern bool StartPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", SetLastError = true)]
            public static extern bool EndPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", SetLastError = true)]
            public static extern bool WritePrinter(IntPtr hPrinter, IntPtr buffer, int bufferLength, out int bytesWritten);

            public static bool SendStringToPrinter(string printerName, string data)
            {
                IntPtr pBytes = IntPtr.Zero;
                IntPtr diPtr = IntPtr.Zero;
                try
                {
                    int dwCount = (data.Length + 1) * Marshal.SystemMaxDBCSCharSize;
                    pBytes = Marshal.StringToCoTaskMemAnsi(data);

                    DOCINFOA di = new DOCINFOA
                    {
                        pDocName = "Etiqueta",
                        pDataType = "RAW"
                    };

                    diPtr = Marshal.AllocHGlobal(Marshal.SizeOf(di));
                    Marshal.StructureToPtr(di, diPtr, false);

                    bool success = SendBytesToPrinter(printerName, pBytes, dwCount);
                    MetodosGerais.RegistrarLog("Impressão", $"Sucesso no envio da string para a impressora!");

                    return success;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao enviar string para a impressora: {ex.Message}");
                    MetodosGerais.RegistrarLog("Impressão", $"Erro ao enviar string para a impressora: {ex.Message}");

                    return false;
                }
                finally
                {
                    if (pBytes != IntPtr.Zero)
                        Marshal.FreeCoTaskMem(pBytes);
                    if (diPtr != IntPtr.Zero)
                        Marshal.FreeHGlobal(diPtr);
                }
            }

            private static bool SendBytesToPrinter(string printerName, IntPtr buffer, int bufferLength)
            {
                IntPtr hPrinter = IntPtr.Zero;
                IntPtr diPtr = IntPtr.Zero;
                bool success = false;

                try
                {
                    if (OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
                    {
                        DOCINFOA di = new DOCINFOA
                        {
                            pDocName = "Etiqueta",
                            pDataType = "RAW"
                        };

                        diPtr = Marshal.AllocHGlobal(Marshal.SizeOf(di));
                        Marshal.StructureToPtr(di, diPtr, false);

                        if (StartDocPrinter(hPrinter, 1, diPtr))
                        {
                            if (StartPagePrinter(hPrinter))
                            {
                                success = WritePrinter(hPrinter, buffer, bufferLength, out int bytesWritten);
                                EndPagePrinter(hPrinter);
                                MetodosGerais.RegistrarLog("Impressão", $"Impressora aberta");
                            }
                            EndDocPrinter(hPrinter);
                        }
                    }
                    else
                    {
                        MetodosGerais.RegistrarLog("Impressão", $"Erro ao abrir a impressora");
                        MessageBox.Show("Erro ao abrir a impressora.");
                    }
                }
                catch (Exception ex)
                {
                    MetodosGerais.RegistrarLog("Impressão", $"Erro ao enviar bytes para a impressora: {ex.Message}");
                    MessageBox.Show($"Erro ao enviar bytes para a impressora: {ex.Message}");
                }
                finally
                {
                    if (diPtr != IntPtr.Zero)
                        Marshal.FreeHGlobal(diPtr);

                    if (hPrinter != IntPtr.Zero)
                        ClosePrinter(hPrinter);
                    

                }

                return success;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public class DOCINFOA
            {
                public string pDocName;
                public string pOutputFile;
                public string pDataType;
            }
        }
    }
}
