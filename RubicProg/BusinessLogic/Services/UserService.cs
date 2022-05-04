using RubicProg.BusinessLogic.Core.Interfaces;
using RubicProg.BusinessLogic.Core.Models;
using System;
using System.Threading.Tasks;
using AutoMapper;
using RubicProg.DataAccess.Core.Interfaces.DBContext;
using RubicProg.DataAccess.Core.Models;
using Microsoft.EntityFrameworkCore;
using Share.Exceptions;

namespace RubicProg.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public UserService(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserInformationBlo> RegistrationUser(UserRegistrBlo userRegistrBlo)
        {
            bool result = await _context.Users.AnyAsync(y => y.Email == userRegistrBlo.Email);
            
            if (result == true) throw new BadRequestException($"Пользователь с почтой {userRegistrBlo.Email} уже зарегистрирован");

            if (userRegistrBlo.Email == null || userRegistrBlo.Nickname == null || userRegistrBlo.Password == null ||
                userRegistrBlo.Name == null || userRegistrBlo.Surname == null) throw new BadRequestException($"Вы заполнили не все поля");

            if (userRegistrBlo.Password.Length < 6) throw new BadRequestException($"Длина пароля должна быть не менее 6 символов");

            UserRto user = new UserRto()
            {
                Email = userRegistrBlo.Email,
                Nickname = userRegistrBlo.Nickname,
                Password = userRegistrBlo.Password,
                IsBoy = userRegistrBlo.IsBoy,
                Name = userRegistrBlo.Name,
                Surname = userRegistrBlo.Surname,
                DateRegistration = DateTime.Now,
                AvatarUrl = ""
            };
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return ConvertToUserInformationBlo(user);
        }

        public async Task<UserInformationBlo> AuthenticationUser(UserIdentityBlo userIdentityBlo)
        {
            UserRto user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Email == userIdentityBlo.Email! && p.Password == userIdentityBlo.Password!);

            if (user == null) throw new BadRequestException($"Неверное имя пользователя или пароль");
            
            return ConvertToUserInformationBlo(user);
        }

        public async Task<UserInformationBlo> GetUser(int userId)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) throw new NotFoundException($"Пользователя с id {userId} нет");

            return ConvertToUserInformationBlo(user);
        }

        public async Task<UserInformationBlo> UpdateUser(int userId, UserUpdateBlo userUpdateBlo)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(y => y.Id == userId);
            if (user == null) throw new NotFoundException($"Пользователя с id {userId} нет");

            user.Nickname = userUpdateBlo.Nickname;
            user.Password = userUpdateBlo.Password;
            user.IsBoy = userUpdateBlo.IsBoy;
            user.Name = userUpdateBlo.Name;
            user.Surname = userUpdateBlo.Surname;
            user.AvatarUrl = userUpdateBlo.AvatarUrl;

            await _context.SaveChangesAsync();

            return ConvertToUserInformationBlo(user); ;
        }

        public async Task<string> UpdateAvatar(int userId, string avatarUrl)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(y => y.Id == userId);
            if (user == null) throw new NotFoundException($"Пользователя с id {userId} нет");

            user.AvatarUrl = avatarUrl;

            await _context.SaveChangesAsync();

            return avatarUrl;
        }

        public async Task<bool> DoesExistUser(int userId)
        {
            bool result = await _context.Users.AnyAsync(y => y.Id == userId);
            return result;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            bool result = await _context.Users.AnyAsync(y => y.Id == userId);
            if (result == false) throw new NotFoundException($"Пользователя с id {userId} нет");

            var userIsDelete = await _context.Users
                .FindAsync(userId);

            if (userIsDelete != null)
            {
                _context.Users.Remove(userIsDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        private UserInformationBlo ConvertToUserInformationBlo(UserRto userRto)
        {
            if (userRto == null) throw new ArgumentNullException(nameof(userRto));

            UserInformationBlo userInformationBlo = _mapper.Map<UserInformationBlo>(userRto);

            return userInformationBlo;
        }
    }
}
