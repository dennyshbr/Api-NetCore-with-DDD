using AutoMapper;
using CrossCutting.Mappings;
using System;
using Xunit;

namespace Api.Service.Test
{
    public abstract class BaseTestService
    {
        public IMapper Mapper { get; private set; }

        public BaseTestService()
        {
            Mapper = new AutoMapperFixture().GetMapper();
        }
    }

    public class AutoMapperFixture : IDisposable
    {
        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new DtoToEntityProfile());
            });

            return config.CreateMapper();
        }

        public void Dispose()
        {
            
        }
    }
}
