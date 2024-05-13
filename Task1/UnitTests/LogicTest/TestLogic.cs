using Logic;
using UnitTests.LogicTest.MockClasses;

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
            MockProduct deliveredProduct = new MockProduct(0, "testProduct", (decimal)9.99);
            MockSupplier supplier = new MockSupplier(0, "KOKO");
            mockedDataLayer.AddSupplier(supplier);
            int quantity = 5;
            dataService.DeliverProduct(supplier, deliveredProduct, quantity);
            Assert.IsTrue(mockedDataLayer.deliveryEventCreated);
        }

        [TestMethod]
        public void TestSold()
        {
            MockProduct soldProduct = new MockProduct(0, "testProduct", (decimal)9.99);
            MockCustomer customer = new MockCustomer(0, "Martin");
            int quantity = 5;
            mockedDataLayer.AddCatalogItem(soldProduct);
            mockedDataLayer.AddCustomer(customer);
            mockedDataLayer.AddWarehouseEntry(soldProduct, 5);

            dataService.SellProduct(soldProduct, customer, quantity);
            Assert.IsTrue(mockedDataLayer.saleEventCreated);
        }
    }
}
