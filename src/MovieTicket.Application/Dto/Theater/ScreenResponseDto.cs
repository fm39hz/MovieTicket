namespace MovieTicket.Application.Dto.Theater;

using Common;
using Domain.Entity.Theater;

public class ScreenResponseDto(ScreenModel screen) : IResponseDto {
	public Guid Id { get; init; } = screen.Id;
	public Guid TheaterId { get; init; } = screen.TheaterId;
	public int ScreenNumber { get; init; } = screen.ScreenNumber;
	public string ScreenType { get; init; } = screen.ScreenType;
	public int TotalSeats { get; init; } = screen.TotalSeats;
	public string AudioSystem { get; init; } = screen.AudioSystem;
	public string SeatLayout { get; init; } = screen.SeatLayout;
}