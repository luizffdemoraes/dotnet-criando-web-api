using FilmesApi.Data;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
    

    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {

        private FilmeContext _context;

        public FilmeController(FilmeContext context) {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            // Console.WriteLine(filme.Titulo);
            // Console.WriteLine(filme.Duracao);
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new {id = filme.Id}, filme);
        }


        // IEnumerable<Filme>
        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes([FromQuery] int skip = 0, int take = 50)
        {
            return _context.Filmes.Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            return Ok(filme);
        }
    }
}
