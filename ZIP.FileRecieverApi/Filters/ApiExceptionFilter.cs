using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace ZIP.FileRecieverApi.Filters {
    public class ApiExceptionFilterAttribute: ExceptionFilterAttribute {
        public override void OnException(ExceptionContext context) {
            ApiError apiError = null;
            if (context.Exception is ZipException) {
                // handle explicit 'known' API errors
                var ex = context.Exception as ApiException;
                context.Exception = null;
                apiError = new ApiError(ex.Message);

                context.HttpContext.Response.StatusCode = ex.StatusCode;
            }
            else if (context.Exception is UnauthorizedAccessException) {
                apiError = new ApiError("Unauthorized Access");
                context.HttpContext.Response.StatusCode = 401;

                // handle logging here
            }
            else {
                // Unhandled errors
#if !DEBUG
                var msg = "An unhandled error occurred.";                
                string stack = null;
#else
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
#endif

                apiError = new ApiError(msg);
                apiError.detail = stack;

                context.HttpContext.Response.StatusCode = 500;

                // handle logging here
            }

            // always return a JSON result
            context.Result = new JsonResult(apiError);

            base.OnException(context);
        }
    }

    public class ApiException: Exception {
        public int StatusCode { get; set; }


        public ApiException(string message,
                            int statusCode = 500) :
            base(message) {
            StatusCode = statusCode;
        }
        public ApiException(Exception ex, int statusCode = 500) : base(ex.Message) {
            StatusCode = statusCode;
        }
    }

    public class ApiError {
        public string message { get; set; }
        public bool isError { get; set; }
        public string detail { get; set; }

        public ApiError(string message) {
            this.message = message;
            isError = true;
        }

        public ApiError(ModelStateDictionary modelState) {
            this.isError = true;
            if (modelState != null && modelState.Any(m => m.Value.Errors.Count > 0)) {
                message = "Please correct the specified errors and try again.";
            }
        }
    }
}
