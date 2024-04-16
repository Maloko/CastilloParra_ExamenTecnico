using ClienteAPI.Data;
using ClienteAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace ClienteAPI.Controllers
{

    /// <summary>
    /// Controlador para administrar los clientes.
    /// </summary>
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly ClienteData _clientedData;

        public ClienteController(ClienteData clientedData)
        {
            _clientedData= clientedData;    
        }

        /// <summary>
        /// Obtiene lista de todos los clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListarCliente")]
        public async Task<IActionResult> Listar()
        {
            List<Cliente> lista = await _clientedData.Listar();
            return StatusCode(StatusCodes.Status200OK,lista);
        }


        /// <summary>
        /// Crea un cliente
        /// </summary>
        /// <returns></returns>
        [HttpPost("CrearCliente")]
        public async Task<IActionResult> Crear([FromBody] Cliente cliente)
        {
            bool rpta = await _clientedData.CrearCliente(cliente);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess=rpta});
        }


        /// <summary>
        /// Obtener cliente específico por Id (todos los datos más edad)
        /// </summary>
        /// <param name="idCliente">ID Cliente</param>
        /// <returns>El cliente correspondiente al ID proporcionado.</returns>
        [HttpGet("{idCliente}/GetCliente")]
        public async Task<IActionResult> GetCliente(int idCliente)
        {
            var resultado = await _clientedData.BuscarCliente(idCliente);
            return StatusCode(StatusCodes.Status200OK, resultado);
        }



        /// <summary>
        /// Obtiene lista de los 3 primeros clientes con mayor Edad
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListarClienteConMayorEdad")]
        public async Task<IActionResult> ListarMayorEdad()
        {
            List<Cliente> lista = await _clientedData.Listar();

            // Obtener los tres primeros clientes con mayor edad
            List<Cliente> listaConMayorEdad = lista.OrderByDescending(c => c.Edad).Take(3).ToList();
            return StatusCode(StatusCodes.Status200OK, listaConMayorEdad);
        }



    }
}
