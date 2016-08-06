namespace WebApi
{
    using System;
    using System.Diagnostics;
    using PostSharp.Aspects;

    /// <summary>
    /// This class require you to install PostSharp (free express version would be good enough for CSe136)
    /// </summary>
    [Serializable]
    class ConsoleWriteLineAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            LogInfoNow("OnEntry");
            LogInfoNow(args.Method.DeclaringType.Name);
            LogInfoNow(args.Method.Name);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            LogInfoNow("OnSuccess");
            LogInfoNow(args.Method.DeclaringType.Name);
            LogInfoNow(args.Method.Name);
            LogInfoNow(args.ReturnValue.ToString());
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            LogInfoNow(args.Method.DeclaringType.Name);
            LogInfoNow(args.Method.Name);
        }

        private void LogInfoNow(string info)
        {
            Debug.WriteLine("----> " + info);
        }
    }
}