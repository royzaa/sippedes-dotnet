using sippedes.Commons.Constants;

namespace sippedes.Src.Features.Users.DTO
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ERole Role { get; set; }
    }
}
