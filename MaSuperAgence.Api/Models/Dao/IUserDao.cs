using MaSuperAgence.Api.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaSuperAgence.Api.Models.Dao
{
  public interface IUserDao
  {
    Task<UserDto> Create(UserDto obj);

    Task<UserDto> Read(int id);

    Task<UserDto> Read(string login, string password);

    Task<IEnumerable<UserDto>> Read();

    Task Update(UserDto obj);

    Task<UserDto> Delete(int id);
  }
}
