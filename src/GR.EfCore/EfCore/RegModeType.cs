namespace GR.EfCore
{
    /// <summary>
    /// 注册方式
    /// </summary>
    public enum AddDbContextModeType
    {
        /// <summary>
        /// 用于注册非常独立和短暂的 DbContext 对象，这些对象通常只是一个封装的数据服务，重点是简单而且只用一次。
        /// </summary>
        /// <remarks>
        /// 这个方法创建 DbContext 的实例时，每次都会使用新实例，而不是创建对象的池。每当服务需要 DbContext 实例时，该方法都会创建新的实例。通常用于在应用程序启动时注册 DbContext 并使用 Scoped 生命周期进行注入。
        /// </remarks>
        AddDbContext,
        /// <summary>
        /// 用于带有 DbContext 实例的有限、可重用池的注册
        /// </summary>
        /// <remarks>
        /// 如果 DbContext 经常被请求，那么它很可能成为系统的瓶颈，因为开启太多的 DbContext 实例会非常慢，而且创建太多的 DbContext 实例可能会变得昂贵。AddDbContextPool会帮助开发人员在我们的应用程序中实现对 DbContext 实例的有限重用和更快速的创建 DbContext 实例。在每个上下文中的所有 DbContext 实例之间共享资源。这是最常见的方法，用于设置 DbContext 实例，因为它适用于 MVC、WebAPI 和前后端分离的架构中。
        /// </remarks>
        AddDbContextPool,
        /// <summary>
        /// 用于在运行时创建独立的、跨越多个应用程序层级的 DbContext 实例
        /// </summary>
        /// <remarks>
        ///  如果我们希望让具体的实例创建者管理 DbContext 实例的状态，那么该方法就会比较有用。使用 AddDbContextFactory，运行时中获取 IDbContextFactory《TContext》 客户端便可以使用所需的 DbContext 实例来插入 CreateDbContext/Async 中执行的逻辑。
        /// </remarks>
        AddDbContextFactory
    }
}
