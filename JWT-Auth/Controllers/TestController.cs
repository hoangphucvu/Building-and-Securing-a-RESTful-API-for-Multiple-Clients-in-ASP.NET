using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JWT_Auth.Controllers
{
    public class TestController : Controller
    {
        [HttpPost]
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        public string getData()
        {
            return "success";
        }
    }
}