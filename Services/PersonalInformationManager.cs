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
    public class PersonalInformationManager : IPersonalInformationService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        public PersonalInformationManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<PersonDto> CreateOnePerson(PersonDto person, string email)
        {
            var entity = _mapper.Map<PersonalInformation>(person);

            entity.UserId = await _manager.AppUser.GetUserIdByEmail(email);
            if (entity.UserId is null)
                throw new Exception("user üye olmamış");


            _manager.PersonalInformation.CreateOnePerson(entity);
            await _manager.SaveAsync();
            return _mapper.Map<PersonDto>(entity);
        }

        public async Task DeleteOnePerson(int id, bool trackChanges)
        {
            var entity = await _manager.PersonalInformation.GetOnePersonAsync(id,trackChanges);
            entity.IsActive = false;
            await _manager.SaveAsync();

        }

        public async Task<List<PersonalInformation>> GetAllPersonsAsync(bool trackChanges)
        {
            var persons = await _manager.PersonalInformation.GetAllPersonsAsync(trackChanges);
            return persons;
        }

        public async Task<PersonDto> GetOnePersonAsync(int id, bool trackChanges)
        {
            var person = await GetOnePersonAndCheckExists(id,trackChanges);
            return _mapper.Map<PersonDto>(person);
        }

        public async Task<int> GetPersonByUserId(string userid)
        {
            return await _manager.PersonalInformation.FindByCondition(x => x.UserId.Equals(userid), false).Select(y => y.PersonalInformationId).FirstOrDefaultAsync();
        }

        public async Task UpdateOnePerson(PersonDto person, int id)
        {
            var entity = await _manager.PersonalInformation.GetOnePersonAsync(id,false);
            if(entity is null)
                throw new ArgumentNullException(nameof(entity));

            entity = _mapper.Map<PersonalInformation>(person);
            _manager.PersonalInformation.Update(entity);
            await _manager.SaveAsync();

        }

        private async Task<PersonalInformation> GetOnePersonAndCheckExists(int id, bool trackChanges)
        {
            var entity = await _manager.PersonalInformation.GetOnePersonAsync(id, trackChanges);
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
            return entity;
        }
    }
}
