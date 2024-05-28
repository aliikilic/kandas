using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RecepientManager : IRecepientService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public RecepientManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task CreateRecepient(CreateRecepientDto entity)
        {
            var recepient = _mapper.Map<Recepient>(entity);
            _manager.Recepient.CreateRecepient(recepient);
            
            await _manager.SaveAsync();
        }

        public async Task<List<Recepient>> GetAllRecepients(bool trackChanges)
        {
            return await _manager.Recepient.GetAllRecepients(trackChanges);
        }

        public async Task<int> GetRecepientIdByTCKN(string tckn)
        {
            int id = await _manager.Recepient.FindByCondition(x=> x.TcNo.Equals(tckn),false).Select(x=>x.Id).FirstOrDefaultAsync();
            return id;
        }
    }
}
