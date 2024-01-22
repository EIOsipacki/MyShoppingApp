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
        public abstract void AddShopping(float sum);

        public abstract Statistics GetStatistics();

        public abstract void ShowResultStatistics(float min, float max);

    }
}
