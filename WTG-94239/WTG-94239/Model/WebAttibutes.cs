using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WTG_94239.Model.WebAttributes
{
    public class WebAttibutes 
    {
        
    }
    public class CheckLoginAttibute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
        public void OnActionExecuting(ActionExecutingContext context) {

        }
    }
}
