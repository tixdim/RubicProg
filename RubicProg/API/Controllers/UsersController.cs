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

namespace RubicProg.Controllers
{
    /// <summary>
    /// User's controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        /// <summary>
        /// Регистрирует пользователя в приложение и возвращает информацию о нём
        /// </summary>
        /// <param name="email">Почта пользователя</param>
        /// <param name="nickname">Ник пользователя</param>
        /// <param name="password">Пароль</param>
        /// <param name="IsBoy">Пол</param>
        /// <param name="Name">Имя пользователя</param>
        /// <param name="Surname">Фамилия пользователя</param>
        [ProducesResponseType(typeof(UserInformationDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost("Registration")]
        public async Task<ActionResult<UserInformationDto>> RegistrationUser(UserRegistrDto userRegistrDto)
        {
            UserRegistrBlo userRegistrBlo = _mapper.Map<UserRegistrBlo>(userRegistrDto);
            UserInformationBlo userInformationBlo;

            try
            {
                userInformationBlo = await _userService.RegistrationUser(userRegistrBlo);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }

            return Created("", ConvertToUserInformationDto(userInformationBlo));
        }

        /// <summary>
        /// Аутентифицирует пользователя в приложение и возвращает информацию о нём
        /// </summary>
        /// <param name="email">Почта пользователя</param>
        /// <param name="password">Пароль</param>
        [ProducesResponseType(typeof(UserInformationDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost("Authentication")]
        public async Task<ActionResult<UserInformationDto>> AuthenticationUser(UserIdentityDto userIdentityDto)
        {
            UserIdentityBlo userIdentityBlo = _mapper.Map<UserIdentityBlo>(userIdentityDto);
            UserInformationBlo userInformationBlo;

            try
            {
                userInformationBlo = await _userService.AuthenticationUser(userIdentityBlo);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(ConvertToUserInformationDto(userInformationBlo));
        }

        /// <summary>
        /// Возвращает информацию о пользователе приложения
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        [ProducesResponseType(typeof(UserInformationDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("[action]/{userId}")]
        public async Task<ActionResult<UserInformationDto>> GetUser(int userId)
        {
            UserInformationBlo userInformationBlo;

            try
            {
                userInformationBlo = await _userService.GetUser(userId);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok(ConvertToUserInformationDto(userInformationBlo));
        }

        /// <summary>
        /// Обновляет информацию пользователя приложения
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="userUpdateDto">Объект обновления информации пользователя</param>
        [ProducesResponseType(typeof(UserInformationDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPatch("[action]/{userId}")]
        public async Task<ActionResult<UserInformationDto>> UpdateUser([FromRoute] int userId, [FromBody] UserUpdateDto userUpdateDto)
        {
            UserUpdateBlo userUpdateBlo = _mapper.Map<UserUpdateBlo>(userUpdateDto);
            UserInformationBlo userInformationBlo;

            try
            {
                userInformationBlo = await _userService.UpdateUser(userId, userUpdateBlo);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok(ConvertToUserInformationDto(userInformationBlo));

        }

        /// <summary>
        /// Обновляет только аватарку пользователя приложения
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="userUpdateAvatarDto">Объект аватарки пользователя</param>
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPatch("[action]/{userId}")]
        public async Task<ActionResult<string>> UpdateAvatar([FromRoute] int userId, [FromBody] UserUpdateAvatarDto userUpdateAvatarDto)
        {
            try
            {
                return Ok(await _userService.UpdateAvatar(userId, userUpdateAvatarDto.AvatarUrl));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Проверяет, существует ли пользователь с указанным id
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [HttpGet("[action]/{userId}")]
        public async Task<ActionResult<bool>> DoesExistUser(int userId)
        {
            return Ok(await _userService.DoesExistUser(userId));
        }

        /// <summary>
        /// Возвращает получилось ли удалить юзера с указанным id или нет
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("[action]/{userId}")]
        public async Task<ActionResult<bool>> DeleteUser(int userId)
        {
            try
            {
                return Ok(await _userService.DeleteUser(userId));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        private UserInformationDto ConvertToUserInformationDto(UserInformationBlo userInformationBlo)
        {
            if (userInformationBlo == null)
                throw new ArgumentNullException(nameof(userInformationBlo));

            UserInformationDto userInformationDto = _mapper.Map<UserInformationDto>(userInformationBlo);
            return userInformationDto;
        }




        /// <summary>
        /// Добавляет тренировку и возвращает информацию о нём
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
                workoutInformationBlo = await _userService.AddWorkoutPlan(workoutPlanAddBlo);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            return Created("", ConvertToWorkoutInformationDto(workoutInformationBlo));
        }

        /// <summary>
        /// Обновляет информацию тренировки
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
                workoutInformationBlo = await _userService.UpdateWorkoutPlan(workoutPlanId, workoutPlanUpdateBlo);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok(ConvertToWorkoutInformationDto(workoutInformationBlo));

        }

        /// <summary>
        /// Возвращает тренировкe с указанным id
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
                workoutInformationBlo = await _userService.GetWorkoutPlan(workoutPlanId);
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
                workoutInformationBlos = await _userService.GetAllWorkoutPlans(userId, count, skipCount);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok(ConvertToListWorkoutInformationDto(workoutInformationBlos));
        }

        /// <summary>
        /// Проверяет, существует ли тренировка с указанным id
        /// </summary>
        /// <param name="workoutPlanId">Идентификатор пользователя</param>
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [HttpGet("[action]/{workoutPlanId}")]
        public async Task<ActionResult<bool>> DoesExistWorkout(int workoutPlanId)
        {
            return Ok(await _userService.DoesExistWorkout(workoutPlanId));
        }

        /// <summary>
        /// Возвращает получилось ли удалить тренировку с указанным id или нет
        /// </summary>
        /// <param name="workoutPlanId">Идентификатор тренировки</param>
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("[action]/{workoutPlanId}")]
        public async Task<ActionResult<bool>> DeleteWorkoutPlan(int workoutPlanId)
        {
            try
            {
                return Ok(await _userService.DeleteWorkoutPlan(workoutPlanId));
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
