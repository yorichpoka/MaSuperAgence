using MaSuperAgence.Api.Models.Dtos;
using MaSuperAgence.Api.Models.Entities.SQLServer;

namespace MaSuperAgence.Api.Models.Tools
{
  public static class Extension
  {
    public static void ExtUpdate(this Photo value, PhotoDto obj)
    {
      value.Content = obj.Content;
      value.FileName = obj.FileName;
      value.IdProperty = obj.IdProperty;
    }

    public static void ExtUpdate(this Property value, PropertyDto obj)
    {
      value.Category = obj.Category;
      value.Description = obj.Description;
      value.Price = obj.Price;
      value.Rooms = obj.Rooms;
      value.Surface = obj.Surface;
      value.Title = obj.Title;
    }

    public static void ExtUpdate(this User value, UserDto obj)
    {
      value.Login = obj.Login;
      value.Password = obj.Password;
    }

    public static int ExtConvertToInt32(this string value)
    {
      return
        int.Parse(value);
    }
  }
}
