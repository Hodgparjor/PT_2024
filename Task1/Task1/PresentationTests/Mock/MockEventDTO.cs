using Logic.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTests.Mock
{
    internal class MockEventDTO : IEventDTO
    {
        public MockEventDTO(int id, int stateId, int userId, int quantity)
        {
            this.Id = id;
            this.ProductId = stateId;
            this.UserId = userId;
            this.Date = DateTime.Now;
            this.Quantity = quantity;
        }

        public int Id { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public int Quantity { get; set; }
    }
}
