namespace Eco.Shared;

/// <summary>
/// Punkt zaczepienia pod przyszłe wspólne logowanie (SSO). Dziś aplikacja jest
/// jednoużytkownikowa; gdy dojdzie tożsamość, wystarczy podmienić implementację.
/// </summary>
public interface ICurrentUser
{
    string UserId { get; }
}

public sealed class SingleUser : ICurrentUser
{
    public string UserId => "local";
}
