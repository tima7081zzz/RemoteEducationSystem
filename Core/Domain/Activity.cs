using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain
{
    public partial class Activity
    {
        public Activity()
        {
            UserActivities = new HashSet<UserActivity>();
        }

        public int Id { get; set; }
        public EActivityType Type { get; set; }
        public string Name { get; set; } = null!;
        public int MaxGrade { get; set; }
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; } = null!;
        public virtual ICollection<UserActivity> UserActivities { get; set; }
    }
}
