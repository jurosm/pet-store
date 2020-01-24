using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetStore.API.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Filters.GlobalFilters
{
    public class ExceptionActionFilter : ActionFilterAttribute, IExceptionFilter
    {
        public ExceptionActionFilter() { }
        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {

                context.HttpContext.Response.ContentType = "application/json";

                switch (context.Exception)
                {
                    case Auth0.Core.Exceptions.ApiException _:
                        Auth0.Core.Exceptions.ApiException res = (context.Exception as Auth0.Core.Exceptions.ApiException);
                        context.HttpContext.Response.StatusCode = res.ApiError.StatusCode; 
                        context.Result = new JsonResult(new MessageResponse { Message = res.ApiError.Message });
                        break;

                    case FileNotFoundException _:
                        context.HttpContext.Response.StatusCode = 404;
                        context.Result = new JsonResult(new MessageResponse { Message = "Item not found!" });
                        break;

                    default:
                        context.Result = new StatusCodeResult(500);
                        break;
                }

            }
        }
    }
}
