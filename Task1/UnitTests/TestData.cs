using Data;
using Data.Interfaces;

namespace DataTests
{
    [TestClass]
    [DeploymentItem("DatabaseForTests.mdf")]
    public class DataTests
    {
        private static string connectionString;

        private readonly DataLayerAbstract _dataLayer = DataLayerAbstract.CreateMyDataLayer(new DataContext(connectionString));

        [ClassInitialize]
        public static void ClassInitializeMethod(TestContext context)
        {
            string _DBRelativePath = @"TestDB.mdf";
            string _projectRootDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string _DBPath = Path.Combine(_projectRootDir, _DBRelativePath);
            FileInfo _databaseFile = new FileInfo(_DBPath);
            Assert.IsTrue(_databaseFile.Exists, $"{Environment.CurrentDirectory}");
            connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
        }

        [TestMethod]
        public async Task UserTests()
        {
            await _dataLayer.ClearTables();
            int userId = 1;

            await _dataLayer.AddCustomerAsync(userId, "test");

            IUser user = await _dataLayer.GetCustomerAsync(userId);

            Assert.IsNotNull(user);
            Assert.AreEqual(userId, user.Id);
            Assert.AreEqual("test", user.Name);

            Assert.IsNotNull(await _dataLayer.GetAllCustomersAsync());
            Assert.IsTrue(await _dataLayer.GetCustomerCountAsync() > 0);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataLayer.GetCustomerAsync(userId + 2));

            await _dataLayer.UpdateCustomerAsync(userId, "test2");

            IUser userUpdated = await _dataLayer.GetCustomerAsync(userId);

            Assert.IsNotNull(userUpdated);
            Assert.AreEqual(userId, userUpdated.Id);
            Assert.AreEqual("test2", userUpdated.Name);


            await _dataLayer.DeleteCustomerAsync(userId);
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataLayer.GetCustomerAsync(userId));
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataLayer.DeleteCustomerAsync(userId));
        }

        [TestMethod]
        public async Task ProductTests()
        {
            await _dataLayer.ClearTables();
            int productId = 5;

            await _dataLayer.AddProductAsync(productId, "ERC505", 125);

            IProduct product = await _dataLayer.GetProductAsync(productId);

            Assert.IsNotNull(product);
            Assert.AreEqual(productId, product.Id);
            Assert.AreEqual("ERC505", product.Name);
            Assert.AreEqual(125, product.Price);

            Assert.IsNotNull(await _dataLayer.GetAllProductsAsync());
            Assert.IsTrue(await _dataLayer.GetProductsCountAsync() > 0);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataLayer.GetProductAsync(12));

            await _dataLayer.UpdateProductAsync(productId, "ERC505H", 135);

            IProduct productUpdated = await _dataLayer.GetProductAsync(productId);

            Assert.IsNotNull(productUpdated);
            Assert.AreEqual(productId, productUpdated.Id);
            Assert.AreEqual("ERC505H", productUpdated.Name);
            Assert.AreEqual(135, productUpdated.Price);
            await _dataLayer.DeleteProductAsync(productId);
        }

        [TestMethod]
        public async Task WarehouseEntryTest()
        {
            await _dataLayer.ClearTables();
            int productId = 3;
            int entryId = 3;

            await _dataLayer.AddProductAsync(productId, "ERC505", 125);

            IProduct product = await _dataLayer.GetProductAsync(productId);

            await _dataLayer.AddWarehouseEntryAsync(entryId, productId, 5);

            IWarehouseEntry warehouseEntry = await _dataLayer.GetWarehouseEntryAsync(entryId);

            Assert.IsNotNull(warehouseEntry);
            Assert.AreEqual(entryId, warehouseEntry.Id);
            Assert.AreEqual(productId, warehouseEntry.ProductId);
            Assert.AreEqual(5, warehouseEntry.Quantity);

            Assert.IsNotNull(await _dataLayer.GetAllWarehouseEntriesAsync());
            Assert.IsTrue(await _dataLayer.GetWarehouseEntriesCountAsync() > 0);

            await _dataLayer.UpdateWarehouseEntryAsync(entryId, productId, 50);

            IWarehouseEntry updatedWarehouseEntry = await _dataLayer.GetWarehouseEntryAsync(entryId);

            Assert.IsNotNull(updatedWarehouseEntry);
            Assert.AreEqual(entryId, updatedWarehouseEntry.Id);
            Assert.AreEqual(productId, updatedWarehouseEntry.ProductId);
            Assert.AreEqual(50, updatedWarehouseEntry.Quantity);

            await Assert.ThrowsExceptionAsync<Exception>(async () => await _dataLayer.UpdateWarehouseEntryAsync(entryId, productId, -1));

            await _dataLayer.DeleteWarehouseEntryAsync(entryId);
            await _dataLayer.DeleteProductAsync(productId);
        }

        [TestMethod]
        public async Task EventTests()
        {
            await _dataLayer.ClearTables();
            int eventId = 6;
            int userId = 6;
            int productId = 6;
            int entryId = 6;

            await _dataLayer.AddProductAsync(productId, "ERC505", 100);
            await _dataLayer.AddWarehouseEntryAsync(entryId, productId, 10);
            await _dataLayer.AddCustomerAsync(userId, "test");

            IProduct product = await _dataLayer.GetProductAsync(productId);
            IWarehouseEntry entry = await _dataLayer.GetWarehouseEntryAsync(entryId);
            IUser user = await _dataLayer.GetCustomerAsync(userId);

            await _dataLayer.AddEventAsync(eventId, entryId, userId, 5);

            IEventSold soldEvent = await _dataLayer.GetEventAsync(eventId);

            entry = await _dataLayer.GetWarehouseEntryAsync(entryId);
            Assert.IsNotNull(soldEvent);
            Assert.AreEqual(eventId, soldEvent.Id);
            Assert.AreEqual(entryId, soldEvent.WarehouseEntryId);
            Assert.AreEqual(userId, soldEvent.CustomerId);
            Assert.AreEqual(5, entry.Quantity);

            Assert.IsNotNull(await _dataLayer.GetAllEventsAsync());
            Assert.IsTrue(await _dataLayer.GetEventsCountAsync() > 0);

            await _dataLayer.UpdateEventAsync(eventId, entryId, userId, new DateTime(2002, 7, 8), 2);

            IEventSold eventUpdated = await _dataLayer.GetEventAsync(eventId);

            Assert.IsNotNull(eventUpdated);
            Assert.AreEqual(eventId, eventUpdated.Id);
            Assert.AreEqual(entryId, eventUpdated.WarehouseEntryId);
            Assert.AreEqual(userId, eventUpdated.CustomerId);

            await _dataLayer.DeleteEventAsync(eventId);
            await _dataLayer.DeleteWarehouseEntryAsync(entryId);
            await _dataLayer.DeleteProductAsync(productId);
            await _dataLayer.DeleteCustomerAsync(userId);
        }

    }
}