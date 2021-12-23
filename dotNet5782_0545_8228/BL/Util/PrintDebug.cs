using System;

namespace UTIL
{
    class PrintDebug
    {
        /// <summary>
        /// Helper util method which will print the ToString of an object if it isn't null.
        /// If it is null it will return null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static String ToStringOrNull<T>(T t) => t != null ? t.ToString() : "null";
    }
}
