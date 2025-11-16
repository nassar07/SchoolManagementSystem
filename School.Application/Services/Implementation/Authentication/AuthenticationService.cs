using AutoMapper;
using FluentValidation;
using School.Application.DTOs;
using School.Application.DTOs.Identity;
using School.Application.Services.Interfaces;
using School.Application.Services.Interfaces.Authentication;
using School.Application.Validators.Authentication;
using School.Domain.Entities.Identity;
using School.Domain.Interfaces.Authentication;

namespace School.Application.Services.Implementation.Authentication
{
    public class AuthenticationService(ITokenManagement tokenManagement,
        IRoleManagement roleManagement,
        IUserManagement userManagement,
        IValidator<CreateUserDto> CreateUserValidator,
        IValidator<LoginUser> LoginUserValidator,
        IValidationService validationService,
        IAppLogger<AuthenticationService> logger,
        IMapper mapper) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUserDto user)
        {
            var _validationResult = await validationService.ValidateAsync(user, CreateUserValidator);
            if (!_validationResult.Success) return _validationResult;

            var MappedUser = mapper.Map<ApplicationUser>(user);
            MappedUser.UserName = user.Email;
            MappedUser.PasswordHash = user.Password;

            var result = await userManagement.CreateUser(MappedUser);
            if (!result)
                return new ServiceResponse { Message = "Email is Already Exist" };

            var _user = await userManagement.GetUserByEmail(user.Email);
            var users = await userManagement.GetAllUsers();
            var assignedResult = await roleManagement.AddUserToRole(_user, users.Count() > 1 ? user.RoleName : "Admin");


            if (!assignedResult)
            {
                int removeUserResult = await userManagement.RemoveUserByEmail(user!.Email!);

                if (removeUserResult <= 0)
                {
                    logger.LogError(new Exception($"Role assignment failed for user {user.Email}"), "User Couldn't Assign To Role");
                    return new ServiceResponse { Message = "Error in Creating Account" };
                }
            }

            return new ServiceResponse { Success = true, Message = "User Created Successfully" };

        }

        public async Task<LoginResponse> LoginUser(LoginUser user)
        {
            var _validationResult = await validationService.ValidateAsync(user, LoginUserValidator);
            if (!_validationResult.Success) return new LoginResponse { Message = _validationResult.Message };

            var MappedUser = mapper.Map<ApplicationUser>(user);
            MappedUser.PasswordHash = user.Password;

            bool LoginResult = await userManagement.LoginUser(MappedUser);
            if (!LoginResult)
                return new LoginResponse { Message = "Invalid Email or Password" };

            var _user = await userManagement.GetUserByEmail(user.Email);
            var claims = await userManagement.GetUserClaims(_user.Email!);


            var jwtToken = tokenManagement.GenerateToken(claims);
            var refreshToken = tokenManagement.GetRefreshToken();

            int saveTokenResult = 0;
            bool TokenCheck = await tokenManagement.ValidateRefreshToken(refreshToken);
            if (TokenCheck)
            {
                saveTokenResult = await tokenManagement.UpdateRefreshToken(_user.Id, refreshToken);
            }
            else
            {
                saveTokenResult = await tokenManagement.AddRefreshToken(_user.Id, refreshToken);
            }


            return saveTokenResult <= 0 ? new LoginResponse(Message: "Error in Login Process") : new LoginResponse(Success: true,
                Token: jwtToken,
                RefreshToken: refreshToken);
        }

        public async Task<LoginResponse> ReviveToken(string refreshToken)
        {
            var validateResult = await tokenManagement.ValidateRefreshToken(refreshToken);
            if (!validateResult)
                return new LoginResponse { Message = "Invalid Refresh Token" };
            var userId = await tokenManagement.GetUserIdByRefreshToken(refreshToken);
            ApplicationUser user = await userManagement.GetUserById(userId);
            var claims = await userManagement.GetUserClaims(user.Email!);
            var jwtToken = tokenManagement.GenerateToken(claims);
            var newRefreshToken = tokenManagement.GetRefreshToken();
            int updateResult = await tokenManagement.UpdateRefreshToken(userId, newRefreshToken);
            return updateResult <= 0 ? new LoginResponse { Message = "Error in Reviving Token" } : new LoginResponse(Success: true,
                Token: jwtToken,
                RefreshToken: newRefreshToken);
        }
    }
}
