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

namespace WTG_94239.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistController : WebControllerBaseExtend
    {
        ServerResponseMessage ServerResponseMessageObj = new ServerResponseMessage();
        //  註冊  POST  DefaultUser注册  Privilege 之后改用赋予的 或者 干脆自己新建再db     /api/Regist/Regist  
        [HttpPost]
        [Route("[action]")]
        public ContentResult Regist([FromBody]WebUserRegist account)
        {
            try
            {

                var member_DBContext = new Member_DBContext();
                bool AccountIsExist;
                bool UserNameIsExist;
                bool RegexCheack = true;
             
                //功能分开给予权限   account.Privilege = (int)UserPrivilegeEnum.DefaultUser;  // defaultuser = 4
                //    account.IsDeleted = false;
                
                
               
                var encrypt = new EncryptClass();
                // MD5 Password (大寫)  用來驗證跟之後的持續登入驗證
                account.Md5password = encrypt.MD5Encrypt(account.Password, true);
                string patternAP = "[A-Za-z0-9]{4,16}";
                string patternU = "[A-Za-z0-9\u4e00-\u9fa5]{1,16}";
                Regex regexAP = new Regex(patternAP);
                Regex regexU = new Regex(patternU);

                if (regexAP.IsMatch(account.Account1) == false || regexAP.IsMatch(account.Password) == false || regexU.IsMatch(account.UserName) == false)
                {
                    RegexCheack = false;
                    ServerResponseMessageObj.Set("請再次檢查格式，1 < UserName[中英數] <16 、 4< 帳號[英文.數字] <16 、 4< 密碼[英文.數字] <16 ");
                }
                if (RegexCheack)
                {
                    AccountIsExist = member_DBContext.Account.Any(e => e.Account1 == account.Account1);
                    UserNameIsExist = member_DBContext.MemberInfo.Any(e=>e.Name == account.UserName);
                    if (AccountIsExist == true)
                    {

                        ServerResponseMessageObj.Set("此帳號已存在", ServerResponseMessage.ResultCodeEnum.Fail);
                        return Content(ServerResponseMessageObj);

                    }

                    else if (UserNameIsExist == true)
                    {
                        ServerResponseMessageObj.Set("此UserName已存在", ServerResponseMessage.ResultCodeEnum.Fail);
                        return Content(ServerResponseMessageObj);

                    }
                    else
                    {

                        member_DBContext.Account.Add(account);
                        //新增帳號同時  新增個人Detail
                        var memberInfo = new MemberInfo
                        {
                            Id = account.Id
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
                ServerResponseMessageObj.Set("Something Error : " + ex.Message, ServerResponseMessage.ResultCodeEnum.Error);
                return Content(ServerResponseMessageObj);

            }
        }
    }
}