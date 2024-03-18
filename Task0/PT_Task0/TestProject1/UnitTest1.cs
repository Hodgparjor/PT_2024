namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var calc = new ClassLibrary1.Calculator();
            Assert.AreEqual(3, calc.add(1, 2));
        }
    }
}