using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClassLibrary1;
using WTG_94239.Model.Model1;
using WTG_94239.Model.EF;
using System.Net.Http;
using Newtonsoft.Json;

namespace WTG_94239.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : WebControllerBaseExtend
    {
        [HttpPost("[action]")]
        public ContentResult DefaultUserLogin([FromBody]WebUserLogin webUserLogin)
        {
            //目前只有讀取單個logindb  不需要async
            try
            {

                ServerResponseMessage serverResponseMessageObj = new ServerResponseMessage();
                var Encrypt = new EncryptClass();
                Member_DBContext loginDB = new Member_DBContext();
                bool accountExist = false;
                HttpResponseMessage response = new HttpResponseMessage();
                accountExist = loginDB.Account.Any(e => e.Account1 == webUserLogin.Account);
                if (accountExist)
                {
                    Account account = loginDB.Account.FirstOrDefault(e => e.Account1 == webUserLogin.Account);
                    MemberInfo memberInfo = loginDB.MemberInfo.FirstOrDefault(e=>e.Id == account.Id);
                    if (account.Md5password == Encrypt.MD5Encrypt(webUserLogin.Password, true))
                    {
                        // add  if banned  or  isdeleted
                        if (account.IsBanned == true)
                        {
                            serverResponseMessageObj.Set("此账号目前禁用中。This Account was banned.");
                            return Content(serverResponseMessageObj);
                        }


                        // .NET版  重做 .Core version
                        // 將 Password 做 MD5   

                        account.Password = "";  //将Password消除。 Cookies留下MD5password之后快速认证用就好。
                      
                        string UserJson = JsonConvert.SerializeObject(account);
                        CookieOptions cookieOptions = new CookieOptions
                        {
                            Domain = Request.Host.Host
                        };
                        if (webUserLogin.SetCookie30Day)
                        {
                            cookieOptions.Expires = DateTime.Now.AddDays(30);
                        }
                        //可被端存取的
                        Response.Cookies.Append("IsLogin", "true");
                        Response.Cookies.Append("UserName", memberInfo.Name);


                        //  cookieOptions.HttpOnly = true;
                        // 使用後 前端無法讀取
                        Response.Cookies.Append("User", UserJson, cookieOptions);

                        serverResponseMessageObj.Set("成功登入。Login Successful");
                        
                        //Add Seesion
                        HttpContext.Session.SetString("Logined-UserID",account.Id.ToString());
                        // return Html
                        return Content(serverResponseMessageObj);

                        // Success
                    }
                }

                serverResponseMessageObj.Set("账号或密码错误", ServerResponseMessage.ResultCodeEnum.BadRequest);
                return Content(serverResponseMessageObj);
            }


            catch (Exception error)
            {
                ServerResponseMessage serverResponseMessageObj = new ServerResponseMessage();
                serverResponseMessageObj.Set("發生了未知錯誤：" + error.Message + "。請再次確認請求 或者 通知管理员 (´･_･`)", ServerResponseMessage.ResultCodeEnum.BadRequest);
                return Content(serverResponseMessageObj);
            }
        }


        [HttpPost("[action]")]
        public ContentResult Logout()
        {
            try
            {
                ServerResponseMessage serverResponseMessage = new ServerResponseMessage();
                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                //Clean Cookies
                Response.Cookies.Delete("UserName");
                Response.Cookies.Delete("User", cookieOptions);
                Response.Cookies.Append("IsLogin", "false");

                //Clean Session
                HttpContext.Session.Remove("Logined-UserID");

                serverResponseMessage.Content = "成功登出。";
                serverResponseMessage.ResultCode = 0;
                return Content(serverResponseMessage);
            }

            catch (Exception ex)
            {
                return Content("發生了未知錯誤：" + ex.Message + "。請再次確認請求 或者 通知管理员 (´･_･`)");
            }
        }

        /*   用Seesion 目前不用了
        [HttpPost("[action]")]
        public ContentResult QuickLoginCheck([FromBody] Account account)
        {
            try
            {

                var forWebApiFunctions = new ForWebApiFunctions(account);
                if (forWebApiFunctions.VertifyUser())
                {
                    return Content("true");
                }
                else
                {
                    return Content("false");
                }
            }
            catch (Exception ex)
            {
                return ContentError(ex, "SomethingError");
            }
        }
        */






    }
}