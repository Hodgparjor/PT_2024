using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Customer : User
    {
		
		private List<WarehouseEntry> boughtProducts;

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
				if (customer.Id == this.Id && customer.Name.Equals(this.Name)) {
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
