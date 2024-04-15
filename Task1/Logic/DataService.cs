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
            if(!_dataLayer.DoesWarehouseEntryExist(product.Id) || !_dataLayer.DoesCatalogItemExist(product.Id))
            {
                throw new Exception("Product does not exist.");
            }
            if(!_dataLayer.DoesCustomerExist(customer.Id))
            {
                throw new Exception("Customer does not exist.");
            }
            WarehouseEntry entry = _dataLayer.GetWarehouseEntry(product.Id);
            if (entry.Quantity < quantity)
            {
                throw new Exception("There is insufficent amount of product in the warehouse.");
            }
            entry.Quantity -= quantity;

            _dataLayer.AddSoldEvent(customer, product, quantity);
        }

        public void DeliverProduct(Product product, int quantity)
        {
            if (!_dataLayer.DoesCatalogItemExist(product.Id))
            {
                _dataLayer.AddCatalogItem(product);
            }

            if(_dataLayer.DoesWarehouseEntryExist(product.Id))
            {
                WarehouseEntry entry = _dataLayer.GetWarehouseEntry(product.Id);
                entry.Quantity += quantity;
            }
            else
            {
                _dataLayer.AddWarehouseEntry(new WarehouseEntry(product, quantity));
            }

            _dataLayer.AddDeliveryEvent(product, quantity);
        }

    }
}
