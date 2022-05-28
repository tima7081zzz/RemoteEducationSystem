using System;
using System.Collections.Generic;

namespace Domain
{
    public partial class Subject
    {
        public Subject()
        {
            Activities = new HashSet<Activity>();
            Professors = new HashSet<Professor>();
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Professor> Professors { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
