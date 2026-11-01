using CourseDone.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace CourseDone.Web.Data;

public class CourseDoneDb(DbContextOptions<CourseDoneDb> options) : DbContext(options)
{
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Lesson> Lessons => Set<Lesson>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Course>().Ignore(c => c.TotalLessons).Ignore(c => c.DoneLessons)
            .Ignore(c => c.ProgressPercent).Ignore(c => c.Status);
        b.Entity<Course>().HasMany(c => c.Lessons).WithOne().HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
        b.Entity<Course>().HasIndex(c => c.UserId);
    }
}
