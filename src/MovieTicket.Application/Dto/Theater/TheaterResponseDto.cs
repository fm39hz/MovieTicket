namespace MovieTicket.Application.Dto.Theater;

using Common;
using Domain.Entity.Theater;

public class TheaterResponseDto(TheaterModel theater) : IResponseDto {
	public Guid Id { get; init; } = theater.Id;
	public string Name { get; init; } = theater.Name;
	public string Address { get; init; } = theater.Address;
	public string City { get; init; } = theater.City;
	public string State { get; init; } = theater.State;
	public string ZipCode { get; init; } = theater.ZipCode;
	public string PhoneNumber { get; init; } = theater.PhoneNumber;
	public int ScreenCount { get; init; } = theater.ScreenCount;
	public bool ParkingAvailable { get; init; } = theater.ParkingAvailable;
	public string Facilities { get; init; } = theater.Facilities;
}