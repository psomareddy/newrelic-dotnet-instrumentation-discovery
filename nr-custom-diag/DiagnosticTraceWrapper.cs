using NewRelic.Agent.Extensions.Logging;
using NewRelic.Agent.Extensions.Providers.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Tracer.Factories.Diag
{
    public class DiagnosticTraceWrapper : IWrapper
    {
        public bool IsTransactionRequired => false;

        public CanWrapResponse CanWrap(InstrumentedMethodInfo instrumentedMethodInfo)
        {
            bool canWrapMethod = false;
            string requestedWrapper = instrumentedMethodInfo.RequestedWrapperName;
            if (requestedWrapper == "Custom.Tracer.Factories.DiagTracerFactory")
            {
                canWrapMethod = true;
            }
            return new CanWrapResponse(canWrapMethod);
        }

        public AfterWrappedMethodDelegate BeforeWrappedMethod(InstrumentedMethodCall instrumentedMethodCall, IAgent agent, ITransaction transaction)
        {
            var logger = agent.Logger;

            Method method = instrumentedMethodCall.MethodCall.Method;
            logger.Log(Level.Info, "Printing Diagnostic Information for method call: " + method.MethodName);
            logger.Log(Level.Info, "Method parameters: " + method.ParameterTypeNames);

            object[] arguments = instrumentedMethodCall.MethodCall.MethodArguments;
            foreach (var a in arguments)
            {
                logger.Log(Level.Info, "\n");
                logger.Log(Level.Info, "New Argument: " + a.ToString());
                logger.Log(Level.Info, "Argument type: " + a.GetType());
                logger.Log(Level.Info, "Discovering argument information");
                ReflectionUtil.PrintAllProperties(a, logger);
                ReflectionUtil.PrintAllFields(a, logger);
                ReflectionUtil.PrintAllMethods(a, logger);
                logger.Log(Level.Info, "Completed argument discovery for " + a.ToString());
            }

            object invocationObject = instrumentedMethodCall.MethodCall.InvocationTarget;
            if (invocationObject != null)
            {
                ReflectionUtil.PrintAllProperties(invocationObject, logger);
                ReflectionUtil.PrintAllFields(invocationObject, logger);
                ReflectionUtil.PrintAllMethods(invocationObject, logger);
            }
            else
            {
                logger.Log(Level.Info, "Invocation object is NULL for method: " + method.ToString());
            }

            logger.Log(Level.Info, "Printing Stack Trace");
            logger.Log(Level.Info, StackTraceUtil.CurrentStackTrace());
            
            return Delegates.GetDelegateFor(onSuccess: () => {});
        }

    }
}
