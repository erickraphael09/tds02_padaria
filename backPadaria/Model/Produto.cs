using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Produto
    {
        private int id;

        private string nome;

        private decimal preco;

        private string categoria;

        private bool fabricado;

        int MarcaId;


        public int Id { get => id; set => id = value; }

        public string Nome { get => nome; set => nome = value; }

        public decimal Preco { get => preco; set => preco = value; }

        public string Categoria { get => categoria; set => categoria = value; }

        public string Fabricado { get => Fabricado; set => Fabricado = value; }

        public int marcaId { get => MarcaId; set => MarcaId = value; }

        public Produto(int id, string nome, decimal preco, string categoria, string Fabricado, int MarcaId)
        {
            if (nome == "") throw new Exception("O nome dp  não pode ser vazio.");
            this.id = id;
            this.nome = nome;
            this.preco = preco;
            this.categoria = categoria;
            this.Fabricado = Fabricado;
            this.MarcaId = MarcaId;
        }

        public Produto(int id)
        {
            this.id = id;
        }

        public Produto(string nome, decimal preco, string categoria, bool fabricado, int MarcaId)
        {

            this.nome = nome;

            this.preco = preco;

            this.categoria = categoria;

            this.fabricado = fabricado;

            this.MarcaId = MarcaId;
        }

        public static void Inserir(Produto produto)
        {
            
            string sql = "INSERT INTO `produto` (`nome`, `preco`, `categoria`, `fabricado`, `marcaId`) VALUES (@nome, @telefone)";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@nome", produto.Nome);
                cmd.Parameters.AddWithValue("@preco", produto.preco);
                cmd.Parameters.AddWithValue("@categoria", produto.categoria);
                cmd.Parameters.AddWithValue("@fabricado", produto.fabricado);
                cmd.Parameters.AddWithValue("@marcaId", produto.marcaId);

                Banco.executarComando(cmd);
            }
        }

        public static int Deletar(Produto produto)
        {
            string sql = "DELETE FROM `Produto` WHERE `id` = @id";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", produto.Id);
                return Banco.executarComando(cmd);
            }
        }

        public static List<Produto> ListarTodos()

        {

            string sql = "SELECT * FROM `Produto`";

            List<Produto> listaProduto = new();

            using (var cmd = new MySqlCommand(sql))

            {



                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))

                {



                    while (reader.Read())

                    {

                        int id = reader.GetInt32("id");

                        string nome = ValidarCampo(reader["Nome"]);

                        decimal preco = ValidarCampo(reader["Preco"]);

                        string categoria = ValidarCampo(reader["Categoria"]);

                        bool fabricado = ValidarCampo(reader["Fabricado"]);

                        int marcaId = ValidarCampo(reader["MarcaId"]);








                        Produto Produto = new Produto(id, nome, preco, categoria, fabricado, marcaId);


                        listaProduto.Add(Produto);

                    }

                }



                return listaProduto;

            }

        }
        public static Produto? listarPorId(int id)
        {

            string sql = "SELECT * FROM `Produto` WHERE `id` = @id";


            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);

                Produto produto = null;

                while (reader.Read())
                {

                    // Extrai os dados do reader e valida os campos
                    int idBanco = reader.GetInt32("id");

                    string nome = ValidarCampo(reader["Nome"]);
                    decimal preco = ValidarCampo(reader["Preco"]);
                    string Categoria = ValidarCampo(reader["Categoria"]);
                    bool Fabricado = ValidarCampo(reader["Fabricado"]);
                    int MarcaId = ValidarCampo(reader["MarcaId"]);





                    // Cria o objeto Cliente com os dados extraídos
                    produto = new Produto(id, nome, preco, Categoria, Fabricado, MarcaId);

                }

                return Produto;

            }
        }
        public static int Atualizar(Produto produto)
        {
            string sql = "UPDATE `Produto` SET `Nome` = @Nome, `Preco` = @Preco, `Categoria` = @Categoria, `Fabricado` = @Fabricado, `MarcaId` = @MarcaId";
            using (var cmd = new MySqlCommand(sql))
            {
                // Vinculação de parametros: Protege contra SQL Injection e organiza os dados
                cmd.Parameters.AddWithValue("@Nome", Produto.Nome);
                cmd.Parameters.AddWithValue("@Preco", Produto.Preco);
                cmd.Parameters.AddWithValue("@Categoria", Produto.Categoria);
                cmd.Parameters.AddWithValue("Fabricado", Produto.Fabricado);
                cmd.Parameters.AddWithValue("@MarcaId", Produto.MarcaId);



                return Banco.executarComando(cmd);
            }
        }

        private static string ValidarCampo(object campo)

        {

            if (campo == DBNull.Value || campo == null)

            {

                return "";

            }

            else

            {

                return campo.ToString();

            }


        }

    }
}
