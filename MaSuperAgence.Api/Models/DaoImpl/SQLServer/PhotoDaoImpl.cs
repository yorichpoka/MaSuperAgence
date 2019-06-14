using AutoMapper;
using MaSuperAgence.Api.Models.Dao;
using MaSuperAgence.Api.Models.Dtos;
using MaSuperAgence.Api.Models.Entities.SQLServer;
using MaSuperAgence.Api.Models.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaSuperAgence.Api.Models.DaoImpl.SQLServer
{
  public class PhotoDaoImpl : IPhotoDao
  {
    private readonly DataBaseContext _Context;
    private readonly IMapper _Mapper;

    public PhotoDaoImpl(DataBaseContext context, IMapper mapper)
    {
      this._Context = context;
      this._Mapper = mapper;
    }

    public Task<PhotoDto> Create(PhotoDto obj)
    {
      return
        Task.Factory.StartNew<PhotoDto>(() =>
        {
          var entity = this._Mapper.Map<PhotoDto, Photo>(obj);

          this._Context.Photos.Add(entity);

          this._Context.SaveChanges();

          return
            this._Mapper.Map<Photo, PhotoDto>(entity);
        });
    }

    public Task<IEnumerable<PhotoDto>> Create(PhotoDto[] obj)
    {
      return
        Task.Factory.StartNew<IEnumerable<PhotoDto>>(() =>
        {
          var entities = obj.Select(l => this._Mapper.Map<PhotoDto, Photo>(l));

          this._Context.Photos.AddRange(entities);

          this._Context.SaveChanges();

          return
            this._Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoDto>>(entities);
        });
    }

    public Task<PhotoDto> Delete(int id)
    {
      return
        Task.Factory.StartNew<PhotoDto>(() =>
        {
          var entity = this._Context.Photos.Find(id);

          this._Context.Photos.Remove(entity);

          this._Context.SaveChanges();

          return
            this._Mapper.Map<Photo, PhotoDto>(entity);
        });
    }

    public Task<PhotoDto> Read(int id)
    {
      return
        Task.Factory.StartNew<PhotoDto>(() =>
        {
          var entity = this._Context.Photos.Find(id);

          return
            this._Mapper.Map<Photo, PhotoDto>(entity);
        });
    }

    public Task<IEnumerable<PhotoDto>> Read()
    {
      return
        Task.Factory.StartNew<IEnumerable<PhotoDto>>(() =>
        {
          return
            this._Context.Photos
              .ToList()
              .Select(l => new Photo
              {
                FileName = l.FileName,
                Id = l.Id,
                IdProperty = l.IdProperty
              })
              .Select(l => this._Mapper.Map<Photo, PhotoDto>(l));
        });
    }

    public Task Update(PhotoDto obj)
    {
      return
        Task.Factory.StartNew(() =>
        {
          var entity = this._Context.Photos.Find(obj.Id);

          entity.ExtUpdate(obj);

          this._Context.Photos.Update(entity);

          this._Context.SaveChanges();
        });
    }
  }
}
