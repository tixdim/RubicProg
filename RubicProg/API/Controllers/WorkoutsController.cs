using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RubicProg.API.Models;
using RubicProg.BusinessLogic.Core.Interfaces;
using RubicProg.BusinessLogic.Core.Models;
using Share.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RubicProg.API.Controllers
{
    /// <summary>
    /// Workout's controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutService _workoutService;

        public WorkoutsController(IMapper mapper, IWorkoutService workoutService)
        {
            _mapper = mapper;
            _workoutService = workoutService;
        }

        /// <summary>
        /// Добавляет тренировку и возвращает информацию о ней
        /// </summary>
        /// <param name="userWhoTrainingId">Id пользователя, к которому надо добавить тренировку</param>
        /// <param name="exercise">Упражнение</param>
        /// <param name="workoutTime">Время всей тренировки</param>
        /// <param name="startWorkoutDate">Время начала тренировки</param>
        [ProducesResponseType(typeof(WorkoutInformationDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost("[action]")]
        public async Task<ActionResult<WorkoutInformationDto>> AddWorkoutPlan(WorkoutPlanAddDto workoutPlanAddDto)
        {
            WorkoutPlanAddBlo workoutPlanAddBlo = _mapper.Map<WorkoutPlanAddBlo>(workoutPlanAddDto);
            WorkoutInformationBlo workoutInformationBlo;
            try
            {
                workoutInformationBlo = await _workoutService.AddWorkoutPlan(workoutPlanAddBlo);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            return Created("", ConvertToWorkoutInformationDto(workoutInformationBlo));
        }

        /// <summary>
        /// Обновляет информацию тренировки и возвращает информацию о ней
        /// </summary>
        /// <param name="workoutPlanId">Идентификатор тренировки</param>
        /// <param name="exercise">Упражнение</param>
        /// <param name="workoutTime">Время начала</param>
        [ProducesResponseType(typeof(WorkoutInformationDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPatch("[action]/{workoutPlanId}")]
        public async Task<ActionResult<WorkoutInformationDto>> UpdateWorkoutPlan([FromRoute] int workoutPlanId, [FromBody] WorkoutPlanUpdateDto workoutPlanUpdateDto)
        {
            WorkoutPlanUpdateBlo workoutPlanUpdateBlo = _mapper.Map<WorkoutPlanUpdateBlo>(workoutPlanUpdateDto);
            WorkoutInformationBlo workoutInformationBlo;

            try
            {
                workoutInformationBlo = await _workoutService.UpdateWorkoutPlan(workoutPlanId, workoutPlanUpdateBlo);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok(ConvertToWorkoutInformationDto(workoutInformationBlo));

        }

        /// <summary>
        /// Возвращает тренировку с указанным id
        /// </summary>
        /// <param name="workoutPlanId">Идентификатор тренировки</param>
        [ProducesResponseType(typeof(WorkoutInformationDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("[action]/{workoutPlanId}")]
        public async Task<ActionResult<WorkoutInformationDto>> GetWorkoutPlan(int workoutPlanId)
        {
            WorkoutInformationBlo workoutInformationBlo;

            try
            {
                workoutInformationBlo = await _workoutService.GetWorkoutPlan(workoutPlanId);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok(ConvertToWorkoutInformationDto(workoutInformationBlo));
        }

        /// <summary>
        /// Возвращает все тренировки у указанного пользователя по userId
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="count">Сколько тренировок требуется</param>
        /// <param name="skipCount">Сколько тренировок уже есть</param>
        [ProducesResponseType(typeof(List<WorkoutInformationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("[action]/{userId}/{count}/{skipCount}")]
        public async Task<ActionResult<List<WorkoutInformationDto>>> GetAllWorkoutPlans(int userId, int count, int skipCount)
        {
            List<WorkoutInformationBlo> workoutInformationBlos = new List<WorkoutInformationBlo>();

            try
            {
                workoutInformationBlos = await _workoutService.GetAllWorkoutPlans(userId, count, skipCount);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok(ConvertToListWorkoutInformationDto(workoutInformationBlos));
        }

        /// <summary>
        /// Возвращает количество тренировок у указанного пользователя c userId
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("[action]/{userId}/{count}/{skipCount}")]
        public async Task<ActionResult<int>> GetWorkoutCount(int userId)
        {
            try
            {
                return Ok(await _workoutService.GetWorkoutCount(userId));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Проверяет, существует ли тренировка с указанным id
        /// </summary>
        /// <param name="workoutPlanId">Идентификатор тренировки</param>
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [HttpGet("[action]/{workoutPlanId}")]
        public async Task<ActionResult<bool>> DoesExistWorkout(int workoutPlanId)
        {
            return Ok(await _workoutService.DoesExistWorkout(workoutPlanId));
        }

        /// <summary>
        /// Возвращает получилось ли удалить тренировку с указанным id или нет
        /// </summary>
        /// <param name="workoutPlanId">Идентификатор тренировки</param>
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpDelete("[action]/{workoutPlanId}")]
        public async Task<ActionResult<bool>> DeleteWorkoutPlan(int workoutPlanId)
        {
            try
            {
                return Ok(await _workoutService.DeleteWorkoutPlan(workoutPlanId));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Возвращает получилось ли удалить все тренировки у пользователя с указанным id или нет
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpDelete("[action]/{userId}")]
        public async Task<ActionResult<bool>> DeleteAllWorkoutPlan(int userId)
        {
            try
            {
                return Ok(await _workoutService.DeleteAllWorkoutPlan(userId));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }


        private WorkoutInformationDto ConvertToWorkoutInformationDto(WorkoutInformationBlo workoutInformationBlo)
        {
            if (workoutInformationBlo == null)
                throw new ArgumentNullException(nameof(workoutInformationBlo));

            WorkoutInformationDto workoutInformationDto = _mapper.Map<WorkoutInformationDto>(workoutInformationBlo);
            return workoutInformationDto;
        }

        private List<WorkoutInformationDto> ConvertToListWorkoutInformationDto(List<WorkoutInformationBlo> workoutInformationBlos)
        {
            if (workoutInformationBlos == null)
                throw new ArgumentNullException(nameof(workoutInformationBlos));

            List<WorkoutInformationDto> workoutInformationDtos = new List<WorkoutInformationDto>();
            for (int i = 0; i < workoutInformationBlos.Count; i++)
            {
                workoutInformationDtos.Add(ConvertToWorkoutInformationDto(workoutInformationBlos[i]));
            }
            return workoutInformationDtos;
        }
    }
}
