using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RubicProg.API.Models;
using RubicProg.BusinessLogic.Core.Interfaces;
using RubicProg.BusinessLogic.Core.Models;
using Share.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RubicProg.API.Controllers
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
    }
}
