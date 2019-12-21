using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    /// <summary>
    /// 用戶帳號資料表
    /// </summary>
    [DB(TableName = "User")]
    public class UserEntity
    {
        [DB(ColumnName = "Id", AutoGenerate = true)]
        public int? Id { get; set; }

        [DB(ColumnName = "Account")]
        public string Account { get; set; }

        [DB(ColumnName = "Password")]
        public string Password { get; set; }

        [DB(ColumnName = "EMail")]
        public string EMail { get; set; }

        [DB(ColumnName = "CharactorId")]
        public int? CharactorId { get; set; }

        [DB(ColumnName = "Name")]
        public string Name { get; set; }

        [DB(ColumnName = "LineId")]
        public string LineId { get; set; }
    }
}