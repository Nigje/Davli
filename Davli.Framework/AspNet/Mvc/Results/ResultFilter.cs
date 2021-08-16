using Microsoft.AspNetCore.Mvc.Filters;
using Davli.Framework.DI;

namespace Davli.Framework.AspNet.Mvc.Results
{
    public class ResultFilter : IResultFilter, ITransientLifetime
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {

        }
    }
}
