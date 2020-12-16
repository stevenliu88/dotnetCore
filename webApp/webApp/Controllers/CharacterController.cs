using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApp.Dtos.Character;
using webApp.Models;
using webApp.Services.CharacterService;

namespace webApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
   

        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }



        [HttpGet("getAll")]
        public async Task<IActionResult> Get()
        {
            var characters = await _characterService.GetAllCharacters();
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id) 
        {
            var character = await _characterService.GetSingleCharacter(id);
            return Ok(character);
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter) 
        {
            var characters = await _characterService.AddCharacter(newCharacter);
            return Ok(characters);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto character) 
        {
            ServiceResponse<GetCharacterDto> updatedCharacter = await _characterService.UpdateCharacter(character);

            if (updatedCharacter.Success) { 
                return Ok(updatedCharacter);
            }
            return NotFound(updatedCharacter);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCharacter(int Id) 
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = await _characterService.DeleteCharacter(Id);
            if(serviceResponse.Data != null)
            { 
                return Ok(serviceResponse);
            }

            return NotFound(serviceResponse);
        }
    }
}
