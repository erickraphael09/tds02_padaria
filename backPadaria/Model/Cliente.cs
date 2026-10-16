using MySqlConnector;

using System;

using System.Collections.Generic;

using System.Diagnostics.Contracts;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

namespace backPadaria.Model

{

    internal class Cliente

    {

        private int id;

        private string nome;

        private string cpf;

        private string telefone;

        private string endereco;

        private int pontos;

        public int Id { get => id; set => id = value; }

        public string Nome { get => nome; set => nome = value; }

        public string Cpf { get => cpf; set => cpf = value; }

        public string Telefone { get => telefone; set => telefone = value; }

        public string Endereco { get => endereco; set => endereco = value; }

        public int Pontos { get => pontos; set => pontos = value; }

        public Cliente(int id, string nome, string cpf, string telefone, string endereco, int pontos)

        {

            if (nome == "") throw new Exception("O nome do cliente não pode ser vazio.");

            this.Id = id;

            this.Nome = nome;

            this.Cpf = cpf;

            this.Telefone = telefone;

            this.Endereco = endereco;

            this.Pontos = pontos;

        }

        // Gambiarra para testar

        public Cliente(int id)

        {

            this.Id = id;

        }


        public Cliente(string nome, string cpf, string telefone, string endereco, int pontos)

        {

            this.Nome = nome;

            this.Cpf = cpf;

            this.Telefone = telefone;

            this.Endereco = endereco;

            this.Pontos = pontos;

        }

        public void AdicionarPontos(int pontos)

        {

            this.Pontos += pontos;

        }


        public static int inserir(Cliente cliente)

        {

            // Instrução SQL: Define o comando de inserção (INSERT).

            // 0 uso de marcações com '0' (ex: ONone) é una pratica de segurança crucial chanada de "Paranetrização".

            // Ela impede ataques de SQL Injection, pois o banco entende o que e comando e o que e apenas texto do usuário.

            string sql = "INSERT INTO `Cliente` (`nome`, `CPF`, `Telefone`, `Endereco`, `Pontos`) VALUES (@Nome, @CPF, @Telefone, @Endereco, @Pontos)";

            // Gerenciamento de recursos: Cria o objeto de comando ja passando a instrução SQL.

            // 0 'using' garante que os recursos de nemoria ocupados pelo comando sejam Liberados ao final do bloco.

            using (var cmd = new MySqlCommand(sql))

            {

                // Vinculação de Dados: Pega as propriedades do objeto C# (cliente.Nome, cliente.cpf, etc.)

                // e substitui de forna segura as narcacões '0' da string SQL com os valores reais,

                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);

                cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);

                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);

                cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);

                cmd.Parameters.AddWithValue("@Pontos", cliente.Pontos);

                // Execucão: Como o comando de INSERT nao retorna tabelas (apenas executa una ação),

                // ele envia o comando configurado para o metodo generico 'executarComando' da classe 'Banco'.

                // O metodo retorna 1 (se o usuario for inserido con sucesso) ou -1 (se houver erro no banco).

                return Banco.executarComando(cmd);

            }

        }

        public static int Deletar(Cliente cliente)

        {

            // String SQL

            string sql = "DELETE FROM `Cliente` WHERE `ID` = @id";

            using (var cmd = new MySqlCommand(sql))

            {

                cmd.Parameters.AddWithValue("@id", cliente.id);

                return Banco.executarComando(cmd);

            }

        }

        public static List<Cliente> ListarTodos()

        {

            string sql = "SELECT * FROM `Cliente`";

            List<Cliente> listaClientes = new();

            using (var cmd = new MySqlCommand(sql))

            {

                // Executa a leitura. 0 bloco 'using' aqui e FUNDAMENTAL para garantir que o

                // reader seja fechado no final, o que por sua vez fecha a conexao com o banco.

                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))

                {

                    // O while vai rodar uma vez para CADA Linha que existir na tabela Cliente

                    while (reader.Read())

                    {

                        int id = reader.GetInt32("id");

                        string nome = ValidarCampo(reader["Nome"]);

                        string cpf = ValidarCampo(reader["CPF"]);

                        string telefone = ValidarCampo(reader["Telefone"]);

                        string endereco = ValidarCampo(reader["Endereco"]);

                        int pontos = reader.GetInt32("Pontos");

                        // Cria o objeto Cliente con os dados extraidos

                        Cliente cliente = new Cliente(id, nome, cpf, telefone, endereco, pontos);

                        // Adiciona este cliente à nossa lista

                        listaClientes.Add(cliente);

                    }

                }

                // Retorna a lista completa (se a tabela estiver vazia, retorna uma lista con 0 itens)

                return listaClientes;

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

