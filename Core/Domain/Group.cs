using System;
using System.Collections.Generic;

namespace Domain
{
    public partial class Group
    {
        public Group()
        {
            Professors = new HashSet<Professor>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Professor> Professors { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
