namespace Repository
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    using PostSharp.Aspects;
    
    [Serializable]
    public class ApplicationExceptionHandlerAspect : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            // now, you can log the error 
            this.LogInfoNow("OnException");
            this.LogInfoNow(args.Exception.ToString());
            this.LogInfoNow(args.Method.DeclaringType.Name);
            this.LogInfoNow(args.Method.Name);
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
