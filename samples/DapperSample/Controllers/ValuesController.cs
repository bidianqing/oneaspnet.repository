﻿using Dapper;
using Microsoft.AspNetCore.Mvc;
using OneAspNet.Repository.Dapper;
using System.Collections.Generic;

namespace DapperSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly DapperRepository _dapperRepository;
        public ValuesController(DapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _dapperRepository.Connection.Query<dynamic>("select * from [table]");
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
