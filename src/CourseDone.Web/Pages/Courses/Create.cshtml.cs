using System.ComponentModel.DataAnnotations;
using CourseDone.Web.Data;
using CourseDone.Web.Domain;
using Eco.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseDone.Web.Pages.Courses;

public class CreateModel(CourseDoneDb db, ICurrentUser user) : PageModel
{
    [BindProperty, Required, StringLength(200)] public string Title { get; set; } = "";
    [BindProperty, StringLength(100)] public string? Provider { get; set; }
    [BindProperty, Url] public string? CourseUrl { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        var c = new Course { UserId = user.UserId, Title = Title.Trim(), Provider = Provider?.Trim(), Url = CourseUrl?.Trim() };
        db.Courses.Add(c);
        await db.SaveChangesAsync();
        return RedirectToPage("Details", new { id = c.Id });
    }
}
