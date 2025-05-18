using Microsoft.AspNetCore.Mvc;

namespace restApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProbaController : ControllerBase
    {
        private IProbaService _probaService;
        public ProbaController(IProbaService probaService)
        {
            _probaService = probaService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Proba>> GetAll()
        {
            var probes = _probaService.GetAll();
            if (probes == null || !probes.Any())
            {
                return NotFound("No probes found.");
            }
            return Ok(probes);
        }
        [HttpGet("{id}")]
        public ActionResult<Proba> GetById(string id)
        {
            try
            {
                var proba = _probaService.GetById(id);
                if (proba == null)
                {
                    return NotFound($"Proba with id {id} not found.");
                }
                return Ok(proba);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<Proba> Delete(string id)
        {
            try
            {
                var proba = _probaService.Delete(id);
                if (proba == null)
                {
                    return NotFound($"Proba with id {id} not found.");
                }
                return Ok(proba);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<string> Add([FromBody] ProbaDTO dto)
        {
            try
            {
                var id = _probaService.Add(dto.Nume, Enum.Parse<Categorie>(dto.Categorie));
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Update(string id, [FromBody] ProbaDTO dto)
        {
            try
            {
                var proba = _probaService.Update(id, dto.Nume, Enum.Parse<Categorie>(dto.Categorie));
                if(proba == null)
                {
                    return NotFound($"Proba with id {id} not found.");
                }
                return Ok(proba);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class ProbaDTO
    {
        public string Nume { get; set; }
        public string Categorie { get; set; }

        public ProbaDTO() { }

        public ProbaDTO(string nume, string categorie)
        {
            Nume = nume;
            Categorie = categorie;
        }
    }
}
