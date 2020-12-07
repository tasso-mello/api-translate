namespace domain.translate.Utilities
{
    using Newtonsoft.Json;
    using System;
    using System.Reflection;

    public static class Extensions
    {
        #region Model Conversions

        #region Example
        //public static Models.Example ToModelExample(this data.translate.Entities.Example example)
        //{
        //    return new Models.Example
        //    {
        //        IdExample = example.IdExample,
        //        NameExample = example.NameExample
        //    };
        //}

        //public static data.translate.Entities.Example ToEntityExample(this Models.Example example)
        //{
        //    return new data.translate.Entities.Example
        //    {
        //        IdExample = example.IdExample,
        //        NameExample = example.NameExample
        //    };
        //}

        #endregion

        #endregion

        #region String Conversions

        public static string ToStringObject(this object value, string property)
        {
            PropertyInfo pi = value.GetType().GetProperty(property);
            return (string)(pi.GetValue(value, null));
        }

        public static object ToJasonObject(this string value)
        {
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(value.Replace('=', ':')));
        }

        public static string ToSerializableObject(this string value, string oldString, string newString)
        {
            return value.Replace(oldString, newString);
        }

        public static string ToSplitSecontObject(this string value, string spliter)
        {
            try
            {
                return value.Split(spliter)[1];
            }
            catch (Exception)
            {
                return value.Split(spliter)[0];
            }

        }

        #endregion
    }
}
