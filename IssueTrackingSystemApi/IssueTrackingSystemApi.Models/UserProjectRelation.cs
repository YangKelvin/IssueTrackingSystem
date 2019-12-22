using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models
{
    public class UserProjectRelation
    {
        public User User { get; set; }
        public Project Project { get; set; }
        public int ProjectCharactorId { get; set; }
    }
}
