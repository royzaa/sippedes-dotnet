    using BCrypt.Net;
using sippedes.Commons.Constants;
using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;
using sippedes.Cores.Security;
using sippedes.Features.Admin.Services;
using sippedes.Features.Auth.Dto;
using sippedes.Features.CivilDatas.Services;

namespace sippedes.Features.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<UserCredential> _repository;
        private readonly IPersistence _persistence;
        private readonly IRoleService _roleService;
        private readonly IAdminDataService _adminDataService;
        private readonly ICivilDataService _civilDataService;

        private readonly IJwtUtils _jwtUtils;

        public AuthService(IRepository<UserCredential> repository, IPersistence persistence, IJwtUtils jwtUtils, IRoleService roleService, IAdminDataService adminDataService, ICivilDataService civilDataService)
        {
            _repository = repository;
            _persistence = persistence;
            _jwtUtils = jwtUtils;
            _roleService = roleService;
            _adminDataService = adminDataService;
            _civilDataService = civilDataService;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _repository.Find(credential => credential.Email.Equals(request.Email), new[] { "Role" });
            if (user == null) throw new NotFoundException("Email Not Registered");

            var verify = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!verify) throw new UnauthorizedException("Password Salah");

            var token = _jwtUtils.GenerateToken(user);

            return new LoginResponse
            {
                Email = user.Email,
                Role = user.Role.ERole.ToString(),
                Token = token
            };
        }

        public async Task<RegisterResponse> RegisterAdmin(RegisterRequest request)
        {
            var user = await _repository.Find(credential => credential.Email.Equals(request.Email));
            if (user is not null) throw new UnauthorizedException("Email ready");

            var registerResponse = await _persistence.ExecuteTransactionAsync(async () =>
            {
                var role = await _roleService.GetOrSave(ERole.Admin);

                var userCredential = new UserCredential
                {
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Role = role
                };

                var saveUser = await _repository.Save(userCredential);

                await _adminDataService.CreateAdminData(new AdminData
                {
                    FullName = request.FullName,
                    IsActive = 0,
                    UserCredential = saveUser
                });

                return new RegisterResponse
                {
                    Email = saveUser.Email,
                    Role = saveUser.Role.ERole.ToString()
                };

            });

            return registerResponse;
        }

        public async Task<RegisterResponse> RegisterCivilin(RegisterCivilinRequest request)
        {
            var user = await _repository.Find(credential => credential.Email.Equals(request.Email));
            if (user is not null) throw new UnauthorizedException("Email ready");

            var civilData = await _civilDataService.GetByNIK(request.NIK);
            if (civilData == null) throw new NotFoundException("NIK Not Found");

            var registerResponse = await _persistence.ExecuteTransactionAsync(async () =>
            {
                var role = await _roleService.GetOrSave(ERole.Civilian);

                var userCredential = new UserCredential
                {
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    CivilDataId = request.NIK,
                    Role = role
                };

                var saveUser = await _repository.Save(userCredential);
                await _persistence.SaveChangesAsync();

                return new RegisterResponse
                {
                    Email = saveUser.Email,
                    Role = saveUser.Role.ERole.ToString()
                };

            });
            return registerResponse;
        }
    }
}
