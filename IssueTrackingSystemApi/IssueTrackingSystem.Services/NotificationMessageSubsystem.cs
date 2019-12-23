using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IssueTrackingSystemApi.Models;
using Microsoft.Extensions.Configuration;

namespace IssueTrackingSystemApi.Services
{
    public class NotificationMessageSubsystem : INotificationMessageSubsystem
    {
        public string LintHostString { get => @"https://mysterious-wave-50057.herokuapp.com/SendIssueNotification/"; }

        public IConfiguration Config { get; }

        public NotificationMessageSubsystem(IConfiguration configuration)
        {
            Config = configuration;
        }

        /// <summary>
        /// 發Mail
        /// </summary>
        /// <param name="subject">郵件標題</param>
        /// <param name="mailMessage">郵件內容</param>
        /// <param name="addressees">收件人</param>
        /// <param name="carbonCopys">CC</param>
        /// <param name="attachments">附件</param>
        /// <returns></returns>
        public bool SendMail(string subject ,string mailMessage, string[] addressees, string[] carbonCopys = null, Attachment[] attachments = null)
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
                msg.From = new MailAddress(Config["NMSSetting:Mail:UserName"], Config["NMSSetting:Mail:DisplayName"], Encoding.UTF8);
                /* 上面3個參數分別是發件人地址（可以隨便寫），發件人姓名，編碼*/
                msg.Subject = subject;//郵件標題
                msg.SubjectEncoding = Encoding.UTF8;//郵件標題編碼
                msg.Body = mailMessage; //郵件內容
                msg.BodyEncoding = Encoding.UTF8;//郵件內容編碼 

                if (attachments != null && attachments.Any())
                {
                    Array.ForEach(attachments, a => msg.Attachments.Add(a));  //附件
                }

                msg.IsBodyHtml = true;//是否是HTML郵件 
                                      //msg.Priority = MailPriority.High;//郵件優先級 

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential(Config["NMSSetting:Mail:UserName"], Config["NMSSetting:Mail:Password"]); //這裡要填正確的帳號跟密碼
                client.Host = Config["NMSSetting:Mail:Host"]; //設定smtp Server
                client.Port = int.Parse(Config["NMSSetting:Mail:Port"]); //設定Port
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

        /// <summary>
        /// 送Line Bot通知
        /// </summary>
        /// <param name="lineMessage">訊息</param>
        /// <param name="users">User</param>
        /// <returns></returns>
        public string SendLineMessage(string lineMessage, User[] users)
        {
            return PostResponse<LineBotResponse, LineBotMessage>(Config["NMSSetting:LineBot:BotUrl"], new LineBotMessage()
            {
                Users = users.Where(u => !string.IsNullOrEmpty(u.LineId))
                             .Select(u => u.LineId)
                             .ToArray(),
                Message = lineMessage
            }).Message;
        }

        // 泛型：Post請求
        private Tresult PostResponse<Tresult, TpostData>(string url, TpostData postData) where Tresult : class, new() where TpostData : class, new()
        {
            Tresult result = default(Tresult);

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(postData));
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            httpContent.Headers.ContentType.CharSet = "utf-8";
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;
                    //Newtonsoft.Json
                    string json = JsonConvert.DeserializeObject(s).ToString();
                    result = JsonConvert.DeserializeObject<Tresult>(json);
                }
            }
            return result;
        }
    }
}
