export function synchronizeFileWithIndexedDb(filename) {
    return new Promise((res, rej) => {
        const db = window.indexedDB.open('SqliteStorage', 1);
        db.onupgradeneeded = () => {
            db.result.createObjectStore('Files', { keypath: 'id' });
        };

        db.onsuccess = () => {
            const req = db.result.transaction('Files', 'readonly').objectStore('Files').get('file');
            req.onsuccess = () => {
                Module.FS.createDataFile('/', filename, req.result, true, true, true);
                res(1);
            };
        };

        let lastModifiedTime = new Date();
        setInterval(() => {
            const path = `/${filename}`;
            if (Module.FS.analyzePath(path).exists) {
                const mtime = Module.FS.stat(path).mtime;
                if (mtime.valueOf() !== lastModifiedTime.valueOf()) {
                    lastModifiedTime = mtime;
                    const data = Module.FS.readFile(path);
                    //THIS IS THE ERROR APPEARS IN BROWSER FOR PRESENTING THE DATA:
                    //Uncaught DOMException: Failed to read the 'result' property from 'IDBRequest': The request has not finished.
                    db.result.transaction('Files', 'readwrite').objectStore('Files').put(data, 'file');
                    return 0;
                }
            }
        }, 1000);
    });
}

export async function generateDownloadLink(parent, file) {
    const backupPath = `${file}`;
    const cachePath = `/data/cache/${file.substring(0, file.indexOf('_bak'))}`;
    const db = window.sqlitedb;
    const resp = await db.cache.match(cachePath);

    if (resp && resp.ok) {
        const res = await resp.blob();
        if (res) {
            const a = document.createElement("a");
            a.href = URL.createObjectURL(res);
            a.download = backupPath;
            a.target = "_self";
            a.innerText = `Download ${backupPath}`;
            parent.innerHTML = '';
            parent.appendChild(a);
            return true;
        }
    }

    return false;
}