﻿using MyShoppingApp;
using System;

int choice;
const string shoppingLogFile = "log.txt";

ShoppingInMemory shoppingMemory = new ShoppingInMemory(2023);
ShoppingInFile shoppingFile = new ShoppingInFile(2023);

shoppingMemory.ShoppingAddedEvent += ShoppingSumMemoryAdded;

shoppingFile.ShoppingAddedEvent += ShoppingAddedToFile;

void ShoppingSumMemoryAdded(object sender, EventArgs args)
{
    using (var writer = File.AppendText(shoppingLogFile))
    {
        writer.WriteLine($"{DateTime.Now}: added new shopping");
    }
}

void WriteMessageToLogFile(string message)
{
    using (var writer = File.AppendText(shoppingLogFile))
    {
        writer.WriteLine($"{DateTime.Now}: " + message);
    }
}

void ShoppingAddedToFile(object sender, EventArgs args)
{
    using (var writer = File.AppendText(shoppingLogFile))
    {
        writer.WriteLine($"{DateTime.Now}: the new shopping saved to file ");
    }
}


void WriteInConsoleChoiceText(string text)
{
    Console.Clear();
    Console.WriteLine($"Calculating Shopping statistics from {text}:");
    Console.WriteLine("");
    Console.WriteLine("First, please input Shopping (Shop, Date, Sum):");
}

WriteMessageToLogFile($" User '{Environment.UserName}' started program on computer '{Environment.MachineName}', on system - '{Environment.OSVersion} '");

do
{
    Console.Clear();
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
                ItemOfMenu(1, shoppingMemory);
                break;
            case 2:
                ItemOfMenu(2, shoppingFile);
                break;
            case 3:
                WriteMessageToLogFile("Close application  ");
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

void InputFromKeyboard(IShopping item)
{
    while (true)
    {
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
            item.AddShopping(shop, dateString, sumString);
        }
    }
}

void ItemOfMenu(int choice, IShopping item)
{
    string stringChoice = "";
    if (choice == 1)
    {
        stringChoice = "memory";
    }
    else
    {
        stringChoice = "file";
    }

    WriteMessageToLogFile($"selected '{choice}' - Statistics from {stringChoice}");
    WriteInConsoleChoiceText(stringChoice);
    InputFromKeyboard(item);

    if (shoppingFile.ListOfShoppingNotNull() == true)
    {
        item.ShowShopping();
        var statisticsRezult = item.GetStatistics();
        statisticsRezult.WriteLineStatistics();
        item.ShowResultStatistics(statisticsRezult.Min, statisticsRezult.Max);
        WriteMessageToLogFile($"the Statistics from {stringChoice} was solves");
    }
    else
    {
        Console.WriteLine($"List of Shoppiing from {stringChoice} Empty, please input Shopping, and try again");
        Console.ReadLine();
    }
}










