using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApp.Dtos.Character;
using webApp.Models;

namespace webApp.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private IMapper _mapper;
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character(){ Id =1 , Name = "steven" }
        };

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>> { };
            Character character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = (characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>> { };
            serviceResponse.Data = (characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetSingleCharacter(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto> { };
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto character) {
            ServiceResponse<GetCharacterDto> serviceResposne = new ServiceResponse<GetCharacterDto>();

            try { 
                Character updatedCharacter = characters.FirstOrDefault(c => c.Id == character.Id);
                updatedCharacter.Name = character.Name;
                updatedCharacter.HitPoints = character.HitPoints;
                updatedCharacter.Strength = character.Strength;
                updatedCharacter.Defense = character.Defense;
                updatedCharacter.Intelligence = character.Intelligence;
                updatedCharacter.Class = character.Class;
                serviceResposne.Data = _mapper.Map<Character, GetCharacterDto>(updatedCharacter);
            } catch (Exception ex)
            {
                serviceResposne.Success = false;
                serviceResposne.Message = ex.Message;
            }

            return serviceResposne; 
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>> { };
            try
            {
                Character deleteCharacter = characters.FirstOrDefault(c => c.Id == id);
                characters.Remove(deleteCharacter);

                serviceResponse.Data = (characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            }
            catch (Exception ex) 
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
