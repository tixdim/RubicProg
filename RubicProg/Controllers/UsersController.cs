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
        
        // проверено, всё робит
        [HttpPost("register")]
        public async Task<ActionResult<UserUpdateBlo>> RegistrationWithEmail(UserUpdateBlo userUpdateBlo)
        {
            UserUpdateBlo user;
            try
            {
                user = await _userService.RegistrationWithEmail(userUpdateBlo.Email, userUpdateBlo.Password, userUpdateBlo.NickName);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
 
            return Created("", user);
        }

        // проверено, всё робит
        [HttpPost("authorization")]
        public async Task<ActionResult<UserUpdateBlo>> AuthWithEmail(UserUpdateBlo userUpdateBlo)
        {
            UserUpdateBlo user;
            try
            {
                user = await _userService.AuthWithEmail(userUpdateBlo.Email, userUpdateBlo.Password);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            return Ok(user);
        }

        // проверено, всё робит
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

        // Проверено, всё робит
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<bool>> DoesExist(int id)
        {
            return await _userService.DoesExist(id);
        }

        // Проверено, всё робит
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

        // Нельзя проверить, так как мы нигде не добавляем юзер профиль
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

        // Нельзя проверить, так как мы нигде не добавляем воркаут профиль
        [HttpPatch("[action]/{userWhoProfileId}")]
        public async Task<ActionResult<WorkoutPlanBlo>> UpdateWorkoutPlanBlo([FromRoute] int userWhoProfileId, [FromBody] WorkoutPlanUpdateBlo workoutPlanUpdateBlo)
        {
            WorkoutPlanBlo user;
            try
            {
                user = await _userService.UpdateWorkoutPlan(userWhoProfileId, workoutPlanUpdateBlo);
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
