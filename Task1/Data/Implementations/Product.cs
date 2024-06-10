using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Product : IProduct
    {
		private int id;
		private string name;
		private decimal price;
		private string? description;

		public string? Description
		{
			get { return description; }
			set { description = value; }
		}


		public decimal Price
		{
			get { return price; }
			set { price = value; }
		}


		public string Name
		{
			get { return name; }
			set { name = value; }
		}


		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		public Product(int id, string name, decimal price, string description) 
		{ 
			Id = id;
			Name = name;
			Price = price;
			Description = description;
		}

		public Product(int id, string name, decimal price) 
		{
			Id = id;
			Name = name;
			Price = price;
		}

	}
}
