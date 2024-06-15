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
        private Random gen = new Random();
        public void GenerateEventModels(EventMasterVM vm)
        {
            IEventModelHandler handler = IEventModelHandler.CreateModelHandler(new MockEventCRUD());

            for(int i = 1; i <= 5; i++)
            {
                DateTime start = new DateTime(1995, 1, 1);
                int range = (DateTime.Today - start).Days;
                DateTime randomDate = start.AddDays(gen.Next(range));
                int quantity = gen.Next(1, 10);
                vm.Events.Add(new EventDetailVM(i, i, i, randomDate, quantity, handler));
            }
        }

        public void GenerateProductModels(ProductMasterVM vm)
        {
            IProductModelHandler handler = IProductModelHandler.CreateModelHandler(new MockProductCRUD());
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];

            for (int i = 1; i <= 5; i++)
            {
                for (int j = 0; j < stringChars.Length; j++)
                {
                    stringChars[j] = chars[gen.Next(chars.Length)];
                }
                vm.Products.Add(new ProductDetailVM(i, new String(stringChars), (decimal)(gen.NextDouble() + 0.1) * 100, handler));
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
