﻿
namespace Logic.API.DTO
{
    public interface IWarehouseEntryDTO
    {
        public int Id { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }
    }
}
