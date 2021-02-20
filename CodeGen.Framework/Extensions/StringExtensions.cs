namespace CodeGen.Framework.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return char.ToLower(str[0]) + str.Substring(1);
            }

            return str;
        }

        public static string ToPlural(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                if (str.EndsWith("y"))
                {
                    str = str.Remove(str.Length - 1);
                    str += "ies";
                }
                else
                {
                    str += "s";
                }
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
                    case "decimal":
                        return "double";
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

        public static string ToGraphQL(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                switch (str)
                {
                    case "string":
                        return "StringGraphType";
                    case "int":
                        return "IntGraphType";
                    case "float":
                    case "double":
                        return "FloatGraphType";
                    case "bool":
                        return "BooleanGraphType";
                    case "Guid":
                        return "IdGraphType";
                    case "Date":
                        return "DateGraphType";
                    case "DateTime":
                        return "DateTimeGraphType";
                }
            }

            return str;
        }
    }
}
