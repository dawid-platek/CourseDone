namespace Eco.Shared;

/// <summary>Wspólna koperta eksportu/importu danych dla wszystkich aplikacji.</summary>
public sealed record ExportEnvelope<T>(
    string App,
    int DataVersion,
    DateTime ExportedAtUtc,
    T Data);
