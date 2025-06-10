using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos
{
    public class Pedido
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; }
        public DateTime FechaPedido { get; set; }
        public List<ItemPedido> Items { get; set; } = new List<ItemPedido>();
        public decimal Comision => Items.Count(i => i.Seleccionado) * 0.50M;
        public decimal Subtotal => Items.Where(i => i.Seleccionado).Sum(i => i.Precio);
        public decimal Total => Subtotal + Comision;
    }
}