namespace MyShoppingApp
{
    internal class Shopping
    {
        public Shopping(string shop, DateTime date, float sum)
        {
            this.Shop = shop;
            this.Date = date;
            this.Sum = sum;
        }

        public string Shop { get; set; }

        public DateTime Date { get; set; }

        public float Sum { get; set; }

    }
}
