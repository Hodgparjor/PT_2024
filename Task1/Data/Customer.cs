using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Customer
    {
		private int id;
		private string name;
		private List<WarehouseEntry> boughtProducts;

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

		public List<WarehouseEntry> BoughtProducts
		{
			get { return boughtProducts; }
			set { boughtProducts = value; }
		}

		public Customer(int id, string name)
		{
			Id = id;
			Name = name;
			boughtProducts = new List<WarehouseEntry>();
		}

        public override bool Equals(Object? obj)
        {
			if (obj == null) throw new ArgumentNullException();

			if (obj is Customer customer)
			{
				if (customer.Id == this.Id && customer.name.Equals(this.Name)) {
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
        }
    }
}
