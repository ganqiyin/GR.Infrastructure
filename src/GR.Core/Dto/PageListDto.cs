namespace GR.Dto
{
    /// <summary>
    /// 分页列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class PageListDto<T> : GetListWithPagingRequest
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items"></param>
        /// <param name="recordCount"></param>
        /// <param name="request"></param>
        protected PageListDto(IList<T> items, int recordCount, GetListWithPagingRequest request)
        {
            Items.AddRange(items);
            RecordCount = recordCount;
            PageIndex = request.PageIndex;
            PageSize = request.PageSize;
        }

        /// <summary>
        /// 记录
        /// </summary>
        public List<T> Items { get; private set; } = new List<T>();

        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount { get; private set; }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="items"></param>
        /// <param name="recordCount"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static PageListDto<T> Create(IList<T> items, int recordCount, GetListWithPagingRequest request)
        {
            return new PageListDto<T>(items, recordCount, request);
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="items"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PageListDto<T> Create(IList<T> items, int recordCount, int pageIndex, int pageSize)
        {
            return new PageListDto<T>(items, recordCount, new GetListWithPagingRequest { PageIndex = pageIndex, PageSize = pageSize });
        }
    }
}
