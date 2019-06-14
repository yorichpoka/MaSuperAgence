using MaSuperAgence.Api.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaSuperAgence.Api.Models.Dao
{
  public interface IPropertyDao
  {
    Task<PropertyDto> Create(PropertyDto obj);

    Task<PropertyDto> Read(int id);

    Task<IEnumerable<PropertyDto>> Read();

    Task Update(PropertyDto obj);

    Task<PropertyDto> Delete(int id);
  }
}
