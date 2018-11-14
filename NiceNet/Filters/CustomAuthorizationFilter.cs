using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;

namespace NiceNet.Filters
{
    public class CustomAuthorizationFilter : IAutofacAuthorizationFilter
    {
        public Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}