using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetStore.API.Exceptions.Services.Order;
using PetStore.API.Models.Response;
using System;
using System.IO;

namespace PetStore.API.Filters.GlobalFilters
{
    public class ExceptionActionFilter : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                context.HttpContext.Response.ContentType = "application/json";

                switch (context.Exception)
                {
                    case Auth0.Core.Exceptions.ApiException _:
                        Auth0.Core.Exceptions.ApiException res = context.Exception as Auth0.Core.Exceptions.ApiException;
                        context.HttpContext.Response.StatusCode = 401;
                        context.Result = new JsonResult(new MessageResponse { Message = res.Message });
                        break;

                    case FileNotFoundException _:
                        context.HttpContext.Response.StatusCode = 404;
                        context.Result = new JsonResult(new MessageResponse { Message = "Item not found!" });
                        break;

                    case MessageException _:
                        context.HttpContext.Response.StatusCode = 400;
                        MessageException e = context.Exception as MessageException;
                        context.Result = new JsonResult(new MessageResponse { Message = e.Message });
                        break;

                    default:
                        Console.WriteLine(context.Exception.Message);
                        context.Result = new StatusCodeResult(500);
                        break;
                }
            }
        }
    }
}
