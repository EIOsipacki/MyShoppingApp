using NUnit.Framework.Interfaces;

namespace MyShoppingApp.Tests
{
    public class ShoppingTests
    {
        
        [Test]
        public void WhenInputCorrectDataOfShoppingsStatisticsShouldBeTrue()
        {
            // arrange
            ShoppingInMemory shoppingMemory = new ShoppingInMemory(2023);
            shoppingMemory.AddShopping("Biedronka", "01.01.2023", "123");
            shoppingMemory.AddShopping("Lidl", "02.02.2023", 13);

            // act
            var statistics = shoppingMemory.GetStatistics();

            //assert
            Assert.AreEqual(statistics.Min, 13);
            Assert.AreEqual(statistics.Max, 123);
            Assert.AreEqual(statistics.Average, 68);
            Assert.AreEqual(statistics.Sum, 136);

        }


        [Test]
        public void WhenInputInCorrectSumOfShoppingsStatisticsShouldBeTrue()
        {
            // arrange
            ShoppingInMemory shoppingMemory = new ShoppingInMemory(2023);
            shoppingMemory.AddShopping("Biedronka", "01.01.2023", "123");
            shoppingMemory.AddShopping("Lidl", "02.02.2023", -13);

            // act
            var statistics2 = shoppingMemory.GetStatistics();

            //assert
            Assert.AreEqual(statistics2.Min, 123);
            Assert.AreEqual(statistics2.Max, 123);
            Assert.AreEqual(statistics2.Average, 123);
            Assert.AreEqual(statistics2.Sum, 123);

        }

        [Test]
        public void WhenInputInCorrectDateOfShoppingsStatisticsShouldBeTrue()
        {
            // arrange
            ShoppingInMemory shoppingMemory = new ShoppingInMemory(2023);
            shoppingMemory.AddShopping("Biedronka", "01.01.2023", "0");//sum=0
            shoppingMemory.AddShopping("Biedronka", "01.01.2023", "123");
            shoppingMemory.AddShopping("Lidl", "02,02,2023", 13);//nie poprawny format Daty
            shoppingMemory.AddShopping("Aldi", "02.02.2028", 123);//rok <> 2023
            shoppingMemory.AddShopping("", "02.02.2023", 13); // shop = null

            // act
            var statistics3 = shoppingMemory.GetStatistics();

            //assert
            Assert.AreEqual(statistics3.Min, 123);
            Assert.AreEqual(statistics3.Max, 123);
            Assert.AreEqual(statistics3.Average, 123);
            Assert.AreEqual(statistics3.Sum, 123);

        }
    }
}