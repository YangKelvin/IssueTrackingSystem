using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.View
{
    public class ProjectFront
    {
        /// <summary>
        /// 專案名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 創建者Id
        /// </summary>
        public int? managerId { get; set; }

        /// <summary>
        /// 開發者Id
        /// </summary>
        public List<int?> developersId { get; set; }

        /// <summary>
        /// 提問者 Id
        /// </summary>
        public List<int?> generalsId { get; set; }
    }
}
