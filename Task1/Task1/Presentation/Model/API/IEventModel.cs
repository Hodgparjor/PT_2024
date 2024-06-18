using System;

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
