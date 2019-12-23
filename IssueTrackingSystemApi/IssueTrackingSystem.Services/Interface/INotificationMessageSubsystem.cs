using System.Net.Mail;
using IssueTrackingSystemApi.Models;

namespace IssueTrackingSystemApi.Services
{
    public interface INotificationMessageSubsystem
    {
        string LintHostString { get; }

        /// <summary>
        /// 送Line Bot通知
        /// </summary>
        /// <param name="lineMessage">訊息</param>
        /// <param name="users">User</param>
        /// <returns></returns>
        string SendLineMessage(string lineMessage, User[] users);

        /// <summary>
        /// 發Mail
        /// </summary>
        /// <param name="subject">郵件標題</param>
        /// <param name="mailMessage">郵件內容</param>
        /// <param name="addressees">收件人</param>
        /// <param name="carbonCopys">CC</param>
        /// <param name="attachments">附件</param>
        /// <returns></returns>
        bool SendMail(string subject, string mailMessage, string[] addressees, string[] carbonCopys = null, Attachment[] attachments = null);
    }
}