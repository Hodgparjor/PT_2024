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
    internal class RandomDataGenerator : IModelGenerator
    {
        public void GenerateEventModels(EventMasterVM vm)
        {
            IEventModelHandler handler = IEventModelHandler.CreateModelHandler(new MockEventCRUD());

            for(int i = 1; i <= 5; i++)
            {
                //vm.Events.Add()
            }
        }

        public void GenerateProductModels(ProductMasterVM vm)
        {
            IProductModelHandler handler = IProductModelHandler.CreateModelHandler(new MockProductCRUD());

            for(int i = 1; i <= 5; i++)
            {
                //vm.Products.Add(new ProductDetailVM())
            }
        }

        public void GenerateUserModels(UserMasterVM vm)
        {
            IUserModelHandler handler = IUserModelHandler.CreateModelHandler(new MockUserCRUD());

            for (int i = 1; i <= 5; i++)
            {

            }
        }

        public void GenerateWarehouseEntryModels(WarehouseEntryMasterVM vm)
        {
            IWarehouseEntryModelHandler handler = IWarehouseEntryModelHandler.CreateModelHandler(new MockWarehouseEntryCRUD());

            for (int i = 1; i <= 5; i++)
            {

            }
        }
    }
}
