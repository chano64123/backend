using AutoMapper;
using BackendPrueba.Models;
using BackendPrueba.Models.DTO;

namespace BackendPrueba {
    public class MappingConfig {
        public static MapperConfiguration RegisterMaps() {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<Client, ClientDTO>();
                config.CreateMap<ClientDTO, Client>();
                config.CreateMap<User, UserDTO>();
                config.CreateMap<UserDTO, User>();
            });
            return mappingConfig;
        }
    }
}
