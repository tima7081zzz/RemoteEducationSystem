using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain
{
    public partial class Resource
    {
        public int Id { get; set; }
        public EResourceType Type { get; set; }
        public string Name { get; set; } = null!;
        public int? SubjectId { get; set; }

        public virtual Subject? Subject { get; set; }
    }
}
