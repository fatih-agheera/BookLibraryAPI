using System.Net;
using System.Text.Json;
using BusinessLayer.Exceptions;
using FluentValidation;

namespace BookLibraryAPI.MiddlewareHandlers
{
	public class GlobalErrorHandler
	{
		private readonly RequestDelegate _next;

		public GlobalErrorHandler(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception error)
			{
				var response = context.Response;
				response.ContentType = "application/json";

				response.StatusCode = error switch
				{
					ValidationException => (int)HttpStatusCode.BadRequest,
					DbEntityNotFoundException => (int)HttpStatusCode.NotFound,
					ItemAlreadyExistsException => (int)HttpStatusCode.BadRequest,
					UserLoginIsNotFoundException => (int)HttpStatusCode.Unauthorized,
					WrongUserPasswordException => (int)HttpStatusCode.Unauthorized,
					_ => (int)HttpStatusCode.InternalServerError,
				};

				var result = JsonSerializer.Serialize(new { message = error.Message });
				await response.WriteAsync(result);
			}
		}
	}
}
