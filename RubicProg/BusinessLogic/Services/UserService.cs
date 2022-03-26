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

        // всё найс 1
        public async Task<UserUpdateBlo> RegistrationWithEmail(string email, string password)
        {
            bool result = await _context.Users.AnyAsync(y => y.Email == email && y.Password == password);
            
            if (result == true) throw new BadRequestException("Такой пользователь уже есть");
            
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

        // всё найс 2
        public async Task<UserUpdateBlo> AuthWithEmail(string email, string password)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(p => p.Email == email && p.Password == password);
            
            if (user == null) throw new NotFoundException($"Пользователь с почтой {email} не найден");
            
            return await ConvertToUserInformationAsync(user);
        }

        // всё найс 3
        public async Task<UserIdGetBlo> Get(int userId)
        {

            UserRto user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) throw new NotFoundException("Пользователь не найден");

            return await ConvertToUserInformationGetAsync(user);
        }

        // всё найс 4
        public async Task<bool> DoesExist(int id)
        {
            bool result = await _context.Users.AnyAsync(y => y.Id == id);

            return result;
        }

        // всё найс 5
        public async Task<UserUpdateBlo> Update(int id, UserUpdateDobleBlo userUpdateDobleBlo)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(y => y.Id == id);
            if (user == null) throw new NotFoundException("Такого пользователя нет");

            user.Email = userUpdateDobleBlo.Email;
            user.Password = userUpdateDobleBlo.Password;
            user.NickName = userUpdateDobleBlo.NickName;

            UserUpdateBlo userInfoBlo = await ConvertToUserInformationAsync(user);
            
            return userInfoBlo;
        }

        // всё найс 6
        public async Task<UserProfileBlo> UpdateUserProfile(int userWhoProfileId, UserProfileUpdateBlo userProfileUpdateBlo)
        {
            ProfileUserRto profileUser = await _context.ProfileUsers.FirstOrDefaultAsync(y => y.UserWhoProfileId == userWhoProfileId);
            
            if (profileUser == null) throw new NotFoundException("Профиль с таким id не найден");

            profileUser.IsBoy = userProfileUpdateBlo.IsBoy;
            profileUser.Name = userProfileUpdateBlo.Name;
            profileUser.Surname = userProfileUpdateBlo.Surname;
            profileUser.Birthday = userProfileUpdateBlo.Birthday;
            profileUser.AvatarUrl = userProfileUpdateBlo.AvatarUrl;

            UserProfileBlo userProfileBloInfo = await ConvertToProfileInfo(profileUser);
            
            return userProfileBloInfo;
        }

        // всё найс 7 
        public async Task<WorkoutPlanBlo> UpdateWorkoutPlanBlo(int userWhoProfileId, WorkoutPlanUpdateBlo workoutPlanUpdateBlo)
        {
            WorkoutRto workout = await _context.Workouts.FirstOrDefaultAsync(y => y.UserWhoTrainingId == userWhoProfileId);
            
            if (workout == null) throw new NotFoundException("Тренировка с таким id не найдена");
            
            workout.Exercise = workoutPlanUpdateBlo.Exercise;
            workout.WorkoutTime = workoutPlanUpdateBlo.WorkoutTime;

            WorkoutPlanBlo workoutInfo = await ConvertToWorkoutInfoAsync(workout);
            
            return workoutInfo;
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

        private async Task<UserIdGetBlo> ConvertToUserInformationGetAsync(UserRto userRto)
        {
            if (userRto == null) throw new ArgumentNullException(nameof(userRto));

            UserIdGetBlo userGetInformationBlo = _mapper.Map<UserIdGetBlo>(userRto);

            return userGetInformationBlo;
        }

        private async Task<UserProfileBlo> ConvertToProfileInfo(ProfileUserRto profileUser)
        {
            if(profileUser == null) throw new ArgumentNullException(nameof(profileUser));

            UserProfileBlo userProfileInfoBlo = _mapper.Map<UserProfileBlo>(profileUser);

            return userProfileInfoBlo;
        }
    }
}
