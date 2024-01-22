namespace MyShoppingApp
{
    internal class ShoppingInMemory : ShoppingBase
    {
        private List<Shopping> listShopping = new List<Shopping>();
        private List<float> sumes = new List<float>();
        public ShoppingInMemory(int year)
            : base(year)
        {

        }

        public override event ShoppingAddedDelegate ShoppingAddedEvent;

        public override void AddShopping(float sum)
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

        public void AddShopping(Shopping shopping)
        {
            if (shopping.Sum > 0)
            {
                this.listShopping.Add(shopping);

                if (ShoppingAddedEvent != null)
                {
                    ShoppingAddedEvent(this, new EventArgs());
                }
            }
            else
            {
                throw new Exception("Invalid grade value");
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

        public override void ShowResultStatistics(float min, float max)
        {
            if (listShopping.Count == 0)
            {
                Console.WriteLine(" List of Shoppiing is Empty, please input Shopping by Menu ->'0' ");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine(" --------------- LIST OF SHOPPING FROM MEMORY ---------------");
                foreach (var item in listShopping)
                {

                    Console.WriteLine($"{item.Shop} ; {item.Date.ToShortDateString()} ; {item.Sum:N2}");
                }

                Console.WriteLine("");

                ////pokazanie Min i Max zakupow 
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
        }
    }
}
