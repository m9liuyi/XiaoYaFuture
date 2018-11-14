using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace NiceNet.Filters
{
    public class CustomExceptionFilter : IAutofacExceptionFilter
    {
        public Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}