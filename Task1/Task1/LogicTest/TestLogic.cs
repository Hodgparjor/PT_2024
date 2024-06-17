using Logic;
using Logic.API.CRUD;
using Logic.API.DTO;
using LogicTest.MockClasses;

namespace LogicTest
{
    [TestClass]
    public class TestLogic
    {
        private readonly MockRepository _dataLayer = new MockRepository();

        [TestMethod]
        public async Task UserServiceTests()
        {
            IUserCRUD userCrud = IUserCRUD.CreateUserCRUD(this._dataLayer);

            await userCrud.AddUserAsync(1, "test1");

            IUserDTO user = await userCrud.GetUserAsync(1);

            Assert.IsNotNull(user);
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("test1", user.Name);

            Assert.IsNotNull(await userCrud.GetAllUsersAsync());
            Assert.IsTrue(await userCrud.GetUsersCountAsync() > 0);

            await userCrud.UpdateUserAsync(1, "test2");

            IUserDTO updatedUser = await userCrud.GetUserAsync(1);

            Assert.IsNotNull(updatedUser);
            Assert.AreEqual(1, updatedUser.Id);
            Assert.AreEqual("test2", updatedUser.Name);

            await userCrud.DeleteUserAsync(1);
        }

        [TestMethod]
        public async Task ProductServiceTests()
        {
            IProductCRUD productCrud = IProductCRUD.CreateProductCRUD(this._dataLayer);

            await productCrud.AddProductAsync(1, "ERC18", 95);

            IProductDTO product = await productCrud.GetProductAsync(1);

            Assert.IsNotNull(product);
            Assert.AreEqual(1, product.Id);
            Assert.AreEqual("ERC18", product.Name);
            Assert.AreEqual(95, product.Price);

            Assert.IsNotNull(await productCrud.GetAllProductsAsync());
            Assert.IsTrue(await productCrud.GetProductsCountAsync() > 0);

            await productCrud.UpdateProductAsync(1, "ERC22", 250);

            IProductDTO updatedProduct = await productCrud.GetProductAsync(1);

            Assert.IsNotNull(updatedProduct);
            Assert.AreEqual(1, updatedProduct.Id);
            Assert.AreEqual("ERC22", updatedProduct.Name);
            Assert.AreEqual(250, updatedProduct.Price);

            await productCrud.DeleteProductAsync(1);
        }

        [TestMethod]
        public async Task WarehouseEntryServiceTests()
        {
            IProductCRUD productCrud = IProductCRUD.CreateProductCRUD(this._dataLayer);

            await productCrud.AddProductAsync(1, "ERC22", 250);

            IProductDTO product = await productCrud.GetProductAsync(1);

            IWarehouseEntryCRUD warehouseEntryCrud = IWarehouseEntryCRUD.CreateWarehouseEntryCRUD(this._dataLayer);

            await warehouseEntryCrud.AddWarehouseEntryAsync(1, product.Id, 50);

            IWarehouseEntryDTO state = await warehouseEntryCrud.GetWarehouseEntryAsync(1);

            Assert.IsNotNull(state);
            Assert.AreEqual(1, state.Id);
            Assert.AreEqual(1, state.productId);
            Assert.AreEqual(50, state.quantity);

            await warehouseEntryCrud.UpdateWarehouseEntryAsync(1, product.Id, 5);

            IWarehouseEntryDTO updatedState = await warehouseEntryCrud.GetWarehouseEntryAsync(1);

            Assert.IsNotNull(updatedState);
            Assert.AreEqual(1, updatedState.Id);
            Assert.AreEqual(1, updatedState.productId);
            Assert.AreEqual(5, updatedState.quantity);

            await warehouseEntryCrud.DeleteWarehouseEntryAsync(1);
            await productCrud.DeleteProductAsync(1);
        }

        [TestMethod]
        public async Task EventServiceTests()
        {
            IProductCRUD productCrud = IProductCRUD.CreateProductCRUD(this._dataLayer);

            await productCrud.AddProductAsync(1, "ERC22", 250);

            IProductDTO product = await productCrud.GetProductAsync(1);

            IWarehouseEntryCRUD warehouseEntryCrud = IWarehouseEntryCRUD.CreateWarehouseEntryCRUD(this._dataLayer);

            await warehouseEntryCrud.AddWarehouseEntryAsync(1, product.Id, 50);

            IWarehouseEntryDTO state = await warehouseEntryCrud.GetWarehouseEntryAsync(1);

            IUserCRUD userCrud = IUserCRUD.CreateUserCRUD(this._dataLayer);

            await userCrud.AddUserAsync(1, "user");

            IUserDTO user = await userCrud.GetUserAsync(1);

            IEventCRUD eventCrud = IEventCRUD.CreateEventCRUD(this._dataLayer);

            await eventCrud.AddEventAsync(1, state.Id, user.Id, 25);

            user = await userCrud.GetUserAsync(1);
            state = await warehouseEntryCrud.GetWarehouseEntryAsync(1);

            Assert.AreEqual(25, state.quantity);

            await eventCrud.DeleteEventAsync(1);
            await warehouseEntryCrud.DeleteWarehouseEntryAsync(1);
            await productCrud.DeleteProductAsync(1);
            await userCrud.DeleteUserAsync(1);
        }
        //[TestMethod]
        //public void TestDelivery()
        //{
        //    MockProduct deliveredProduct = new MockProduct(0, "testProduct", (decimal)9.99);
        //    MockSupplier supplier = new MockSupplier(0, "KOKO");
        //    mockedDataLayer.AddSupplier(supplier);
        //    int quantity = 5;
        //    dataService.DeliverProduct(supplier, deliveredProduct, quantity);
        //    Assert.IsTrue(mockedDataLayer.deliveryEventCreated);
        //}

        //[TestMethod]
        //public void TestSold()
        //{
        //    MockProduct soldProduct = new MockProduct(0, "testProduct", (decimal)9.99);
        //    MockCustomer customer = new MockCustomer(0, "Martin");
        //    int quantity = 5;
        //    mockedDataLayer.AddCatalogItem(soldProduct);
        //    mockedDataLayer.AddCustomer(customer);
        //    mockedDataLayer.AddWarehouseEntry(soldProduct, 5);

        //    dataService.SellProduct(soldProduct, customer, quantity);
        //    Assert.IsTrue(mockedDataLayer.saleEventCreated);
        //}
    }
}
