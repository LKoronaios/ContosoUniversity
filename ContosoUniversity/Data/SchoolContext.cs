using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
    public DbSet<Membership> Memberships { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().ToTable(nameof(Course))
            .HasMany(c => c.Instructors)
            .WithMany(i => i.Courses);
        modelBuilder.Entity<Student>().ToTable(nameof(Student));
        modelBuilder.Entity<Instructor>().ToTable(nameof(Instructor));

        modelBuilder.Entity<Membership>()
        .HasKey(m => m.MemberID);

        modelBuilder.Entity<Membership>()
        .HasOne(m => m.Student1)
        .WithMany()
        .HasForeignKey(m => m.Student1ID)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Membership>()
            .HasOne(m => m.Student2)
            .WithMany()
            .HasForeignKey(m => m.Student2ID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
