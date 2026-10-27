using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Model
{
    internal class Compra
    {
        private int _id;
        private DateTime _dataHora;
        private int _funcionarioId;
        private int _clienteId;

        private List<ItemCompra> Itens = new List<ItemCompra>();

        public Compra(DateTime dataHora, int funcionarioId, int clienteId, List<ItemCompra> itens)
        {
            if (funcionarioId <= 0) throw new ArgumentOutOfRangeException("O funcionario responsavel é invalido");

            _dataHora = dataHora;
            this._funcionarioId = funcionarioId;
            this._clienteId = clienteId;
            
        }

        public Compra(int id, DateTime dataHora, int funcionarioId, int clienteId)
            :this(dataHora, funcionarioId, clienteId)
        {
            if(id <= 0) throw new ArgumentOutOfRangeException("id da compra invalido");
            this._id = id;
            
            
        }

        public decimal CalcularTotal()
        {
            decimal vTotal = 0;
            foreach (ItemCompra itemCompra in Itens) {

                vTotal += itemCompra.CalcularSubtotal();
            }

            return Itens.Sum(item => item.CalcularSubtotal()); 

        }

        public void AdicionarItem(Produto produto, int quantidade)
        {
            if (produto == null) throw new ArgumentNullException("Produto Inexistente");

            ItemCompra novoItem = new ItemCompra(produto.Id, quantidade, produto.Preco);
            this.Itens.Add(novoItem);
        }

        public void RemoverItem(ItemCompra item)
        {
            this.itens.Remove(item);
        }

        public static int Inserir(Compra compra)
        {
            if(compra._clienteId == 0) compra._clienteId = 10000;
            using (var cmd = new MySqlCommand("sp_InserirCompra"))
            {
                cmd.Parameters.AddWithValue("p_DataHora", compra.DataHora);
                cmd.Parameters.AddWithValue("p_FuncionarioId", compra._funcionarioId);
                cmd.Parameters.AddWithValue("p_ClienteId", compra._dataHora);

                object resultado = Banco.executarComandoComRetornoId(cmd);

                if(resultado != null && int.TryParse(resultado.ToString(), out int idGerado)){ 
                    compra.Id = idGerado;
                    return idGerado;
                }

                return -1;

            }
        }
    }
}
