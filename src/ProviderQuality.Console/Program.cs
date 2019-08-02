using System;
using System.Collections.Generic;

namespace ProviderQuality.Console
{
    public class Program
    {
        private enum AwardName
        {
            GovQualityPlus,
            BlueFirst,
            AcmePartnerFacility,
            BlueDistinctionPlus,
            BlueCompare,
            TopConnectedProviders
        }

        readonly static Dictionary<AwardName, string> AwardNames = new Dictionary<AwardName, string>
        {
            { AwardName.GovQualityPlus, "Gov Quality Plus" },
            { AwardName.BlueFirst, "Blue First"},
            { AwardName.AcmePartnerFacility, "ACME Partner Facility" },
            { AwardName.BlueDistinctionPlus, "Blue Distrinction Plus" },
            { AwardName.BlueCompare, "Blue Compare" },
            { AwardName.TopConnectedProviders, "Top Connected Providers" }
        };

        public IList<Award> Awards { get; set; } = new List<Award>
        {
            new Award { Name = AwardNames[AwardName.GovQualityPlus], SellIn = 10, Quality = 20 },
            new Award { Name = AwardNames[AwardName.BlueFirst], SellIn = 2, Quality = 0 },
            new Award { Name = AwardNames[AwardName.AcmePartnerFacility], SellIn = 5, Quality = 7 },
            new Award { Name = AwardNames[AwardName.BlueDistinctionPlus], SellIn = 0, Quality = 80 },
            new Award { Name = AwardNames[AwardName.BlueCompare], SellIn = 15, Quality = 20 },
            new Award { Name = AwardNames[AwardName.TopConnectedProviders], SellIn = 3, Quality = 6 }
        };

        static void Main(string[] args)
        {
            try
            {
                System.Console.WriteLine("Updating award metrics...!");

                Program app = new Program();
                app.UpdateQuality();

                System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Failed to update award metrics. {ex.Message}");
                System.Console.ReadKey();
            }
        }

        public void UpdateQuality()
        {
            foreach (Award award in Awards)
            {
                if (award.Name != AwardNames[AwardName.BlueFirst] && award.Name != AwardNames[AwardName.BlueCompare])
                {
                    if (award.Quality > 0)
                    {
                        if (award.Name != AwardNames[AwardName.BlueDistinctionPlus])
                        {
                            award.Quality = award.Quality - 1;
                        }
                    }
                }
                else
                {
                    if (award.Quality < 50)
                    {
                        award.Quality = award.Quality + 1;

                        if (award.Name == AwardNames[AwardName.BlueCompare])
                        {
                            if (award.SellIn < 11)
                            {
                                if (award.Quality < 50)
                                {
                                    award.Quality = award.Quality + 1;
                                }
                            }

                            if (award.SellIn < 6)
                            {
                                if (award.Quality < 50)
                                {
                                    award.Quality = award.Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (award.Name != AwardNames[AwardName.BlueDistinctionPlus])
                {
                    award.SellIn = award.SellIn - 1;
                }

                if (award.SellIn < 0)
                {
                    if (award.Name != AwardNames[AwardName.BlueFirst])
                    {
                        if (award.Name != AwardNames[AwardName.BlueCompare])
                        {
                            if (award.Quality > 0)
                            {
                                if (award.Name != AwardNames[AwardName.BlueDistinctionPlus])
                                {
                                    award.Quality = award.Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            award.Quality = award.Quality - award.Quality;
                        }
                    }
                    else
                    {
                        if (award.Quality < 50)
                        {
                            award.Quality = award.Quality + 1;
                        }
                    }
                }
            }
        }

    }

}
