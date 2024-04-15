using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAPI
    {

        public abstract void AddCustomer(int id, string name);
        public abstract bool RemoveCustomer(int id);

        public abstract void AddWarehouseEntry(int id, string name, decimal price, int quantity);
        public abstract void RemoveWarehouseEntry(int id);
        public abstract void UpdateWarehouseEntry(int id, string name, decimal price, int quantity);
        public abstract int GetAmountInWarehouseById(int id);

        public abstract void AddCatalogItem(int id, string name);
        public abstract void RemoveCatalogItem(int id);
        public abstract string GetCatalogItem(int id); //TODO


        //TODO Events
        public abstract void AddDeliveryEvent();
    }
}
