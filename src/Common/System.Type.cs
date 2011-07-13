namespace System
{
    public static class TypeExtensions
    {
        public static bool AssignableTo(this Type @this, Type other)
        {
            return other.IsAssignableFrom(@this);
        }

        public static bool AssignableTo<T>(this Type @this)
        {
            return @this.AssignableTo(typeof(T));
        }

        public static bool CanBeInstantiated(this Type @this)
        {
            return @this.IsClass && !@this.IsAbstract;
        }

        public static bool Is<TOther>(this Type @this)
        {
            return @this == (typeof (TOther));
        }
 
        public static bool IsNot<TOther>(this Type @this)
        {
            return !@this.Is<TOther>();
        }
    }
}