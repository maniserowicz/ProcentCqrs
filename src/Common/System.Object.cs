using System.ComponentModel;
using System.Web.Script.Serialization;

namespace System
{
    public static class ObjectExtensions
    {
        public static string ToJSON(this object @this)
        {
            return new JavaScriptSerializer().Serialize(@this);
        }

        /// <summary>
        /// Checks if any of given objects is equal to this instance.
        /// </summary>
        public static bool IsAnyOf<T>(this T @this, params T[] objects)
        {
            return Array.IndexOf(objects, @this) >= 0;
        }

        /// <summary>
        /// Checks if all of given objects are different than this instance.
        /// </summary>
        public static bool IsNoneOf<T>(this T @this, params T[] objects)
        {
            return !IsAnyOf(@this, objects);
        }
    }
}