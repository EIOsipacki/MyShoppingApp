namespace MyShoppingApp
{
    public class ShoppingInMemory : ShoppingBase
    {
        private List<ShoppingItem> listShopping = new List<ShoppingItem>();
        private List<float> sumes = new List<float>();
        
        public ShoppingInMemory(int year)
            : base(year)
        {
        }

        public override event ShoppingAddedDelegate ShoppingAddedEvent;

        public void AddShopping(float sum)
        {
            if (sum > 0)
            {
                this.sumes.Add(sum);
            }
            else
            {
                throw new Exception("Invalid grade value");
            }
        }

        public override void AddShopping(string shop, string dateString, float sum)
        {
            string sumString = "";
            sumString += sum;
            this.AddShopping(shop, dateString, sumString);
        }

        public override void AddShopping(string shop, string dateString, string sumString)
        {
            bool shopBool = false;
            bool dateBool = false;
            bool sumBool = false;
            bool exitBool = false;

            if (shop.Length > 0)
            {
                shopBool = true;
            }
            else
            {
                shopBool = false;
            }

            if (shop.ToUpper() == "Q")
            {
                exitBool = false;
            }
            else
            {
                exitBool = true;
            }

            DateTime date;
            if (DateTime.TryParseExact(dateString, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out date) == true)
            {
                if (int.Parse(date.Year.ToString()) == 2023)
                {
                    dateBool = true;
                }
                else
                {
                    dateBool = false;
                }
            }
            else
            {
                dateBool = false;
            }

            float sum;
            if (!float.TryParse(sumString, out sum) || sum <= 0)
            {
                sumBool = false;
            }
            else
            {
                sumBool = true;
            }
                        
            if ((shopBool == true) && (dateBool == true) && (sumBool == true) && (exitBool == true))
            {
                ShoppingItem shopping = new ShoppingItem(shop, date, sum);
                this.AddShopping(shopping);
            }
            else
            {
                Console.WriteLine("Incorrect Shopping data. Try again.");
            }
        }

        public override void AddShopping(ShoppingItem shopping)
        {
            if (shopping.Sum > 0)
            {
                this.listShopping.Add(shopping);
                this.AddShopping(shopping.Sum);

                if (ShoppingAddedEvent != null)
                {
                    ShoppingAddedEvent(this, new EventArgs());
                }
            }
            else
            {
                Console.WriteLine("Invalid Shopping data");
            }
        }

        public override Statistics GetStatistics()
        {
            Statistics statistics = new Statistics();
            foreach (var sum in this.sumes)
            {
                statistics.AddShopping(sum);
            }
            return statistics;
        }

        public override void ShowShopping()
        {
            Console.WriteLine("");
            Console.WriteLine(" --------------- LIST OF SHOPPING FROM MEMORY ---------------");
            foreach (var item in listShopping)
            {

                Console.WriteLine($"{item.Shop} ; {item.Date.ToShortDateString()} ; {item.Sum:N2}");
            }
            Console.WriteLine("");
        }

        public override void ShowResultStatistics(float min, float max)
        {
            Console.WriteLine("--------------- MIN shoppings ---------------");
            foreach (var item in listShopping)
            {
                if (item.Sum == min)
                {
                    Console.WriteLine($"{item.Shop} ; {item.Date.ToShortDateString()} ; {item.Sum:N2}");
                }
            }
            Console.WriteLine(" --------------- MAX shoppings ---------------");
            foreach (var item in listShopping)
            {
                if (item.Sum == max)
                {
                    Console.WriteLine($"{item.Shop} : {item.Date.ToShortDateString()} ; {item.Sum:N2}");
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Press Any key to continue");
            Console.ReadLine();
        }

        public bool ListOfShoppingNotNull()
        {
            return (sumes.Count > 0);
        }

    }
}
