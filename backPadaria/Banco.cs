using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria
{
    public static class Banco
    {
        private static string stringDeConexao = "Server=10.91.45.51;Port=3306;Database=padaria_bd;Uid=admin;Pwd=123456";

        public static void connectAndTest()


        {
            using (var connection = new MySqlConnection(stringDeConexao))
            {
                try
                {
                    Console.WriteLine("Tentando conectar ao banco de dados");
                    connection.Open();

                    Console.WriteLine("Conexão bem sucedida");

                    using (var command = new MySqlCommand("SELECT VERSION();", connection))
                    {
                        string? version = command.ExecuteScalar()?.ToString();
                        Console.WriteLine($"Versão do MariaDB {version}");


                    }

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Erro ao connecta no banco de dados");
                    Console.WriteLine(ex.Message);
                }

            }

        }

        public static int executarComando(MySqlCommand command)
        {



            using (var conexao = new MySqlConnection(stringDeConexao))
            {

              command.Connection = conexao;


                try
                {

                    conexao.Open();


                    return command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    {

                        Console.WriteLine($"Erro de Banco de dados {ex.Message}");

                        return -1;


                    }

                }
            }
        }


        // Declaração do metodo: publico e estatico, prjetado especificamente para executar comandos
        // (geralmente INSERTs seguidos de SELECT LAST_INSERT_ID()) e retornar o ID gerado pelo banco.

        public static int executarComandoComRetornoId(MySqlCommand command)
        {
            using (var conexao = new MySqlConnection(stringDeConexao))
            {
                command.Connection = conexao;

                try
                {
                    conexao.Open();

                    /* O ExecuteScalar executa o comando e retorna APENAS a primeira coluna da primeira
                     linha do resultado. É perfeito para pegar um único valor (como um ID recém-gerado).
                     Ele retorna um tipo 'object' porque nao sabe de antemao qual o tipo do dado no banco.*/
                  
                        object objRetorno = command.ExecuteScalar();

                    int retorno = 0;

                    /* Verificação de segurança: Checa se o banco realmente retornou algo.
                    Verifica tanto se o objeto C# não é nulo (null) quanto se
                    não é um nulo do próprio banco de dados (DBNull.Value).*/


                    if (objRetorno !=null && objRetorno != DBNull.Value) {

                       /* Se passou na verificação, converte o objeto (que veio como texto/objeto)
                       para um numero inteiro (int).*/

                        retorno = int.Parse(objRetorno.ToString());

                    }

                    return retorno;
                }
                catch(MySqlException ex)
                {
                    Console.WriteLine($"Erro de Banco de dados {ex.Message}");
                    return -1;

                }
            }


        } // Metodo RetornoComId

        // Metodo central para executar comandos de consulta (SELECT) no banco de dados.
        // Ele retorna um MySqlDataReader, que e uma especie de "ponteiro" para ler os dados linha por linha.

        public static MySqlDataReader executarComRetornoDeDados(MySqlCommand command)
        {
            // Cria a conexão com o banco.
            // ATENÇÃO: Aqui NÃO usamos o bloco 'using' para a conexão. Se usássemos, a conexão
            // seria fechada antes de retornar os dados, e o programa quebraria ao tentar ler a tabela.
            
            var conexao = new MySqlConnection(stringDeConexao);

            try
            {
                // Abre a porta de comunicação com o MySql
                command.Connection = conexao;
                conexao.Open();

                // Executa a consulta e retorna o "leitor" de dados.
                // O CommandBehavior.CloseConnection e a peca-chave deste metodo: ele avisa ao leitor
                // que assin que ele (o leitor) for fechado ou descartado la na tela/classe que o chamou,
                // a conexao com o banco deve ser fechada automaticamente junto con ele.
                return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (MySqlException ex) {

                // O Dispone() ja executa o Close() e libera a memoria.
                conexao.Dispose();


                Console.WriteLine($"Erro na leitura do banco de dados: {ex. Message}");

                // Ao inves de retornar null, relança o erro (throw) para que o metodo que
                // chamou este ExecuteReader saiba que a pesquisa falhou e possa tratar a tela do usuário.

                throw;
            }

            


        } 
    } // Classe

} // Naspace
