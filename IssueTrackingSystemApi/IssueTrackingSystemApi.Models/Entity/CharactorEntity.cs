using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    /// <summary>
    /// 資料表
    /// </summary>
    [DB(TableName = "Charactor")]
    public class CharactorEntity
    {
        [DB(ColumnName = "Id", AutoGenerate = true)]
        public int?Id { get; set; }

        [DB(ColumnName = "Name")]
        public string Name { get; set; }
    }
}
