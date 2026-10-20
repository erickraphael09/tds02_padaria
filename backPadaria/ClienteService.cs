using backPadaria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Service
{
    public static class ClienteService
    {

        public static void MenuCliente()
        {
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("===== GESTÃO DE CLIENTES =====");

                Console.WriteLine("1 - Cadastrar Cliente");
                Console.WriteLine("2 - Listar Todos os Clientes");
                Console.WriteLine("3 - Buscar Cliente por ID");
                Console.WriteLine("4 - Atualizar Cliente");
                Console.WriteLine("5 - Excluir Cliente");
                Console.WriteLine("6 - Visualizar pontuação");
                Console.WriteLine("0 - Sair do Programa");

                Console.WriteLine("======================");
                Console.WriteLine("Digite uma opção: ");

                string opcao = Console.ReadLine();

                Console.Clear();


                try
                {
                    switch (opcao)
                    {
                        case "1": // Cadastrar

                            cadastrarClienteMenu();

                            break;

                        case "2": // Listar
                            listarClientesMenu();
                            break;

                        case "3": // Listar pelo ID
                            buscarClientePeloId();
                            break;

                        case "4": // Atualizar
                            atualizar();
                            break;

                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO DE VALIDAÇÃO]: {ex.Message}");
                }



            }




        } // FIM MENU

        private static void cadastrarClienteMenu()
        {
            Console.WriteLine("------ CADASTRAR CLIENTE ----- ");

            Console.WriteLine("Digite o nome do cliente: ");
            string nomeCliente = Console.ReadLine();

            Console.WriteLine("Digite o endereço do cliente: ");
            string enderecoCliente = Console.ReadLine();

            Console.WriteLine("Digite o cpf do cliente: ");
            string cpfCliente = Console.ReadLine();

            Console.WriteLine("Digite o telefone do cliente: ");
            string telefoneCliente = Console.ReadLine();

            Console.WriteLine("Digite a quantidade de pontos");
            int pontosCliente = int.Parse(Console.ReadLine());


            Cliente NovoCliente = new Cliente(
                nome: nomeCliente,
                cpf: cpfCliente,
                telefone: telefoneCliente,
                endereco: enderecoCliente,
                pontos: pontosCliente
            );

            int resultadoInsert = Cliente.inserir(NovoCliente);

            Console.Clear();

            if (resultadoInsert > 0) Console.WriteLine("Cliente cadastrado com sucesso!!");

        }

        private static void listarClientesMenu()
        {
            Console.WriteLine("------ LISTA DE CLIENTES ----- ");

            List<Cliente> lista = Cliente.ListarTodos();

            if (lista.Count == 0)

            {
                Console.WriteLine("Nenhum cliente cadastrado no sistema");

            }
            else
            {
                foreach (var cliente in lista)
                {
                    Console.WriteLine($"ID: {cliente.Id} | Nome {cliente.Nome} | CPF: {cliente.Cpf}| Pontos: {cliente.Pontos} ");
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        private static void buscarClientePeloId()
        {
            Console.WriteLine("------ BUSCAR CLIENTE PELO ID ----- ");
            Console.WriteLine("Digite o ID do cliente: ");
            int idCliente = int.Parse(Console.ReadLine());
            Cliente? clienteEncontrado = Cliente.listarPorId(idCliente);
            if (clienteEncontrado != null)
            {
                Console.WriteLine($"ID: {clienteEncontrado.Id} | Nome: {clienteEncontrado.Nome} | CPF: {clienteEncontrado.Cpf} | Pontos: {clienteEncontrado.Pontos}");
            }
            else
            {
                Console.WriteLine("Cliente não encontrado.");
            }
            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        private static void atualizar()
        {
            Console.WriteLine("------ ATUALIZAR CLIENTE ----- ");
            Console.WriteLine("Digite o ID do cliente que deseja atualizar: ");
            int idCliente = int.Parse(Console.ReadLine());
            Cliente? clienteEncontrado = Cliente.listarPorId(idCliente);
            if (clienteEncontrado != null)
            {
                Console.WriteLine($"ID: {clienteEncontrado.Id} | Nome: {clienteEncontrado.Nome} | CPF: {clienteEncontrado.Cpf} | Pontos: {clienteEncontrado.Pontos}");
                Console.WriteLine("Digite o novo nome do cliente: ");
                string novoNome = Console.ReadLine();
                Console.WriteLine("Digite o novo endereço do cliente: ");
                string novoEndereco = Console.ReadLine();
                Console.WriteLine("Digite o novo cpf do cliente: ");
                string novoCpf = Console.ReadLine();
                Console.WriteLine("Digite o novo telefone do cliente: ");
                string novoTelefone = Console.ReadLine();
                Console.WriteLine("Digite a nova quantidade de pontos");
                int novosPontos = int.Parse(Console.ReadLine());
                clienteEncontrado.Nome = novoNome;
                clienteEncontrado.Endereco = novoEndereco;
                clienteEncontrado.Cpf = novoCpf;
                clienteEncontrado.Telefone = novoTelefone;
                clienteEncontrado.Pontos = novosPontos;
                int resultadoUpdate = Cliente.Atualizar(clienteEncontrado);
                if (resultadoUpdate > 0) Console.WriteLine("Cliente atualizado com sucesso!!");
            }
            else
            {
                Console.WriteLine("Cliente não encontrado.");
            }
            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
            private static void deletar 






        }
    }
}