using System;

namespace UTIL
{
    class PrintDebug
    {
        public static String ToStringOrNull<T>(T t) => t != null ? t.ToString() : "null";
    }
}
