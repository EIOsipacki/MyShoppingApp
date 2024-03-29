﻿namespace MyShoppingApp
{
    public class Statistics
    {
        public Statistics()
        {
            this.Count = 0;
            this.Sum = 0;
            this.Max = float.MinValue;
            this.Min = float.MaxValue;
        }
        public float Min { get; private set; }

        public float Max { get; private set; }

        public float Average
        {
            get
            {
                return this.Sum / this.Count;
            }
        }
        public float Sum { get; private set; }

        public int Count { get; private set; }

        public void AddShopping(float suma)
        {
            this.Count++;
            this.Sum += suma;
            this.Min = Math.Min(suma, this.Min);
            this.Max = Math.Max(suma, this.Max);
        }
        public void WriteLineStatistics()
        {
            Console.WriteLine("--------------- Statistics of Shopping ---------------");
            Console.WriteLine($"SUM: {Sum:N2}");
            Console.WriteLine($"Average: {Average:N2}");
            Console.WriteLine($"Min: {Min:N2}");
            Console.WriteLine($"Max: {Max:N2}");
            Console.WriteLine();
        }
    }
}
