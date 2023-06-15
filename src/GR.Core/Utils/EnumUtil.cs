using System.ComponentModel;
using GR.Extensions;

namespace GR.Utils
{
    /// <summary>
    /// 枚举工具
    /// </summary>
    public class EnumUtil
    {
        /// <summary>
        /// 转换成列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<EnumEntityDto> ToList<T>()
        {
            var list = new List<EnumEntityDto>();
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var model = new EnumEntityDto();
                object[] objArr = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objArr.IsNotEmpty())
                {
                    if (objArr[0] is DescriptionAttribute desc)
                    {
                        model.Desc = desc.Description;
                    }
                }
                model.Code = item.ToString();
                model.Val = Convert.ToInt32(item);
                list.Add(model);
            }
            return list;
        }
    }

    /// <summary>
    /// 枚举实体,值默认是int 类型
    /// </summary>
    public class EnumEntityDto : EnumEntityDto<int>
    {

    }

    /// <summary>
    /// 枚举实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumEntityDto<T>
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public T Val { get; set; }
    }
}
