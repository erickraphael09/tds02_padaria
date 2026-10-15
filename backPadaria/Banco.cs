using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
