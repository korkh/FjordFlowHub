using AutoMapper;
using FreightService.Data;
using FreightService.DTOs;
using FreightService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreightService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FreightsController : ControllerBase
    {
        private readonly FreightDbContext _context;
        private readonly IMapper _mapper;

        public FreightsController(FreightDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<FreightDto>>> GetAllFreights()
        {
            var freights = await _context
                .Freights.Include(x => x.Cargo)
                .OrderBy(x => x.Cargo.Description)
                .ToListAsync();

            return _mapper.Map<List<FreightDto>>(freights);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FreightDto>> GetFreightById(Guid id)
        {
            var freight = await _context
                .Freights.Include(x => x.Cargo)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (freight == null)
            {
                return NotFound();
            }

            return _mapper.Map<FreightDto>(freight);
        }

        [HttpPost]
        public async Task<ActionResult<FreightDto>> CreateFreight(CreateFreightDto freightDto)
        {
            var freight = _mapper.Map<Freight>(freightDto);
            // Temporary hardcoded seller
            freight.Seller = "Test Seller";

            _context.Freights.Add(freight);

            var result = await _context.SaveChangesAsync() > 0; // Returns true if at least one row was affected

            if (!result)
                return BadRequest("Could not save changes to the DB");

            return CreatedAtAction(
                nameof(GetFreightById),
                new { freight.Id },
                _mapper.Map<FreightDto>(freight)
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFreight(Guid id, UpdateFreightDto updateDto)
        {
            // Fetch freight with its associated cargo
            var freight = await _context
                .Freights.Include(x => x.Cargo)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (freight == null)
                return NotFound("Cannot find your freight");

            // TODO: check seller == username

            // Manually update cargo properties
            // Using null-coalescing operator ?? to keep old values if DTO fields are null
            freight.Cargo.Description = updateDto.Description ?? freight.Cargo.Description;
            freight.Cargo.WeightKg =
                updateDto.WeightKg != 0 ? updateDto.WeightKg : freight.Cargo.WeightKg;
            freight.Cargo.PickupCity = updateDto.PickupCity ?? freight.Cargo.PickupCity;
            freight.Cargo.DeliveryCity = updateDto.DeliveryCity ?? freight.Cargo.DeliveryCity;

            // Save changes to the database
            var result = await _context.SaveChangesAsync() > 0;

            if (result)
                return Ok();

            return BadRequest("Problem saving changes");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFreight(Guid id)
        {
            // Find the freight entity
            var freight = await _context.Freights.FindAsync(id);

            if (freight == null)
                return NotFound();

            // TODO: check seller == username (when auth is ready)

            _context.Freights.Remove(freight);

            // Save changes and check result
            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
                return BadRequest("Could not update DB");

            return Ok();
        }
    }
}
