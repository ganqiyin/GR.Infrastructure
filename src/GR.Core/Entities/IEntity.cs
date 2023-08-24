namespace GR.Entities
{
    /// <summary>
    /// 默认TKey 类型为 long 类型
    /// </summary>
    public interface IEntity : IEntity<long>
    {
    }

    /// <summary>
    /// 定义基本实体类型的接口。系统中的所有实体都必须实现该接口。
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 此实体的唯一标识符。
        /// </summary>
        TKey Id { get; set; }
    }
}
