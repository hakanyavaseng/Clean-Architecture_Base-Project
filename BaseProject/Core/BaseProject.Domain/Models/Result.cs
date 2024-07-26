using BaseProject.Domain.Models.Errors;
using Microsoft.AspNetCore.Http;

namespace BaseProject.Domain.Models
{
	public class Result
	{
        public Result(bool isSuccess, Error error)
        {
			if(isSuccess && error != Error.None ||
				!isSuccess && error == Error.None)
				throw new ArgumentException("Invalid error.", nameof(error));    
			Success = isSuccess;
			Error = error;
        }
        public bool Success { get;}
		//public bool Failure => !Success;
		public int HttpStatusCode => Success ? StatusCodes.Status200OK : Error.HttpStatusCode;
		public Error Error { get;}
		public static Result Successful() => new Result(true, Error.None);
		public static Result Fail(Error error) => new Result(false, error);
	}

	
}
