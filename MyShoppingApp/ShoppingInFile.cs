namespace MyShoppingApp
{
    public class ShoppingInFile : ShoppingBase
    {
        string fileName;

        public ShoppingInFile(int year)
            : base(year)
        {
            fileName = year + ".txt";
        }

        public override event ShoppingAddedDelegate ShoppingAddedEvent;

        public override void AddShopping(ShoppingItem item)
        {
            if (item.Sum > 0)
            {
                using (var writer = File.AppendText(fileName))
                {

                    writer.WriteLine($"{item.Shop};{item.Date.ToShortDateString()};{item.Sum:N2}");
                }

                if (ShoppingAddedEvent != null)
                {
                    ShoppingAddedEvent(this, new EventArgs());
                }
            }
            else
            {
                throw new Exception("Invalid sum value");
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
                if (int.Parse(date.Year.ToString()) == this.Year)
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

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            string line;
            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
                {
                    int kollines = 1;
                    line = reader.ReadLine();

                    while (line != null)
                    {
                        string[] elements = line.Split(';');
                        if (elements.Length == 3)
                        {
                            float rezult = float.Parse(elements[2].Trim());
                            statistics.AddShopping(rezult);
                        }
                        else
                        {
                            Console.WriteLine($"Error in line: {line}, skip it");
                        }
                        line = reader.ReadLine();
                        kollines++;
                    }
                }
            }
            return statistics;
        }

        public override void ShowShopping()
        {
            string line;
            Console.WriteLine("");
            if (File.Exists(fileName))
            {
                Console.WriteLine("--------------- LIST OF SHOPPING FROM FILE ---------------");
                using (var reader = File.OpenText(fileName))
                {
                    int kollines = 1;
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        Console.WriteLine(line);
                        line = reader.ReadLine();
                        kollines++;
                    }
                }
                Console.WriteLine("");
            }
        }


        public override void ShowResultStatistics(float min, float max)
        {
            string line;
            Console.WriteLine("--------------- MIN shoppings ---------------");
            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
                {
                    int kollines = 1;
                    line = reader.ReadLine();

                    while (line != null)
                    {
                        string[] elements = line.Split(';');
                        if (elements.Length == 3)
                        {
                            float rezult = float.Parse(elements[2].Trim());

                            if (rezult == min)
                            {
                                Console.WriteLine(line);
                            }

                        }
                        else
                        {
                        }
                        line = reader.ReadLine();
                        kollines++;
                    }
                }
            }
            Console.WriteLine(" --------------- MAX shoppings ---------------");
            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
                {
                    int kollines = 1;
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        string[] elements = line.Split(';');
                        if (elements.Length == 3)
                        {
                            float rezult = float.Parse(elements[2].Trim());

                            if (rezult == max)
                            {
                                Console.WriteLine(line);
                            }
                        }
                        else
                        {
                        }
                        line = reader.ReadLine();
                        kollines++;
                    }
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Press Any key to continue");
            Console.ReadLine();
        }

        public bool FileExist()
        {
            return File.Exists(fileName);
        }
    }
}


