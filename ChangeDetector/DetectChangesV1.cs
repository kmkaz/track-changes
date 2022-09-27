namespace ChangeDetector
{
    public static class DetectChangesV1
    {
        public static bool IsChanged(Object obj1, Object obj2, string[] fields)
        {
            foreach (var field in fields)
            {
                var val1 = GetPropertyValue(obj1, field) ?? "";
                var val2 = GetPropertyValue(obj2, field);
                if (val1 == null && val2 == null) return false;

                if (val1 != null && val2 != null && !val1.Equals(val2)) return true;
            }
            return false;
        }

        private static object? GetPropertyValue(object src, string propName)
        {
            if (src == null) return null;
            if (propName == null) return null;

            if (propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }
    }
}
