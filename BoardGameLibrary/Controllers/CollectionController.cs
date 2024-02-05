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
using BoardGameLibrary.Services.CollectionService;

namespace BoardGameLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly BoardGameLibraryContext _context;
        private readonly IMapper _mapper;
        private readonly ICollectionService _collectionService;

        public CollectionsController(BoardGameLibraryContext context, IMapper mapper, ICollectionService collectionService)
        {
            _context = context;
            _mapper = mapper;
            _collectionService = collectionService;
        }

        // GET: api/Collections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionDto>>> GetCollections()
        {
            if (_context.Collections == null)
            {
                return NotFound();
            }
            return Ok(await _collectionService.GetAll());
        }

        [HttpGet("get-collections-for-user/{userId}")]
        public async Task<ActionResult<IEnumerable<CollectionDto>>> GetCollectionsForUser(Guid userId)
        {
            if (_context.Collections == null)
            {
                return NotFound();
            }
            return Ok(await _collectionService.GetCollectionsForUser(userId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionDto>> GetCollection(Guid id)
        {
            if (_context.Collections == null)
            {
                return NotFound();
            }

            var collectionDto = await _collectionService.GetCollectionById(id);
            return Ok(collectionDto);
        }

       

        [HttpPost("{userId}")]
        public async Task<ActionResult<Collection>> PostCollection(Guid userId, CollectionCreateDto collectionDto)
        {
            if (_context.Collections == null)
            {
                return Problem("Entity set 'MusicCollectionContext.Collections' is null.");
            }
            var collectionEntity = _mapper.Map<Collection>(collectionDto);
            collectionEntity.UserId = userId;

            _context.Collections.Add(collectionEntity);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollection", new { id = collectionEntity.Id }, collectionEntity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollection(Guid id)
        {
            if (_context.Collections == null)
            {
                return NotFound();
            }
            var collection = await _context.Collections.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }

            _context.Collections.Remove(collection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //private bool CollectionExists(Guid id)
        //{
        //    return (_context.Collections?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
