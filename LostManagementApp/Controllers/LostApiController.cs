using LostManagementApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LostManagementApp.DatabaseContext;
using LostManagementApp.Dao;

namespace LostManagementApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LostApiController : ControllerBase
    {
        private readonly LostContext _context;
        private readonly LostDao lostDao;
        public LostApiController(LostContext LostContext)
        {
            _context = LostContext;
            lostDao = new LostDao(_context);

        }

        [HttpPost("GetLost")]
        public IActionResult GetLostList([FromBody] LostDto lostDto)
        {
            if (lostDto == null)
            {
                return BadRequest("Invalid lost item data.");
            }
            var lostItems = lostDao.GetLostList(lostDto);
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
            lostDao.InsertLost(lost);
            return CreatedAtAction(nameof(GetLostList), new { id = lost.LostId }, lost);
        }

        [HttpPost("UpdateLost")]
        public IActionResult UpdateLost([FromBody] Lost lost)
        {
            if (lost == null || lost.LostId <= 0)
            {
                return BadRequest("Invalid lost item data.");
            }
            lostDao.UpdateLost(lost);
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
            //var lost = lostDao.GetLost(lostId);
            lostDao.DeleteLostIds(lostIds);
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
            //var lost = lostDao.GetLost(lostId);
            lostDao.DeleteLost(lostId);
            return NoContent();
        }
    }
}
