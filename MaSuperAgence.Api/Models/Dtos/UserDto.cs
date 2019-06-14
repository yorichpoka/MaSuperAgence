using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaSuperAgence.Api.Models.Dtos
{
  public class UserDto : BaseDto
  {
    public string Login { get; set; }
    public string Password { get; set; }
  }
}
