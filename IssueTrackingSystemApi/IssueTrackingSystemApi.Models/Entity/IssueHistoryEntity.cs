using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    /// <summary>
    /// 問題內容修改記錄資料表
    /// </summary>
    [DB(TableName = "IssueHistory")]
    public class IssueHistoryEntity
    {
        /// <summary>
        /// 記錄流水號
        /// </summary>
        [DB(ColumnName = "Id")]
        public int?Id { get; set; }

        /// <summary>
        /// 問題流水號
        /// </summary>
        [DB(ColumnName = "IssueId")]
        public int?IssueId { get; set; }

        /// <summary>
        /// 更改使用者的Id
        /// </summary>
        [DB(ColumnName = "UserId")]
        public int?UserId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DB(ColumnName = "WorkDescription")]
        public string WorkDescription { get; set; }

        /// <summary>
        /// 更改紀錄時間
        /// </summary>
        [DB(ColumnName = "CreateTime", Nullable = true)]
        public DateTime? CreateTime { get; set; }

    }
}
