namespace MovieTicket.Application.Dto.Payment;

public sealed record PaymentWebhookDto {
	public string? Code { get; init; }
	public string? Desc { get; init; }
	public bool Success { get; init; }
	public PaymentWebhookDataDto? Data { get; init; }
	public string? Signature { get; init; }
}

public sealed record PaymentWebhookDataDto {
	public long OrderCode { get; init; }
	public decimal Amount { get; init; }
	public string? Description { get; init; }
	public string? AccountNumber { get; init; }
	public string? Reference { get; init; }
	public string? TransactionDateTime { get; init; }
	public string? VirtualAccountName { get; init; }
	public string? VirtualAccountNumber { get; init; }
	public string? CounterAccountBankId { get; init; }
	public string? CounterAccountBankName { get; init; }
	public string? CounterAccountName { get; init; }
	public string? CounterAccountNumber { get; init; }
}