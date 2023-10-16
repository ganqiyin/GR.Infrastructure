using System.Text.Json;

namespace GR.Json
{
    /// <summary>
    /// 转小写
    /// </summary>
    public class TextJsonLowercasePolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name.ToLower();
        }
    }
}
