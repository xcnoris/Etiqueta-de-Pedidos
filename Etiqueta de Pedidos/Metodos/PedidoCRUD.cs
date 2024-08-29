using Etiqueta_de_Pedidos.DataBase;
using System.Data;
using System.Data.SqlClient;

namespace Etiqueta_de_Pedidos.Metodos
{
    internal class PedidoCRUD
    {
        private ConnetionDB _connetionDB;
        private CommandsDB _commandsDB;

        internal string Mensagem;
        internal bool Status;
        string teste = "";
        public PedidoCRUD()
        {
            _connetionDB = new ConnetionDB(teste);
            _commandsDB = new CommandsDB(_connetionDB);

            Status = true;
        }
        internal DataTable BuscarPedidosInDB(string nsu)
        {
            DataTable servicosTable = new DataTable();
            string query = @"
                select 
                    pv.nsu,
                    ent.nome,
                    CONVERT(VARCHAR, pv.data_hora_geracao, 103) AS data_formatada,
                    ipv.descricao_item,
                    ipv.observacao,
                    pv.nsu,
                    us.nome AS vendedor
                from 
                    item_pedido_venda ipv
                inner join 
                    pedido_venda pv on pv.id_pedido_venda = ipv.id_pedido_venda
                inner join 
                    entidade ent on ent.id_entidade = pv.id_entidade_cliente
                inner join 
                    usuario us on us.id_usuario = pv.id_usuario_geracao
                where pv.nsu = @nsu";

            try
            {
                using (SqlConnection connection = _connetionDB.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@nsu", nsu);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(servicosTable);
                        }
                    }
                    Status = true;
                }

                MetodosGerais.RegistrarLog("Pedido", $"Foram encontradas {servicosTable.Rows.Count} registros no banco de dados\n");
                return servicosTable;
            }
            catch (Exception ex)
            {
                MetodosGerais.RegistrarLog("Pedido", $"[ERROR]: {ex.Message} - {_commandsDB.Mensagem}");
                Mensagem = $"[ERROR]: {ex.Message} - {_commandsDB.Mensagem}";
                Status = false;
                return servicosTable;
            }
        }
    }
}