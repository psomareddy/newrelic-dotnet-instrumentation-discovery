using NewRelic.Agent.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Tracer.Factories.Diag
{
    public class StackTraceUtil
    {
        public static string CurrentStackTrace()
        {
            var l_StackLog = new System.Text.StringBuilder();
            var l_CurrentStack = new System.Diagnostics.StackTrace(true);
            // the true value is used to include source file info

            for (Int32 x = 0; x < l_CurrentStack.FrameCount; ++x)
            {
                var l_MethodCall = l_CurrentStack.GetFrame(x);
                l_StackLog.AppendLine(MethodCallLog(l_MethodCall));
            }
            return l_StackLog.ToString();
        }

        private static String MethodCallLog(System.Diagnostics.StackFrame p_MethodCall)
        {
            System.Text.StringBuilder l_MethodCallLog =
                        new System.Text.StringBuilder();

            var l_Method = p_MethodCall.GetMethod();
            l_MethodCallLog.Append(l_Method.DeclaringType.ToString());
            l_MethodCallLog.Append(".");
            l_MethodCallLog.Append(p_MethodCall.GetMethod().Name);

            var l_MethodParameters = l_Method.GetParameters();
            l_MethodCallLog.Append("(");
            for (Int32 x = 0; x < l_MethodParameters.Length; ++x)
            {
                if (x > 0)
                    l_MethodCallLog.Append(", ");
                var l_MethodParameter = l_MethodParameters[x];
                l_MethodCallLog.Append(l_MethodParameter.ParameterType.Name);
                l_MethodCallLog.Append(" ");
                l_MethodCallLog.Append(l_MethodParameter.Name);
            }
            l_MethodCallLog.Append(")");

            var l_SourceFileName = p_MethodCall.GetFileName();
            if (!String.IsNullOrEmpty(l_SourceFileName))
            {
                l_MethodCallLog.Append(" in ");
                l_MethodCallLog.Append(l_SourceFileName);
                l_MethodCallLog.Append(": line ");
                l_MethodCallLog.Append(p_MethodCall.GetFileLineNumber().ToString());
            }

            return l_MethodCallLog.ToString();
        }

    }
}
