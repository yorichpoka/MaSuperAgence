using AutoMapper;
using MaSuperAgence.Api.Models.Dtos;
using MaSuperAgence.Api.Models.Entities.SQLServer;

namespace MaSuperAgence.Api.Models.Tools
{
  public sealed class MapperSingleton
  {
    private static IMapper instanceMapper = null;
    private static readonly object padlock = new object();

    private MapperSingleton()
    {
    }

    public static IMapper Instance
    {
      get {
        lock (padlock)
        {
          if (instanceMapper == null)
          {
            instanceMapper = new MapperConfiguration(cfg =>
            {
              #region User

              cfg.CreateMap<User, UserDto>()
                  .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                  .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                  .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password));
              cfg.CreateMap<UserDto, User>()
                              .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                              .ForMember(destination => destination.Login, opts => opts.MapFrom(source => source.Login))
                              .ForMember(destination => destination.Password, opts => opts.MapFrom(source => source.Password));

              #endregion User

              #region Photo

              cfg.CreateMap<Photo, PhotoDto>()
                  .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                  .ForMember(destination => destination.Content, opts => opts.MapFrom(source => source.Content))
                  .ForMember(destination => destination.FileName, opts => opts.MapFrom(source => source.FileName))
                  .ForMember(destination => destination.IdProperty, opts => opts.MapFrom(source => source.IdProperty));
              cfg.CreateMap<PhotoDto, Photo>()
                            .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                            .ForMember(destination => destination.Content, opts => opts.MapFrom(source => source.Content))
                            .ForMember(destination => destination.FileName, opts => opts.MapFrom(source => source.FileName))
                            .ForMember(destination => destination.IdProperty, opts => opts.MapFrom(source => source.IdProperty));

              #endregion Photo

              #region Property

              cfg.CreateMap<Property, PropertyDto>()
                    .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                    .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title))
                    .ForMember(destination => destination.Category, opts => opts.MapFrom(source => source.Category))
                    .ForMember(destination => destination.Surface, opts => opts.MapFrom(source => source.Surface))
                    .ForMember(destination => destination.Rooms, opts => opts.MapFrom(source => source.Rooms))
                    .ForMember(destination => destination.Description, opts => opts.MapFrom(source => source.Description))
                    .ForMember(destination => destination.Price, opts => opts.MapFrom(source => source.Price));
              cfg.CreateMap<PropertyDto, Property>()
                              .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                              .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.Title))
                              .ForMember(destination => destination.Category, opts => opts.MapFrom(source => source.Category))
                              .ForMember(destination => destination.Surface, opts => opts.MapFrom(source => source.Surface))
                              .ForMember(destination => destination.Rooms, opts => opts.MapFrom(source => source.Rooms))
                              .ForMember(destination => destination.Description, opts => opts.MapFrom(source => source.Description))
                              .ForMember(destination => destination.Price, opts => opts.MapFrom(source => source.Price));

              #endregion Property
            })
            .CreateMapper();
          }

          return instanceMapper;
        }
      }
    }
  }
}
