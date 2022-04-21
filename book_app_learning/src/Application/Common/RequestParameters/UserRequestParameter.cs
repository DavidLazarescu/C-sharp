namespace Application.Common.RequestParameters
{
    public class UserRequestParameter
    {
        private const int _maxPageSize = 50;
        
        public int PageNumber { get; set; }
        
        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = (value <= _maxPageSize) ? value : _maxPageSize;
            }
        }

        public bool IsValid => PageSize > 0 && PageNumber > 0;
    }
}