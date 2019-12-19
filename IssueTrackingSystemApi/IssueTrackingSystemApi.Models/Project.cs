using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models
{
    public class Project
    {
        /// <summary>
        /// 專案編號
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 專案名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 持有人
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// 開發團隊
        /// </summary>
        public List<User> Developers { get; set; }
    }
}
