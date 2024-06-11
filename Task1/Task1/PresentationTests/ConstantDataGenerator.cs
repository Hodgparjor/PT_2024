using Presentation.Model.API;
using Presentation.ViewModel;
using PresentationTests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTests
{
    internal class ConstantDataGenerator : IModelGenerator
    {
        public void GenerateEventModels(EventMasterVM vm)
        {
            IEventModelHandler handler = IEventModelHandler.CreateModelHandler(new MockEventCRUD());
            vm.Events.Add(new EventDetailVM(1, 1, 1, new DateTime(2001, 1, 1), 1, handler));
            vm.Events.Add(new EventDetailVM(2, 2, 2, new DateTime(2002, 2, 2), 2, handler));
            vm.Events.Add(new EventDetailVM(3, 3, 3, new DateTime(2003, 3, 3), 3, handler));
            vm.Events.Add(new EventDetailVM(4, 4, 4, new DateTime(2004, 4, 4), 4, handler));
        }

        public void GenerateProductModels(ProductMasterVM vm)
        {
            IProductModelHandler handler = IProductModelHandler.CreateModelHandler(new MockProductCRUD());
            vm.Products.Add(new ProductDetailVM(1, "test1", 10, handler));
            vm.Products.Add(new ProductDetailVM(2, "test2", 20, handler));
            vm.Products.Add(new ProductDetailVM(3, "test3", 30, handler));
            vm.Products.Add(new ProductDetailVM(4, "test4", 40, handler));

        }

        public void GenerateUserModels(UserMasterVM vm)
        {
            IUserModelHandler handler = IUserModelHandler.CreateModelHandler(new MockUserCRUD());
            vm.Users.Add(new UserDetailVM(1, "test1", handler));
            vm.Users.Add(new UserDetailVM(2, "test2", handler));
            vm.Users.Add(new UserDetailVM(3, "test3", handler));
            vm.Users.Add(new UserDetailVM(4, "test4", handler));
        }

        public void GenerateWarehouseEntryModels(WarehouseEntryMasterVM vm)
        {
            IWarehouseEntryModelHandler handler = IWarehouseEntryModelHandler.CreateModelHandler(new MockWarehouseEntryCRUD());
            vm.WarehouseEntries.Add(new WarehouseEntryDetailVM(1, 1, 1, handler));
            vm.WarehouseEntries.Add(new WarehouseEntryDetailVM(2, 2, 2, handler));
            vm.WarehouseEntries.Add(new WarehouseEntryDetailVM(3, 3, 3, handler));
            vm.WarehouseEntries.Add(new WarehouseEntryDetailVM(4, 4, 4, handler));
        }
    }
}
