﻿using Presentation.Model.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    internal class ProductModel : IProductModel
    {
        public ProductModel(int id, string name, decimal price) {
            Id = id;
            Name = name;
            Price = price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
