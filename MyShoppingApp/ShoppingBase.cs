namespace MyShoppingApp
{
    public abstract class ShoppingBase : IShopping
    {
        public int Year { get; set; }

        public delegate void ShoppingAddedDelegate(object sender, EventArgs args);

        public abstract event ShoppingAddedDelegate ShoppingAddedEvent;

        public ShoppingBase(int year)
        {
            this.Year = year;
        }
        public abstract void AddShopping(Shopping item);

        public abstract void AddShopping(string shop, string dateString, float sum);

        public abstract void AddShopping(string shop, string dateString, string sumString); 

        public abstract Statistics GetStatistics();

        public abstract void ShowShopping();

        public abstract void ShowResultStatistics(float min, float max);

    }
}
