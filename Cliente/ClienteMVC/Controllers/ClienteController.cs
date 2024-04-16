using ClienteMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace ClienteMVC.Controllers
{
    public class ClienteController : Controller
    {

        private readonly HttpClient _httpClient;

        public ClienteController(IHttpClientFactory httpClienteFactory)
        {
            _httpClient = httpClienteFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://www.crudoechsle.somee.com/api");
            //_httpClient.BaseAddress = new Uri("https://localhost:7231/api");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Cliente/ListarCliente");
            if (response.IsSuccessStatusCode)
            {
                var content= await response.Content.ReadAsStringAsync();
                var clientes = JsonConvert.DeserializeObject<IEnumerable<ClienteModel>>(content);

                return View("Index",clientes);
            }

            return View(new List<ClienteController>());
        }

    

        public IActionResult Create()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteModel cliente)
        {
            if (ModelState.IsValid)
            {
                var json =JsonConvert.SerializeObject(cliente);

                var content = new StringContent(json,Encoding.UTF8,"application/json");
                var response = await _httpClient.PostAsync("/api/Cliente/CrearCliente", content);


                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"Error al crear cliente");
                }
            }

            return View(cliente);
        }

    }
}
