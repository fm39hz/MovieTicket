namespace MovieTicket.Api.Controller.Payment;

using Application.Dto.Payment;
using Application.Service.Contract;
using Domain.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route(RouteConstant.CONTROLLER)]
[Authorize(RoleConstant.USER)]
public sealed class PaymentController(IPaymentService paymentService) : ControllerBase {

	[HttpPost("create")]
	public async Task<IValueHttpResult<PaymentResponseDto>> CreatePaymentLink([FromBody] CreatePaymentRequestDto request) {
		var payment = await paymentService.CreatePaymentLinkAsync(request);
		return TypedResults.Ok(payment);
	}

	[HttpGet("booking/{bookingId:guid}")]
	public async Task<IValueHttpResult<PaymentResponseDto>> GetPaymentByBookingId(Guid bookingId) {
		var payment = await paymentService.GetPaymentByBookingIdAsync(bookingId);
		return payment == null ? TypedResults.NotFound<PaymentResponseDto>(null) : TypedResults.Ok(payment);
	}

	[HttpGet("{paymentId:guid}")]
	public async Task<IValueHttpResult<PaymentResponseDto>> GetPaymentById(Guid paymentId) {
		var payment = await paymentService.GetPaymentByIdAsync(paymentId);
		return payment == null ? TypedResults.NotFound<PaymentResponseDto>(null) : TypedResults.Ok(payment);
	}

	[HttpPost("webhook")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<object>> HandleWebhook([FromBody] PaymentWebhookDto webhook) {
		var isValid = await paymentService.VerifyWebhookAsync(webhook);
		if (isValid) {
			return TypedResults.Ok(new { success = true, message = "Webhook processed successfully" });
		}

		return TypedResults.BadRequest(new { success = false, message = "Invalid webhook data" });
	}

	[HttpPut("{paymentId:guid}/status")]
	[Authorize(RoleConstant.ADMIN)]
	public async Task<IValueHttpResult<PaymentResponseDto>> UpdatePaymentStatus(Guid paymentId, [FromBody] UpdatePaymentStatusRequest request) {
		var payment = await paymentService.UpdatePaymentStatusAsync(paymentId, request.Status);
		return TypedResults.Ok(payment);
	}

	[HttpGet("status/{status}")]
	[Authorize(RoleConstant.ADMIN)]
	public async Task<IValueHttpResult<IEnumerable<PaymentResponseDto>>> GetPaymentsByStatus(string status) {
		var payments = await paymentService.GetPaymentsByStatusAsync(status);
		return TypedResults.Ok(payments);
	}
}

public sealed record UpdatePaymentStatusRequest(string Status);