using Presentation.Model.API;
using System;

namespace Presentation.Model
{
    internal class EventModel : IEventModel
    {
        public EventModel(int id, int productId, int userId, int quantity, DateTime Time) {
            Id = id;
            this.productId = productId;
            this.userId = userId;
            this.quantity = quantity;
            this.date = Time;
        }

        public int Id { get; set; }
        public int productId { get; set; }
        public int userId { get; set; }
        public int quantity { get; set; }
        public DateTime date { get; set; }
    }
}
