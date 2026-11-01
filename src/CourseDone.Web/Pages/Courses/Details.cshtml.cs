using CourseDone.Web.Data;
using CourseDone.Web.Domain;
using Eco.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CourseDone.Web.Pages.Courses;

public class DetailsModel(CourseDoneDb db, ICurrentUser user) : PageModel
{
    public Course Course { get; private set; } = null!;

    private Task<Course?> Load(int id) =>
        db.Courses.Include(c => c.Lessons).FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.UserId);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var c = await Load(id);
        if (c is null) return NotFound();
        Course = c;
        return Page();
    }

    public async Task<IActionResult> OnPostAddLessonAsync(int id, string lessonTitle)
    {
        var c = await Load(id);
        if (c is null) return NotFound();
        if (!string.IsNullOrWhiteSpace(lessonTitle))
        {
            c.Lessons.Add(new Lesson { Title = lessonTitle.Trim(), Order = c.Lessons.Count == 0 ? 0 : c.Lessons.Max(l => l.Order) + 1 });
            await db.SaveChangesAsync();
        }
        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostToggleAsync(int id, int lessonId)
    {
        var l = (await Load(id))?.Lessons.FirstOrDefault(x => x.Id == lessonId);
        if (l is null) return NotFound();
        l.IsDone = !l.IsDone;
        l.CompletedAtUtc = l.IsDone ? DateTime.UtcNow : null;
        await db.SaveChangesAsync();
        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostDeleteLessonAsync(int id, int lessonId)
    {
        var l = (await Load(id))?.Lessons.FirstOrDefault(x => x.Id == lessonId);
        if (l is null) return NotFound();
        db.Lessons.Remove(l);
        await db.SaveChangesAsync();
        return RedirectToPage(new { id });
    }

    public async Task<IActionResult> OnPostDeleteCourseAsync(int id)
    {
        var c = await Load(id);
        if (c is null) return NotFound();
        db.Courses.Remove(c);
        await db.SaveChangesAsync();
        return RedirectToPage("/Index");
    }
}
