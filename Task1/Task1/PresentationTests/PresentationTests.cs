using Logic.API.CRUD;
using Presentation.Model.API;
using Presentation.ViewModel;
using PresentationTests.Mock;

namespace PresentationTests
{
    [TestClass]
    public class PresentationTests
    {

        [TestMethod]
        public void UserMasterVM_Tests()
        {
            IUserCRUD mockUserCRUD = new MockUserCRUD();

            IUserModelHandler handler = IUserModelHandler.CreateModelHandler(mockUserCRUD);

            UserMasterVM viewModel = new UserMasterVM(handler);

            viewModel.Name = "Test";

            Assert.IsNotNull(viewModel.CreateUser);
            Assert.IsNotNull(viewModel.RemoveUser);

            Assert.IsTrue(viewModel.CreateUser.CanExecute(null));

            Assert.IsTrue(viewModel.RemoveUser.CanExecute(null));
        }

        [TestMethod]
        public void UserDetailVM_Tests()
        {
            IUserCRUD mockUserCRUD = new MockUserCRUD();

            IUserModelHandler handler = IUserModelHandler.CreateModelHandler(mockUserCRUD);

            UserDetailVM viewModel = new UserDetailVM(1, "Test", handler);

            Assert.AreEqual(1, viewModel.Id);
            Assert.AreEqual("Test", viewModel.Name);

            Assert.IsTrue(viewModel.UpdateUser.CanExecute(null));
        }

        [TestMethod]
        public void ProductMasterViewModelTests()
        {
            IProductCRUD MockProductCRUD = new MockProductCRUD();

            IProductModelHandler handler = IProductModelHandler.CreateModelHandler(MockProductCRUD);

            ProductMasterVM master = new ProductMasterVM(handler);

            master.Name = "test";
            master.Price = 100;

            Assert.IsNotNull(master.CreateProduct);
            Assert.IsNotNull(master.RemoveProduct);

            Assert.IsTrue(master.CreateProduct.CanExecute(null));

            master.Price = -1;

            Assert.IsFalse(master.CreateProduct.CanExecute(null));

            Assert.IsTrue(master.RemoveProduct.CanExecute(null));
        }

        [TestMethod]
        public void ProductDetailViewModelTests()
        {
            IProductCRUD MockProductCRUD = new MockProductCRUD();

            IProductModelHandler handler = IProductModelHandler.CreateModelHandler(MockProductCRUD);

            ProductDetailVM detail = new ProductDetailVM(1, "test", 100, handler);

            Assert.AreEqual(1, detail.Id);
            Assert.AreEqual("test", detail.Name);
            Assert.AreEqual(100, detail.Price);

            Assert.IsTrue(detail.UpdateProduct.CanExecute(null));
        }

        [TestMethod]
        public void WarehouseEntryMasterViewModelTests()
        {
            IWarehouseEntryCRUD MockWarehouseEntryCRUD = new MockWarehouseEntryCRUD();

            IWarehouseEntryModelHandler handler = IWarehouseEntryModelHandler.CreateModelHandler(MockWarehouseEntryCRUD);

            WarehouseEntryMasterVM master = new WarehouseEntryMasterVM(handler);

            master.ProductId = 1;
            master.Quantity = 1;

            Assert.IsNotNull(master.CreateWarehouseEntry);
            Assert.IsNotNull(master.RemoveWarehouseEntry);

            Assert.IsTrue(master.CreateWarehouseEntry.CanExecute(null));

            master.Quantity = -1;

            Assert.IsFalse(master.CreateWarehouseEntry.CanExecute(null));

            Assert.IsTrue(master.RemoveWarehouseEntry.CanExecute(null));
        }

        [TestMethod]
        public void WarehouseEntryDetailViewModelTests()
        {
            IWarehouseEntryCRUD MockWarehouseEntryCRUD = new MockWarehouseEntryCRUD();

            IWarehouseEntryModelHandler handler = IWarehouseEntryModelHandler.CreateModelHandler(MockWarehouseEntryCRUD);

            WarehouseEntryDetailVM detail = new WarehouseEntryDetailVM(1, 1, 1, handler);

            Assert.AreEqual(1, detail.Id);
            Assert.AreEqual(1, detail.ProductId);
            Assert.AreEqual(1, detail.Quantity);

            Assert.IsTrue(detail.UpdateWarehouseEntry.CanExecute(null));
        }

