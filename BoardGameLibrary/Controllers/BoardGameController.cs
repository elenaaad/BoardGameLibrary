using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using DAL.Data;
using DAL.Models;
using DAL.Models.Dtos;
using BoardGameLibrary.Services.BoardGameService;
using BoardGameLibrary.Services.BoardgameService;

namespace BoardGameLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        private readonly BoardGameLibraryContext _context;
        private readonly IMapper _mapper;
        private readonly IBoardGameService _boardGameService;
        public BoardGamesController(BoardGameLibraryContext context, IMapper mapper, IBoardGameService boardGameService)
        {
            _context = context;
            _mapper = mapper;
            _boardGameService = boardGameService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BoardGameDto>>> GetBoardGames()
        {
            if (_context.BoardGames == null)
            {
                return NotFound();
            }
            var boardGameDtos = await _boardGameService.GetAll();
            foreach (var boardGame in boardGameDtos)
            {
                boardGame.ProducerName = await _boardGameService.GetProducerName(boardGame);
            }

            return Ok(boardGameDtos);
        }
        [HttpGet("get-boardGames-for-collection/{collectionId}")]
        public async Task<ActionResult<List<BoardGameDto>>> GetBoardGamesForCollection(Guid collectionId)
        {
            if (_context.Collections == null)
            {
                return NotFound();
            }
            var boardGameDtos = await _boardGameService.GetBoardGamesForCollection(collectionId);
            foreach (var boardGame in boardGameDtos)
            {
                boardGame.ProducerName = await _boardGameService.GetProducerName(boardGame);
            }
            return Ok(boardGameDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BoardGameDto>> GetBoardGame(Guid id)
        {
            if (_context.BoardGames == null)
            {
                return NotFound();
            }
            var boardGameDto = await _boardGameService.GetBoardGameById(id);
            if (boardGameDto == null)
            {
                return NotFound();
            }
            boardGameDto.ProducerName = await _boardGameService.GetProducerName(boardGameDto);
            return Ok(boardGameDto);
        }


        [HttpPost("{Id}")]
        public async Task<ActionResult<BoardGameDto>> PostBoardGame(Guid producerId, BoardGameCreateDto boardGameCreateDto)
        {
            if (_context.BoardGames == null)
            {
                return Problem("Entity set 'BoardGameContext.BoardGames'  is null.");
            }
            var boardGameDto = await _boardGameService.AddBoardGame(boardGameCreateDto, producerId);

            return CreatedAtAction("GetBoardGame", new { id = boardGameDto.Id }, boardGameDto);
        }

        [HttpPost("add-boardgame-in-collection/{collectionId}/{boardgameId}")]
        public async Task<ActionResult> AddBoardGameInCollection(Guid collectionId, Guid boardGameId)
        {
            if (_context.BoardGames == null)
            {
                return Problem("Entity set 'BoardGameLibraryContext.BoardGames'  is null.");
            }

            await _boardGameService.AddBoardGameInCollection(collectionId, boardGameId);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoardGame(Guid id)
        {
            if (_context.BoardGames == null)
            {
                return NotFound();
            }
            var boardGame = await _boardGameService.GetBoardGameById(id);
            if (boardGame == null)
            {
                return NotFound();
            }

            await _boardGameService.DeleteBoardGame(id);
            return NoContent();
        }

        private bool BoardGameExists(Guid id)
        {
            return (_context.BoardGames?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}