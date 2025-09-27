namespace MovieTicket.Application.Dto.Theater;

using Common;
using Domain.Entity.Theater;

public sealed class BookingHistoryResponseDto : IResponseDto {
	public BookingHistoryResponseDto(BookingModel booking) {
		Id = booking.Id;
		UserId = booking.UserId;
		ShowtimeId = booking.ShowtimeId;
		SeatNumbers = booking.SeatNumbers.Split(',', StringSplitOptions.RemoveEmptyEntries);
		BookingDate = booking.BookingDate;
		TotalAmount = booking.TotalAmount;
		PaymentStatus = booking.PaymentStatus;
		BookingStatus = booking.BookingStatus;
		BookingReference = booking.BookingReference;

		// Movie details
		if (booking.Showtime?.Movie != null) {
			MovieTitle = booking.Showtime.Movie.Title;
			MoviePosterUrl = booking.Showtime.Movie.PosterUrl;
			MovieGenre = booking.Showtime.Movie.Genre;
			MovieDuration = booking.Showtime.Movie.Duration;
			MovieRating = booking.Showtime.Movie.Rating;
			MovieLanguage = booking.Showtime.Movie.Language;
		}

		// Showtime details
		if (booking.Showtime != null) {
			ShowtimeDate = booking.Showtime.Date;
			ShowtimeStartTime = booking.Showtime.StartTime;
			ShowtimeEndTime = booking.Showtime.EndTime;
			TicketPrice = booking.Showtime.TicketPrice;
		}

		// Theater and Screen details
		if (booking.Showtime?.Theater != null) {
			TheaterName = booking.Showtime.Theater.Name;
			TheaterAddress = booking.Showtime.Theater.Address;
			TheaterCity = booking.Showtime.Theater.City;
		}

		if (booking.Showtime?.Screen != null) {
			ScreenNumber = booking.Showtime.Screen.ScreenNumber;
			ScreenType = booking.Showtime.Screen.ScreenType;
		}
	}

	// Basic booking information
	public Guid Id { get; init; }
	public Guid UserId { get; init; }
	public Guid ShowtimeId { get; init; }
	public string[] SeatNumbers { get; init; } = [];
	public DateTime BookingDate { get; init; }
	public decimal TotalAmount { get; init; }
	public string PaymentStatus { get; init; } = string.Empty;
	public string BookingStatus { get; init; } = string.Empty;
	public string BookingReference { get; init; } = string.Empty;

	// Movie information
	public string MovieTitle { get; init; } = string.Empty;
	public string MoviePosterUrl { get; init; } = string.Empty;
	public string MovieGenre { get; init; } = string.Empty;
	public int MovieDuration { get; init; }
	public decimal MovieRating { get; init; }
	public string MovieLanguage { get; init; } = string.Empty;

	// Showtime information
	public DateTime ShowtimeDate { get; init; }
	public DateTime ShowtimeStartTime { get; init; }
	public DateTime ShowtimeEndTime { get; init; }
	public decimal TicketPrice { get; init; }

	// Theater information
	public string TheaterName { get; init; } = string.Empty;
	public string TheaterAddress { get; init; } = string.Empty;
	public string TheaterCity { get; init; } = string.Empty;

	// Screen information
	public int ScreenNumber { get; init; }
	public string ScreenType { get; init; } = string.Empty;

	// Payment information (to be populated separately)
	public string? PaymentUrl { get; set; }
	public DateTime? PaymentCreatedAt { get; set; }
	public DateTime? PaymentUpdatedAt { get; set; }
}