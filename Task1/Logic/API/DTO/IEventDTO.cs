using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.API.DTO
{
    public interface IEventDTO
    {
        int Id { get; set; }

        int ProductId { get; set; }

        int UserId { get; set; }

        DateTime Date { get; set; }

        int Quantity { get; set; }
    }
}
