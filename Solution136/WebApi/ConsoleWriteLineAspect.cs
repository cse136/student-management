namespace WebApi
{
    using System;
    using System.Diagnostics;
    using PostSharp.Aspects;

    /// <summary>
    /// This class require you to install PostSharp (free express version would be good enough for CSe136)
    /// </summary>
    [Serializable]
    public class ConsoleWriteLineAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            this.LogInfoNow("OnEntry");
            this.LogInfoNow(args.Method.DeclaringType.Name);
            this.LogInfoNow(args.Method.Name);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            this.LogInfoNow("OnSuccess");
            this.LogInfoNow(args.Method.DeclaringType.Name);
            this.LogInfoNow(args.Method.Name);
            this.LogInfoNow(args.ReturnValue.ToString());
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            this.LogInfoNow(args.Method.DeclaringType.Name);
            this.LogInfoNow(args.Method.Name);
        }

        private void LogInfoNow(string info)
        {
            Debug.WriteLine("----> " + info);
        }
    }
}