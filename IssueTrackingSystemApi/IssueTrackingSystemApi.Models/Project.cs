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
        public int? Id { get; set; }
        /// <summary>
        /// 專案名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 管理者
        /// </summary>
        public User Manager { get; set; }

        /// <summary>
        /// 開發團隊
        /// </summary>
        public List<User> Developers { get; set; }

        /// <summary>
        /// 提問者
        /// </summary>
        public List<User> General { get; set; }
    }
}
