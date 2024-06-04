using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Block4Controller : Controller
    {
        private readonly Context _context;

        public Block4Controller(Context context)
        {
            _context = context;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlock(int id, [FromBody] Block4 block)
        {
            // Получение блока из базы данных
            var existingBlock = await _context.Block4s.FindAsync(id);

            // Если блок не найден, вернуть ошибку
            if (existingBlock == null)
            {
                return NotFound();
            }

            // Обновление блока
            existingBlock.Question1 = block.Question1;
            existingBlock.Question2 = block.Question2;
            existingBlock.Question3 = block.Question3;
            existingBlock.Question4 = block.Question4;
            existingBlock.Question5 = block.Question5;

            await _context.SaveChangesAsync();

            // Возврат обновленного блока
            return Ok(existingBlock);
        }
    }
}
