using Eco.Shared;

namespace CourseDone.Web;

public static class AppInfo
{
    public static readonly AppManifest Manifest = new(
        Id: "coursedone",
        Name: "CourseDone",
        Description: "Śledzenie postępów w kursach",
        BasePath: "/",
        DataVersion: 1);
}
