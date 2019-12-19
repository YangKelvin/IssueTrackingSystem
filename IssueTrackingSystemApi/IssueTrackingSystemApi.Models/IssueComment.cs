using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models
{
    public class IssueComment
    {
        /// <summary>
        /// 問題流水號
        /// </summary>
        public int IssueId { get; set; }

        /// <summary>
        /// 留言流水號
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// 留言人
        /// </summary>
        public int CommenterId { get; set; }

        /// <summary>
        /// 留言內容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 留言問題時間
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 留言人 Id
        /// </summary>
        public int CreateUesr { get; set; }

        /// <summary>
        /// 最後修改留言時間
        /// </summary>
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 最後修改人 Id
        /// </summary>
        public int ModifyUser { get; set; }
    }
}
