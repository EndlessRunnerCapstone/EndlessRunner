using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ScoreService.Contracts;

namespace ScoreService.Controllers
{
    [Route("api/[controller]")]
    public class ScoresController : Controller
    {
        [HttpGet]
        public IEnumerable<Score> Get(int? top, int? skip)
        {
            return new Score[] { new Score { User = "TestUser", Value = 100 }, new Score { User = "User2", Value = 30  }};
        }

        [HttpGet("{id}")]
        public Score Get(int position)
        {
            return new Score { User = "TestUser", Value = 100 };
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
