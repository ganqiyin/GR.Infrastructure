namespace GR.Dto
{
    /// <summary>
    /// 下拉框选项
    /// </summary>
    public class DropListItemDto : DropListItemDto<string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    /// <summary>
    /// 下拉框选项
    /// </summary>
    public class DropListItemDto<TKey>
    {
        /// <summary>
        /// 值
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name { get; set; }
    }
}
