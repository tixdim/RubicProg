using Microsoft.AspNetCore.Mvc;
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
        public readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<UserUpdateBlo>> RegistrationWithEmail(UserUpdateBlo userUpdateBlo)
        {
            UserUpdateBlo user;
            try
            {
                user = await _userService.RegistrationWithEmail(userUpdateBlo.Email, userUpdateBlo.Password);
            }
            catch(BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            return Created("", user);
        }

        [HttpPost("authorization")]
        public async Task<ActionResult<UserUpdateBlo>> AuthWithEmail(UserUpdateBlo userUpdateBlo)
        {
            UserUpdateBlo user;
            try
            {
                user = await _userService.AuthWithEmail(userUpdateBlo.Email, userUpdateBlo.Password);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            return Ok(user);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserIdGetBlo>> Get(int userId)
        {
            UserIdGetBlo user;
            try
            {
                user = await _userService.Get(userId);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok(user);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<bool>> DoesExist(int id)
        {
            return await _userService.DoesExist(id);
        }

        [HttpPatch("[action]/{id}")]
        public async Task<ActionResult<UserUpdateBlo>> Update([FromRoute] int id, [FromBody] UserUpdateDobleBlo userUpdateDobleBlo )
        {
            UserUpdateBlo user;
            try
            {
                user = await _userService.Update(id, userUpdateDobleBlo);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            return user;

        }
        [HttpPatch("[action]/{userWhoProfileId}")]
        public async Task<ActionResult<UserProfileBlo>> UpdateUserProfile([FromRoute] int userWhoProfileId, [FromBody] UserProfileUpdateBlo userProfileUpdateBlo)
        {
            UserProfileBlo user;
            try
            {
                user = await _userService.UpdateUserProfile(userWhoProfileId, userProfileUpdateBlo);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            return user;
        }

        [HttpPatch("[action]/{userWhoProfileId}")]
        public async Task<ActionResult<WorkoutPlanBlo>> UpdateWorkoutPlanBlo([FromRoute] int userWhoProfileId, [FromBody] WorkoutPlanUpdateBlo workoutPlanUpdateBlo)
        {
            WorkoutPlanBlo user;
            try
            {
                user = await _userService.UpdateWorkoutPlanBlo(userWhoProfileId, workoutPlanUpdateBlo);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            return user;

        }
    }
}
