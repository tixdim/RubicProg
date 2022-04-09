using RubicProg.BusinessLogic.Core.Interfaces;
using RubicProg.BusinessLogic.Core.Models;
using System;
using System.Threading.Tasks;
using AutoMapper;
using RubicProg.DataAccess.Core.Interfaces.DBContext;
using RubicProg.DataAccess.Core.Models;
using Microsoft.EntityFrameworkCore;
using Share.Exceptions;
using System.Linq;
using Z.EntityFramework.Extensions.EFCore;

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
        public async Task<UserUpdateBlo> RegistrationWithEmail(string email, string password, string nickname)
        {
            bool result = await _context.Users.AnyAsync(y => y.Email == email && y.Password == password && y.NickName == nickname);
            
            if (result == true) throw new BadRequestException("Такой пользователь уже есть");
            
            UserRto user = new UserRto()
            {
                Password = password,
                Email = email,
                NickName = nickname
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

        // сделать контроллер
        public async Task<UserProfileBlo> GetUserProfile(int userWhoProfileId)
        {
            ProfileUserRto userProfile = await _context.ProfileUsers.FirstOrDefaultAsync(x => x.Id == userWhoProfileId);

            if (userProfile == null) throw new NotFoundException("Профиль не найден");

            return await ConvertToProfileInfo(userProfile);
        }

        // сделать контроллер
        public async Task<UserProfileDoubleBlo> AddUserProfile(UserProfileAddBlo userProfileAddBlo)
        {
            bool result = await _context.ProfileUsers.AnyAsync(y => y.Id == userProfileAddBlo.Id);
            if (result == true) throw new BadRequestException("Такая тренировка уже есть");

            ProfileUserRto profileUser = new ProfileUserRto()
            {
                IsBoy = userProfileAddBlo.IsBoy,
                Name = userProfileAddBlo.Name,
                Surname = userProfileAddBlo.Surname,
                DateRegistration = DateTime.UtcNow,
                Birthday = userProfileAddBlo.Birthday,
                AvatarUrl = userProfileAddBlo.AvatarUrl
            };

            _context.ProfileUsers.Add(profileUser);
            await _context.SaveChangesAsync();

            UserProfileDoubleBlo userProfileBloInfo = await ConvertToProfileFullInfo(profileUser);

            return userProfileBloInfo;
        }

        // сделать контроллер
        public async Task<bool> DeleteUserProfile(int userWhoProfileId)
        {
            bool result = await _context.ProfileUsers.AnyAsync(y => y.Id == userWhoProfileId);
            if (result == false) throw new NotFoundException("Такой тренировки нет");

            var workPlanWhoIsDelete = await _context.ProfileUsers
                .FindAsync(userWhoProfileId);

            if (workPlanWhoIsDelete != null)
            {
                _context.ProfileUsers.Remove(workPlanWhoIsDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // всё найс 7 
        public async Task<WorkoutPlanBlo> UpdateWorkoutPlan(int userWhoProfileId, WorkoutPlanUpdateBlo workoutPlanUpdateBlo)
        {
            WorkoutRto workout = await _context.Workouts.FirstOrDefaultAsync(y => y.UserWhoTrainingId == userWhoProfileId);
            
            if (workout == null) throw new NotFoundException("Тренировка с таким id не найдена");
            
            workout.Exercise = workoutPlanUpdateBlo.Exercise;
            workout.WorkoutTime = workoutPlanUpdateBlo.WorkoutTime;

            WorkoutPlanBlo workoutInfo = await ConvertToWorkoutInfoAsync(workout);
            
            return workoutInfo;
        }

        // сделать контроллер
        public async Task<WorkoutPlanBlo> GetWorkoutPlan(int workoutPLanId)
        {
            WorkoutRto workoutPLan = await _context.Workouts.FirstOrDefaultAsync(x => x.Id == workoutPLanId);

            if (workoutPLan == null) throw new NotFoundException("Тренировка не найдена");

            return await ConvertToWorkoutInfoAsync(workoutPLan);
        }

        // сделать контроллер
        public async Task<WorkoutPlanBlo> AddWorkoutPlan(WorkoutPlanAddBlo workoutPlanAddBlo)
        {
            bool result = await _context.Workouts.AnyAsync(y => y.Id == workoutPlanAddBlo.Id);

            if (result == true) throw new BadRequestException("Такая тренировка уже есть");

            WorkoutRto workout = new WorkoutRto()
            {
                WorkoutTime = workoutPlanAddBlo.WorkoutTime,
                Exercise = workoutPlanAddBlo.Exercise,
                IsDone = workoutPlanAddBlo.IsDone,
                StartWorkoutDate = workoutPlanAddBlo.StartWorkoutDate
            };

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            WorkoutPlanBlo workoutPlanBlo = await ConvertToWorkoutInfoAsync(workout);

            return workoutPlanBlo;
        }

        // сделать контроллер
        public async Task<bool> DeleteWorkoutPlan(int workoutPlanId)
        {
            bool result = await _context.Workouts.AnyAsync(y => y.Id == workoutPlanId);
            if (result == false) throw new NotFoundException("Такой тренировки нет");

            var workPlanWhoIsDelete = await _context.Workouts
                .FindAsync(workoutPlanId);

            if (workPlanWhoIsDelete != null)
            {
                _context.Workouts.Remove(workPlanWhoIsDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
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

        private async Task<UserProfileDoubleBlo> ConvertToProfileFullInfo(ProfileUserRto profileUser)
        {
            if (profileUser == null) throw new ArgumentNullException(nameof(profileUser));

            UserProfileDoubleBlo userProfileInfoFullBlo = _mapper.Map<UserProfileDoubleBlo>(profileUser);

            return userProfileInfoFullBlo;
        }
    }
}
