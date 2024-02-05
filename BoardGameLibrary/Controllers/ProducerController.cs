using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dtos;
using BoardGameLibrary.Services.ProducerService;
using BoardGameLibrary.Services.BoardGameService;

namespace BoardGameLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly BoardGameLibraryContext _context;
        private readonly IMapper _mapper;
        private readonly IProducerService _producerService;

        public ProducersController(BoardGameLibraryContext context, IMapper mapper, IProducerService producerService)
        {
            _context = context;
            _mapper = mapper;
            _producerService = producerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProducerDto>>> GetProducers()
        {
            if (_context.Producers == null)
            {
                return NotFound();
            }
            return Ok(await _producerService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProducerDto>> GetProducer(Guid id)
        {
            if (_context.Producers == null)
            {
                return NotFound();
            }
            var producer = await _context.Producers.FindAsync(id);

            if (producer == null)
            {
                return NotFound();
            }
            var producerDto = _mapper.Map<ProducerDto>(producer);
            return Ok(producerDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducer(Guid id, Producer producer)
        {
            if (id != producer.Id)
            {
                return BadRequest();
            }

            _context.Entry(producer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProducerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProducerDto>> PostProducer(ProducerCreateDto producerDto)
        {
            if (_context.Producers == null)
            {
                return Problem("Entity set 'BoardGameLibraryContext.Producers' is null.");
            }
            var producerEntity = await _producerService.AddProducer(producerDto);
            return CreatedAtAction("GetProducer", new { id = producerEntity.Id }, producerEntity);
        }

        private bool ProducerExists(Guid id)
        {
            return (_context.Producers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
