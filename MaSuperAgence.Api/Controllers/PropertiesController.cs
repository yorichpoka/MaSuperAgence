using MaSuperAgence.Api.Models.Dao;
using MaSuperAgence.Api.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MaSuperAgence.Api.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class PropertiesController : ControllerBase
  {
    private readonly IPropertyDao _Dao;

    public PropertiesController(IPropertyDao dao)
    {
      this._Dao = dao;
    }

    // GET api/propertys
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var values = await this._Dao.Read();

        if (values == null)
          throw new Exception();

        return Ok(values);
      }
      catch (Exception ex)
      {
        switch (ex.InnerException)
        {
          case Exception exception:
            return BadRequest(exception.Message);
          // Not InnerException
          default:
            return NoContent();
        }
      }
    }

    // GET api/propertys/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        var values = await this._Dao.Read(id);

        if (values == null)
          throw new Exception();

        return Ok(values);
      }
      catch (Exception ex)
      {
        switch (ex.InnerException)
        {
          case Exception exception:
            return BadRequest(exception.Message);
          // Not InnerException
          default:
            return NoContent();
        }
      }
    }

    // POST api/propertys
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PropertyDto property)
    {
      try
      {
        if (property == null)
          throw new Exception("Missing parameter (property)!");

        var value = await this._Dao.Create(property);

        if (value == null || value.Id == 0)
          throw new Exception();

        return Created($"property/{value.Id}", value);
      }
      catch (Exception ex)
      {
        switch (ex.InnerException)
        {
          case Exception exception:
            return BadRequest(exception.Message);
          // Not InnerException
          default:
            return NoContent();
        }
      }
    }

    // PUT api/propertys/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int? id, [FromBody] PropertyDto property)
    {
      try
      {
        if (property == null)
          throw new Exception("Missing parameter (property)!");

        if (id.Value == 0)
          throw new Exception("The resource id must not be 0!");

        property.Id = id.Value;

        await this._Dao.Update(property);

        return Accepted();
      }
      catch (Exception ex)
      {
        switch (ex.InnerException)
        {
          case Exception exception:
            return BadRequest(exception.Message);
          // Not InnerException
          default:
            return NoContent();
        }
      }
    }

    // DELETE api/propertys/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
      try
      {
        if (id.Value == 0)
          throw new Exception("The resource id must not be 0!");

        await this._Dao.Delete(id.Value);

        return Ok();
      }
      catch (Exception ex)
      {
        switch (ex.InnerException)
        {
          case Exception exception:
            return BadRequest(exception.Message);
          // Not InnerException
          default:
            return NoContent();
        }
      }
    }
  }
}
