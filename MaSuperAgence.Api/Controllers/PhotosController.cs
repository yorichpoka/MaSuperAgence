using MaSuperAgence.Api.Models.Dao;
using MaSuperAgence.Api.Models.Dtos;
using MaSuperAgence.Api.Models.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MaSuperAgence.Api.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class PhotosController : ControllerBase
  {
    private readonly IPhotoDao _Dao;

    public PhotosController(IPhotoDao dao)
    {
      this._Dao = dao;
    }

    // GET api/photos
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

    // GET api/photos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        var values = await this._Dao.Read(id);

        if (values == null)
          throw new Exception();

        return
          File(
            values.Content,
              MimeTypeMap.GetMimeType(
                Path.GetExtension(values.FileName)
              ),
              values.FileName
            );
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

    // POST api/photos
    [HttpPost]
    public async Task<IActionResult> Post()
    {
      try
      {
        if (Request.Form.Files.Count == 0)
          throw new Exception("No files submited.");

        StringValues idProperty;
        if (!Request.Form.TryGetValue("IdProperty", out idProperty))
          throw new Exception("Missing parameter (IdProperty)!");

        List<PhotoDto> values = new List<PhotoDto>();

        foreach (var file in Request.Form.Files)
        {
          using (var stream = file.OpenReadStream())
          {
            using (var mStream = new MemoryStream())
            {
              stream.CopyTo(mStream);

              values.Add(
                new PhotoDto
                {
                  Content = mStream.ToArray(),
                  FileName = file.FileName,
                  IdProperty = idProperty.ToString().ExtConvertToInt32()
                }
              );
            }
          }
        }

        values = (await this._Dao.Create(values.ToArray())).ToList();

        if (values == null || values.Count == 0)
          throw new Exception();

        return StatusCode(201, values);
      }
      catch (Exception ex)
      {
        switch (ex.InnerException)
        {
          case Exception exception:
            return BadRequest(exception.Message);
          // Not InnerException
          default:
            return BadRequest(ex.Message);
        }
      }
    }

    // PUT api/photos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int? id, [FromBody] PhotoDto photo)
    {
      try
      {
        if (id.Value == 0)
          throw new Exception("The resource id must not be 0!");

        if (Request.Form.Files.Count == 0)
          throw new Exception("No files submited.");

        var file = Request.Form.Files[0];

        StringValues idProperty;
        if (!Request.Form.TryGetValue("IdProperty", out idProperty))
          throw new Exception("Missing parameter (IdProperty)!");

        using (var stream = file.OpenReadStream())
        {
          using (var mStream = new MemoryStream())
          {
            stream.CopyTo(mStream);

            await this._Dao.Update(
                    new PhotoDto
                    {
                      Id = id.Value,
                      Content = mStream.ToArray(),
                      FileName = file.FileName,
                      IdProperty = idProperty.ToString().ExtConvertToInt32()
                    }
            );
          }
        }

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
            return BadRequest(ex.Message);
        }
      }
    }

    // DELETE api/photos/5
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
