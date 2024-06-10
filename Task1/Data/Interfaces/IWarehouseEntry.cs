using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IWarehouseEntry
    {
        public int Quantity { get; set; }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public IProduct Product { get; set; }
    }
}
