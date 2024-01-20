using static MyShoppingApp.ShoppingBase;

namespace MyShoppingApp
{
    internal interface IShopping
    {

        int Year { get; set; }
        void AddShopping(float sum);
        Statistics GetStatistics();

        void ShowResultStatistics(float min, float max);

        event ShoppingAddedDelegate ShoppingAddedEvent;

    }
}
