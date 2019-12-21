using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    /// <summary>
    /// 問題緊急性資料表
    /// </summary>
    [DB(TableName = "IssueUrgency")]
    public class IssueUrgencyEntity
    {
        [DB(ColumnName = "Id", AutoGenerate = true)]
        public int?Id { get; set; }

        [DB(ColumnName = "Name")]
        public string Name { get; set; }
    }
}
