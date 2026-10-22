using System;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Fornecedor
    {
        private int id;

        private string nome;

        private string Cnpj;

        private string telefone;


        public int Id { get => id; set => id = value; }

        public string Nome { get => nome; set => nome = value; }

        public string cnpj { get => cnpj; set => cnpj = value; }

        public string Telefone { get => telefone; set => telefone = value; }

        public Fornecedor(int id, string nome, string cpf, string cargo)
        {
            if (nome == "") throw new Exception("O nome do funcionário não pode ser vazio.");
            this.id = id;
            this.nome = nome;
            this.cnpj = cnpj;
            this.telefone = cnpj;
        }

        public Fornecedor(int id)
        {
            this.id = id;
        }

        public Fornecedor(string nome, string cpf, string cargo)
        {

            this.nome = nome;

            this.cnpj = cnpj;

            this.telefone = telefone;
        }

        public static int Inserir(Fornecedor fornecedor)
        {
            return Fornecedor.Inserir(fornecedor.Id);
            string sql = "INSERT INTO `fornecedor` (`nome`, `cnpj`, `telefone`) VALUES (@nome, @cnpj, @telefone)";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@nome", fornecedor.Nome);
                cmd.Parameters.AddWithValue("@cpf", fornecedor.Cnpj);
                cmd.Parameters.AddWithValue("@cargo", fornecedor.telefone);
                Banco.executarComando(cmd);
            }
        }
        public static int Deletar(Fornecedor fornecedor)
        {
            string sql = "DELETE FROM `fornecedor` WHERE `id` = @id";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", fornecedor.Id);
                return Banco.executarComando(cmd);
            }
        }
        public static List<Fornecedor> ListarTodos()

        {

            string sql = "SELECT * FROM `Fornecedor`";

            List<Fornecedor> listaFornecedor = new();

            using (var cmd = new MySqlCommand(sql))

            {



                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))

                {



                    while (reader.Read())

                    {

                        int id = reader.GetInt32("id");

                        string nome = ValidarCampo(reader["Nome"]);

                        string cnpj = ValidarCampo(reader["cnpj"]);

                        string telefone = ValidarCampo(reader["telefone"]);






                        Fornecedor fornecedor = new Fornecedor(id, nome, cnpj, telefone);


                        listaFornecedor.Add(fornecedor);

                    }

                }



                return listaFornecedor;

            }

        }
        public static Fornecedor? listarPorId(int id)
        {

            string sql = "SELECT * FROM `Fornecedor` WHERE `id` = @id";


            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);

                Fornecedor fornecedor = null;

                while (reader.Read())
                {

                    // Extrai os dados do reader e valida os campos
                    int idBanco = reader.GetInt32("id");

                    string nome = ValidarCampo(reader["Nome"]);
                    string cpf = ValidarCampo(reader["cnpj"]);
                    string cargo = ValidarCampo(reader["Cargo"]);


                    int pontos = reader.GetInt32("Pontos");

                    // Cria o objeto Cliente com os dados extraídos
                    funcionario = new Funcionario(id, nome, cpf, cargo);

                }

                return funcionario;

            }
        }


    }
}
