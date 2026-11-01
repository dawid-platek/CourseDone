using CourseDone.Web.Data;
using CourseDone.Web.Domain;
using Eco.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CourseDone.Web.Pages;

public class IndexModel(CourseDoneDb db, ICurrentUser user) : PageModel
{
    public List<Course> Courses { get; private set; } = new();

    public async Task OnGetAsync() =>
        Courses = await db.Courses.Include(c => c.Lessons)
            .Where(c => c.UserId == user.UserId)
            .OrderByDescending(c => c.CreatedAtUtc).ToListAsync();
}
