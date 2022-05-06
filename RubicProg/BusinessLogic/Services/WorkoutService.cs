using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RubicProg.BusinessLogic.Core.Interfaces;
using RubicProg.BusinessLogic.Core.Models;
using RubicProg.DataAccess.Core.Interfaces.DBContext;
using RubicProg.DataAccess.Core.Models;
using Share.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RubicProg.BusinessLogic.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        public WorkoutService(IMapper mapper, IDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<WorkoutInformationBlo> AddWorkoutPlan(WorkoutPlanAddBlo workoutPlanAddBlo)
        {
            bool result = await _context.Users
                .AnyAsync(x => x.Id == workoutPlanAddBlo.UserWhoTrainingId);

            if (result == false) 
                throw new NotFoundException($"Пользователя с id {workoutPlanAddBlo.UserWhoTrainingId} нет");

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
            if (workout == null) throw new NotFoundException($"Тренировки с id {workoutPlanId} нет");

            workout.Exercise = workoutPlanUpdateBlo.Exercise == null ? workout.Exercise : workoutPlanUpdateBlo.Exercise;
            workout.WorkoutTime = workoutPlanUpdateBlo.WorkoutTime;

            await _context.SaveChangesAsync();

            return ConvertToWorkoutInfoBlo(workout);
        }

        public async Task<WorkoutInformationBlo> GetWorkoutPlan(int workoutPlanId)
        {
            WorkoutRto workout = await _context.Workouts.FirstOrDefaultAsync(x => x.Id == workoutPlanId);

            if (workout == null) throw new NotFoundException($"Тренировки с id {workoutPlanId} нет");

            return ConvertToWorkoutInfoBlo(workout);
        }

        public async Task<List<WorkoutInformationBlo>> GetAllWorkoutPlans(int userId, int count, int skipCount)
        {
            bool doesExsist = await _context.Users
                .AnyAsync(x => x.Id == userId);

            if (doesExsist == false)
                throw new NotFoundException($"Пользователя с id {userId} нет");

            List<WorkoutRto> workouts = await _context.Workouts
                .Where(e => e.UserWhoTrainingId == userId)
                .Skip(skipCount)
                .Take(count)
                .ToListAsync();

            if (workouts.Count == 0)
                throw new NotFoundException($"У пользователя с id {userId} нет тренировок");

            return ConvertToWorkoutInfoBloList(workouts);
        }

        public async Task<int> GetWorkoutCount(int userId)
        {
            bool doesExsist = await _context.Users
                .AnyAsync(x => x.Id == userId);

            if (doesExsist == false)
                throw new NotFoundException($"Пользователя с id {userId} нет");

            int workoutCount = await _context.Workouts
                .Where(e => e.UserWhoTrainingId == userId)
                .CountAsync();

            return workoutCount;
        }

        public async Task<bool> DoesExistWorkout(int workoutPlanId)
        {
            bool result = await _context.Workouts.AnyAsync(y => y.Id == workoutPlanId);
            return result;
        }

        public async Task<bool> DeleteWorkoutPlan(int workoutPlanId)
        {
            bool result = await _context.Workouts.AnyAsync(y => y.Id == workoutPlanId);
            if (result == false) throw new NotFoundException($"Тренировки с id {workoutPlanId} нет");

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

        public async Task<bool> DeleteAllWorkoutPlan(int userId)
        {
            bool doesExsist = await _context.Users
                .AnyAsync(x => x.Id == userId);

            if (doesExsist == false)
                throw new NotFoundException($"Пользователя с id {userId} нет");

            List<WorkoutRto> workouts = await _context.Workouts
                .Where(e => e.UserWhoTrainingId == userId)
                .ToListAsync();

            if (workouts.Count == 0 || workouts == null)
                throw new NotFoundException($"У пользователя с id {userId} нет тренировок");

            foreach (WorkoutRto workout in workouts)
            {
                _context.Workouts.Remove(workout);
            }

            await _context.SaveChangesAsync();

            List<WorkoutRto> newWorkouts= await _context.Workouts
                .Where(e => e.UserWhoTrainingId == userId)
                .ToListAsync();

            if (newWorkouts.Count == 0 || newWorkouts == null)
                return true;

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
            if (workoutRto == null) throw new ArgumentNullException(nameof(workoutRto));

            WorkoutInformationBlo workoutPlanBlo = _mapper.Map<WorkoutInformationBlo>(workoutRto);

            return workoutPlanBlo;
        }
    }
}
