using static MyShoppingApp.ShoppingBase;

namespace MyShoppingApp
{
    internal interface IShopping
    {

        int Year { get; set; }
        void AddShopping(Shopping item);

        void AddShopping(string shop, string dateString, float sum);

        void AddShopping(string shop, string dateString, string sumString);

        Statistics GetStatistics();

        void ShowShopping();

        void ShowResultStatistics(float min, float max);

        event ShoppingAddedDelegate ShoppingAddedEvent;

    }
}
