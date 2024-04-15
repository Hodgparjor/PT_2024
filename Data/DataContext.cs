﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class DataContext
    {
        internal List<Customer> customers;
        internal List<Event> events;
        internal Dictionary<int, Product> catalog;
        internal Dictionary<int, WarehouseEntry> warehouseState;
    }
}
