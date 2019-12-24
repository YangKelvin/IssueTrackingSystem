using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    /// <summary>
    /// 角色資料表
    /// </summary>
    [DB(TableName = "UserProjectRelation")]
    public class UserProjectRelationEntity
    {
        [DB(ColumnName = "UserId")]
        public int? UserId { get; set; }
        
        [DB(ColumnName = "ProjectId")]
        public int? ProjectId { get; set; }

        [DB(ColumnName = "ProjectCharactorId")]
        public int? ProjectCharactorId { get; set; }
    }
}
