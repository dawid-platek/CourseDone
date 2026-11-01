// Service worker CourseDone. Podbij VERSION, aby wymusić odświeżenie cache.
const VERSION = "v1";
const STATIC = `coursedone-static-${VERSION}`;
const PAGES = `coursedone-pages-${VERSION}`;
const PRECACHE = ["/offline.html", "/icons/icon-192.png", "/manifest.webmanifest",
  "/lib/bootstrap/dist/css/bootstrap.min.css", "/lib/bootstrap/dist/js/bootstrap.bundle.min.js"];

self.addEventListener("install", e => {
  e.waitUntil(caches.open(STATIC).then(c => c.addAll(PRECACHE)).then(() => self.skipWaiting()));
});

self.addEventListener("activate", e => {
  e.waitUntil(caches.keys()
    .then(keys => Promise.all(keys.filter(k => ![STATIC, PAGES].includes(k)).map(k => caches.delete(k))))
    .then(() => self.clients.claim()));
});

self.addEventListener("fetch", e => {
  const req = e.request;
  const url = new URL(req.url);
  if (req.method !== "GET" || url.origin !== location.origin) return; // POST-y zawsze idą do sieci

  if (req.mode === "navigate") {
    // Strony: sieć najpierw (dane muszą być aktualne), przy braku sieci ostatnia kopia lub strona offline.
    e.respondWith(fetch(req).then(res => {
      if (res.ok) { const copy = res.clone(); caches.open(PAGES).then(c => c.put(req, copy)); }
      return res;
    }).catch(() => caches.match(req).then(r => r || caches.match("/offline.html"))));
    return;
  }

  // Zasoby statyczne: cache najpierw, w tle odświeżanie.
  e.respondWith(caches.match(req).then(cached => {
    const net = fetch(req).then(res => {
      if (res.ok) { const copy = res.clone(); caches.open(STATIC).then(c => c.put(req, copy)); }
      return res;
    }).catch(() => cached);
    return cached || net;
  }));
});
