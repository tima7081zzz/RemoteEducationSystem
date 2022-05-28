using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces;

public interface IcourseWorkDbContext
{
    DbSet<Activity> Activities { get; set; }
    DbSet<Group> Groups { get; set; }
    DbSet<Professor> Professors { get; set; }
    DbSet<Resource> Resources { get; set; }
    DbSet<Subject> Subjects { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<UserActivity> UserActivities { get; set; }
    DbSet<UserGroup> UserGroups { get; set; }
}