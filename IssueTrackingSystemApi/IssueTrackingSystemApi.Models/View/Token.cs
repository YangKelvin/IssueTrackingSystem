using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models.View
{
    public class Token
    {
        /// <summary>
        /// token
        /// </summary>
        public string token {get; set; }

        /// <summary>
        /// 使用者Id
        /// </summary>
        public int? userId { get; set; }
    }
}
