﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data;

namespace Logic
{
    public class DataService
    {
        private DataLayerAbstract _dataLayer;
        public DataService(DataLayerAbstract dataLayer) 
        {
            _dataLayer = dataLayer;
        }

        public void SellProduct(int productID, int customerID, int quantity)
        {
            if(!_dataLayer.DoesWarehouseEntryExist(productID) || !_dataLayer.DoesCatalogItemExist(productID))
            {
                throw new Exception("Product does not exist.");
            }
            if(!_dataLayer.DoesCustomerExist(customerID))
            {
                throw new Exception("Customer does not exist.");
            }
            IWarehouseEntry entry = _dataLayer.GetWarehouseEntry(productID);
            if (entry.Quantity < quantity)
            {
                throw new Exception("There is insufficent amount of product in the warehouse.");
            }
            entry.Quantity -= quantity;

            _dataLayer.AddSoldEvent(customerID, productID, quantity);
        }

        public void DeliverProduct(ISupplier supplier, IProduct product, int quantity)
        {
            if (!_dataLayer.DoesCatalogItemExist(product.Id))
            {
                _dataLayer.AddCatalogItem(product);
            }

            if(_dataLayer.DoesWarehouseEntryExist(product.Id))
            {
                IWarehouseEntry entry = _dataLayer.GetWarehouseEntry(product.Id);
                entry.Quantity += quantity;
            }
            else
            {
                _dataLayer.AddWarehouseEntry(product, quantity);
            }

            _dataLayer.AddDeliveryEvent(supplier, product, quantity);
        }

    }
}