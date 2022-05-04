using RubicProg.BusinessLogic.Core.Models;
using System.Threading.Tasks;

namespace RubicProg.BusinessLogic.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserInformationBlo> RegistrationUser(UserRegistrBlo userRegistrBlo);
        Task<UserInformationBlo> AuthenticationUser(UserIdentityBlo userIdentityBlo);
        Task<UserInformationBlo> GetUser(int userId);
        Task<UserInformationBlo> UpdateUser(int userId, UserUpdateBlo userUpdateBlo);
        Task<string> UpdateAvatar(int userId, string avatarUrl);
        Task<bool> DoesExistUser(int userId);
        Task<bool> DeleteUser(int userId);
    }
}
