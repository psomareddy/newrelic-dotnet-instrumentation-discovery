using NewRelic.Agent.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Tracer.Factories.Diag
{
    public static class ReflectionUtil
    {
        public static void PrintAllProperties(object obj, NewRelic.Agent.Extensions.Logging.ILogger logger)
        {
            BindingFlags flags = BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance;

            PropertyInfo[] props = obj.GetType().GetProperties(flags);
            foreach (PropertyInfo p in props)
            {
                logger.Log(Level.Info, "New Property found- " + p.Name);
                logger.Log(Level.Info, "\tName=" + p.Name);
                logger.Log(Level.Info, "\tType=" + p.PropertyType);
                logger.Log(Level.Info, "\tValue=" + p.GetValue(obj));
                logger.Log(Level.Info, "\tDeclType=" + p.DeclaringType);
                logger.Log(Level.Info, "\tAttributes=" + p.Attributes);
                logger.Log(Level.Info, "\tGetMethod=" + p.GetMethod);
                logger.Log(Level.Info, "---");
                logger.Log(Level.Info, "");
            }
        }

        public static void PrintAllFields (object obj, NewRelic.Agent.Extensions.Logging.ILogger logger)
        {
            BindingFlags flags = BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance;

            FieldInfo[] props = obj.GetType().GetFields(flags);
            foreach (FieldInfo p in props)
            {
                logger.Log(Level.Info, "New Field found- " + p.Name);
                logger.Log(Level.Info, "\tName=" + p.Name);
                logger.Log(Level.Info, "\tType=" + p.FieldType);
                logger.Log(Level.Info, "\tValue=" + p.GetValue(obj));
                logger.Log(Level.Info, "\tDeclType=" + p.DeclaringType);
                logger.Log(Level.Info, "\tAttributes=" + p.Attributes);
                logger.Log(Level.Info, "\tIsStatic=" + p.IsStatic);
                logger.Log(Level.Info, "\tIsPrivate=" + p.IsPrivate);
                logger.Log(Level.Info, "\tIsPublic=" + p.IsPublic);
                logger.Log(Level.Info, "---");
                logger.Log(Level.Info, "");
            }
        }

        public static void PrintAllMethods(object obj, NewRelic.Agent.Extensions.Logging.ILogger logger)
        {
            BindingFlags flags = BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance;

            MethodInfo[] methods = obj.GetType().GetMethods(flags);
            foreach (MethodInfo p in methods)
            {
                if (p.DeclaringType.FullName != "System.Object")
                {
                    logger.Log(Level.Info, "New Method found- " + p.Name);
                    logger.Log(Level.Info, "\tName=" + p.Name);
                    logger.Log(Level.Info, "\tHandle=" + p.MethodHandle);
                    logger.Log(Level.Info, "\tDeclType=" + p.DeclaringType);
                    logger.Log(Level.Info, "\tAttributes=" + p.Attributes);
                    logger.Log(Level.Info, "\tIsStatic=" + p.IsStatic);
                    logger.Log(Level.Info, "\tIsPrivate=" + p.IsPrivate);
                    logger.Log(Level.Info, "\tIsPublic=" + p.IsPublic);
                    logger.Log(Level.Info, "---");
                    logger.Log(Level.Info, "");
                }
            }
        }
        
    }
}
