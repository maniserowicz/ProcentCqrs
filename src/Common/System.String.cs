namespace System
{
    public static class StringExtensions
    {
        public static string FormatWith(this string @this, params object[] args)
        {
            return string.Format(@this, args);
        }

        public static bool DoesNotStartWith(this string @this, string value)
        {
            return !@this.StartsWith(value);
        }

        public static bool DoesNotContain(this string @this, string value)
        {
            return !@this.Contains(value);
        }

        public static bool IsSomething(this string @this)
        {
            return !string.IsNullOrEmpty(@this);
        }

        public static bool IsNullOrEmpty(this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }

        public static bool HasText(this string @this)
        {
            return @this.IsSomething() && @this.Trim().Length > 0;
        }

        public static bool HasNoText(this string @this)
        {
            return !@this.HasText();
        }
    }
}