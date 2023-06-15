using System.ComponentModel;

namespace GR.Extensions
{
    public static partial class Extension
    {
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetDesc(this Enum val)
        {
            var valSrt = val.ToString();
            var field = val.GetType().GetField(valSrt);
            var desc = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return desc == null ? valSrt : ((DescriptionAttribute)desc).Description;
        }
    }
}
