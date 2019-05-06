using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WTG_94239.Model.EF;
using NLog;

namespace WTG_94239.Model.Model1
{
    public class Model1
    {
        /// <summary>
        /// 這裡調整後端統一使用的UrlHost
        /// </summary>
        public string UrlHost = "http://localhost:5001";
        //note core 的iis 建置 default port 在 外面的 D:\SamuriWtg\Wtg\WebTextGameClient\CoreWebClient\Properties\lauchSetting.json 改
    }
    public class ChatMessage
    {
        [JsonProperty("Task")]
        public string Task { get; set; }
        [JsonProperty("UserName")]
        public string UserName { get; set; }
        [JsonProperty("Message")]
        public string Message { get; set; }
    }
   



    public class WebUserLogin
    {

        public string Account { get; set; }
        public string Password { get; set; }

        public bool SetCookie30Day { get; set; } = false;

    }
    public class WebRegist :Account
    {
        public string EMail { get; set; }
    }

    public class ServerResponseMessage
    {
        public enum ResultCodeEnum
        {
            Success,
            Fail,
            BadRequest,
            Error,
        }


        public string Content { get; set; } = "Something Error，发生错误。";
        public int ResultCode { get; set; } = (int)ResultCodeEnum.Error;

        /// <summary>
        /// 往前端的回傳
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="resultCode">(預設)Success : 0 | Fail = 1 | BadRequest = 2 | Error = 3</param>
        public void Set(string msg, ResultCodeEnum resultCode = ResultCodeEnum.Success)
        {
            Content = msg;
            ResultCode = (int)resultCode;
         
        }
    }



    /// <summary>
    /// 繼承複寫ControllerBase
    /// </summary>
    public class WebControllerBaseExtend : ControllerBase
    {
      

        /// <summary>
        /// 传ServerResponseMessage会回传json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public ContentResult Content(ServerResponseMessage obj)
        {
            string jsonresult = JsonConvert.SerializeObject(obj);

            return Content(jsonresult);
        }


        /// <summary>
        /// 直接丟Exception 參數
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="ErrorFunctionDesc"></param>
        /// <returns>會回傳exception.Message</returns>
        public ContentResult ContentError(Exception exception, string ErrorFunctionDesc = "")
        {
            var obj = new ServerResponseMessage();
            if (obj == null)
            {
                obj.Set("Something Error : " + exception.Message + "出錯了(´；ω；｀)" + "出错位置或资讯 : " + ErrorFunctionDesc, ServerResponseMessage.ResultCodeEnum.Error);
            }
            return Content(obj);
        }

    }



    public class ForumSelectClass
    {
        public string TopicName { set; get; }
        /// <summary>
        /// ContentTitlePart (關鍵字搜尋)
        /// </summary>
        public string ContentTitlePart { set; get; }
        /// <summary>
        /// 依作者[發文者]搜尋
        /// </summary>
        public string UserName { set; get; }
    }

}
