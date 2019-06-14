using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaSuperAgence.Api.Models.Dtos
{
  public class PropertyDto : BaseDto
  {
    public string Title { get; set; }
    public string Category { get; set; }
    public string Surface { get; set; }
    public int? Rooms { get; set; }
    public string Description { get; set; }
    public int? Price { get; set; }
  }
}
