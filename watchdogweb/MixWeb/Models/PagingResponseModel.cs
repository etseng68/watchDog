namespace MixWeb.Models
{
	public class PagingResponseModel<T>
	{
		public int TotalCount { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public List<T> Data { get; set; } = new List<T>();
	}
}
