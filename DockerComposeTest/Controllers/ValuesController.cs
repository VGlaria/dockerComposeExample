using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DockerComposeTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public ValuesController(IConfiguration configuration, ILogger<ValuesController> logger )
        {
            _configuration = configuration;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values
        [HttpGet]
        [Route("GetValuesFromApi2")]
        public async Task<ActionResult<IEnumerable<string>>> GetValuesFromApi2()
        {
            _logger.LogDebug("Peticion recibida");
            var urlService = _configuration["Url"];

            _logger.LogDebug($"url in {urlService}");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{urlService}api2/values"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<string>>(apiResponse);
                }
            }
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
