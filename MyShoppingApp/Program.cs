using MyShoppingApp;


Console.WriteLine("Hello");


//program wyliczani statystyk zakupow w Podanym roku (tu = 2023)
// Po uruchomienu programu mamy Menu z możliwosciu wyboru:
//0 - wprowadzenia nowych zakupów (zachowują sie w list w pamięci i automatycznie
//    zapisują sie do pliku 'shop.txt'
//1 - wyliczanie i pokazanie Statystyk z pamieci; plus pokazanie listy Min i Max zakupów
//2 - wyliczanie statystyk z zapisanego pliku 'shop.txt', plus pokazanie listy Min i Max zakupow z pliku
//3 - Wyjscie z programu
//  Do pliku log.txt zapisuje sie informacjia o wydarzeniach po uruchomieniu Programu 
//  przez Eventy przy dodaniu paragonow; przez zaps w plik punkta   Menu , Uruchomieniu, Zamknięciu programu 

int choice;
const string ShoppingFileName = "2023.txt";
const string ShoppingLogFile = "log.txt";

ShoppingInMemory shoppingMemory = new ShoppingInMemory(2023);
ShoppingInFile shoppingFile = new ShoppingInFile(2024);

shoppingMemory.ShoppingAddedEvent += ShoppingSumMemoryAdded;
shoppingMemory.ShoppingAddedEvent += ShoppingAddedToFile;

shoppingFile.ShoppingAddedEvent += ShoppingReadFromFile;


void ShoppingSumMemoryAdded(object sender, EventArgs args)
{
    using (var writer = File.AppendText(ShoppingLogFile))
    {
        writer.WriteLine($"{DateTime.Now} : added new shopping");
    }
}
void ProgramBegin()
//+ info do pliku log.xtx
{
    using (var writer = File.AppendText(ShoppingLogFile))
    {
        writer.WriteLine($"{DateTime.Now} :User '{Environment.UserName}' started program on computer '{Environment.MachineName}',  on system -  '{Environment.OSVersion} '");
    }
}

void ShoppingAddedToFile(object sender, EventArgs args)
{
    using (var writer = File.AppendText(ShoppingLogFile))
    {
        writer.WriteLine($"{DateTime.Now} : the new shopping saved to file ");
    }
}

void ShoppingReadFromFile(object sender, EventArgs args)
{
    using (var writer = File.AppendText(ShoppingLogFile))
    {
        writer.WriteLine($"{DateTime.Now} : the new shopping readed from file ");
    }
}

void ProgramMenu0()
//+ info do pliku log.xtx
{
    using (var writer = File.AppendText(ShoppingLogFile))
    {
        writer.WriteLine($"{DateTime.Now} : selected '0' - Input Shopping to program ");
    }
}

void ProgramMenu1()
//+ info do pliku log.xtx
//Menu - 1 - Wyliczanie statystyk z pamięci 
//Pokazanie Min i Max zakupow
{
    using (var writer = File.AppendText(ShoppingLogFile))
    {
        writer.WriteLine($"{DateTime.Now} : selected '1' - Statistics from memory ");
    }
}

void ProgramMenu2()
//+ info do pliku log.xtx
//Menu - 2 - Wyliczanie statystyk z pliku 
//Pokazanie Min i Max zakupow po odczytu z pliku
{
    using (var writer = File.AppendText(ShoppingLogFile))
    {
        writer.WriteLine($"{DateTime.Now} : selected '2' - Statistics from file ");
    }
}

void ProgramMenu3()
//+ info do pliku log.xtx
//EXIT from program
{
    using (var writer = File.AppendText(ShoppingLogFile))
    {
        writer.WriteLine($"{DateTime.Now} : Close application  ");
    }
}

//ZACZYNA SIE WYKONANIE
ProgramBegin();

do
{
    Console.Clear();
    //MENU
    Console.WriteLine("Program Menu for calculating Shopping statistics  :");
    Console.WriteLine("0. Input shopping receipts");
    Console.WriteLine("1. Statistics from memory");
    Console.WriteLine("2. Statistics from file");
    Console.WriteLine("3. EXIT");
    Console.Write("Choose  (1-3): ");

    if (int.TryParse(Console.ReadLine(), out choice))
    {
        switch (choice)
        {
            case 0:
                ProgramMenu0();
                Console.Clear();
                InputFromKeyboard();
                break;
            case 1:
                ProgramMenu1();
                Console.Clear();
                var statisticsMemory = shoppingMemory.GetStatistics();
                shoppingMemory.ShowResultStatistics(statisticsMemory.Min, statisticsMemory.Max);
                break;
            case 2:
                ProgramMenu2();
                Console.Clear();
                var statisticsFile = shoppingFile.GetStatistics();
                shoppingFile.ShowResultStatistics(statisticsFile.Min, statisticsFile.Max);
                break;
            case 3:
                ProgramMenu3();
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("Selected '3' - GOOD BY");
                break;
            default:
                Console.WriteLine("Incorrect selection. Select a number from 0 to 3.");
                break;
        }
    }
    else
    {
        Console.WriteLine("Incorrect selection. Select a number from 0 to 3.");
    }

    Console.WriteLine();

} while (choice != 3);

void InputFromKeyboard()
//Wprowadzenie zakupow z klawiatury
{
    while (true)
    {
        Shopping shopping = InputShopping();

        if (shopping == null)
        {
            break;
        }
        else
        {
            SaveToFile(shopping);
            shoppingMemory.AddShopping(shopping.Sum);
            shoppingMemory.AddShopping(shopping);
        }
        var statisticsFromMemory = shoppingMemory.GetStatistics();
    }
}

static Shopping InputShopping()
//Wprowadzenie jednego zakupa
{
    Console.WriteLine("Input Shopping data (Shop, Date, Sum) :");
    Console.Write("Shop name :  ('q/Q'- to EXIT)");
    string shop = Console.ReadLine();
    if (shop.ToUpper() == "Q")
        return null;
    DateTime date;
    do
    {
        Console.Write("Date Shopping (dd.MM.yyyy): ");
        string stringData = Console.ReadLine();

        if (DateTime.TryParseExact(stringData, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out date))
        {
            if (int.Parse(date.Year.ToString()) == 2023)
            {
                break;
            }
            else
            {
                Console.WriteLine("Incorrect Year. Try again.");
            }
        }
        else
        {
            Console.WriteLine("Incorrect date format. Try again.");
        }

    } while (true);

    Console.Write("shopping Sum: ");
    float sum;

    while (!float.TryParse(Console.ReadLine(), out sum) || sum <= 0)
    {
        Console.WriteLine("Incorrect Sum of Shopping. Try again.");
    }

    return new Shopping(shop, date, sum);
}

static void SaveToFile(Shopping item)
//Dodanie zakupów do txt pliku
{

    using (var writer = File.AppendText(ShoppingFileName))
    {

        writer.WriteLine($"{item.Shop};{item.Date.ToShortDateString()};{item.Sum:N2}");
    }
}




