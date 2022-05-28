using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain
{
    public partial class User
    {
        public User()
        {
            Professors = new HashSet<Professor>();
            UserActivities = new HashSet<UserActivity>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int Id { get; set; }
        public EUserRole Role { get; set; }
        public string Fullname { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Professor> Professors { get; set; }
        public virtual ICollection<UserActivity> UserActivities { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
