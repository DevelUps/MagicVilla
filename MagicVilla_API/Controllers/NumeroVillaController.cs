using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using MagicVilla_API.Modelos.villaDto;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepositorio _villaRepo;
        private readonly INumeroVillaRepositorio _NumeroRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepositorio villaRepo, 
                                                                            INumeroVillaRepositorio NumeroRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo= villaRepo;
            _mapper = mapper;
            _response = new();
            _NumeroRepo = NumeroRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("Optener Numeros Villas");

                IEnumerable<NumeroVilla> NumerovillaList = await _NumeroRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(NumerovillaList); ;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMensasges = new List<string>() { ex.ToString() };
            }
            return _response;
            
        }



        [HttpGet("id:int", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)

        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer Numero villa con Id: " + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso=false;
                    return BadRequest(_response);
                }
                //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
                var numeroVilla = await _NumeroRepo.Obtener(v => v.VillaNo == id);

                if (numeroVilla == null)
                {
                    _response.StatusCode=HttpStatusCode.NotFound;
                    _response.IsExitoso=false;
                    return NotFound(_response);
                }
                _response.Resultado =_mapper.Map<NumeroVillaDto>(numeroVilla);
                _response.StatusCode=(HttpStatusCode.OK);
                
               
            }
            catch (Exception ex)
            {

               _response.IsExitoso = false;
                _response.ErrorMensasges= new List<string>() {ex.ToString()};
            }
           return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearNumeroVilla([FromBody] NumeroVillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _NumeroRepo.Obtener(v => v.VillaNo == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("NombreExiste", "El numero de villa con ese Nombre ya existe!");
                    return BadRequest(ModelState);
                }
                if (await _villaRepo.Obtener(v=>v.Id == createDto.VillaId) == null)
                {
                    ModelState.AddModelError("Claveforanea", "El Id de la villa no ya existe!");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(createDto);

                modelo.FechaCreacion= DateTime.Now;
                modelo.FechaActualizacion= DateTime.Now;
                 await _NumeroRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.VillaNo}, _response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso = false;
                _response.ErrorMensasges = new List<string>() {ex.ToString() };
            }

            return _response;
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> DeleteNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }
                var Numerovilla = await _NumeroRepo.Obtener(v =>v.VillaNo == id);
                if (Numerovilla == null)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return NotFound(_response);
                }
                await _NumeroRepo.Remover(Numerovilla);

                _response.StatusCode=HttpStatusCode.NoContent;
                
                return Ok(_response);
            }
            catch (Exception ex)
            {

               _response.IsExitoso=false;
               _response.ErrorMensasges = new List<string>() {ex.ToString() };
            }
            return BadRequest(_response);
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto updateDto)
        {
            if (updateDto == null || id!= updateDto.VillaNo)
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest();
            }
            if (await _villaRepo.Obtener(v => v.Id == updateDto.VillaId) == null)
            {
                ModelState.AddModelError("Claveforanea", "El Id de la villa no  existe!");
                return BadRequest(ModelState);
            }
            NumeroVilla modelo = _mapper.Map<NumeroVilla>(updateDto);


            await _NumeroRepo.Actualizar(modelo);
            _response.StatusCode=HttpStatusCode.NoContent;

            return Ok(_response);

        }
      
    }
}   
