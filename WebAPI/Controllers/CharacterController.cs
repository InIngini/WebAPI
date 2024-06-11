using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : Controller
    {
        private readonly Context _context;

        public CharacterController(Context context)
        {
            _context = context;
        }
        //Создание персонажа и блоков
        [HttpPost]
        public async Task<IActionResult> CreateCharacter([FromBody] BookCharacterData bookCharacterData)
        {
            Character character = new Character()
            { 
                IdBook = bookCharacterData.IdBook,
                IdPicture = bookCharacterData.IdPicture
            };

            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            // Сохранение блоков с персонажем
            Block1 block1 = new Block1()
            { IdCharacter=character.IdCharacter};
            _context.Block1s.Add(block1);
            Block2 block2 = new Block2()
            { IdCharacter = character.IdCharacter };
            _context.Block2s.Add(block2);
            Block3 block3 = new Block3()
            { IdCharacter = character.IdCharacter };
            _context.Block3s.Add(block3);
            Block4 block4 = new Block4()
            { IdCharacter = character.IdCharacter };
            _context.Block4s.Add(block4);

            // Возврат созданного персонажа
            return CreatedAtAction(nameof(GetCharacter), new { id = character.IdCharacter }, character);
        }
        //вспомогательный тип
        public class BookCharacterData
        {
            public int IdBook { get; set; }
            public int? IdPicture { get; set; }
        }
        // Изменение персонажа и блоков
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter(int id, [FromBody] CharacterWithBlocks characterWithBlocks)
        {
            // Получение персонажа из базы данных
            var existingCharacter = await _context.Characters.FindAsync(id);

            // Если персонаж не найден, вернуть ошибку
            if (existingCharacter == null)
            {
                return NotFound();
            }

            // Обновление персонажа
            existingCharacter.IdBook = characterWithBlocks.IdBook;
            existingCharacter.IdPicture = characterWithBlocks.IdPicture;
            // Обновление блоков
            var block1 = await _context.Block1s.FindAsync(id);
            block1.Name = characterWithBlocks.Name;
            block1.Question1 = characterWithBlocks.Block1Question1;
            block1.Question2 = characterWithBlocks.Block1Question2;
            block1.Question3 = characterWithBlocks.Block1Question3;
            block1.Question4 = characterWithBlocks.Block1Question4;
            block1.Question5 = characterWithBlocks.Block1Question5;
            block1.Question6 = characterWithBlocks.Block1Question6;
            var block2 = await _context.Block2s.FindAsync(id);
            block2.Question1 = characterWithBlocks.Block2Question1;
            block2.Question2 = characterWithBlocks.Block2Question2;
            block2.Question3 = characterWithBlocks.Block2Question3;
            block2.Question4 = characterWithBlocks.Block2Question4;
            block2.Question5 = characterWithBlocks.Block2Question5;
            block2.Question6 = characterWithBlocks.Block2Question6;
            block2.Question7 = characterWithBlocks.Block2Question7;
            block2.Question8 = characterWithBlocks.Block2Question8;
            block2.Question9 = characterWithBlocks.Block2Question9;
            var block3 = await _context.Block3s.FindAsync(id);
            block3.Question1 = characterWithBlocks.Block3Question1;
            block3.Question2 = characterWithBlocks.Block3Question2;
            block3.Question3 = characterWithBlocks.Block3Question3;
            block3.Question4 = characterWithBlocks.Block3Question4;
            block3.Question5 = characterWithBlocks.Block3Question5;
            block3.Question6 = characterWithBlocks.Block3Question6;
            block3.Question7 = characterWithBlocks.Block3Question7;
            block3.Question8 = characterWithBlocks.Block3Question8;
            block3.Question9 = characterWithBlocks.Block3Question9;
            block3.Question10 = characterWithBlocks.Block3Question10;
            var block4 = await _context.Block4s.FindAsync(id);
            block4.Question1 = characterWithBlocks.Block4Question1;
            block4.Question2 = characterWithBlocks.Block4Question2;
            block4.Question3 = characterWithBlocks.Block4Question3;
            block4.Question4 = characterWithBlocks.Block4Question4;
            block4.Question5 = characterWithBlocks.Block4Question5;

            await _context.SaveChangesAsync();

            // Возврат обновленного персонажа
            return Ok(existingCharacter);
        }


        //вспомогательный тип
        public class CharacterWithBlocks
        {
            public int IdCharacter { get; set; }
            public int IdBook { get; set; }
            public int? IdPicture { get; set; }
            public string Name { get; set; }
            public string Block1Question1 { get; set; }
            public string Block1Question2 { get; set; }
            public string Block1Question3 { get; set; }
            public string Block1Question4 { get; set; }
            public string Block1Question5 { get; set; }
            public string Block1Question6 { get; set; }
            public string Block2Question1 { get; set; }
            public string Block2Question2 { get; set; }
            public string Block2Question3 { get; set; }
            public string Block2Question4 { get; set; }
            public string Block2Question5 { get; set; }
            public string Block2Question6 { get; set; }
            public string Block2Question7 { get; set; }
            public string Block2Question8 { get; set; }
            public string Block2Question9 { get; set; }
            public string Block3Question1 { get; set; }
            public string Block3Question2 { get; set; }
            public string Block3Question3 { get; set; }
            public string Block3Question4 { get; set; }
            public string Block3Question5 { get; set; }
            public string Block3Question6 { get; set; }
            public string Block3Question7 { get; set; }
            public string Block3Question8 { get; set; }
            public string Block3Question9 { get; set; }
            public string Block3Question10 { get; set; }
            public string Block4Question1 { get; set; }
            public string Block4Question2 { get; set; }
            public string Block4Question3 { get; set; }
            public string Block4Question4 { get; set; }
            public string Block4Question5 { get; set; }
        }
        //Удаление персонажа и блоков
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            // Получение персонажа из базы данных
            var character = await _context.Characters.FindAsync(id);

            // Если персонаж не найден, вернуть ошибку
            if (character == null)
            {
                return NotFound();
            }

            // Удаление блоков
            var block1 = await _context.Block1s.FindAsync(id);
            _context.Block1s.Remove(block1);
            var block2 = await _context.Block2s.FindAsync(id);
            _context.Block2s.Remove(block2);
            var block3 = await _context.Block3s.FindAsync(id);
            _context.Block3s.Remove(block3);
            var block4 = await _context.Block4s.FindAsync(id);
            _context.Block4s.Remove(block4);

            // Удаление персонажа
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            // Возврат подтверждения удаления
            return NoContent();
        }
        

        //Получить персонажа
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }
    }
}
