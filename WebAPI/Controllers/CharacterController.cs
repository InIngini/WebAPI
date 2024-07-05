using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("User/Book/[controller]")]
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
            { 
                IdCharacter=character.IdCharacter,
                Name = String.Empty,
                Question1 = String.Empty,
                Question2 = String.Empty,
                Question3 = String.Empty,
                Question4 = String.Empty,
                Question5 = String.Empty,
                Question6 = String.Empty,

            };
            _context.Block1s.Add(block1);
            Block2 block2 = new Block2()
            { 
                IdCharacter = character.IdCharacter,
                Question1 = String.Empty,
                Question2 = String.Empty,
                Question3 = String.Empty,
                Question4 = String.Empty,
                Question5 = String.Empty,
                Question6 = String.Empty,
                Question7 = String.Empty,
                Question8 = String.Empty,
                Question9 = String.Empty,
            };
            _context.Block2s.Add(block2);
            Block3 block3 = new Block3()
            { 
                IdCharacter = character.IdCharacter,
                Question1 = String.Empty,
                Question2 = String.Empty,
                Question3 = String.Empty,
                Question4 = String.Empty,
                Question5 = String.Empty,
                Question6 = String.Empty,
                Question7 = String.Empty,
                Question8 = String.Empty,
                Question9 = String.Empty,
                Question10 = String.Empty,
            };
            _context.Block3s.Add(block3);
            Block4 block4 = new Block4()
            { 
                IdCharacter = character.IdCharacter,
                Question1 = String.Empty,
                Question2 = String.Empty,
                Question3 = String.Empty,
                Question4 = String.Empty,
                Question5 = String.Empty,
            };
            _context.Block4s.Add(block4);

            await _context.SaveChangesAsync();


            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(character, options);

            // Возврат созданного персонажа в виде JSON
            return CreatedAtAction(nameof(GetCharacter), new { id = character.IdCharacter }, json);
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
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string json = JsonSerializer.Serialize(existingCharacter, options);

            return Ok(json);
        }


        //вспомогательный тип
        public class CharacterWithBlocks
        {
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

            // Удаление атрибутов
            var attributes = _context.AddedAttributes.Where(aa => aa.IdCharacter == id);
            _context.AddedAttributes.RemoveRange(attributes);
            await _context.SaveChangesAsync();

            // Удалить все изображения
            var gallery = _context.Galleries.Where(aa => aa.IdCharacter == id);

            // Перебрать найденные записи
            foreach (var item in gallery)
            {
                // Получить значение idPicture из текущей записи
                var idPicture = item.IdPicture;

                // Удалить запись из таблицы Picture по полученному значению idPicture
                var picture = _context.Pictures.Find(idPicture);
                if (picture != null)
                {
                    _context.Pictures.Remove(picture);
                }

                // Удалить текущую запись из таблицы Gallery
                _context.Galleries.Remove(item);
            }

            var pictureAvatar = _context.Pictures.Find(character.IdPicture);
            if (pictureAvatar != null)
            {
                _context.Pictures.Remove(pictureAvatar);
            }

            // Сохранить изменения в базе данных
            await _context.SaveChangesAsync();



            // Удаление связи
            // Получение всех связей, связанных с указанным персонажем
            var connections = _context.Connections.Where(c => c.IdCharacter1 == character.IdCharacter || c.IdCharacter2 == character.IdCharacter);

            // Удаление всех связей, связанных с указанным персонажем
            _context.Connections.RemoveRange(connections);

            // Сохранение изменений в базе данных
            await _context.SaveChangesAsync();


            // Удаление событий
            // Получение всех событий, в которых участвует указанный персонаж
            var events = _context.Events.Where(e => e.IdCharacters.Contains(character));

            // Для каждого события проверяем, участвуют ли в нем еще какие-либо персонажи
            foreach (var e in events)
            {
                if (e.IdCharacters.Count() <= 1)
                {
                    // Если в событии участвует только указанный персонаж, удаляем его
                    _context.Events.Remove(e);
                }
            }

            // Сохранение изменений в базе данных
            await _context.SaveChangesAsync();



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
            var character = await _context.Characters
                .Where(c => c.IdCharacter == id)
                .Select(c => new
                {
                    NameCharacter = c.Block1.Name,
                    IdPicture = c.IdPicture,
                    Block1 = new
                    {
                        c.Block1.Name,
                        c.Block1.Question1,
                        c.Block1.Question2,
                        c.Block1.Question3,
                        c.Block1.Question4,
                        c.Block1.Question5,
                        c.Block1.Question6
                    },
                    Block2 = new
                    {
                        c.Block2.Question1,
                        c.Block2.Question2,
                        c.Block2.Question3,
                        c.Block2.Question4,
                        c.Block2.Question5,
                        c.Block2.Question6,
                        c.Block2.Question7,
                        c.Block2.Question8,
                        c.Block2.Question9
                    },
                    Block3 = new
                    {
                        c.Block3.Question1,
                        c.Block3.Question2,
                        c.Block3.Question3,
                        c.Block3.Question4,
                        c.Block3.Question5,
                        c.Block3.Question6,
                        c.Block3.Question7,
                        c.Block3.Question8,
                        c.Block3.Question9,
                        c.Block3.Question10
                    },
                    Block4 = new
                    {
                        c.Block4.Question1,
                        c.Block4.Question2,
                        c.Block4.Question3,
                        c.Block4.Question4,
                        c.Block4.Question5
                    },
                    Attributes = c.AddedAttributes.Select(a => new
                    {
                        a.IdAttribute,
                        a.NumberBlock,
                        a.NameAttribute,
                        a.ContentAttribute
                    }),

                })
                .FirstOrDefaultAsync();

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        //Получить всех персонажей
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCharacter([FromBody] int id)
        {

            var characters = _context.Characters
                .Where(c => c.IdBook == id)
                .Select(c => new
                {
                    c.IdCharacter,
                    NameCharacter = c.Block1.Name,
                    IdPicture = c.IdPicture
                })
                .AsEnumerable();

            return Ok(characters);
        }

        ////////////////////////////Для добавленного атрибута//////////////////////////////
        /////Создать атрибут
        [HttpPost("{id}/addedattribute")]
        public async Task<IActionResult> CreateAddedAttribute(int id, [FromBody] AAData aa)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AddedAttribute addedAttribute = new AddedAttribute()
            { 
                IdCharacter = id,
                NameAttribute = aa.NameAttribute,
                NumberBlock = aa.NumberBlock,
                ContentAttribute = String.Empty,
            };

            // Сохранение атрибута в базе данных
            _context.AddedAttributes.Add(addedAttribute);
            await _context.SaveChangesAsync();

            // Возврат созданного атрибута
            return CreatedAtAction(nameof(GetAddedAttribute), new { id = addedAttribute.IdAttribute }, addedAttribute);
        }
        //вспомогательный тип
        public class AAData
        {
            public int NumberBlock { get; set; }
            public string NameAttribute { get; set; }
        }
        //Изменить атрибут
        [HttpPut("{idc}/addedattribute/{ida}")]
        public async Task<IActionResult> UpdateAddedAttribute(int ida, [FromBody] string content)
        {
            // Получение атрибута из базы данных
            var existingAddedAttribute = await _context.AddedAttributes.FindAsync(ida);

            // Если атрибут не найден, вернуть ошибку
            if (existingAddedAttribute == null)
            {
                return NotFound();
            }

            // Обновление атрибута
            existingAddedAttribute.ContentAttribute = content;

            await _context.SaveChangesAsync();

            // Возврат обновленного атрибута
            return Ok(existingAddedAttribute);
        }
        //Удалить атрибут
        [HttpDelete("{idc}/addedattribute/{ida}")]
        public async Task<IActionResult> DeleteAddedAttribute(int ida)
        {
            // Получение атрибута из базы данных
            var addedAttribute = await _context.AddedAttributes.FindAsync(ida);

            // Если атрибут не найден, вернуть ошибку
            if (addedAttribute == null)
            {
                return NotFound();
            }

            // Удаление атрибута
            _context.AddedAttributes.Remove(addedAttribute);
            await _context.SaveChangesAsync();

            // Возврат подтверждения удаления
            return NoContent();
        }
        //Получить атрибут
        [HttpGet("addedattribute/{id}")]
        public async Task<IActionResult> GetAddedAttribute(int id)
        {
            var addedAttribute = await _context.AddedAttributes.FindAsync(id);

            if (addedAttribute == null)
            {
                return NotFound();
            }

            return Ok(addedAttribute);
        }
    }
}
