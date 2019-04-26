using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClassLibrary1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WTG_94239.Model.EF;
using WTG_94239.Model.Model1;
using NLog;

namespace WTG_94239.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistController : WebControllerBaseExtend
    {
        private Logger _logger;
        ServerResponseMessage ServerResponseMessageObj = new ServerResponseMessage();



        //  註冊  POST  DefaultUser注册  Privilege 之后改用赋予的 或者 干脆自己新建再db     /api/Regist/Regist  
        [HttpPost]
        [Route("[action]")]
        public ContentResult Regist([FromBody]WebRegist account)
        {
            try
            {

                var member_DBContext = new Member_DBContext();
                bool accountIsExist;
                bool userNameIsExist;
                bool emailIsExist;
                bool regexCheack = true;
               
                //功能分开给予权限   account.Privilege = (int)UserPrivilegeEnum.DefaultUser;  // defaultuser = 4
                //    account.IsDeleted = false;
                
                
               
                var encrypt = new EncryptClass();
                // MD5 Password (大寫)  用來驗證跟之後的持續登入驗證
                account.Md5password = encrypt.MD5Encrypt(account.Password, true);
                account.IsBanned = false;
                string patternAP = "[A-Za-z0-9]{4,16}";
                string patternU = "[A-Za-z0-9\u4e00-\u9fa5]{1,16}";
                string pattenEmail = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                     @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
                Regex regexAP = new Regex(patternAP);
                Regex regexU = new Regex(patternU);
                Regex regexEmail = new Regex(pattenEmail,RegexOptions.IgnoreCase);

                if (regexAP.IsMatch(account.Account1) == false || regexAP.IsMatch(account.Password) == false || regexU.IsMatch(account.SiteUserName) == false || regexEmail.IsMatch(account.EMail)==false)
                {
                    regexCheack = false;
                    ServerResponseMessageObj.Set("請再次檢查格式，1 < UserName[中英數] <16 、 4< 帳號[英文.數字] <16 、 4< 密碼[英文.數字] <16 ");
                }
                if (regexCheack)
                {
                    accountIsExist = member_DBContext.Account.Any(e => e.Account1 == account.Account1);
                    if (accountIsExist)
                    {

                        ServerResponseMessageObj.Set("此帳號已存在", ServerResponseMessage.ResultCodeEnum.Fail);
                        return Content(ServerResponseMessageObj);

                    }
                    userNameIsExist = member_DBContext.Account.Any(e => e.SiteUserName == account.SiteUserName);
                    if (userNameIsExist)
                    {
                        ServerResponseMessageObj.Set("此UserName已存在", ServerResponseMessage.ResultCodeEnum.Fail);
                        return Content(ServerResponseMessageObj);
                    }

                    emailIsExist = member_DBContext.MemberInfo.Any(e => e.EMail == account.EMail);
                    if (emailIsExist)
                    {
                        ServerResponseMessageObj.Set("此Email已存在", ServerResponseMessage.ResultCodeEnum.Fail);
                        return Content(ServerResponseMessageObj);
                    }
                    else
                    {
                        member_DBContext.Account.Add(account);
                        //新增帳號同時  新增個人Detail
                        var memberInfo = new MemberInfo
                        {
                            Id = account.Id,
                            EMail = account.EMail
                        };
                        member_DBContext.MemberInfo.Add(memberInfo);
                        member_DBContext.SaveChanges();
                        ServerResponseMessageObj.Set("帳號成功建立");
                        return Content(ServerResponseMessageObj);
                    }
                }
                else
                {
                    return Content(ServerResponseMessageObj);
                }
            }
            catch (Exception ex)
            {
                ServerResponseMessageObj.Set("Something Error : " + ex.InnerException.Message, ServerResponseMessage.ResultCodeEnum.Error);
                return Content(ServerResponseMessageObj);
            }
        }
    }
}