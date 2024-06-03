using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IEventSold : IEvent
    {
        int Id { get; set; }
        int Quantity { get; set; }
        int CustomerId { get; set; }
        int ProductId { get; set; }
    }
}
