using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    /// <summary>
    /// 專案角色資料表
    /// </summary>
    [DB(TableName = "ProjectCharactor")]
    public class ProjectCharactorEntity
    {
        [DB(ColumnName = "Id", AutoGenerate = true)]
        public int?Id { get; set; }
        
        [DB(ColumnName = "Name")]
        public string Name { get; set; }
    }
}
