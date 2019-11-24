namespace CodeGenCLI.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return char.ToLowerInvariant(str[0]) + str.Substring(1);
            }

            return str;
        }

        public static string ToSingular(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                if (str.EndsWith("ies"))
                {
                    str = str.Remove(str.Length - 3);
                    str += "y";
                }
                else if (str.EndsWith("s"))
                {
                    str = str.Remove(str.Length - 1);
                }
            }

            return str;
        }

        public static string ToCSharpDataType(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                switch (str)
                {
                    case "uniqueidentifier":
                        return "Guid";
                    case "nvarchar":
                        return "string";
                    case "datetime2":
                        return "DateTime";
                    case "bit":
                        return "bool";
                }
            }

            return str;
        }

        public static string ToTypeScript(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                switch (str)
                {
                    case "bool":
                        return "boolean";
                    case "Guid":
                        return "string";
                    case "DateTime":
                        return "string";
                }
            }

            return str;
        }
    }
}
