﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model.API
{
    internal interface IWarehouseEntryModel
    {
        public int Id { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }
    }
}