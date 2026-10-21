using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Funcionario
    {
        private int id;

        private string nome;

        private string cpf;

        private string cargo;    

        public int Id { get => id; set =>  id = value; }

        public string Nome { get => nome; set => nome = value; }

        public string Cpf { get => cpf; set => cpf = value; }

        public string Cargo { get => cargo; set => cargo = value; }


        public Funcionario(int id, string nome, string cpf, string cargo)
        {
            if (nome == "") throw new Exception("O nome do funcionário não pode ser vazio.");
            this.id = id;
            this.nome = nome;
            this.cpf = cpf;
            this.cargo = cargo;
        }

        public Funcionario(int id)
        {
            this.id = id;
        }   

        public Funcionario(string nome, string cpf, string cargo)
        {
            
            this.nome = nome;

            this.cpf = cpf;

            this.cargo = cargo;
        }

        public static void Inserir(Funcionario funcionario)
        {
            string sql = "INSERT INTO `Funcionario` (`nome`, `cpf`, `cargo`) VALUES (@nome, @cpf, @cargo)";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@cpf", funcionario.Cpf);
                cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
                Banco.executarComando(cmd);
            }
        }
        public static int Deletar(Funcionario funcionario)
        {
            string sql = "DELETE FROM `Funcionario` WHERE `id` = @id";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", funcionario.Id);
                return Banco.executarComando(cmd);
            }
        }

        public static List<Funcionario> ListarTodos()

        {

            string sql = "SELECT * FROM `Funcionario`";

            List<Funcionario> listaFuncionario = new();

            using (var cmd = new MySqlCommand(sql))

            {

                

                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))

                {

                    

                    while (reader.Read())

                    {

                        int id = reader.GetInt32("id");

                        string nome = ValidarCampo(reader["Nome"]);

                        string cpf = ValidarCampo(reader["CPF"]);

                        string cargo = ValidarCampo(reader["Cargo"]);

                        

                        

                        
                        Funcionario funcionario = new Funcionario(id, nome, cpf, cargo);

                        
                        listaFuncionario.Add(funcionario);

                    }

                }

                

                return listaFuncionario;

            }

        }

        public static Funcionario? listarPorId(int id)
        {

            string sql = "SELECT * FROM `Funcionario` WHERE `id` = @id";


            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);

                Funcionario funcionario = null;

                while (reader.Read())
                {

                    // Extrai os dados do reader e valida os campos
                    int idBanco = reader.GetInt32("id");

                    string nome = ValidarCampo(reader["Nome"]);
                    string cpf = ValidarCampo(reader["CPF"]);
                    string cargo = ValidarCampo(reader["Cargo"]);
                    

                    int pontos = reader.GetInt32("Pontos");

                    // Cria o objeto Cliente com os dados extraídos
                    funcionario = new Funcionario(id, nome, cpf, cargo);

                }

                return funcionario;

            }
        }

        public static int Atualizar(Funcionario funcionario)
        {
            string sql = "UPDATE `Funcionario` SET `Nome` = @Nome, `Cpf` = @CPF, `Cargo` = @Cargo WHERE `Id` = @Id";
            using (var cmd = new MySqlCommand(sql))
            {
                // Vinculação de parametros: Protege contra SQL Injection e organiza os dados
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@CPF", funcionario.Cpf);
                cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);
                cmd.Parameters.AddWithValue("@Id", funcionario.Id);
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
