namespace MovieTicket.Application.Dto.Common;

public class PaginationRequestDto {
	public int Page { get; set; } = 1;
	public int PageSize { get; set; } = 10;
	public string? SortBy { get; set; }
	public bool SortDescending { get; set; }

	public int Skip => (Page - 1) * PageSize;

	public void Validate() {
		if (Page < 1) Page = 1;
		if (PageSize < 1) PageSize = 10;
		if (PageSize > 100) PageSize = 100;
	}
}