using MyShoppingApp;
using System;

//program wyliczania statystyk zakupow w Podanym Roku (tu tylko jeden rok = 2023)
// Po uruchomienu programu mamy Menu z możliwosciu wyboru:
//0 - wprowadzenia nowych zakupów (zachowują sie w list w pamięci i automatycznie
//    zapisują sie do pliku 'ROK.txt'
//1 - wyliczanie i pokazanie Statystyk z pamieci; plus pokazanie listy Min i Max zakupów
//2 - wyliczanie statystyk z zapisanego pliku '2023.txt', plus pokazanie listy Min i Max zakupow z pliku
//3 - Wyjscie z programu
//  Do pliku 'log.txt' zapisuje sie informacjia o wydarzeniach po uruchomieniu Programu 
//  przez Eventy przy dodaniu paragonow; przez zapisanie w plik resultata wyboru Menu, Uruchomienia, Zamknięcia programu 

int choice;
const string ShoppingLogFile = "log.txt";

ShoppingInMemory shoppingMemory = new ShoppingInMemory(2023);
ShoppingInFile shoppingFile = new ShoppingInFile(2023);

shoppingMemory.ShoppingAddedEvent += ShoppingSumMemoryAdded;

shoppingFile.ShoppingAddedEvent += ShoppingAddedToFile;

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

void DoneStatisticsMemory()
{
    using (var writer = File.AppendText(ShoppingLogFile))
    {
        writer.WriteLine($"{DateTime.Now} :the Statistics from Memory was solved ");
    }
}

void DoneStatisticsFile()
//+ info do pliku log.xtx
{
    using (var writer = File.AppendText(ShoppingLogFile))
    {
        writer.WriteLine($"{DateTime.Now} :the Statistics from File was solves ");
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
    Console.Clear();
    Console.WriteLine("Calculating Shopping statistics from memory:");
    Console.WriteLine("");
    Console.WriteLine("First, please input Shopping (Shop, Date, Sum):");
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
    Console.Clear();
    Console.WriteLine("Calculating Shopping statistics from file:");
    Console.WriteLine("");
    Console.WriteLine("First, please input Shopping (Shop, Date, Sum):");
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
    Console.WriteLine("1. Statistics from memory");
    Console.WriteLine("2. Statistics from file");
    Console.WriteLine("3. EXIT");
    Console.Write("Choose  (1-3): ");

    if (int.TryParse(Console.ReadLine(), out choice))
    {
        switch (choice)
        {
            case 1:
                ProgramMenu1();
                InputFromKeyboard(1);

                if (shoppingMemory.SumesLength() > 0)
                {
                    shoppingMemory.ShowShopping();
                    var statisticsMemory = shoppingMemory.GetStatistics();
                    statisticsMemory.WriteLineStatistics();
                    shoppingMemory.ShowResultStatistics(statisticsMemory.Min, statisticsMemory.Max);
                    DoneStatisticsMemory();
                }
                else
                {
                    Console.WriteLine("List of Shoppiing is Empty, please input Shopping, and try again");
                    Console.ReadLine();
                }
                break;
            case 2:
                ProgramMenu2();
                InputFromKeyboard(2);

                if (shoppingFile.FileExist() == true)
                {
                    shoppingFile.ShowShopping();
                    var statisticsFile = shoppingFile.GetStatistics();
                    statisticsFile.WriteLineStatistics();
                    shoppingFile.ShowResultStatistics(statisticsFile.Min, statisticsFile.Max);
                    DoneStatisticsFile();
                }
                else
                {
                    Console.WriteLine("File of Shoppiing does not exist, please input Shopping, and try again");
                    Console.ReadLine();
                }
                break;
            case 3:
                ProgramMenu3();
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("Selected '3' - GOOD BY");
                break;
            default:
                Console.WriteLine("Incorrect selection. Select a number from 1 to 3");
                break;
        }
    }
    else
    {
        Console.WriteLine("Incorrect selection. Select a number from 0 to 3.");
    }

    Console.WriteLine();

} while (choice != 3);

void InputFromKeyboard(int i)
//Wprowadzenie zakupow z klawiatury
{
    while (true)
    {
        //Shopping shopping = InputShopping();

        //if (shopping == null)
        Console.Write("   input Shop or 'q/Q'- to EXIT and show statistics:");
        string shop = Console.ReadLine();
        if (shop == "q" || shop == "Q")
        {
            break;
        }
        else
        {
            Console.Write("   input Date (dd.MM.yyyy): ");
            string dateString = Console.ReadLine();
            Console.Write("   input Sum: ");
            string sumString = Console.ReadLine();
            if (i == 1)
            {
                shoppingMemory.AddShopping(shop, dateString, sumString);
            }
            else
                if (i == 2)
            {
                shoppingFile.AddShopping(shop, dateString, sumString);
            }

        }

    }
}










