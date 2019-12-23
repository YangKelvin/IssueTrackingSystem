using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTrackingSystemApi.Models
{
    public class LineBotMessage
    {
        public string[] Users { get; set; }

        public string Message { get; set; }
    }

    public class LineBotResponse
    {
        public string Message { get; set; }
    }
}
