using Microsoft.AspNetCore.Http;

namespace BaseProject.Domain.Models.Errors
{
	public record CommonErrors
	{
		public static Error NotFoundException => new Error("The requested resource was not found.", StatusCodes.Status404NotFound);

	}
}
