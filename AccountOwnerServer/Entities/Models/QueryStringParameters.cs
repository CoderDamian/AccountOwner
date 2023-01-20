namespace Entities.Models
{
    public class QueryStringParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1; // Data's page that I want to see

        private int _pageSize = 10; // Numbers of rows I want to see

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string OrderBy { get; set; } = string.Empty;
    }
}
