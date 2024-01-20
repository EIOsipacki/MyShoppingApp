namespace MyShoppingApp
{
    internal class ShoppingInFile : ShoppingBase
    {
        string fileName = "shop.txt";
        
        
        public ShoppingInFile(int year)
            : base(year)
        {
        }

        public event ShoppingAddedDelegate ShoppingAddedEvent;

        public override void AddShopping(float sum)
        {
            if (ShoppingAddedEvent != null)
            {
                ShoppingAddedEvent(this, new EventArgs());
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

        public override void ShowResultStatistics(float min, float max)
        {
            string line;
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
                
                //pokazanie Min i Max paragonow z pliku 
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
            else
            {
                Console.WriteLine("The File is empty, please input Shopping by Menu ->'0''0'");
                Console.ReadLine();
            }
        }
    }

}
