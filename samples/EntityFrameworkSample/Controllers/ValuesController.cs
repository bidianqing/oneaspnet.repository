using EntityFrameworkSample.Domain;
using Microsoft.AspNetCore.Mvc;
using OneAspNet.Repository.EntityFramework;
using System.Collections.Generic;

namespace EntityFrameworkSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IEfRepository<User> _userRepository;
        public ValuesController(IEfRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var user = _userRepository.Find(1);
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
