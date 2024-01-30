using NUnit.Framework.Interfaces;

namespace MyShoppingApp.Tests
{
    public class ShoppingTestsFile
    {
        [Test]
        public void WhenAllInputIncorrectDataOfShoppingsFileDoesntExist()
        {
            // arrange
            ShoppingInFile shoppingFile = new ShoppingInFile(2023);

            var filename = shoppingFile.Year + ".txt";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            shoppingFile.AddShopping("Biedronka", "01.01.2023", "0");
            shoppingFile.AddShopping("Biedronka", "02.02.2023", "-123");
            shoppingFile.AddShopping("Lidl", "03,03,2023", 13);
            shoppingFile.AddShopping("Aldi", "04.04.2028", 124);
            shoppingFile.AddShopping("", "05.05.2023", 14);

            // act
            var statistics = shoppingFile.GetStatistics();
            shoppingFile.ShowShopping();

            //assert
            Assert.AreEqual(statistics.Min, float.MaxValue);
            Assert.AreEqual(statistics.Max, float.MinValue);
            Assert.AreEqual(statistics.Count, 0);
            Assert.AreEqual(shoppingFile.FileExist(), false);

        }

        [Test]
        public void WhenInputCorrectDataOfShoppingsStatisticsShouldBeTrue()
        {
            // arrange
            ShoppingInFile shoppingFile = new ShoppingInFile(2024);
            var filename = shoppingFile.Year + ".txt";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            shoppingFile.AddShopping("Biedronka", "01.01.2024", "123");
            shoppingFile.AddShopping("Lidl", "02.02.2024", 13);

            // act
            var statistics = shoppingFile.GetStatistics();
            shoppingFile.ShowShopping();

            //assert
            Assert.AreEqual(statistics.Min, 13);
            Assert.AreEqual(statistics.Max, 123);
            Assert.AreEqual(statistics.Average, 68);
            Assert.AreEqual(statistics.Sum, 136);
            Assert.AreEqual(statistics.Count, 2);
            Assert.AreEqual(shoppingFile.FileExist(), true);
        }


        [Test]
        public void WhenInputIncorrectSumOfShoppingsStatisticsShouldBeTrue()
        {
            // arrange
            ShoppingInFile shoppingFile = new ShoppingInFile(2025);
            var filename = shoppingFile.Year + ".txt";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            shoppingFile.AddShopping("Biedronka", "01.01.2025", "123");
            shoppingFile.AddShopping("Lidl", "02.02.2025", -13);

            // act
            var statistics = shoppingFile.GetStatistics();
            shoppingFile.ShowShopping();

            //assert
            Assert.AreEqual(statistics.Min, 123);
            Assert.AreEqual(statistics.Max, 123);
            Assert.AreEqual(statistics.Average, 123);
            Assert.AreEqual(statistics.Sum, 123);
            Assert.AreEqual(statistics.Count, 1);
            Assert.AreEqual(shoppingFile.FileExist(), true);

        }
    }
}