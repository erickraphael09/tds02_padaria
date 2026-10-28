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

        private int produtoId;
        private string descricaoProducao;

        public int ProdutoId { get => produtoId; set => produtoId = value; }
        public string DescricaoProducao1 { get => descricaoProducao; set => descricaoProducao = value; }


        public ProdFabricado(int produtoId, string descricaoProducao)
        {
            this.produtoId = produtoId;
            this.descricaoProducao = descricaoProducao;
        }

        public ProdFabricado(string descricaoProducao)
        {
            this.descricaoProducao = descricaoProducao;
        }


        public static int Inserir(ProdFabricado prodFabricado)
        {
            // Instrução SQL: Define o comando de inserção (INSERT).
            // 0 uso de marcações com '0' (ex: ONone) é una pratica de segurança crucial chanada de "Paranetrização".
            // Ela impede ataques de SQL Injection, pois o banco entende o que e comando e o que e apenas texto do usuário.

            string sql = "INSERT INTO `Prodfabricado` (`DescricaoProduto`) VALUES (@DescricaoProduto)";

            // Gerenciamento de recursos: Cria o objeto de comando ja passando a instrução SQL.
            // 0 'using' garante que os recursos de nemoria ocupados pelo comando sejam Liberados ao final do bloco.
            using (var cmd = new MySqlCommand(sql))
            {
                // Vinculação de Dados: Pega as propriedades do objeto C# (cliente.Nome, cliente.cpf, etc.)
                // e substitui de forna segura as narcacões '0' da string SQL com os valores reais,

                cmd.Parameters.AddWithValue("@DescricaoProduto", prodFabricado.descricaoProducao);

                // Execucão: Como o comando de INSERT nao retorna tabelas (apenas executa una ação),
                // ele envia o comando configurado para o metodo generico 'executarComando' da classe 'Banco'.
                // O metodo retorna 1 (se o usuario for inserido con sucesso) ou -1 (se houver erro no banco).

                return Banco.ExecutarComando(cmd);
            }
        }

        public static int Deletar(ProdFabricado prodFabricado)
        {
            // String SQL
            string sql = "DELETE FROM `ProdFabricado` WHERE `ID` = @ProdutoId";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@ProdutoId", prodFabricado.produtoId);

                return Banco.ExecutarComando(cmd);
            }
        }

        public static List<ProdFabricado> ListarTodos()
        {
            string sql = "SELECT * FROM `ProdFabricado`";

            List<ProdFabricado> listaProdFabricados = new();

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
                        int produtoId = reader.GetInt32("ProdutoId");
                        string descricaoProduto = ValidarCampo(reader["DescricaoProduto"]);

                        // Cria o objeto Cliente con os dados extraidos
                        ProdFabricado prodFabricado = new ProdFabricado(produtoId, descricaoProduto);

                        // Adiciona este cliente à nossa lista
                        listaProdFabricados.Add(prodFabricado);

                    }

                }

                // Retorna a lista completa (se a tabela estiver vazia, retorna uma lista con 0 itens)
                return listaProdFabricados;

            }

        }


        public static ProdFabricado? ListarPorId(int produtoId)
        {
            string sql = "SELECT * FROM `ProdFabricado` WHERE `produtoId` = @ProdutoId";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@ProdutoId", produtoId);

                MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);

                ProdFabricado prodFabricado = null;

                while (reader.Read())
                {

                    // Extrai os dados da linha atual.
                    int produtoIdBanco = reader.GetInt32("Produtoid");
                    string descricaoProduto = ValidarCampo(reader["DescricaoProdutos"]);

                    // Cria o objeto Cliente con os dados extraidos
                    prodFabricado = new ProdFabricado(produtoId, descricaoProduto);
                }

                return prodFabricado;
            }
        }

        public static int Atualizar(ProdFabricado prodFabricado)
        {
            string sql = "UPDATE `Prodfabricado` SET `DescricaoProduto` = @DescricaoProduto";

            using (var cmd = new MySqlCommand(sql))
            {

                // Vinculacão de paranetros: Protege contra SQL Injection e organiza os dados
                cmd.Parameters.AddWithValue("@DescricaoProduto", prodFabricado.descricaoProducao);

                // A chave de busca: Quem sera atualizado?
                cmd.Parameters.AddWithValue("@ProdutoId", prodFabricado.produtoId);

                // Execução: 0 UPOATE tambén é uma instrução de ação (cono INSERT e DELETE).
                // Ele vai retornar 1 (se encontrou o ID e atualizou con sucesso) ou 0 (se o ID nao existia).
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


    }
}