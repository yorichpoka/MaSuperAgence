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
  public class PropertyDaoImpl : IPropertyDao
  {
    private readonly DataBaseContext _Context;
    private readonly IMapper _Mapper;

    public PropertyDaoImpl(DataBaseContext context, IMapper mapper)
    {
      this._Context = context;
      this._Mapper = mapper;
    }

    public Task<PropertyDto> Create(PropertyDto obj)
    {
      return
        Task.Factory.StartNew<PropertyDto>(() =>
        {
          var entity = this._Mapper.Map<PropertyDto, Property>(obj);

          this._Context.Properties.Add(entity);

          this._Context.SaveChanges();

          return
            this._Mapper.Map<Property, PropertyDto>(entity);
        });
    }

    public Task<PropertyDto> Delete(int id)
    {
      return
        Task.Factory.StartNew<PropertyDto>(() =>
        {
          var entity = this._Context.Properties.Find(id);

          this._Context.Properties.Remove(entity);

          this._Context.SaveChanges();

          return
            this._Mapper.Map<Property, PropertyDto>(entity);
        });
    }

    public Task<PropertyDto> Read(int id)
    {
      return
        Task.Factory.StartNew<PropertyDto>(() =>
        {
          var entity = this._Context.Properties.Find(id);

          return
            this._Mapper.Map<Property, PropertyDto>(entity);
        });
    }

    public Task<IEnumerable<PropertyDto>> Read()
    {
      return
        Task.Factory.StartNew<IEnumerable<PropertyDto>>(() =>
        {
          return
            this._Context.Properties
              .ToList()
              .Select(l => this._Mapper.Map<Property, PropertyDto>(l));
        });
    }

    public Task Update(PropertyDto obj)
    {
      return
        Task.Factory.StartNew(() =>
        {
          var entity = this._Context.Properties.Find(obj.Id);

          entity.ExtUpdate(obj);

          this._Context.Properties.Update(entity);

          this._Context.SaveChanges();
        });
    }
  }
}
