using System;
using System.Collections.Generic;

namespace ProviderQuality.Console
{
    public class Program
    {
        private const string GovQualityPlus = "Gov Quality Plus";
        private const string BlueFirst = "Blue First";
        private const string AcmePartnerFacility = "ACME Partner Facility";
        private const string BlueDistinctionPlus = "Blue Distinction Plus";
        private const string BlueCompare = "Blue Compare";
        private const string TopConnectedProviders = "Top Connected Providers";

        public static IList<Award> Awards { get; set; } = new List<Award>
        {
            new Award { Name = GovQualityPlus, SellIn = 10, Quality = 20 },
            new Award { Name = BlueFirst, SellIn = 2, Quality = 0 },
            new Award { Name = AcmePartnerFacility, SellIn = 5, Quality = 7 },
            new Award { Name = BlueDistinctionPlus, SellIn = 0, Quality = 80 },
            new Award { Name = BlueCompare, SellIn = 15, Quality = 20 },
            new Award { Name = TopConnectedProviders, SellIn = 3, Quality = 6 }
        };

        static void Main(string[] args)
        {
            try
            {
                System.Console.WriteLine("Updating award metrics...!");
                PrintAwards();

                Program app = new Program();
                app.UpdateQuality();

                System.Console.WriteLine("Successfully updated award metrics!");
                PrintAwards();

                System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Failed to update award metrics. {ex.Message}");
                System.Console.ReadKey();
            }
        }

        private static void PrintAwards()
        {
            foreach (Award award in Awards)
                System.Console.WriteLine($"Award Name: {award.Name}, Award Sell In: {award.SellIn}, Award Quality: {award.Quality}");
        }

        public void UpdateQuality()
        {
            foreach (Award award in Awards)
            {
                CalculateInitialAwardQuality(award);
                DecrementAwardSellIn(award);
                if (award.SellIn >= 0) continue;
                CalculatePostAwardQuality(award);
            }
        }

        private static void CalculateInitialAwardQuality(Award award)
        {
            switch (award.Name)
            {
                case BlueFirst:
                    IncrementAwardQuality(award);

                    break;

                case BlueCompare:
                    IncrementAwardQuality(award);

                    if (award.SellIn < 11) IncrementAwardQuality(award);
                    if (award.SellIn < 6) IncrementAwardQuality(award);

                    break;

                default:
                    DecrementAwardQuality(award);
                    break;
            }
        }

        private static void CalculatePostAwardQuality(Award award)
        {
            switch (award.Name)
            {
                case BlueFirst:
                    IncrementAwardQuality(award);
                    break;

                case BlueCompare:
                    award.Quality = 0;
                    break;

                default:
                    DecrementAwardQuality(award);
                    break;
            }
        }

        private static void DecrementAwardSellIn(Award award)
        {
            if (award.Name != BlueDistinctionPlus) award.SellIn -= 1;
        }

        private static void DecrementAwardQuality(Award award)
        {
            if (award.Name != BlueDistinctionPlus && award.Quality > 0) award.Quality -= 1;
        }

        private static void IncrementAwardQuality(Award award)
        {
            if (award.Quality < 50) award.Quality += 1;
        }
    }
}
