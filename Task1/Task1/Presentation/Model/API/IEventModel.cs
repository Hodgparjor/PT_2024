using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model.API
{
    public interface IEventModel
    {
        int Id { get; set; }
        int productId { get; set; }
        int userId { get; set; }
        int quantity { get; set; }
        DateTime date { get; set; }

    }
}
