using Microsoft.AspNetCore.Mvc;
using Sprint03.adapter.input.dto;
using Sprint03.domain.model;
using Sprint03.infra.exception;

namespace Sprint03.app.controller
{
    [ApiController]
    [Route("api/unit")]
    public class UnitController : ControllerBase
    {
        private readonly IUnitAdapter _unitAdapter;

        public UnitController(IUnitAdapter unitAdapter)
        {
            _unitAdapter = unitAdapter;
        }

        [HttpGet("{id}")]
        public ActionResult<Unit> FindById(string id)
        {
            try
            {
                var unit = _unitAdapter.FindById(id);
                return Ok(unit);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidIdFormatException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult<List<Unit>> List()
        {
            try
            {
                var units = _unitAdapter.ListAll();
                return Ok(units);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<Unit> Create([FromBody] Unit unit)
        {
            try
            {
                _unitAdapter.Create(unit);
                return CreatedAtAction(nameof(FindById), new { id = unit.Id }, unit);
            }
            catch (InvalidException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Unit> Update(string id, [FromBody] Unit unit)
        {
            try
            {
                var updatedUnit = _unitAdapter.Update(id, unit);
                return Ok(updatedUnit);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidIdFormatException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _unitAdapter.Delete(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidIdFormatException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
