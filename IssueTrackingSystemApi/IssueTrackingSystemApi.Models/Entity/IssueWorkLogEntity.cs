using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    /// <summary>
    /// 問題工作記錄資料表
    /// </summary>
    [DB(TableName = "IssueWorkLog")]
    public class IssueWorkLogEntity
    {
        /// <summary>
        /// 問題流水號
        /// </summary>
        [DB(ColumnName = "IssueId")]
        public int?IssueId { get; set; }

        /// <summary>
        /// 記錄流水號
        /// </summary>
        [DB(ColumnName = "LogId", AutoGenerate = true)]
        public int?LogId { get; set; }

        /// <summary>
        /// 花費時間 (小時)
        /// </summary>
        [DB(ColumnName = "SpentTime")]
        public double? SpentTime { get; set; }

        /// <summary>
        /// 開始時間
        /// </summary>
        [DB(ColumnName = "DateStart")]
        public DateTime? DateStart { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DB(ColumnName = "WorkDescription")]
        public string WorkDescription { get; set; }

        /// <summary>
        /// 建立問題時間
        /// </summary>
        [DB(ColumnName = "CreateTime")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 建立人 Id
        /// </summary>
        [DB(ColumnName = "CreateUesr")]
        public int?CreateUesr { get; set; }

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
