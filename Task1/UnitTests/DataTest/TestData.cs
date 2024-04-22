using Data;
using UnitTests.DataTest;

namespace UnitTests
{
    [TestClass]
    public class TestData
    {
        DataLayerAbstract emptyDataLayer, standardDataLayer, randomDataLayer;

        [TestInitialize]
        public void TestInitialize()
        {
            emptyDataLayer = DataLayerAbstract.CreateMyDataLayer();

            IDataGenerator standardGenerator = new StandardDataGenerator();
            //IDataGenerator randomGenerator = new RandomDataGenerator();

            DataContext standardDataContext = new DataContext(standardGenerator.GenerateCustomers(),
                                                                standardGenerator.GenerateSuppliers(),
                                                                standardGenerator.GenerateEvents(),
                                                                standardGenerator.GenerateProducts(),
                                                                standardGenerator.GenerateWarehouseEntries());

            standardDataLayer = DataLayerAbstract.CreateMyDataLayer(standardDataContext);
        }

        [TestMethod]
        public void TestEmptyData_Catalog()
        {
            Assert.AreEqual(emptyDataLayer.GetAllCatalogItems().Count, 0);
            emptyDataLayer.AddCatalogItem(new Product(0, "ERC18", (decimal)999.9));
            Assert.AreEqual(emptyDataLayer.GetAllCatalogItems().Count, 1);
            Assert.IsTrue(emptyDataLayer.DoesCatalogItemExist(0));
            Assert.IsTrue(emptyDataLayer.RemoveCatalogItem(0));
            Assert.AreEqual(emptyDataLayer.GetAllCatalogItems().Count, 0);
            Assert.IsFalse(emptyDataLayer.DoesCatalogItemExist(0));
        }

        [TestMethod]
        public void TestEmptyData_Customers()
        {
            Assert.AreEqual(emptyDataLayer.GetAllCustomers().Count, 0);
            emptyDataLayer.AddCustomer(new Customer(0, "John Doe"));
            Assert.AreEqual(emptyDataLayer.GetAllCustomers().Count, 1);
            Assert.IsTrue(emptyDataLayer.DoesCustomerExist(0));
            Assert.IsTrue(emptyDataLayer.GetCustomer(0).Equals(new Customer(0, "John Doe")));
            Assert.IsTrue(emptyDataLayer.RemoveCustomer(0));
            Assert.AreEqual(emptyDataLayer.GetAllCustomers().Count, 0);
            Assert.IsFalse(emptyDataLayer.DoesCustomerExist(0));
        }

        [TestMethod]
        public void TestEmptyData_Warehouse()
        {
            Assert.AreEqual(emptyDataLayer.GetAllWarehouseEntries().Count, 0);
            emptyDataLayer.AddWarehouseEntry(new WarehouseEntry(new Product(1, "test", (decimal)12.34), 55));
            Assert.AreEqual(emptyDataLayer.GetAllWarehouseEntries().Count, 1);
            Assert.IsTrue(emptyDataLayer.GetWarehouseEntry(1).Quantity == 55);
            Assert.IsTrue(emptyDataLayer.DoesWarehouseEntryExist(1));
            Assert.IsTrue(emptyDataLayer.RemoveWarehouseEntry(1));
            Assert.AreEqual(emptyDataLayer.GetAllWarehouseEntries().Count, 0);
            Assert.IsFalse(emptyDataLayer.DoesWarehouseEntryExist(1));
        }

        [TestMethod]
        public void TestEmptyData_Suppliers()
        {
            Assert.AreEqual(emptyDataLayer.GetAllSuppliers().Count, 0);
            emptyDataLayer.AddSupplier(new Supplier(1, "test supplier"));
            Assert.AreEqual(emptyDataLayer.GetAllSuppliers().Count, 1);
            Assert.IsTrue(emptyDataLayer.DoesSupplierExists(1));
            Assert.IsTrue(emptyDataLayer.GetSupplier(1).Name.Equals("test supplier"));
            Assert.IsTrue(emptyDataLayer.RemoveSupplier(1));
            Assert.AreEqual(emptyDataLayer.GetAllSuppliers().Count, 0);
            Assert.IsFalse(emptyDataLayer.DoesSupplierExists(1));
        }

        [TestMethod]
        public void TestStandardData_Catalog()
        {
            Assert.AreEqual(standardDataLayer.GetAllCatalogItems().Count, 3);
            standardDataLayer.AddCatalogItem(new Product(0, "ERC18", (decimal)999.9));
            Assert.AreEqual(standardDataLayer.GetAllCatalogItems().Count, 4);
            Assert.IsTrue(standardDataLayer.DoesCatalogItemExist(0));
            Assert.IsTrue(standardDataLayer.RemoveCatalogItem(0));
            Assert.AreEqual(standardDataLayer.GetAllCatalogItems().Count, 3);
            Assert.IsFalse(standardDataLayer.DoesCatalogItemExist(0));
        }

        [TestMethod]
        public void TestStandardData_Customers()
        {
            Assert.AreEqual(standardDataLayer.GetAllCustomers().Count, 4);
            standardDataLayer.AddCustomer(new Customer(0, "Margaret Doe"));
            Assert.AreEqual(standardDataLayer.GetAllCustomers().Count, 5);
            Assert.IsTrue(standardDataLayer.DoesCustomerExist(2));
            Assert.IsTrue(standardDataLayer.GetCustomer(2).Equals(new Customer(2, "Jane Doe")));
            Assert.IsTrue(standardDataLayer.RemoveCustomer(2));
            Assert.AreEqual(standardDataLayer.GetAllCustomers().Count, 4);
            Assert.IsFalse(standardDataLayer.DoesCustomerExist(2));
        }

        [TestMethod]
        public void TestStandardData_Warehouse()
        {
            Assert.AreEqual(standardDataLayer.GetAllWarehouseEntries().Count, 3);
            standardDataLayer.AddWarehouseEntry(new WarehouseEntry(new Product(0, "test", (decimal)12.34), 55));
            Assert.AreEqual(standardDataLayer.GetAllWarehouseEntries().Count, 4);
            Assert.IsTrue(standardDataLayer.GetWarehouseEntry(3).Quantity == 124);
            Assert.IsTrue(standardDataLayer.DoesWarehouseEntryExist(2));
            Assert.IsTrue(standardDataLayer.RemoveWarehouseEntry(2));
            Assert.AreEqual(standardDataLayer.GetAllWarehouseEntries().Count, 3);
            Assert.IsFalse(standardDataLayer.DoesWarehouseEntryExist(2));
        }

        [TestMethod]
        public void TestStandardData_Suppliers()
        {
            Assert.AreEqual(emptyDataLayer.GetAllSuppliers().Count, 0);
            emptyDataLayer.AddSupplier(new Supplier(1, "test supplier"));
            Assert.AreEqual(emptyDataLayer.GetAllSuppliers().Count, 1);
            Assert.IsTrue(emptyDataLayer.DoesSupplierExists(1));
            Assert.IsTrue(emptyDataLayer.GetSupplier(1).Name.Equals("test supplier"));
            Assert.IsTrue(emptyDataLayer.RemoveSupplier(1));
            Assert.AreEqual(emptyDataLayer.GetAllSuppliers().Count, 0);
            Assert.IsFalse(emptyDataLayer.DoesSupplierExists(1));
        }
    }
}