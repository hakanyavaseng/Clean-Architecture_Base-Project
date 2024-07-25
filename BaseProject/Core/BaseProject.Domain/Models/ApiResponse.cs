using Microsoft.AspNetCore.Http;

namespace BaseProject.Domain.Models
{
    public record ApiResponse<T> 
    {
        public bool Success { get; init; }
        public string? Message { get; init; }
        public int StatusCode { get; init; } = StatusCodes.Status200OK;
        public T? Data { get; init; }
    }
}
