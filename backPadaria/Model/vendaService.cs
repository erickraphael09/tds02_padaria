using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class vendaService
    {
        public static void MenuVendas()
        {
            bool sair = false;

            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("========PDV - Padaria Pão & Cia");
                Console.WriteLine("1 - Iniciar nova venda");
                Console.WriteLine("0 - Voltar ao menu principal");
                Console.WriteLine("====================");
                Console.Write("Digite uma opção: ");

                string opcao = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch(opcao) 
                    {
                        case "1":
                            iniciarNovaVenda();
                        break;
                        case "2":
                            sair = true;
                        break;
                        default:
                            Console.WriteLine("Opcoes Invalida");
                        break;


                    }




                } catch (Exception ex) {
                    Console.WriteLine($"\nErro na venda {ex.Message}");
                    Console.WriteLine("\nPressione uma tecla para continuar...");
                    Console.ReadKey();
                }

            }
        }//

        public static void iniciarNovaVenda()
        {
            Console.WriteLine("Deu certo");
            Console.WriteLine("-------- NOVA VENDA --------");
        }
    }
}
