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
  public class UserDaoImpl : IUserDao
  {
    private readonly DataBaseContext _Context;
    private readonly IMapper _Mapper;

    public UserDaoImpl(DataBaseContext context, IMapper mapper)
    {
      this._Context = context;
      this._Mapper = mapper;
    }

    public Task<UserDto> Create(UserDto obj)
    {
      return
        Task.Factory.StartNew<UserDto>(() =>
        {
          var entity = this._Mapper.Map<UserDto, User>(obj);

          this._Context.Users.Add(entity);

          this._Context.SaveChanges();

          return
            this._Mapper.Map<User, UserDto>(entity);
        });
    }

    public Task<UserDto> Delete(int id)
    {
      return
        Task.Factory.StartNew<UserDto>(() =>
        {
          var entity = this._Context.Users.Find(id);

          this._Context.Users.Remove(entity);

          this._Context.SaveChanges();

          return
            this._Mapper.Map<User, UserDto>(entity);
        });
    }

    public Task<UserDto> Read(int id)
    {
      return
        Task.Factory.StartNew<UserDto>(() =>
        {
          var entity = this._Context.Users.Find(id);

          return
            this._Mapper.Map<User, UserDto>(entity);
        });
    }

    public Task<UserDto> Read(string login, string password)
    {
      return
        Task.Factory.StartNew<UserDto>(() =>
        {
          var entity = this._Context.Users.FirstOrDefault(l => l.Login == login && l.Password == password);

          return
            this._Mapper.Map<User, UserDto>(entity);
        });
    }

    public Task<IEnumerable<UserDto>> Read()
    {
      return
        Task.Factory.StartNew<IEnumerable<UserDto>>(() =>
        {
          return
            this._Context.Users
              .ToList()
              .Select(l => this._Mapper.Map<User, UserDto>(l));
        });
    }

    public Task Update(UserDto obj)
    {
      return
        Task.Factory.StartNew(() =>
        {
          var entity = this._Context.Users.Find(obj.Id);

          entity.ExtUpdate(obj);

          this._Context.Users.Update(entity);

          this._Context.SaveChanges();
        });
    }
  }
}
