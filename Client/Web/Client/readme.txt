how to fetch the browser DB:
URL.createObjectURL(await (await (await caches.open("SqliteWasmHelper")).match("/data/cache/StapleSource.sqlite3")).blob());


 Scaffold-DbContext "Data Source=recbell.database.windows.net;Initial Catalog=recbell;User ID=sharif;Password=biaTelegram123;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
	
	
	Scaffold-DbContext "Data Source=efi-stg-database-smi.77e4410e91a5.database.windows.net;Initial Catalog=BELL_INTEGRATION_DEV;Persist Security Info=True;User ID=log_util_bell;Password=test;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models


===============================================

DATABASE SWITCHING STEPS:
1. Index.html
2. wwroot/appsetting
3. appseting in server
4. data folder - db context