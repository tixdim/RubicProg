using RubicProg.BusinessLogic.Core.Interfaces;
using RubicProg.BusinessLogic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<UserUpdateBlo> AuthWithEmail(string email, string password)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(p => p.Email == email && p.Password == password);
            if (user == null) throw new NotFoundException($"Пользователь с почтой {email} не найден");
            return await ConvertToUserInformationAsync(user);
        }

        public async Task<bool> DoesExist(string email, string password)
        {
            bool result = await _context.Users.AnyAsync(y => y.Email == email && y.Password == password);
            return result;
        }

        public async Task<UserUpdateBlo> Get(int userId)
        {

            UserRto user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) throw new NotFoundException("Пользователь не найден");

            return await ConvertToUserInformationAsync(user);
        }

        public async Task<UserUpdateBlo> RegistrationWithEmail(string email, string password)
        {
            bool result = await _context.Users.AnyAsync(y => y.Email == email && y.Password == password);
            if(result == true) throw new BadRequestException("Такой пользователь уже есть");
            UserRto user = new UserRto()
            {
                Password = password,
                Email = email
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            UserUpdateBlo userUpdateBlo = await ConvertToUserInformationAsync(user);
            return userUpdateBlo;
        }

 

        public async Task<UserUpdateBlo> Update(string email, string password, UserUpdateDobleBlo userUpdateDobleBlo)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(y => y.Email == email && y.Password == password);
            if (user == null) throw new NotFoundException("Токого пользователя нет");

            user.Email = userUpdateDobleBlo.Email;
            user.Password = userUpdateDobleBlo.Password;
            user.NickName = userUpdateDobleBlo.NickName;

            UserUpdateBlo userInfoBlo = await ConvertToUserInformationAsync(user);
            return userInfoBlo;

        }

        private async Task<UserUpdateBlo> ConvertToUserInformationAsync(UserRto userRto)
        {
            if (userRto == null) throw new ArgumentNullException(nameof(userRto));

            UserUpdateBlo userInformationBlo = _mapper.Map<UserUpdateBlo>(userRto);

            return userInformationBlo;
        }
    }
}
