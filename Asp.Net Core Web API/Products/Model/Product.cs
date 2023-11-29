using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Model
{
    internal class Product : IProduct
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }

        public void AddProducts()
        {
            throw new NotImplementedException();
        }
    }
}
