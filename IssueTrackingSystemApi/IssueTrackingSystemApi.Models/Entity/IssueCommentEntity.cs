using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    /// <summary>
    /// 問題留言資料表
    /// </summary>
    [DB(TableName = "IssueComment")]
    public class IssueCommentEntity
    {
        /// <summary>
        /// 問題流水號
        /// </summary>
        [DB(ColumnName = "IssueId")]
        public int?IssueId { get; set; }

        /// <summary>
        /// 留言流水號
        /// </summary>
        [DB(ColumnName = "CommentId")]
        public int?CommentId { get; set; }

        /// <summary>
        /// 留言人
        /// </summary>
        [DB(ColumnName = "CommenterId")]
        public int?CommenterId { get; set; }

        /// <summary>
        /// 留言內容
        /// </summary>
        [DB(ColumnName = "Content")]
        public string Content { get; set; }

        /// <summary>
        /// 留言問題時間
        /// </summary>
        [DB(ColumnName = "CreateTime")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 留言人 Id
        /// </summary>
        [DB(ColumnName = "CreateUesr")]
        public int?CreateUesr { get; set; }

        /// <summary>
        /// 最後修改留言時間
        /// </summary>
        [DB(ColumnName = "ModifyTime")]
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 最後修改人 Id
        /// </summary>
        [DB(ColumnName = "ModifyUser")]
        public int?ModifyUser { get; set; }

    }
}
