using Bell.Reconciliation.Web.Server.Data;
using Mapster;

namespace Bell.Reconciliation.Web.Server.Services
{
    public class BellRecRepository
    {
        public BellRecRepository()
        {
        }

        public string DBGenerator(int RecordNom, string DeleteOldRecords, int DifferenceRate)
        {
            try
            {
                BellRecContext db = new BellRecContext();
                Random rand = new Random();

                string[] Brands = { "Bell", "Virgin", "Lucky Mobile" };
                string[] LOBs = { "Wireless", "IoT", "Mobile Data", "Home Internet", "Home Internet", "Home Television", "Home Phone", "SMB Internet" };
                string[] Locations = { "Toronto", "Ottawa", "London", "Kingston", "Windsor", "Hamilton", "Barrie", "Kitchener", "Mississagua" };
                string[] RebateTypes = { "Commission", "Down Payment", "Tax Rebate" };
                string[] Products = { "iPhone 13 128GB Red", "iPhone 14 128GB Black", "iPhone 13 256GB Gold","2-Year NAC WRLS 35+",
        "2-Year NAC WRLS 55+","iPhone 13 128GB Gold","iPhone 14 256GB Gold","iPhone 14 256GB Black"};
                List<string> salesPersons = new();
                for (int i = 0; i < 10; i++)
                    salesPersons.Add(getSampleName(db, rand));

                if (DeleteOldRecords == "y" || DeleteOldRecords == "Y")
                {
                    db.BellSources.RemoveRange(db.BellSources);
                    db.StaplesSources.RemoveRange(db.StaplesSources);
                    db.SaveChanges();
                }

                Data.BellSource bellSource;
                Data.StaplesSource staplesSource;
                for (int i = 1; i < RecordNom; i += 3)
                {
                    bellSource = new Data.BellSource();
                    staplesSource = new Data.StaplesSource();

                    bellSource.Id = i;
                    bellSource.Phone = rand.NextInt64(11234567890, 99999999999);
                    bellSource.Amount = rand.Next(1, 10);
                    bellSource.Comment = string.Empty;
                    bellSource.CommissionDetails = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
                    bellSource.CustomerName = getSampleName(db, rand);
                    bellSource.Imei = rand.NextInt64(1234567890, 9876543210);
                    bellSource.Lob = LOBs[rand.Next(0, LOBs.Length - 1)];
                    bellSource.OrderNumber = rand.NextInt64(11234567890, 99999999999);
                    bellSource.TransactionDate = DateTime.Now.AddDays(new Random().Next(-100000, 0)).ToShortDateString();
                    db.BellSources.Add(bellSource);

                    staplesSource.OrderNumber = bellSource.OrderNumber;
                    if (rand.Next(0, 100) <= DifferenceRate)
                    {
                        staplesSource.CustomerName = getSampleName(db, rand);
                        staplesSource.Amount = rand.Next(1, 10);
                        staplesSource.Imei = rand.NextInt64(1234567890, 9876543210);
                        staplesSource.Phone = long.Parse(((long)new Random().Next(0, 100000) * (long)new Random().Next(0, 100000)).ToString().PadLeft(10, '0'));
                        staplesSource.Comment = string.Empty;
                    }
                    else
                    {
                        staplesSource.CustomerName = bellSource.CustomerName;
                        staplesSource.Amount = bellSource.Amount;
                        staplesSource.Imei = bellSource.Imei;
                        staplesSource.Phone = bellSource.Phone;
                        staplesSource.Comment = bellSource.Comment;
                    }

                    staplesSource.Brand = Brands[rand.Next(0, Brands.Length - 1)];
                    staplesSource.DeviceCo = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
                    staplesSource.Location = Locations[rand.Next(0, Locations.Length - 1)];
                    staplesSource.Msf = rand.Next(5, 1400);
                    staplesSource.Product = Products[rand.Next(0, Products.Length - 1)];
                    staplesSource.Rec = "FALSE";
                    staplesSource.SalesPerson = salesPersons[rand.Next(0, salesPersons.Count - 1)];
                    staplesSource.TaxCode = rand.NextInt64(11234567890, 99999999999);
                    staplesSource.TransactionDate = DateTime.Now.AddDays(rand.Next(-100000, 0)).ToShortDateString();

                    var staplesSource2 = staplesSource.Adapt<Data.StaplesSource>();
                    var staplesSource3 = staplesSource.Adapt<Data.StaplesSource>();

                    staplesSource.Id = bellSource.Id;
                    staplesSource.RebateType = RebateTypes[0];

                    staplesSource2.Id = bellSource.Id + 1;
                    staplesSource2.RebateType = RebateTypes[1];

                    staplesSource3.Id = bellSource.Id + 2;
                    staplesSource3.RebateType = RebateTypes[2];

                    db.StaplesSources.Add(staplesSource);
                    db.StaplesSources.Add(staplesSource2);
                    db.StaplesSources.Add(staplesSource3);

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
    }
}