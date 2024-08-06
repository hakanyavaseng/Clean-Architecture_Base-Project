namespace BaseProject.Domain.DTOs.AppUserDtos
{
    public record Create_User_Dto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
