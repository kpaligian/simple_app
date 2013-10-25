using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Simple.Web.Windsor.Owin.Pipeline
{
    using AppFunc = System.Func<IDictionary<string, object>, Task>;

    public class CustomTracer
    {
        private readonly AppFunc _next;

        public CustomTracer(AppFunc next)
        {
            _next = next;
        }

        public Task Invoke(IDictionary<string, object> env)
        {
            var requestPath = (string)env["owin.RequestPath"];
            
            //get sessionid from cookie
            //get data from redis
            //store as Account in the request pipeline (container, i.e. windsor) 
            //e.g IAccountInfo Can(string)

            Trace.WriteLine(string.Format("Received request for: {0}", requestPath));

            // We must pass-through onto the next module
            return _next(env);
        }
    }
}