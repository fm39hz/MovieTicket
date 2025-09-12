namespace MovieTicket.Application.Dto.Common;

using System.ComponentModel;

public record PaginationRequestDto {
	[DefaultValue(1)] public int Page { get; set; }
	[DefaultValue(10)] public int PageSize { get; set; }
	public string? SortBy { get; set; }
	public bool SortDescending { get; set; }

	public int Skip => (Page - 1) * PageSize;

	public void Validate() {
		if (Page < 1) {
			Page = 1;
		}

		if (PageSize < 1) {
			PageSize = 10;
		}

		if (PageSize > 100) {
			PageSize = 100;
		}
	}
}
