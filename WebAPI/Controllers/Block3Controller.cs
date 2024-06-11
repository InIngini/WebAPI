//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace WebAPI.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class Block3Controller : Controller
//    {
//        private readonly Context _context;

//        public Block3Controller(Context context)
//        {
//            _context = context;
//        }
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateBlock(int id, [FromBody] Block3 block)
//        {
//            // Получение блока из базы данных
//            var existingBlock = await _context.Block3s.FindAsync(id);

//            // Если блок не найден, вернуть ошибку
//            if (existingBlock == null)
//            {
//                return NotFound();
//            }

//            // Обновление блока
//            existingBlock.Question1 = block.Question1;
//            existingBlock.Question2 = block.Question2;
//            existingBlock.Question3 = block.Question3;
//            existingBlock.Question4 = block.Question4;
//            existingBlock.Question5 = block.Question5;
//            existingBlock.Question6 = block.Question6;
//            existingBlock.Question7 = block.Question7;
//            existingBlock.Question8 = block.Question8;
//            existingBlock.Question9 = block.Question9;
//            existingBlock.Question10 = block.Question10;

//            await _context.SaveChangesAsync();

//            // Возврат обновленного блока
//            return Ok(existingBlock);
//        }
//    }
//}
