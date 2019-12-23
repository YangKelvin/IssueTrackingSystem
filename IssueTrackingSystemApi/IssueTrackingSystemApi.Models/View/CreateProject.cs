using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.View
{
    public class CreateProject
    {
        /// <summary>
        /// 專案名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 創建者Id
        /// </summary>
        public int managerId { get; set; }
    }
}
