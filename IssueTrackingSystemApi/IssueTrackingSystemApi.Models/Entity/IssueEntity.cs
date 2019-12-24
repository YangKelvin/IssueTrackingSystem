using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    /// <summary>
    /// 問題種類資料表
    /// </summary>
    [DB(TableName = "Issue")]
    public class IssueEntity
    {
        /// <summary>
        /// 流水號
        /// </summary>
        [DB(ColumnName = "Id", AutoGenerate = true)]
        public int?Id { get; set; }


        /// <summary>
        /// 問題單號
        /// </summary>
        [DB(ColumnName = "IssueNumber")]
        public string Number { get; set; }

        /// <summary>
        /// 問題名稱
        /// </summary>
        [DB(ColumnName = "IssueSummary")]
        public string Summary { get; set; }

        /// <summary>
        /// 問題描述
        /// </summary>
        [DB(ColumnName = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// 附件(用附件編號串起來?)
        /// </summary>
        [DB(ColumnName = "Attachments")]
        public string Attachments { get; set; }

        /// <summary>
        /// 受讓者
        /// </summary>
        [DB(ColumnName = "AssigneeId")]
        public int?AssigneeId { get; set; }

        /// <summary>
        /// 回報者
        /// </summary>
        [DB(ColumnName = "ReporterId")]
        public int?ReporterId { get; set; }

        /// <summary>
        /// 估計處理時間(給分配問題的人填寫)
        /// </summary>
        [DB(ColumnName = "Estimated")]
        public double? Estimated { get; set; }

        /// <summary>
        /// 估計開始時間(給問題處理的人填寫)
        /// </summary>
        [DB(ColumnName = "EstimatedStartTime")]
        public DateTime? EstimatedStartTime { get; set; }

        /// <summary>
        /// 估計結束時間(給問題處理的人填寫)
        /// </summary>
        [DB(ColumnName = "EstimatedEndTime")]
        public DateTime? EstimatedEndTime { get; set; }

        /// <summary>
        /// 實際開始時間(給問題處理的人填寫)
        /// </summary>
        [DB(ColumnName = "ActualStartTime")]
        public DateTime? ActualStartTime { get; set; }

        /// <summary>
        /// 實際結束時間(給問題處理的人填寫)
        /// </summary>
        [DB(ColumnName = "ActualEndTime")]
        public DateTime? ActualEndTime { get; set; }

        /// <summary>
        /// 解決問題時間(系統帶入)
        /// </summary>
        [DB(ColumnName = "ResolveTime")]
        public DateTime? ResolveTime { get; set; }

        /// <summary>
        /// 問題種類 Id
        /// </summary>
        [DB(ColumnName = "KindId")]
        public int?KindId { get; set; }

        /// <summary>
        /// 問題嚴重性 Id
        /// </summary>
        [DB(ColumnName = "ServerityId")]
        public int?ServerityId { get; set; }

        /// <summary>
        /// 問題狀態 Id
        /// </summary>
        [DB(ColumnName = "StatusId")]
        public int?StatusId { get; set; }

        /// <summary>
        /// 問題緊急性 Id
        /// </summary>
        [DB(ColumnName = "UrgencyId")]
        public int?UrgencyId { get; set; }

        /// <summary>
        /// 專案 Id
        /// </summary>
        [DB(ColumnName = "ProjectId")]
        public int?ProjectId { get; set; }

        /// <summary>
        /// 建立問題時間
        /// </summary>
        [DB(ColumnName = "CreateTime")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 建立人 Id
        /// </summary>
        [DB(ColumnName = "CreateUser")]
        public int?CreateUser { get; set; }

        /// <summary>
        /// 最後修改問題時間
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
