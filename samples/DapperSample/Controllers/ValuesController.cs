﻿using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using OneAspNet.Repository.Dapper;
using System.Data.Common;

namespace DapperSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly ConnectionFactory _connectionFactory;
        public ValuesController(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {
            using DbConnection connection = _connectionFactory.CreateConnection();

            var user = new User
            {
                Name = "bidianqing"
            };

            var users = new User[]
            {
                new User { Name = "1" },
                new User { Name = "2" },
                new User { Name = "3" },
                new User { Name = "4" },
            };

            connection.Insert(users);

            var userList = connection.Query<User>("select * from tb_user");

            return userList;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
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
