using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using CoreArk.Packages.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreArk.Packages.Web
{
    [Route("error")]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [HttpGet, HttpPost, HttpPut, HttpDelete]
        public virtual IActionResult Index()
        {
            // fetch the exception from the context
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = exceptionHandlerFeature?.Error;

            // exception is a functional exception. It is safe to give back all information that comes within that exception
            var statusCode = (int) GetStatusCode(error);

            // write the error to the log file

            _logger.LogError(new EventId(statusCode, "INTERNAL_SERVER_ERROR (functional)"), error,
                "There has been an functional server error.");

            // render a json response
            Response.StatusCode = statusCode;
            return Json(new
            {
                Code = statusCode,
                Type = error?.GetType().FullName,
                error.Message
            });
        }

        private HttpStatusCode GetStatusCode(Exception functionalException)
        {
            return functionalException switch
            {
                NotFoundException _ => HttpStatusCode.NotFound,
                ArgumentException _ => HttpStatusCode.BadRequest,
                BadRequestException _ => HttpStatusCode.BadRequest,
                NotSupportedException _ => HttpStatusCode.BadRequest,
                ValidationException _ => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
        }


    }
}