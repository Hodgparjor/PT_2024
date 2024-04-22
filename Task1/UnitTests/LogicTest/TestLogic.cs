using Data;
using Logic;
using UnitTests.LogicTest;

namespace UnitTests
{
    [TestClass]
    public class TestLogic
    {
        DataService dataService;
        MockDataLayer mockedDataLayer;

        [TestInitialize]
        public void TestInitialize()
        {
            mockedDataLayer = new MockDataLayer();
            dataService = new DataService(mockedDataLayer);
        }

        [TestMethod]
        public void TestDelivery()
        {
            Product deliveredProduct = new Product(0, "testProduct", (decimal)9.99);
            Supplier supplier = new Supplier(0, "KOKO");
            mockedDataLayer.AddSupplier(supplier);
            int quantity = 5;
            dataService.DeliverProduct(supplier, deliveredProduct, quantity);
            Assert.IsTrue(mockedDataLayer.deliveryEventCreated);
        }

        [TestMethod]
        public void TestSold()
        {
            Product soldProduct = new Product(0, "testProduct", (decimal)9.99);
            Customer customer = new Customer(0, "Martin");
            int quantity = 5;
            mockedDataLayer.AddCatalogItem(soldProduct);
            mockedDataLayer.AddCustomer(customer);
            mockedDataLayer.AddWarehouseEntry(new WarehouseEntry(soldProduct, 5));

            dataService.SellProduct(soldProduct, customer, quantity);
            Assert.IsTrue(mockedDataLayer.saleEventCreated);
        }
    }
}
