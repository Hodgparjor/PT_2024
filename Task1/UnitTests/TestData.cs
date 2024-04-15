using Data;

namespace UnitTests
{
    [TestClass]
    public class TestData
    {
        [TestMethod]
        public void TestEmptyData()
        {
            DataLayerAbstract testedData = DataLayerAbstract.CreateMyDataLayer();

            Assert.AreEqual(testedData.GetAllCatalogItems().Count, 0);
            testedData.AddCatalogItem(new Product(0, "ERC18", (decimal)999.9));
            Assert.AreEqual(testedData.GetAllCatalogItems(), 1);
            Assert.IsTrue(testedData.DoesCatalogItemExist(0));
            Assert.IsTrue(testedData.RemoveCatalogItem(0));
            Assert.AreEqual(testedData.GetAllCatalogItems().Count, 0);
            Assert.IsFalse(testedData.DoesCatalogItemExist(0));


            Assert.AreEqual(testedData.GetAllCustomers().Count, 0);
            testedData.AddCustomer(new Customer(0, "John Doe"));
            Assert.AreEqual(testedData.GetAllCustomers().Count, 1);
            Assert.IsTrue(testedData.DoesCustomerExist(0));
            Assert.IsTrue(testedData.GetCustomer(0).Equals(new Customer(0, "John Doe")));
            Assert.IsTrue(testedData.RemoveCustomer(0));
            Assert.AreEqual(testedData.GetAllCustomers().Count, 0);
            Assert.IsFalse(testedData.DoesCustomerExist(0));



        }
    }
}