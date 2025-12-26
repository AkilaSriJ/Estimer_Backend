using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Model.Client;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Mapper
{
    public class ClientProfile: Profile
    {
        public ClientProfile()
        {   
            CreateMap<CreateClientDto, Client>();
            CreateMap<UpdateClientDto, Client>().ForAllMembers(opt=>opt.Condition((src,dest,srcMember) => srcMember != null));
            CreateMap<Client, ClientDto>();
        }
    }
}
