using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Product
    {
		private int id;
		private string name;
		private decimal price;

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

		public Product(int id, string name, decimal price) 
		{ 
			Id = id;
			Name = name ?? "";
			Price = price;
		}

	}
}
