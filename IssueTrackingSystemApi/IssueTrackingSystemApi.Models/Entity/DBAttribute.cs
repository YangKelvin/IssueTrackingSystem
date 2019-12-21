using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.Entity
{
    public enum ConitionOperation
    {
        AND,
        OR
    }

    public class DBAttribute : Attribute
    {
        public string TableName { get; set; }
        public bool TableDeleteAble { get; set; }

        public string ColumnName { get; set; }

        /// <summary>
        /// 條件間接
        /// </summary>
        public ConitionOperation Operation { get; set; } = ConitionOperation.AND;

        /// <summary>
        /// 是否為自動產生
        /// </summary>
        public bool AutoGenerate { get; set; }

        /// <summary>
        ///  可以是NULL
        /// </summary>
        public bool Nullable { get; set; }
    }
}
