using System;
using System.Collections.Generic;

namespace Domain
{
    public partial class Professor
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
