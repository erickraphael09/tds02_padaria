using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class ProdFabricado
    {


        private int ProdutoId;

        private string DescricaoProducao;


        public int produtoId { get => produtoId; set => produtoId = value; }

        public string descricaoProducao { get => descricaoProducao; set => descricaoProducao = value; }


        public ProdFabricado(int ProdutoId, string DescricaoProducao)
        {

            this.ProdutoId = ProdutoId;
            this.DescricaoProducao = DescricaoProducao;

        }

        public ProdFabricado(int id)
        {
            this.id = id;
        }

        public ProdFabricado(string DescricaoProducao)
        {
            this.produtoId = produtoId
            this.DescricaoProducao = DescricaoProducao;


        }

        public static int Inserir(ProdFabricado prodFabricado)
        {
            return ProdFabricado.Inserir(prodFabricado.Id);
            string sql = "INSERT INTO `ProdFabricado` (`DescricaoProducao`) VALUES (@DescricaoProducao)";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@nome", ProdFabricado.DescricaoProducao);

                Banco.executarComando(cmd);
            }
        }

        public static int Deletar(ProdFabricado prodFabricado)
        {
            string sql = "DELETE FROM `ProdFabricado` WHERE `id` = @id";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", ProdFabricado.Id);
                return Banco.executarComando(cmd);
            }
        }

        public static List<ProdFabricado> ListarTodos()

        {

            string sql = "SELECT * FROM `ProdFabricado`";

            List<ProdFabricado> listaProdFabricado = new();

            using (var cmd = new MySqlCommand(sql))

            {



                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))

                {



                    while (reader.Read())

                    {

                        int Produtoid = reader.GetInt32("Produtoid");

                        string DescricaoProducao = ValidarCampo(reader["DescricaoProduto"]);


                        ProdFabricado ProdFabricadoa = new ProdFabricado(ProdutoId, DescricaoProducao);


                        listaProdFabricado.Add(ProdFabricado);

                    }

                }

            }
        }

        public static ProdFabricado? listarPorId(int id)
        {

            string sql = "SELECT * FROM `ProdFabricado` WHERE `Produtoid` = @Produtoid";


            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);

                Marca marca = null;

                while (reader.Read())
                {

                    // Extrai os dados do reader e valida os campos
                    int idBanco = reader.GetInt32("Produtoid");

                    string ProdutoId = ValidarCampo(reader["ProdutoId"]);
                    string telefone = ValidarCampo(reader["Descricao"]);




                    // Cria o objeto Cliente com os dados extraídos
                    marca = new ProdFabricado(ProdutoId, DescricaoProducao);

                }

                return ProdFabricado;

            }
        }
        public static int Atualizar(Marca marca)
        {
            string sql = "UPDATE `Marca` SET `Nome` = @Nome, `Telefone` = @telefone";
            using (var cmd = new MySqlCommand(sql))
            {
                // Vinculação de parametros: Protege contra SQL Injection e organiza os dados
                cmd.Parameters.AddWithValue("@ProdutoId", ProdFabricado.ProdutoId);
                cmd.Parameters.AddWithValue("@DescricaoProducao", ProdFabricado.Descricaoproducao);
                return Banco.executarComando(cmd);
            }
        }

        public static string ValidarCampo(object campo)
        {
            if (campo == DBNull.Value || campo == null)
            {
                return string.Empty;
            }
            else
            {
                return campo.ToString();
            }
        }

    } 
}