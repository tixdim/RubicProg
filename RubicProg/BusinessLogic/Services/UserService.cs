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

 


        private async Task<WorkoutPlanBlo> ConvertToWorkoutInfoAsync(WorkoutRto workout)
        {
            if(workout == null) throw new ArgumentNullException(nameof(workout));

            WorkoutPlanBlo workoutPlanBlo = _mapper.Map<WorkoutPlanBlo>(workout);

            return workoutPlanBlo;
        }
        private async Task<UserUpdateBlo> ConvertToUserInformationAsync(UserRto userRto)
        {
            if (userRto == null) throw new ArgumentNullException(nameof(userRto));

            UserUpdateBlo userInformationBlo = _mapper.Map<UserUpdateBlo>(userRto);

            return userInformationBlo;
        }
        private async Task<UserProfileBlo> ConvertToPrfileInfo(ProfileUserRto profileUser)
        {
            if(profileUser == null) throw new ArgumentNullException(nameof(profileUser));

            UserProfileBlo userProfileInfoBlo = _mapper.Map<UserProfileBlo>(profileUser);
            return userProfileInfoBlo;
        }

       

        public async Task<WorkoutPlanBlo> UpdateWorkoutPlanBlo(int two, WorkoutPlanUpdateBlo workoutPlanUpdateBlo)
        {
            WorkoutRto workout = await _context.Workouts.FirstOrDefaultAsync(y => y.UserWhoTrainingId == two);
            if(workout == null) throw new NotFoundException("тренеровка с таким id не найдена");
            workout.Exercise = workoutPlanUpdateBlo.Exercise;
            workout.WorkoutTime = workoutPlanUpdateBlo.WorkoutTime;
            workout.IsDone = workoutPlanUpdateBlo.IsDone;

            WorkoutPlanBlo workoutInfo = await ConvertToWorkoutInfoAsync(workout);
            return workoutInfo;

        }

        public async Task<UserProfileBlo> UpdateUserProfile(int one, UserProfileUpdateBlo userProfileUpdateBlo)
        {
            ProfileUserRto profileUser = await _context.ProfileUsers.FirstOrDefaultAsync(y => y.UserWhoProfileId == one);
            if (profileUser == null) throw new NotFoundException("профиль с таким id не найден");
            profileUser.IsBoy = userProfileUpdateBlo.IsBoy;
            profileUser.Name = userProfileUpdateBlo.Name;
            profileUser.Surname = userProfileUpdateBlo.Surname;
            profileUser.AvatarUrl = userProfileUpdateBlo.AvatarUrl;

            UserProfileBlo userProfileBloInfo = await ConvertToPrfileInfo(profileUser);
            return userProfileBloInfo;

        }
    }
}
