using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Linq;

namespace IssueTrackingSystemApi.Services
{
    public class NotificationMessageSubsystem
    {
        public bool SendMail(string mailMessage, string[] addressees, string[] carbonCopys = null, Attachment[] attachments = null)
        {
            try
            {
                MailMessage msg = new MailMessage();

                if (addressees == null || !addressees.Any()) return false; // 沒收件者就是錯的

                Array.ForEach(addressees, i => msg.To.Add(i)); // 正本
                if (carbonCopys != null && carbonCopys.Any()) 
                { 
                    Array.ForEach(carbonCopys, i => msg.CC.Add(i)); // CC
                }

                //這裡可以隨便填，不是很重要
                msg.From = new MailAddress("issuetrackingsystemmail@gmail.com", " ", Encoding.UTF8);
                /* 上面3個參數分別是發件人地址（可以隨便寫），發件人姓名，編碼*/
                msg.Subject = "測試標題";//郵件標題
                msg.SubjectEncoding = Encoding.UTF8;//郵件標題編碼
                msg.Body = mailMessage; //郵件內容
                msg.BodyEncoding = Encoding.UTF8;//郵件內容編碼 

                if(attachments != null && attachments.Any())
                {
                    Array.ForEach(attachments, a => msg.Attachments.Add(a));  //附件
                }

                msg.IsBodyHtml = true;//是否是HTML郵件 
                                      //msg.Priority = MailPriority.High;//郵件優先級 

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("issuetrackingsystemmail@gmail.com", "IssueTracking"); //這裡要填正確的帳號跟密碼
                client.Host = "smtp.gmail.com"; //設定smtp Server
                client.Port = 25; //設定Port
                client.EnableSsl = true; //gmail預設開啟驗證
                client.Send(msg); //寄出信件
                client.Dispose();
                msg.Dispose();
                //MessageBox.Show(this, "郵件寄送成功！");
                return true;
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(this, ex.Message);
                return false;
            }
        }
    }
}
