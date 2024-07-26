using Microsoft.AspNetCore.Http;

namespace BaseProject.Domain.Models.Errors
{
	public sealed record Error(string Message, int Code = StatusCodes.Status500InternalServerError)
	{
		public static readonly Error None = new Error(string.Empty, 0);
	}
}
