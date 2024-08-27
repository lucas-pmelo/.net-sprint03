using Microsoft.AspNetCore.Mvc;
using Sprint03.adapter.input.dto;
using Sprint03.domain.model;
using Sprint03.infra.exception;

namespace Sprint03.app.controller
{
    [ApiController]
    [Route("api/agreement")]
    public class AgreementController : ControllerBase
    {
        private readonly IAgreementAdapter _agreementAdapter;

        public AgreementController(IAgreementAdapter agreementAdapter)
        {
            _agreementAdapter = agreementAdapter;
        }

        [HttpGet("{id}")]
        public ActionResult<Agreement> FindById(string id)
        {
            try
            {
                var agreement = _agreementAdapter.FindById(id);
                return Ok(agreement);
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
        public ActionResult<List<Agreement>> List()
        {
            try
            {
                var agreements = _agreementAdapter.ListAll();
                return Ok(agreements);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult<Agreement> Create([FromBody] Agreement agreement)
        {
            try
            {
                _agreementAdapter.Create(agreement);
                return CreatedAtAction(nameof(FindById), new { id = agreement.Id }, agreement);
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
        public ActionResult<Agreement> Update(string id, [FromBody] Agreement agreement)
        {
            try
            {
                var updatedAgreement = _agreementAdapter.Update(id, agreement);
                return Ok(updatedAgreement);
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
                _agreementAdapter.Delete(id);
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
