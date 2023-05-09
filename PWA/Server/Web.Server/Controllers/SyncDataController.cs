using Bell.Reconciliation.Web.Server.Data;
using Bell.Reconciliation.Web.Server.Services;

namespace Bell.Reconciliation.Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SyncDataController : Controller
{
    private readonly DatabaseGenerator _databaseGenerator;

    public SyncDataController(DatabaseGenerator databaseGenerator)
    {
        _databaseGenerator = databaseGenerator;
    }

    [HttpGet("GetBellSourceitems/{Id}")]
    public async Task<IActionResult> GetBellSourceitems([FromRoute] int Id = 0)
    {
        var items = await _databaseGenerator.DatabaseBellSourceGenerator(Id);
        return Ok(items);
    }

    [HttpGet("FillSqlite/{RecordNom}/{DeleteOldRecords}/{DifferenceRate}")]
    public async Task<IActionResult> FillSqlite([FromRoute] int RecordNom, string DeleteOldRecords, int DifferenceRate)
    {
        BellRecContext db = new BellRecContext();

        string[] LOBs = { "Mobility", "Laptop", "TV", "Smart Home", "Connected Things", "Bundles", "Internet", "promotions"};
        if (DeleteOldRecords == "y" || DeleteOldRecords == "Y")
        {
            db.BellSources.RemoveRange(db.BellSources);
            db.StaplesSources.RemoveRange(db.StaplesSources);
            db.SaveChanges();
        }

        Data.BellSource bellSource;
        Data.StaplesSource staplesSource;
        for (int i = 1; i < RecordNom; i++)
        {
            bellSource = new Data.BellSource();
            staplesSource = new Data.StaplesSource();

            Random rand = new Random();

            bellSource.Id = i;
            bellSource.Phone = rand.NextInt64(11234567890, 99999999999);
            bellSource.Amount = rand.Next(1, 10);
            bellSource.Comment = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            bellSource.CommissionDetails = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            bellSource.CustomerName = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            bellSource.Imei = rand.NextInt64(1234567890, 9876543210);
            bellSource.Lob = LOBs[rand.Next(0,LOBs.Length-1)];
            bellSource.OrderNumber = rand.NextInt64(11234567890, 99999999999);
            bellSource.TransactionDate = DateTime.Now.AddDays(new Random().Next(-100000, 0)).ToShortDateString();
            db.BellSources.Add(bellSource);

            staplesSource.Id = bellSource.Id;
            staplesSource.OrderNumber = bellSource.OrderNumber;
            if ((new Random()).Next(0, 100) <= DifferenceRate)
            {
                staplesSource.CustomerName = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
                staplesSource.Amount = rand.Next(1, 10);
                staplesSource.Imei = rand.NextInt64(1234567890, 9876543210);
                staplesSource.Phone = long.Parse(((long)new Random().Next(0, 100000) * (long)new Random().Next(0, 100000)).ToString().PadLeft(10, '0'));
                staplesSource.Comment = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            }
            else
            {
                staplesSource.CustomerName = bellSource.CustomerName;
                staplesSource.Amount = bellSource.Amount;
                staplesSource.Imei = bellSource.Imei;
                staplesSource.Phone = bellSource.Phone;
                staplesSource.Comment = bellSource.Comment;
            }


            staplesSource.Brand = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            staplesSource.DeviceCo = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            staplesSource.Location = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            staplesSource.Msf = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            staplesSource.Product = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            staplesSource.RebateType = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            staplesSource.Rec = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            staplesSource.SalesPerson = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
            staplesSource.TaxCode = rand.NextInt64(11234567890, 99999999999);
            staplesSource.TransactionDate = DateTime.Now.AddDays(new Random().Next(-100000, 0)).ToShortDateString();
            db.StaplesSources.Add(staplesSource);


            db.SaveChanges();
        }
        return Ok("Done");
    }
}