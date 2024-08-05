using BaseProject.Domain.Models.Errors;
using Microsoft.AspNetCore.Http;

namespace BaseProject.Domain.Models
{
	public class Result
	{
        public Result(bool isSuccess, Error error, object data)
        {
			if(isSuccess && error != Error.None ||
				!isSuccess && error == Error.None)
				throw new ArgumentException("Invalid error.", nameof(error));    
			Success = isSuccess;
			Error = error;
			Data = data;
        }
        public bool Success { get;}
		//public bool Failure => !Success;
		public int HttpStatusCode => Success ? StatusCodes.Status200OK : Error.Code;
		public object Data { get; set; }
		public Error Error { get;}
		public static Result Successful(object data) => new Result(true, Error.None, data: data);
		public static Result Fail(Error error) => new Result(false, error, null);
	}

	
}
