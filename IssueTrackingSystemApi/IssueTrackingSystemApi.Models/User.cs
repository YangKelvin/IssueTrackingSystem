using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models
{
    public class User
    {
        /// <summary>
        /// 編號
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 帳戶
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 電子信箱
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// 權限
        /// </summary>
        public int? CharactorId { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// LineId
        /// </summary>
        public string LineId { get; set; }
    }
}