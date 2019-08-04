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
        private const string BlueStar = "Blue Star";

        private const int BlueStarQualityLossMultiplier = 2;
        private const int MaxAwardQuality = 50;
        private const int FirstSellInThreshold = 10;
        private const int SecondSellInThreshold = 5;

        public IList<Award> Awards { get; set; } = new List<Award>();

        private static void Main(string[] args)
        {
            try
            {
                Program app = new Program
                {
                    Awards = new List<Award>
                    {
                        new Award {Name = GovQualityPlus, SellIn = 10, Quality = 20},
                        new Award {Name = BlueFirst, SellIn = 2, Quality = 0},
                        new Award {Name = AcmePartnerFacility, SellIn = 5, Quality = 7},
                        new Award {Name = BlueDistinctionPlus, SellIn = 0, Quality = 80},
                        new Award {Name = BlueCompare, SellIn = 15, Quality = 20},
                        new Award {Name = TopConnectedProviders, SellIn = 3, Quality = 6},
                        new Award {Name = BlueStar, SellIn = 0, Quality = 30}
                    }
                };

                System.Console.WriteLine("Updating award metrics...!");
                PrintAwards(app.Awards);

                app.UpdateQuality();

                System.Console.WriteLine("Successfully updated award metrics!");
                PrintAwards(app.Awards);

                System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Failed to update award metrics. {ex.Message}");
                System.Console.ReadKey();
            }
        }

        private static void PrintAwards(IList<Award> awards)
        {
            foreach (Award award in awards)
                System.Console.WriteLine($"Award Name: {award.Name}, Award SellIn: {award.SellIn}, Award Quality: {award.Quality}");

            System.Console.WriteLine();
        }

        public void UpdateQuality()
        {
            foreach (Award award in Awards)
            {
                CalculateInitialAwardQuality(award);
                ReduceAwardSellIn(award);

                if (award.SellIn >= 0) continue;
                CalculatePostAwardQuality(award);
            }
        }

        private static void CalculateInitialAwardQuality(Award award)
        {
            switch (award.Name)
            {
                case BlueFirst:
                    GainAwardQuality(award);
                    break;

                case BlueCompare:
                    GainAwardQuality(award);

                    if (award.SellIn <= FirstSellInThreshold) GainAwardQuality(award);
                    if (award.SellIn <= SecondSellInThreshold) GainAwardQuality(award);
                    break;

                case BlueStar:
                    LoseBlueStarAwardQuality(award);
                    break;

                default:
                    LoseAwardQuality(award);
                    break;
            }
        }

        private static void CalculatePostAwardQuality(Award award)
        {
            switch (award.Name)
            {
                case BlueFirst:
                    GainAwardQuality(award);
                    break;

                case BlueCompare:
                    award.Quality = 0;
                    break;

                case BlueStar:
                    LoseBlueStarAwardQuality(award);
                    break;

                default:
                    LoseAwardQuality(award);
                    break;
            }
        }

        private static void LoseBlueStarAwardQuality(Award award)
        {
            int count = 0;
            while (count < BlueStarQualityLossMultiplier)
            {
                LoseAwardQuality(award);
                count++;
            }
        }

        private static void ReduceAwardSellIn(Award award)
        {
            if (award.Name != BlueDistinctionPlus) award.SellIn -= 1;
        }

        private static void LoseAwardQuality(Award award)
        {
            if (award.Name != BlueDistinctionPlus && award.Quality > 0) award.Quality -= 1;
        }

        private static void GainAwardQuality(Award award)
        {
            if (award.Quality < MaxAwardQuality) award.Quality += 1;
        }
    }
}
