using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommercialActivitiesController : ControllerBase
    {
        private readonly ICommercialActivityService _commercialActivityService;
        public CommercialActivitiesController(ICommercialActivityService commercialActivityService)
        {
            _commercialActivityService = commercialActivityService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var activities = _commercialActivityService.GetAll();
            if(activities is not null)
            {
                return Ok(activities);
            }
            return BadRequest();
        }

        [HttpGet("get")]
        public IActionResult Get(int commercialActivityId)
        {
            var activity = _commercialActivityService.Get(commercialActivityId);
            if(activity is not null)
            {
                return Ok(activity);
            }
            return BadRequest();
        }

        [HttpGet("getallWithDto")]
        public IActionResult GetAll_With_CustomerCommercialActivityDto()
        {
            var activities = _commercialActivityService.GetAll_With_CustomerCommercialActivityDto();
            if(activities is not null)
            {
                return Ok(activities);
            }
            return BadRequest();
        }

        [HttpGet("getWithDto")]
        public IActionResult Get_With_CustomerCommercialActivityDto(int customerId)
        {
            var activity = _commercialActivityService.Get_With_CustomerCommercialActivityDto(customerId);
            if(activity is not null)
            {
                return Ok(activity);
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult Add(CommercialActivity commercialActivity)
        {
            var addedActivity = _commercialActivityService.Add(commercialActivity);
            if(addedActivity is not null)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Update(CommercialActivity commercialActivity)
        {
            var updatedActivity = _commercialActivityService.Update(commercialActivity);
            if (updatedActivity is not null)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(CommercialActivity commercialActivity)
        {
            var deletedActivity = _commercialActivityService.Delete(commercialActivity);
            if(deletedActivity is not null)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
