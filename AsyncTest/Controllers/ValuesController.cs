using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AsyncTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        // Synchronous
        public IEnumerable<string> Get()
        {
            Thread.Sleep(5000);
            return new string[] { "value1", "value2" };
        }

        // Asynchronous
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<string>> Get(int id)
        {
            await Task.Delay(5000);
            return new string[] { "value1", "value2" };
        }
    }
}
