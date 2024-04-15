using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Customer
    {
		private int id;
		private string name;

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

		internal Customer(int id, string name)
		{
			Id = id;
			Name = name;
		}

	}
}
