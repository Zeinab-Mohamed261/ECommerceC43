using AutoMapper;
using Domain.Models.IdentityModule;
using Shared.DataTransferObject.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    internal class IdentityProfile :Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address , AddressDto>().ReverseMap();
        }
    }
}
