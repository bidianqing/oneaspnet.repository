using EntityFrameworkSample.Domain;
using Microsoft.AspNetCore.Mvc;
using OneAspNet.Repository.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IEfRepository<User> _userRepository;
        private readonly SampleDbContext _context;
        public ValuesController(IEfRepository<User> userRepository, SampleDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //_userRepository.Add(new Domain.User
            //{
            //    Name = "tom"
            //});
            //_userRepository.SaveChanges();

            //var user = _userRepository.Find(2);

            var user = _context.Set<User>().SingleOrDefault(u => u.Id == 2);
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
