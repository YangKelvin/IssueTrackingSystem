using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    /// <summary>
    /// 問題種類資料表
    /// </summary>
    [DB(TableName = "IssueKind")]
    public class IssueKindEntity
    {
        [DB(ColumnName = "Id", AutoGenerate = true)]
        public int?Id { get; set; }
        [DB(ColumnName = "Name")]
        public string Name { get; set; }
    }
}
