using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RubicProg.API.Models;
using RubicProg.BusinessLogic.Core.Interfaces;
using RubicProg.BusinessLogic.Core.Models;
using Share.Exceptions;
using System.Threading.Tasks;

namespace RubicProg.Controllers
{
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

            return Created("", _mapper.Map<UserInformationDto>(userInformationBlo));
        }

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

            return Ok(_mapper.Map<UserInformationDto>(userInformationBlo));
        }

        [HttpGet("{userId}")]
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

            return Ok(_mapper.Map<UserInformationDto>(userInformationBlo));
        }

        [HttpPatch("[action]/{id}")]
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
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(_mapper.Map<UserInformationDto>(userInformationBlo));

        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<bool>> DoesExistUser(int id)
        {
            return await _userService.DoesExistUser(id);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            return await _userService.DeleteUser(id);
        }





        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<bool>> DoesExistWorkout(int id)
        {
            return await _userService.DoesExistWorkout(id);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<bool>> DeleteWorkoutPlan(int id)
        {
            return await _userService.DeleteWorkoutPlan(id);
        }
    }
}
