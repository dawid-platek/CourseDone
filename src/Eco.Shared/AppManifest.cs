namespace Eco.Shared;

/// <summary>
/// Opis aplikacji w ekosystemie. Przyszły launcher / wspólna nawigacja
/// może zebrać manifesty wszystkich aplikacji i zbudować menu.
/// </summary>
public sealed record AppManifest(
    string Id,
    string Name,
    string Description,
    string BasePath,
    int DataVersion);
