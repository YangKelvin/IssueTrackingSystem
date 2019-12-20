using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    /// <summary>
    /// 專案資料表
    /// </summary>
    [DB(ColumnName = "Project")]
    public class ProjectEntity
    {
        [DB(ColumnName = "Id")]
        public int Id { get; set; }

        [DB(ColumnName = "Name")]
        public string Name { get; set; }
    }
}
