using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Cargo { get => cargo; set => cargo = value; }


        public Funcionario(int id, string nome, string cpf, string cargo)
        {
            this.id = id;
            this.nome = nome;
            this.cpf = cpf;
            this.cargo = cargo;
        }

        public Funcionario(string nome, string cpf, string cargo)
        {
            this.nome = nome;
            this.cpf = cpf;
            this.cargo = cargo;
        }


        public static int Inserir(Funcionario funcionario)
        {
            // Instrução SQL: Define o comando de inserção (INSERT).
            // 0 uso de marcações com '0' (ex: ONone) é una pratica de segurança crucial chanada de "Paranetrização".
            // Ela impede ataques de SQL Injection, pois o banco entende o que e comando e o que e apenas texto do usuário
            string sql = "INSERT INTO `Funcionario` (`nome`, `cpf`, `cargo`) VALUES (@Nome, @Cpf, @Cargo)";

            // Gerenciamento de recursos: Cria o objeto de comando ja passando a instrução SQL.
            // 0 'using' garante que os recursos de nemoria ocupados pelo comando sejam Liberados ao final do bloco.
            using (var cmd = new MySqlCommand(sql))
            {
                // Vinculação de Dados: Pega as propriedades do objeto C# (cliente.Nome, cliente.cpf, etc.)
                // e substitui de forna segura as narcacões '0' da string SQL com os valores reais,
                cmd.Parameters.AddWithValue("@Nome", funcionario.nome);
                cmd.Parameters.AddWithValue("@Cpf", funcionario.cpf);
                cmd.Parameters.AddWithValue("@Cargo", funcionario.cargo);

                // Execucão: Como o comando de INSERT nao retorna tabelas (apenas executa una ação),
                // ele envia o comando configurado para o metodo generico 'executarComando' da classe 'Banco'.
                // O metodo retorna 1 (se o usuario for inserido con sucesso) ou -1 (se houver erro no banco).

                return Banco.ExecutarComando(cmd);
            }

        }

        public static int Deletar(Funcionario funcionario)
        {
            string sql = "DELETE FROM `Funcionario` WHERE `id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", funcionario.id);

                return Banco.ExecutarComando(cmd);
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


        public static List<Funcionario> ListarTodos()
        {
            string sql = "SELECT * FROM `Funcionario`";
            List<Funcionario> listaFuncionario = new();

            using (var cmd = new MySqlCommand(sql))
            {
                // Executa a leitura. 0 bloco 'using' aqui e FUNDAMENTAL para garantir que o
                // reader seja fechado no final, o que por sua vez fecha a conexao com o banco.

                using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
                {
                    // O while vai rodar uma vez para CADA Linha que existir na tabela Cliente
                    while (reader.Read())
                    {
                        // Extrai os dados da linha atual.
                        int id = reader.GetInt32("@Id");

                        string nome = ValidarCampo(reader["Nome"]);
                        string cpf = ValidarCampo(reader["Cpf"]);
                        string cargo = ValidarCampo(reader["Cargo"]);

                        // Cria o objeto Cliente con os dados extraidos
                        Funcionario funcionario = new Funcionario(nome, cpf, cargo);

                        // Adiciona este cliente à nossa lista
                        listaFuncionario.Add(funcionario);
                    }

                    // Retorna a lista completa (se a tabela estiver vazia, retorna uma lista con 0 itens)
                    return listaFuncionario;

                }

            }
        }


        public static Funcionario? ListarPorId(int id)
        {
            string sql = "SELECT * FROM `Funcionario` WHERE `id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("id", id);

                MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);

                Funcionario funcionario = null;

                while (reader.Read())
                {
                    // Extrai os dados da linha atual.
                    int idBanco = reader.GetInt32("id");

                    string nome = ValidarCampo(reader["Nome"]);
                    string cpf = ValidarCampo(reader["Nome"]);
                    string cargo = ValidarCampo(reader["Cargo"]);

                    // Cria o objeto Cliente con os dados 
                    funcionario = new Funcionario(nome, cpf, cargo);
                }

                return funcionario;


            }
        }



        public static int Atualizar(Funcionario funcionario)
        {
            string sql = "UPDATE `Funcionario` SET `nome` = @Nome, `cpf` = @Cpf, `cargo` = @Cargo WHERE `id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {

                // Vinculacão de paranetros: Protege contra SQL Injection e organiza os 
                cmd.Parameters.AddWithValue("@Nome", funcionario.nome);
                cmd.Parameters.AddWithValue("@Cpf", funcionario.cpf);
                cmd.Parameters.AddWithValue("@Cargo", funcionario.cargo);

                // A chave de busca: Quem sera atualizado?
                cmd.Parameters.AddWithValue("@Id", funcionario.id);

                // Execução: 0 UPOATE tambén é uma instrução de ação (cono INSERT e DELETE).
                // Ele vai retornar 1 (se encontrou o ID e atualizou con sucesso) ou 0 (se o ID nao existia).
                return Banco.ExecutarComando(cmd);

            }
        }


    }
}