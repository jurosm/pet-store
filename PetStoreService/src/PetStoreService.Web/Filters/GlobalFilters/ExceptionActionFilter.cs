using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetStoreService.Application.Exceptions;
using PetStoreService.Application.Models.Response;
using System;

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
                        context.Result = new JsonResult(new ErrorResponse { Message = res.Message });
                        break;

                    case BadRequestException exception:
                        context.HttpContext.Response.StatusCode = 400;
                        context.Result = new JsonResult(new ErrorResponse { Message = exception.Message, ErrorCode = exception.ErrorCode });
                        break;

                    case NotFoundException exception:
                        context.HttpContext.Response.StatusCode = 404;
                        context.Result = new JsonResult(new ErrorResponse { Message = exception.Message });
                        break;

                    default:
                        context.Result = new StatusCodeResult(500);
                        Console.WriteLine(context.Exception.StackTrace);
                        break;
                }
            }
        }
    }
}