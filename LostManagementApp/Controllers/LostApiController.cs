using LostManagementApp.Models;
using LostManagementApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LostManagementApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LostApiController : ControllerBase
    {
        private readonly LostService _lostService;
        public LostApiController(LostService lostService)
        {
            _lostService = lostService;
        }

        [HttpPost("GetLost")]
        public IActionResult GetLostList([FromBody] Lost lost)
        {
            if (lost == null)
            {
                return BadRequest("Invalid lost item data.");
            }
            var lostItems = _lostService.GetLostList(lost);
            if (lostItems == null || !lostItems.Any())
            {
                return NotFound("No lost items found.");
            }
            return Ok(lostItems);
        }

        [HttpPost("InsertLost")]
        public IActionResult InsertLost([FromBody] Lost lost)
        {
            if (lost == null)
            {
                return BadRequest("Invalid lost item data.");
            }
            _lostService.InsertLost(lost);
            return CreatedAtAction(nameof(GetLostList), new { id = lost.LostId }, lost);
        }

        [HttpPost("UpdateLost")]
        public IActionResult UpdateLost([FromBody] Lost lost)
        {
            if (lost == null || lost.LostId <= 0)
            {
                return BadRequest("Invalid lost item data.");
            }
            _lostService.UpdateLost(lost);
            return NoContent();
        }

        [HttpPost("DeleteLostIds")]
        public IActionResult DeleteLost([FromBody] List<int> lostIds)
        {
            /*
            if (lostId == null || lost.LostId <= 0)
            {
                return BadRequest("Invalid lost item data.");
            }
            */
            //var lost = _lostService.GetLost(lostId);
            _lostService.DeleteLostIds(lostIds);
            return NoContent();
        }

        [HttpPost("DeleteLost")]
        public IActionResult DeleteLost([FromBody] int lostId)
        {
            /*
            if (lostId == null || lost.LostId <= 0)
            {
                return BadRequest("Invalid lost item data.");
            }
            */
            //var lost = _lostService.GetLost(lostId);
            _lostService.DeleteLost(lostId);
            return NoContent();
        }
    }
}
