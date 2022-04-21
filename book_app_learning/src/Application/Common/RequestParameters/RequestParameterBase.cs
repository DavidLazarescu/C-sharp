namespace Application.Common.RequestParameters
{
    public abstract class RequestParameterBase
    {
        protected virtual int _maxPageSize { get; set; } = 50;
        
        public virtual int PageNumber { get; set; }
        
        protected int _pageSize;
        public virtual int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = (value <= _maxPageSize) ? value : _maxPageSize;
            }
        }

        public virtual bool IsValid => PageSize > 0 && PageNumber > 0;
    }
}