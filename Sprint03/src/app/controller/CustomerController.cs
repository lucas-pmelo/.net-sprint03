using Microsoft.AspNetCore.Mvc;
using Sprint03.adapter.input.dto;
using Sprint03.domain.model;
using Sprint03.infra.exception;

namespace Sprint03.app.controller
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAdapter _customerAdapter;

        public CustomerController(ICustomerAdapter customerAdapter)
        {
            _customerAdapter = customerAdapter;
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> FindById(string id)
        {
            try
            {
                var customer = _customerAdapter.FindById(id);
                return Ok(customer);
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

        [HttpPost]
        public ActionResult<Customer> Create([FromBody] Customer customer)
        {
            try
            {
                _customerAdapter.Create(customer);
                return CreatedAtAction(nameof(FindById), new { id = customer.Id }, customer);
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
        public ActionResult<Customer> Update(string id, [FromBody] Customer customer)
        {
            try
            {
                var updatedCustomer = _customerAdapter.Update(id, customer);
                return Ok(updatedCustomer);
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
                _customerAdapter.Delete(id);
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
