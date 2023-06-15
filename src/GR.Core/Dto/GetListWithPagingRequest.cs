namespace GR.Dto
{
    /// <summary>
    /// 分页请求
    /// </summary>
    public class GetListWithPagingRequest
    {
        private int _pageIndex;

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (_pageIndex <= 0)
                    _pageIndex = 1;
                return _pageIndex;
            }
            set
            {
                _pageIndex = value;
            }
        }

        private int _pageSize;

        /// <summary>
        /// 每页显示记录数,默认20，最大200
        /// </summary>
        public int PageSize
        {
            get
            {
                if (_pageSize <= 0)
                    _pageSize = 20;
                if (_pageSize > 200)
                {
                    _pageSize = 200;
                }
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
    }
}
