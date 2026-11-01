namespace CourseDone.Web.Domain;

public class Course
{
    public int Id { get; set; }
    /// <summary>Właściciel — gotowe pod wspólną tożsamość w ekosystemie.</summary>
    public string UserId { get; set; } = "local";
    public string Title { get; set; } = "";
    public string? Provider { get; set; }
    public string? Url { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public List<Lesson> Lessons { get; set; } = new();

    public int TotalLessons => Lessons.Count;
    public int DoneLessons => Lessons.Count(l => l.IsDone);

    /// <summary>Postęp 0–100.</summary>
    public int ProgressPercent => TotalLessons == 0 ? 0 : (int)Math.Round(100.0 * DoneLessons / TotalLessons);

    public CourseStatus Status =>
        TotalLessons == 0 || DoneLessons == 0 ? CourseStatus.NotStarted
        : DoneLessons == TotalLessons ? CourseStatus.Completed
        : CourseStatus.InProgress;
}

public enum CourseStatus { NotStarted, InProgress, Completed }

public class Lesson
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string Title { get; set; } = "";
    public bool IsDone { get; set; }
    public DateTime? CompletedAtUtc { get; set; }
    public int Order { get; set; }
}
