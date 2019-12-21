using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    /// <summary>
    /// 問題嚴重性資料表
    /// </summary>
    [DB(TableName = "IssueSeverity")]
    public class IssueSeverityEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [DB(ColumnName = "Id", AutoGenerate = true)]
        public int?Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DB(ColumnName = "Name")]
        public string Name { get; set; }
    }
}
