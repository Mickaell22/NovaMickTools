using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ItemPedido
    {
        public int Numero { get; set; }
        public string Link { get; set; }
        public string Articulo { get; set; }
        public decimal Precio { get; set; }
        public bool Seleccionado { get; set; } = true;
    }
}
