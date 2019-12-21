using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    /// <summary>
    /// 問題處理狀態資料表
    /// </summary>
    [DB(TableName = "IssueStatus")]
    public class IssueStatusEntity
    {
        [DB(ColumnName = "Id", AutoGenerate = true)]
        public int?Id { get; set; }

        [DB(ColumnName = "Name")]
        public string Name { get; set; }
    }
}
