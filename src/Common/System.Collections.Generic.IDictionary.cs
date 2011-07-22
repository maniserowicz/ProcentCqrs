namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        public static TValue ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key)
        {
            TValue result = default(TValue);

            if (@this == null)
            {
                return result;
            }

            @this.TryGetValue(key, out result);

            return result;
        }
    }
}