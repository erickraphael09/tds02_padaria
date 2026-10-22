<<<<<<< HEAD
﻿using backPadaria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
>>>>>>> e06780e54ce42af2772df3e0aa8270fd40d3bc70
using System.Text;
using System.Threading.Tasks;

namespace backPadaria
{
    internal class FuncionarioService
    {
<<<<<<< HEAD
        public static void menuCliente()
        {
            bool sair = false;

            while (sair) {

                Console.WriteLine("=========GESTÃO DE FUNCIONARIOS==========");

                Console.WriteLine("1- Cadastrar funcionario");
                Console.WriteLine("2- Listar todos funcionarios");
                Console.WriteLine("3- Buscar funcionarios por id");
                Console.WriteLine("4- Atualizar funcionario");
                Console.WriteLine("5- Excluir Funcionario");
                Console.WriteLine("0- Sair da programação");

                Console.WriteLine("======================");
                Console.WriteLine("Digite a opção: ");

                string opcao = Console.ReadLine();

                Console.Clear();

                try
                {
                    switch (opcao)
                    {
                        case "1": // cadastrar funcionario
                            cadastrarFuncionario();
                            break;

                        case "2":
                            listarFuncionario();
                            break;
                        
                        case "3": // Buscar funcionarios por ID
                            buscarFuncionariosPeloId();
                            break;

                        case "4": // Atualizar Funcionarios
                        atualizarFuncionario();
                        break;


                    }





                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO DE VALIDAÇÃO]: {ex.Message}");
                }

            }    

        }

        public static void cadastrarFuncionario()
        {
            Console.WriteLine("------ CADASTRAR FUNCIONARIO ----- ");

            Console.WriteLine("Digite o nome do funcionario: ");
            string nomeFuncionario = Console.ReadLine();

            Console.WriteLine("Digite o cpf do funcionario: ");
            string cpfFuncionario = Console.ReadLine();

            Console.WriteLine("Digite o cargo do funcionario: ");
            string cargoFuncionario = Console.ReadLine();


            Funcionario NovoFuncionario = new Funcionario(
                nome: nomeFuncionario,
                cpf: cpfFuncionario,
                cargo: cargoFuncionario
                );

            int resultadoInsert = Funcionario.Inserir(NovoFuncionario);
            Console.Clear();

            if (resultadoInsert > 0) Console.WriteLine("Funcionario cadastrado com sucesso!!");

             



        }
        public static void listarFuncionario()
        {
            Console.WriteLine("------ LISTA DE FUNCIONARIOS ----- ");

            List<Funcionario> lista = Funcionario.ListarTodos();

            if (lista.Count == 0)

            {
                Console.WriteLine("Nenhum funcionarios cadastrado no sistema");

            }
            else
            {
                foreach (var funcionarios in lista)
                {
                    Console.WriteLine($"ID: {funcionarios.Id} | Nome {funcionarios.Nome} | CPF: {funcionarios.Cpf}| Cargo: {funcionarios.Cargo} ");
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }


        private static void buscarFuncionariosPeloId()
        {
            Console.WriteLine("------ BUSCAR FUNCIONARIOS PELO ID ----- ");
            Console.WriteLine("Digite o ID do funcionarios:");
            int idFuncionario = int.Parse(Console.ReadLine());
            Funcionario? FuncionarioEncontrado = Funcionario.listarPorId(idFuncionario);
            if (FuncionarioEncontrado != null)
            {
                Console.WriteLine($"ID: {FuncionarioEncontrado.Id} | Nome: {FuncionarioEncontrado.Nome} | CPF: {FuncionarioEncontrado.Cpf} | Cargo: {FuncionarioEncontrado.Cargo}");
            }
            else
            {
                Console.WriteLine("Cliente não encontrado.");
            }
            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        private static void atualizarFuncionario()
        {
            Console.WriteLine("------ ATUALIZAR Funcionario ----- ");
            Console.WriteLine("Digite o ID do funcionario que deseja atualizar: ");
            int idFuncionario = int.Parse(Console.ReadLine());
            Funcionario? FuncionarioEncontrado = Funcionario.listarPorId(idFuncionario);
            if (FuncionarioEncontrado != null)
            {
                Console.WriteLine($"ID: {FuncionarioEncontrado.Id} | Nome: {FuncionarioEncontrado.Nome} | CPF: {FuncionarioEncontrado.Cpf} | Cargo: {FuncionarioEncontrado.Cargo}");
                Console.WriteLine("Digite o novo nome do Funcionario: ");
                string novoNome = Console.ReadLine();
                Console.WriteLine("Digite o novo cpf do Funcionario: ");
                string novoCpf = Console.ReadLine();
                Console.WriteLine("Digite o novo cargo do Funcionario: ");
                string novoCargo = Console.ReadLine();
                
                FuncionarioEncontrado.Nome = novoNome;
                FuncionarioEncontrado.Cpf = novoCpf;
                FuncionarioEncontrado.Cargo = novoCargo;
                
                int resultadoUpdate = Funcionario.Atualizar(FuncionarioEncontrado);
                if (resultadoUpdate > 0) Console.WriteLine("Funcionario atualizado com sucesso!!");
            }
            else
            {
                Console.WriteLine("Funcionario não encontrado.");
            }
            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        private static void excluirFuncionarioMenu()
        {
            Console.WriteLine("------ EXCLUIR FUNCIONARIO POR ID ----- ");
            Console.WriteLine("Digite o id do Funcionario que deseja excluir");

            int idBusca = 0;
            int.TryParse(Console.ReadLine(), out idBusca);



            Funcionario? FuncionarioParaExcluir = Funcionario.listarPorId(idBusca);

            if (FuncionarioParaExcluir != null)
            {
                Funcionario.Deletar(FuncionarioParaExcluir);
                Console.WriteLine("Funcionario excluído com sucesso");
            }
            else
            {

                Console.WriteLine("Funcionario não encontrado");

            }

            Console.ReadKey();

        } //





    }
        

=======


    }
>>>>>>> e06780e54ce42af2772df3e0aa8270fd40d3bc70
}
