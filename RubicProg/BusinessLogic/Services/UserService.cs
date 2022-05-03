using RubicProg.BusinessLogic.Core.Interfaces;
using RubicProg.BusinessLogic.Core.Models;
using System;
using System.Threading.Tasks;
using AutoMapper;
using RubicProg.DataAccess.Core.Interfaces.DBContext;
using RubicProg.DataAccess.Core.Models;
using Microsoft.EntityFrameworkCore;
using Share.Exceptions;
using System.Collections.Generic;
using System.Linq;

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

            return ConvertToUserInformationBlo(user); ;
        }

        public async Task<UserInformationBlo> AuthenticationUser(UserIdentityBlo userIdentityBlo)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(p => p.Email == userIdentityBlo.Email && p.Password == userIdentityBlo.Password);
            if (user == null) throw new NotFoundException($"Пользователь с почтой {userIdentityBlo.Email} не найден");
            
            return ConvertToUserInformationBlo(user);
        }

        public async Task<UserInformationBlo> GetUser(int userId)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) throw new NotFoundException($"Пользователя с {userId} нет");

            return ConvertToUserInformationBlo(user);
        }

        public async Task<UserInformationBlo> UpdateUser(int userId, UserUpdateBlo userUpdateBlo)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(y => y.Id == userId);
            if (user == null) throw new NotFoundException($"Пользователя с {userId} нет");

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
            if (user == null) throw new NotFoundException($"Пользователя с {userId} нет");

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
            if (result == false) throw new NotFoundException($"Пользователя с {userId} нет");

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



        public async Task<WorkoutInformationBlo> AddWorkoutPlan(WorkoutPlanAddBlo workoutPlanAddBlo)
        {
            bool result = await _context.Users.AnyAsync(y => y.Id == workoutPlanAddBlo.UserWhoTrainingId);
            if (result == false) throw new NotFoundException($"Пользователя с {workoutPlanAddBlo.UserWhoTrainingId} нет");

            if (workoutPlanAddBlo == null)
                throw new ArgumentNullException(nameof(workoutPlanAddBlo));

            WorkoutRto workout = new WorkoutRto()
            {
                UserWhoTrainingId = workoutPlanAddBlo.UserWhoTrainingId,
                Exercise = workoutPlanAddBlo.Exercise,
                WorkoutTime = workoutPlanAddBlo.WorkoutTime,
                IsDone = true,
                StartWorkoutDate = workoutPlanAddBlo.StartWorkoutDate
            };

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            return ConvertToWorkoutInfoBlo(workout);
        }

        public async Task<WorkoutInformationBlo> UpdateWorkoutPlan(int workoutPlanId, WorkoutPlanUpdateBlo workoutPlanUpdateBlo)
        {
            WorkoutRto workout = await _context.Workouts.FirstOrDefaultAsync(x => x.Id == workoutPlanId);
            if (workout == null) throw new NotFoundException($"Тренировки с {workoutPlanId} нет");

            workout.Exercise = workoutPlanUpdateBlo.Exercise;
            workout.WorkoutTime = workoutPlanUpdateBlo.WorkoutTime;

            await _context.SaveChangesAsync();

            return ConvertToWorkoutInfoBlo(workout);
        }

        public async Task<WorkoutInformationBlo> GetWorkoutPlan(int workoutPlanId)
        {
            WorkoutRto workout = await _context.Workouts.FirstOrDefaultAsync(x => x.Id == workoutPlanId);

            if (workout == null) throw new NotFoundException($"Тренировки с {workoutPlanId} нет");

            return ConvertToWorkoutInfoBlo(workout);
        }

        public async Task<List<WorkoutInformationBlo>> GetAllWorkoutPlans(int userId, int count, int skipCount)
        {
            bool doesExsist = await _context.Users
                .AnyAsync(x => x.Id == userId);

            if (doesExsist == false)
                throw new NotFoundException($"Пользователя с {userId} нет");

            List<WorkoutRto> workouts = await _context.Workouts
                .Where(e => e.UserWhoTrainingId == userId)
                .Skip(skipCount)
                .Take(count)
                .ToListAsync();

            if (workouts.Count == 0)
                throw new NotFoundException($"У пользователя с {userId} нет тренировок");

            return ConvertToWorkoutInfoBloList(workouts);
        }

        public async Task<bool> DoesExistWorkout(int workoutPlanId)
        {
            bool result = await _context.Workouts.AnyAsync(y => y.Id == workoutPlanId);
            return result;
        }

        public async Task<bool> DeleteWorkoutPlan(int workoutPlanId)
        {
            bool result = await _context.Workouts.AnyAsync(y => y.Id == workoutPlanId);
            if (result == false) throw new NotFoundException($"Тренировки с {workoutPlanId} нет");

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



        private List<WorkoutInformationBlo> ConvertToWorkoutInfoBloList(List<WorkoutRto> workoutRtos)
        {
            if (workoutRtos == null)
                throw new ArgumentNullException(nameof(workoutRtos));

            List<WorkoutInformationBlo> workoutInformationBlos = new List<WorkoutInformationBlo>();
            for (int i = 0; i < workoutRtos.Count; i++)
            {
                workoutInformationBlos.Add(_mapper.Map<WorkoutInformationBlo>(workoutRtos[i]));
            }

            return workoutInformationBlos;
        }

        private WorkoutInformationBlo ConvertToWorkoutInfoBlo(WorkoutRto workoutRto)
        {
            if(workoutRto == null) throw new ArgumentNullException(nameof(workoutRto));

            WorkoutInformationBlo workoutPlanBlo = _mapper.Map<WorkoutInformationBlo>(workoutRto);

            return workoutPlanBlo;
        }

        private UserInformationBlo ConvertToUserInformationBlo(UserRto userRto)
        {
            if (userRto == null) throw new ArgumentNullException(nameof(userRto));

            UserInformationBlo userInformationBlo = _mapper.Map<UserInformationBlo>(userRto);

            return userInformationBlo;
        }
    }
}
