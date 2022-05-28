using System;
using System.Collections.Generic;

namespace Domain
{
    public partial class UserActivity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ActivityId { get; set; }
        public int Grade { get; set; }
        public DateTime Date { get; set; }

        public virtual Activity Activity { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
