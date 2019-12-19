using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public int CharactorId { get; set; }
        public string Name { get; set; }
        public string LineId { get; set; }
    }
}