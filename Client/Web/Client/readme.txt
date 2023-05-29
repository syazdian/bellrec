how to fetch the browser DB:
URL.createObjectURL(await (await (await caches.open("SqliteWasmHelper")).match("/data/cache/StapleSource.sqlite3")).blob());
