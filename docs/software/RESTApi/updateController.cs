using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BD6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly PostgresContext context;
        public UpdateController(PostgresContext postgresContext)
        {
            context = postgresContext;
        }
        [HttpPost]
        public async Task<IActionResult> Back([FromBody] UpdateRequest request)
        {
            if (request.UpdatedAt.Kind != DateTimeKind.Utc)
            {
                request.UpdatedAt = DateTime.SpecifyKind(request.UpdatedAt, DateTimeKind.Utc);
            }

            var update = await context.Updates
                .Include(u => u.Dataset)
                .Where(u => u.Hash == request.UpdateHash)
                .FirstOrDefaultAsync();

            var datafile = await context.Datafiles
                .Where(f => f.UploadedAt == request.UpdatedAt)
                .FirstOrDefaultAsync();

            if (update == null || datafile == null)
            {
                return BadRequest("Update or datafile wasn't found");
            }

            update.Dataset.DatafileId = datafile.Id;
            update.UpdatetAt = DateTime.UtcNow;

            context.Updates.Update(update);
            context.Datasets.Update(update.Dataset);

            await context.SaveChangesAsync();
            return Ok("Update has been succsessfuly backup");
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAllUpdatesByDatasetIdDes(int id)
        {
            var updates = await context.Updates
                 .Where(u => u.DatasetId == id)
                 .OrderByDescending(u => u.DatasetId)
                 .ToListAsync();

            return Ok(UpdateResponse.ToResponse(updates));
        }
    }
}
