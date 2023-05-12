using Bell.Reconciliation.Web.Server.Data;
using Mapster;
using System.Diagnostics.Eventing.Reader;

namespace Bell.Reconciliation.Web.Server.Services
{
    public class BellRecRepository
    {
        public BellRecRepository()
        {
        }

        string[] Brands = { "Bell", "Virgin", "Lucky Mobile" };
        string[] Lobs = { "Wireless", "Wireline" };
        string[] WirelessSubLOBs = { "Wireless", "IoT", "Mobile Data"};
        string[] WiredSubLOBs = { "Home Internet", "Home Internet", "Home Television", "Home Phone", "SMB Internet" };
        string[] Locations = { "Toronto", "Ottawa", "London", "Kingston", "Windsor", "Hamilton", "Barrie", "Kitchener", "Mississagua" };
        string[] RebateTypes = { "Commission", "Down Payment", "Tax Rebate" };
        string[] Products = { "iPhone 13 128GB Red", "iPhone 14 128GB Black", "iPhone 13 256GB Gold","2-Year NAC WRLS 35+",
"2-Year NAC WRLS 55+","iPhone 13 128GB Gold","iPhone 14 256GB Gold","iPhone 14 256GB Black"};
        List<string> salesPersons = new();
        Random rand = new Random();
        BellRecContext db = new BellRecContext();
        public string DBGenerator(int RecordNom, string DeleteOldRecords, int DifferenceRate)
        {
            try
            {

                for (int i = 0; i < 10; i++)
                    salesPersons.Add(getSampleName(db, rand));

                if (DeleteOldRecords == "y" || DeleteOldRecords == "Y")
                {
                    db.BellSources.RemoveRange(db.BellSources);
                    db.StaplesSources.RemoveRange(db.StaplesSources);
                    db.SaveChanges();
                }

                for (int i = 1; i < RecordNom; i += 3)
                {
                    var randNom = rand.Next(0, 100);
                    if (randNom <= 3 * DifferenceRate) // has difference
                    {
                        if (randNom <= DifferenceRate) //only bell
                        {
                            var bellSource = getBellSource();
                            bellSource.Lob = Lobs[0];
                            bellSource.Id = i;
                            if (rand.Next(0, 100) <= 50) //wireless LOB
                            {
                                bellSource.RebateType = RebateTypes[0];
                                if (rand.Next(0, 100) <= 50) //wireless sublob - mobile with line
                                {
                                    bellSource.SubLob = WirelessSubLOBs[0];
                                    var bellSource2 = bellSource.Adapt<Data.BellSource>();
                                    var bellSource3 = bellSource.Adapt<Data.BellSource>();

                                    bellSource.RebateType = RebateTypes[0];
                                    bellSource2.RebateType = RebateTypes[1];
                                    bellSource3.RebateType = RebateTypes[2];

                                    bellSource.Phone = rand.NextInt64(11234567890, 99999999999);
                                    bellSource2.Phone = bellSource.Phone;
                                    bellSource3.Phone = bellSource.Phone;

                                    bellSource.Imei = rand.NextInt64(1234567890, 9876543210);
                                    bellSource2.Imei = bellSource.Imei;
                                    bellSource3.Imei = bellSource.Imei;

                                    bellSource.Amount = rand.Next(100, 1500);
                                    bellSource2.Amount = rand.Next(100, 1500);
                                    bellSource3.Amount = rand.Next(100, 1500);

                                    bellSource.Id = i;
                                    bellSource2.Id = i + 1;
                                    bellSource3.Id = i + 2;

                                    db.BellSources.Add(bellSource);
                                    db.BellSources.Add(bellSource2);
                                    db.BellSources.Add(bellSource3);

                                    i += 2;
                                }
                                else//lob:wireliss - sublub:non-wireless
                                {
                                    bellSource.Id = i;

                                    bellSource.RebateType = RebateTypes[0];
                                    var r = rand.Next(1, 2);
                                    bellSource.SubLob = WirelessSubLOBs[r];
                                    db.BellSources.Add(bellSource);
                                }
                            }
                            else//Wired lob
                            {
                                bellSource.RebateType = RebateTypes[0];
                                bellSource.Id = i;
                                bellSource.Lob = Lobs[1];
                                bellSource.SubLob = WiredSubLOBs[rand.Next(WiredSubLOBs.Length - 1)];
                                db.BellSources.Add(bellSource);
                            }
                        }
                        else if (randNom <= 2 * DifferenceRate) // only in staple
                        {
                            var stapleSource = getStapleSource();
                            stapleSource.Lob = Lobs[0];
                            stapleSource.Id = i;
                            if (rand.Next(0, 100) <= 50) //wireless LOB
                            {
                                stapleSource.RebateType = RebateTypes[0];
                                if (rand.Next(0, 100) <= 50) //wireless sublob - mobile with line
                                {
                                    stapleSource.SubLob = WirelessSubLOBs[0];
                                    var stapleSource2 = stapleSource.Adapt<Data.StaplesSource>();
                                    var stapleSource3 = stapleSource.Adapt<Data.StaplesSource>();

                                    stapleSource.RebateType = RebateTypes[0];
                                    stapleSource2.RebateType = RebateTypes[1];
                                    stapleSource3.RebateType = RebateTypes[2];

                                    stapleSource.Phone = rand.NextInt64(11234567890, 99999999999);
                                    stapleSource2.Phone = stapleSource.Phone;
                                    stapleSource3.Phone = stapleSource.Phone;

                                    stapleSource.Imei = rand.NextInt64(1234567890, 9876543210);
                                    stapleSource2.Imei = stapleSource.Imei;
                                    stapleSource3.Imei = stapleSource.Imei;

                                    stapleSource.Amount = rand.Next(100, 1500);
                                    stapleSource2.Amount = rand.Next(100, 1500);
                                    stapleSource3.Amount = rand.Next(100, 1500);

                                    stapleSource.Id = i;
                                    stapleSource2.Id = i + 1;
                                    stapleSource3.Id = i + 2;

                                    db.StaplesSources.Add(stapleSource);
                                    db.StaplesSources.Add(stapleSource2);
                                    db.StaplesSources.Add(stapleSource3);

                                    i += 2;
                                }
                                else //lob:wireliss - sublub:non-wireless
                                {
                                    stapleSource.Id = i;

                                    var r = rand.Next(1, 2);
                                    stapleSource.SubLob = WirelessSubLOBs[r];
                                    stapleSource.RebateType = RebateTypes[0];
                                    db.StaplesSources.Add(stapleSource);
                                }
                            }
                            else//Wired lob
                            {
                                stapleSource.Id = i;
                                stapleSource.Lob = Lobs[1];
                                stapleSource.RebateType = RebateTypes[0];
                                stapleSource.SubLob = WiredSubLOBs[rand.Next(WiredSubLOBs.Length - 1)];
                                db.StaplesSources.Add(stapleSource);
                            }

                        }
                        else //if(randNom <= 3*DifferenceRate) //both with difference
                        {
                            var stapleSource = getStapleSource();
                            var bellsource = getBellSource();

                            stapleSource.Lob = Lobs[0];
                            bellsource.Lob = Lobs[0];
                            stapleSource.Id = i;
                            bellsource.Id = i;
                            if (rand.Next(0, 100) <= 50) //wireless LOB
                            {
                                stapleSource.RebateType = RebateTypes[0];
                                bellsource.RebateType = RebateTypes[0];
                                if(false)// (rand.Next(0, 100) <= 50) //wireless sublob - mobile with line
                                {
                                    //stapleSource.SubLob = WirelessSubLOBs[0];
                                    //var stapleSource2 = stapleSource.Adapt<Data.StaplesSource>();
                                    //var stapleSource3 = stapleSource.Adapt<Data.StaplesSource>();

                                    //stapleSource2.RebateType = RebateTypes[0];
                                    //stapleSource2.RebateType = RebateTypes[1];
                                    //stapleSource3.RebateType = RebateTypes[2];

                                    //stapleSource2.Phone = rand.NextInt64(11234567890, 99999999999);
                                    //stapleSource2.Phone = stapleSource.Phone;
                                    //stapleSource3.Phone = stapleSource.Phone;

                                    //stapleSource2.Imei = rand.NextInt64(1234567890, 9876543210);
                                    //stapleSource2.Imei = stapleSource.Imei;
                                    //stapleSource3.Imei = stapleSource.Imei;

                                    //stapleSource.Id = i;
                                    //stapleSource2.Id = i + 1;
                                    //stapleSource3.Id = i + 2;

                                    //db.StaplesSources.Add(stapleSource2);
                                    //db.StaplesSources.Add(stapleSource2);
                                    //db.StaplesSources.Add(stapleSource3);

                                    //i += 2;
                                }
                                else //lob:wireliss - sublub:non-wireless
                                {
                                    var r = rand.Next(1, 2);

                                    stapleSource.Id = i;
                                    stapleSource.SubLob = WirelessSubLOBs[r];
                                    
                                    bellsource.Id = i;
                                    bellsource.SubLob = WirelessSubLOBs[r];

                                    db.StaplesSources.Add(stapleSource);
                                    db.BellSources.Add(bellsource);
                                }
                            }
                            else//Wired lob
                            {
                                stapleSource.Id = i;
                                stapleSource.Lob = Lobs[1];
                                stapleSource.SubLob = WiredSubLOBs[rand.Next(WiredSubLOBs.Length - 1)];
                                stapleSource.RebateType = RebateTypes[0];


                                bellsource.Id = i;
                                bellsource.Lob = Lobs[1];
                                bellsource.SubLob = stapleSource.SubLob;
                                bellsource.RebateType = stapleSource.RebateType;

                                db.StaplesSources.Add(stapleSource);
                                db.BellSources.Add(bellsource);
                            }
                        }

                    }
                    else //nodifference
                    {
                        var stapleSource = getStapleSource();
                        var bellsource = getBellSource();

                        stapleSource.Lob = Lobs[0];
                        bellsource.Lob = Lobs[0];
                        stapleSource.Id = i;
                        bellsource.Id = i;
                        if (rand.Next(0, 100) <= 50) //wireless LOB
                        {
                            stapleSource.RebateType = RebateTypes[0];
                            bellsource.RebateType = RebateTypes[0];
                            if (false)// (rand.Next(0, 100) <= 50) //wireless sublob - mobile with line
                            {
                                //stapleSource.SubLob = WirelessSubLOBs[0];
                                //var stapleSource2 = stapleSource.Adapt<Data.StaplesSource>();
                                //var stapleSource3 = stapleSource.Adapt<Data.StaplesSource>();

                                //stapleSource2.RebateType = RebateTypes[0];
                                //stapleSource2.RebateType = RebateTypes[1];
                                //stapleSource3.RebateType = RebateTypes[2];

                                //stapleSource2.Phone = rand.NextInt64(11234567890, 99999999999);
                                //stapleSource2.Phone = stapleSource.Phone;
                                //stapleSource3.Phone = stapleSource.Phone;

                                //stapleSource2.Imei = rand.NextInt64(1234567890, 9876543210);
                                //stapleSource2.Imei = stapleSource.Imei;
                                //stapleSource3.Imei = stapleSource.Imei;

                                //stapleSource.Id = i;
                                //stapleSource2.Id = i + 1;
                                //stapleSource3.Id = i + 2;

                                //db.StaplesSources.Add(stapleSource2);
                                //db.StaplesSources.Add(stapleSource2);
                                //db.StaplesSources.Add(stapleSource3);

                                //i += 2;
                            }
                            else //lob:wireliss - sublub:non-wireless
                            {
                                var r = rand.Next(1, 2);

                                stapleSource.Id = i;
                                stapleSource.SubLob = WirelessSubLOBs[r];

                                bellsource.Id = i;
                                bellsource.SubLob = WirelessSubLOBs[r];

                                db.StaplesSources.Add(stapleSource);
                                db.BellSources.Add(bellsource);
                            }
                        }
                        else//Wired lob
                        {
                            stapleSource.Id = i;
                            stapleSource.Lob = Lobs[1];
                            stapleSource.SubLob = WiredSubLOBs[rand.Next(WiredSubLOBs.Length - 1)];
                            stapleSource.RebateType = RebateTypes[0];


                            bellsource.Id = i;
                            bellsource.Lob = Lobs[1];
                            bellsource.SubLob = stapleSource.SubLob;
                            bellsource.RebateType = stapleSource.RebateType;

                            db.StaplesSources.Add(stapleSource);
                            db.BellSources.Add(bellsource);
                        }
                    }

                    Console.WriteLine(i);
                    db.SaveChanges();
                }
                return "Done";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string getSampleName(BellRecContext db, Random rand)
        {
            var count = db.SampleNames.Count();
            var inx = rand.Next(0, count - 2);
            var firstname = db.SampleNames.OrderBy(c => c.FirstName).Skip(inx).Take(1).First().FirstName;

            inx = rand.Next(0, count - 2);
            var lastname = db.SampleNames.OrderBy(c => c.FirstName).Skip(inx).Take(1).First().LastName;

            return firstname + " " + lastname;
        }

        private Data.BellSource getBellSource()
        {
            Data.BellSource bellSource = new Data.BellSource(); ;

            bellSource.Phone = 0;// rand.NextInt64(11234567890, 99999999999);
            bellSource.Amount = rand.Next(100, 1500);
            bellSource.Comment = string.Empty;
            bellSource.CommissionDetails = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            bellSource.CustomerName = getSampleName(db, rand);
            bellSource.OrderNumber = rand.NextInt64(11234567890, 99999999999);
            bellSource.TransactionDate = DateTime.Now.AddDays(new Random().Next(-1000, 0)).ToShortDateString();
            bellSource.Reconciled = "False";

            return bellSource;
        }

        private Data.StaplesSource getStapleSource()
        {
            var staplesSource = new Data.StaplesSource();

            staplesSource.Phone = 0;// bellSource.Phone;

            staplesSource.Comment = string.Empty;
            staplesSource.Brand = Brands[rand.Next(0, Brands.Length - 1)];
            staplesSource.DeviceCo = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            staplesSource.Location = Locations[rand.Next(0, Locations.Length - 1)];
            staplesSource.Msf = rand.Next(5, 1400);
            staplesSource.Product = Products[rand.Next(0, Products.Length - 1)];
            staplesSource.Rec = "FALSE";
            staplesSource.SalesPerson = salesPersons[rand.Next(0, salesPersons.Count - 1)];
            staplesSource.TaxCode = rand.NextInt64(11234567890, 99999999999);
            staplesSource.TransactionDate = DateTime.Now.AddDays(rand.Next(-100000, 0)).ToShortDateString();
            staplesSource.CustomerName = getSampleName(db, rand);
            staplesSource.Amount = rand.Next(100, 1500);
            staplesSource.OrderNumber = rand.NextInt64(11234567890, 99999999999);



            return staplesSource;
        }
    }
}