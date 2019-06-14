using MaSuperAgence.Api.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaSuperAgence.Api.Models.Dao
{
  public interface IPhotoDao
  {
    Task<PhotoDto> Create(PhotoDto obj);

    Task<IEnumerable<PhotoDto>> Create(PhotoDto[] obj);

    Task<PhotoDto> Read(int id);

    Task<IEnumerable<PhotoDto>> Read();

    Task Update(PhotoDto obj);

    Task<PhotoDto> Delete(int id);
  }
}
