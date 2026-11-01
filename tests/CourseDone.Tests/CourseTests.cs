using CourseDone.Web.Domain;

namespace CourseDone.Tests;

public class CourseTests
{
    private static Course Make(params bool[] done) =>
        new() { Lessons = done.Select(d => new Lesson { IsDone = d }).ToList() };

    [Fact] public void Empty_course_is_not_started() =>
        Assert.Equal((0, CourseStatus.NotStarted), (Make().ProgressPercent, Make().Status));

    [Fact] public void Partial_progress_is_rounded() =>
        Assert.Equal(33, Make(true, false, false).ProgressPercent);

    [Fact] public void All_done_is_completed() =>
        Assert.Equal(CourseStatus.Completed, Make(true, true).Status);

    [Fact] public void Some_done_is_in_progress() =>
        Assert.Equal(CourseStatus.InProgress, Make(true, false).Status);
}
