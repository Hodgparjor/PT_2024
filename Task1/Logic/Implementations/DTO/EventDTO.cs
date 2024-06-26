﻿using Logic.API.DTO;


namespace Logic.Implementations.DTO
{
    internal class EventDTO : IEventDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public int Quantity { get; set; }

        public EventDTO(int id, int productId, int userId, DateTime occurrenceDate, int quantity)
        {
            this.Id = id;
            this.ProductId = productId;
            this.UserId = userId;
            this.Date = occurrenceDate;
            this.Quantity = quantity;
        }
    }
}
