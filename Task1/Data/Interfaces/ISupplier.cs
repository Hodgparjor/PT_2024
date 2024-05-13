using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ISupplier : IUser
    {
        public List<IWarehouseEntry> SuppliedProducts { get; set; }
    }
}
