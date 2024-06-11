//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace WebAPI.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class AddedAttributeController : Controller
//    {
//        private readonly Context _context;

//        public AddedAttributeController(Context context)
//        {
//            _context = context;
//        }
//        [HttpPost]
//        public async Task<IActionResult> CreateAddedAttribute([FromBody] AddedAttribute addedAttribute)
//        {
//            // Проверка валидности модели
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            // Сохранение атрибута в базе данных
//            _context.AddedAttributes.Add(addedAttribute);
//            await _context.SaveChangesAsync();

//            // Возврат созданного атрибута
//            return CreatedAtAction(nameof(GetAddedAttribute), new { id = addedAttribute.IdAttribute }, addedAttribute);
//        }
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateAddedAttribute(int id, [FromBody] AddedAttribute addedAttribute)
//        {
//            // Получение атрибута из базы данных
//            var existingAddedAttribute = await _context.AddedAttributes.FindAsync(id);

//            // Если атрибут не найден, вернуть ошибку
//            if (existingAddedAttribute == null)
//            {
//                return NotFound();
//            }

//            // Обновление атрибута
//            existingAddedAttribute.NumberBlock = addedAttribute.NumberBlock;
//            existingAddedAttribute.NameAttribute = addedAttribute.NameAttribute;
//            existingAddedAttribute.ContentAttribute = addedAttribute.ContentAttribute;
//            existingAddedAttribute.IdCharacter = addedAttribute.IdCharacter;

//            await _context.SaveChangesAsync();

//            // Возврат обновленного атрибута
//            return Ok(existingAddedAttribute);
//        }
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteAddedAttribute(int id)
//        {
//            // Получение атрибута из базы данных
//            var addedAttribute = await _context.AddedAttributes.FindAsync(id);

//            // Если атрибут не найден, вернуть ошибку
//            if (addedAttribute == null)
//            {
//                return NotFound();
//            }

//            // Удаление атрибута
//            _context.AddedAttributes.Remove(addedAttribute);
//            await _context.SaveChangesAsync();

//            // Возврат подтверждения удаления
//            return NoContent();
//        }
//    }
//}
