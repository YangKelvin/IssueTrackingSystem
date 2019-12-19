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
        public string ColumnName { get; set; }

        public ConitionOperation Operation { get; set; } = ConitionOperation.AND;
    }
}
