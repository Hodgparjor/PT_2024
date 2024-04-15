using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    internal class DataService
    {
        private DataLayerAbstract _dataLayer;
        public DataService(DataLayerAbstract dataLayer) 
        {
            _dataLayer = dataLayer;
        }

        public void SellProduct(Product product, Customer customer, int quantity)
        {

        }
        public void DeliverProduct(Product product, int quantity)
        {

        }

    }
}
