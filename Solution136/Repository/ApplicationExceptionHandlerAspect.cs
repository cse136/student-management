using System;

using PostSharp.Aspects;

namespace Repository
{
    using System.Diagnostics;
    using System.Reflection;

    [Serializable]
    class ApplicationExceptionHandlerAspect : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            // now, you can log the error 
            LogInfoNow("OnException");
            LogInfoNow(args.Exception.ToString());
            LogInfoNow(args.Method.DeclaringType.Name);
            LogInfoNow(args.Method.Name);
        }

        public override Type GetExceptionType(MethodBase targetMethod)
        {
            // since we are only using this class to capture SQL exception, we simply
            // need to return this SqlException for the GetExceptionType method
            return typeof(System.Data.SqlClient.SqlException);
        }

        private void LogInfoNow(string info)
        {
            // for now, I am just writing to the debug screen.
            Trace.WriteLine(">>>>>>>>>>" + info);
        }
    }
}