        [TestMethod]
        public void EventMasterViewModelTests()
        {
            IEventCRUD MockEventCRUD = new MockEventCRUD();

            IEventModelHandler handler = IEventModelHandler.CreateModelHandler(MockEventCRUD);

            EventMasterVM master = new EventMasterVM(handler);

            master.WarehouseEntryId = 1;
            master.UserId = 1;

            Assert.IsNotNull(master.RemoveEvent);

            Assert.IsFalse(master.CreateEvent.CanExecute(null));

            master.Quantity = 1;

            Assert.IsTrue(master.CreateEvent.CanExecute(null));

            Assert.IsTrue(master.RemoveEvent.CanExecute(null));
        }

        [TestMethod]
        public void EventDetailViewModelTests()
        {
            IEventCRUD MockEventCRUD = new MockEventCRUD();

            IEventModelHandler handler = IEventModelHandler.CreateModelHandler(MockEventCRUD);

            EventDetailVM detail = new EventDetailVM(1, 1, 1, new DateTime(2012, 3, 4), 1, handler);

            Assert.AreEqual(1, detail.Id);
            Assert.AreEqual(1, detail.WarehouseEntryId);
            Assert.AreEqual(1, detail.UserId);
            Assert.AreEqual(new DateTime(2012, 3, 4), detail.Date);

            Assert.IsTrue(detail.UpdateEvent.CanExecute(null));
        }

        [TestMethod]
        public void TestConstantData()
        {
            IModelGenerator generator = new ConstantDataGenerator();
            UserMasterVM userMaster = new UserMasterVM(IUserModelHandler.CreateModelHandler(new MockUserCRUD()));
            WarehouseEntryMasterVM warehouseEntryMaster = new WarehouseEntryMasterVM(IWarehouseEntryModelHandler.CreateModelHandler(new MockWarehouseEntryCRUD()));
            ProductMasterVM productMaster = new ProductMasterVM(IProductModelHandler.CreateModelHandler(new MockProductCRUD()));
            EventMasterVM eventMaster = new EventMasterVM(IEventModelHandler.CreateModelHandler(new MockEventCRUD()));

            generator.GenerateProductModels(productMaster);
            generator.GenerateUserModels(userMaster);
            generator.GenerateWarehouseEntryModels(warehouseEntryMaster);
            generator.GenerateEventModels(eventMaster);

            Assert.AreEqual(4, productMaster.Products.Count);
            Assert.AreEqual(4, userMaster.Users.Count);
            Assert.AreEqual(4, warehouseEntryMaster.WarehouseEntries.Count);
            Assert.AreEqual(4, eventMaster.Events.Count);

            Assert.IsNotNull(userMaster.CreateUser);
            Assert.IsNotNull(userMaster.RemoveUser);
            Assert.IsNotNull(productMaster.CreateProduct);
            Assert.IsNotNull(productMaster.RemoveProduct);
            Assert.IsNotNull(warehouseEntryMaster.CreateWarehouseEntry);
            Assert.IsNotNull(warehouseEntryMaster.RemoveWarehouseEntry);
            Assert.IsNotNull(eventMaster.CreateEvent);
            Assert.IsNotNull(eventMaster.RemoveEvent);
        }

        [TestMethod]
        public void TestRandomData()
        {
            IModelGenerator generator = new RandomDataGenerator();
            UserMasterVM userMaster = new UserMasterVM(IUserModelHandler.CreateModelHandler(new MockUserCRUD()));
            WarehouseEntryMasterVM warehouseEntryMaster = new WarehouseEntryMasterVM(IWarehouseEntryModelHandler.CreateModelHandler(new MockWarehouseEntryCRUD()));
            ProductMasterVM productMaster = new ProductMasterVM(IProductModelHandler.CreateModelHandler(new MockProductCRUD()));
            EventMasterVM eventMaster = new EventMasterVM(IEventModelHandler.CreateModelHandler(new MockEventCRUD()));

            generator.GenerateProductModels(productMaster);
            generator.GenerateUserModels(userMaster);
            generator.GenerateWarehouseEntryModels(warehouseEntryMaster);
            generator.GenerateEventModels(eventMaster);

            Assert.AreEqual(5, productMaster.Products.Count);
            Assert.AreEqual(5, userMaster.Users.Count);
            Assert.AreEqual(5, warehouseEntryMaster.WarehouseEntries.Count);
            Assert.AreEqual(5, eventMaster.Events.Count);

            Assert.IsNotNull(userMaster.CreateUser);
            Assert.IsNotNull(userMaster.RemoveUser);
            Assert.IsNotNull(productMaster.CreateProduct);
            Assert.IsNotNull(productMaster.RemoveProduct);
            Assert.IsNotNull(warehouseEntryMaster.CreateWarehouseEntry);
            Assert.IsNotNull(warehouseEntryMaster.RemoveWarehouseEntry);
            Assert.IsNotNull(eventMaster.CreateEvent);
            Assert.IsNotNull(eventMaster.RemoveEvent);
        }


    }
}