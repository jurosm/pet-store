using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetStoreService.Application.Exceptions.Services.Order;
using PetStoreService.Application.Models.Response;
using System;
using System.IO;

namespace PetStoreService.Web.Filters.GlobalFilters
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

                    case FileNotFoundException exception:
                        context.HttpContext.Response.StatusCode = 404;
                        Console.WriteLine(exception.Message);
                        context.Result = new JsonResult(new MessageResponse { Message = "Item not found!" });
                        break;

                    case MessageException _:
                        context.HttpContext.Response.StatusCode = 400;
                        MessageException e = context.Exception as MessageException;
                        context.Result = new JsonResult(new MessageResponse { Message = e.Message });
                        break;

                    default:
                        context.Result = new StatusCodeResult(500);
                        break;
                }
            }
        }
    }
}