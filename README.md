# CourseDone

Aplikacja do śledzenia postępów w kursach. Pierwsza aplikacja w osobistym ekosystemie.

**Stack:** C# / .NET 8, ASP.NET Core Razor Pages, EF Core + SQLite, xUnit.

## Uruchomienie
    dotnet run --project src/CourseDone.Web
    dotnet test

## Układ pod ekosystem
- `src/Eco.Shared` – wspólna biblioteka (manifest aplikacji, koperta eksportu, `ICurrentUser` pod przyszłe SSO).
- `src/CourseDone.Web` – ta aplikacja. Kolejne aplikacje dodawaj jako `src/<Nazwa>.Web` i referuj `Eco.Shared`.
- `/.well-known/eco-app` – manifest, z którego przyszły launcher zbuduje menu.
- Każdy rekord ma `UserId`, więc dodanie wspólnego logowania nie wymaga migracji modelu.

## PWA
Manifest (`wwwroot/manifest.webmanifest`), service worker (`wwwroot/sw.js`), ikony i strona offline.
Strony: sieć najpierw, a bez sieci ostatnia kopia lub `offline.html`. Zasoby statyczne z cache.
Zapisy (POST) zawsze idą do sieci; offline tylko do odczytu.
Service worker i instalacja na iPhonie wymagają HTTPS (poza `localhost`).
iPhone: Safari → Udostępnij → „Dodaj do ekranu początkowego”.
Po zmianie plików statycznych podbij `VERSION` w `sw.js`.
