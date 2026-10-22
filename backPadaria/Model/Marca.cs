using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Marca
    {
        private int id;

        private string nome;

        private string telefone;

        
        public int Id { get => id; set => id = value; }

        public string Nome { get => nome; set => nome = value; }

        public string Telefone { get => telefone; set => telefone = value; }

        public Marca(int id, string nome, string telefone)
        {
            if (nome == "") throw new Exception("O nome da marca não pode ser vazio.");
            this.id = id;
            this.nome = nome;
            this.telefone = telefone;
        }

        public Marca(int id)
        {
            this.id = id;
        }

        public Marca(string nome, string telefone)
        {

            this.nome = nome;

            this.telefone = telefone;
        }

        public static int Inserir(Marca marca)
        {
            return Marca.Inserir(marca.Id);
            string sql = "INSERT INTO `marca` (`nome`, `telefone`) VALUES (@nome, @telefone)";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@nome", marca.Nome);
                cmd.Parameters.AddWithValue("@telefone", marca.telefone);
                Banco.executarComando(cmd);
            }
        }
        public static int Deletar(Marca marca)
        {
            string sql = "DELETE FROM `Marca` WHERE `id` = @id";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", marca.Id);
                return Banco.executarComando(cmd);
            }
        }
        public static List<Marca> ListarTodos()

        {

            string sql = "SELECT * FROM `Marca`";

            List<Marca> listaMarcas = new();

            using (var cmd = new MySqlCommand(sql))

            {



                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))

                {



                    while (reader.Read())

                    {

                        int id = reader.GetInt32("id");

                        string nome = ValidarCampo(reader["Nome"]);

                        string telefone = ValidarCampo(reader["Telefone"]);

                       






                        Marca Marca = new Marca(id, nome, telefone);


                        listaMarca.Add(Marca);

                    }

                }



                return listaMarca;

            }

        }

        public static Marca? listarPorId(int id)
        {

            string sql = "SELECT * FROM `Marca` WHERE `id` = @id";


            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);

                Marca marca = null;

                while (reader.Read())
                {

                    // Extrai os dados do reader e valida os campos
                    int idBanco = reader.GetInt32("id");

                    string nome = ValidarCampo(reader["Nome"]);
                    string telefone = ValidarCampo(reader["Telefone"]);
                   

                   

                    // Cria o objeto Cliente com os dados extraídos
                    marca = new Cliente(id, nome, telefone);

                }

                return marca;

            }
        }
        public static int Atualizar(Marca marca)
        {
            string sql = "UPDATE `Marca` SET `Nome` = @Nome, `Telefone` = @telefone";
            using (var cmd = new MySqlCommand(sql))
            {
                // Vinculação de parametros: Protege contra SQL Injection e organiza os dados
                cmd.Parameters.AddWithValue("@Nome", Marca.Nome);
                cmd.Parameters.AddWithValue("@Telefone", Marca.telefone);
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
