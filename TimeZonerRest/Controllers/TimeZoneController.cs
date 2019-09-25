using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace TimeZonerRest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TimeZoneController : ControllerBase
    {
        private readonly string basePath;

        public TimeZoneController(IHostingEnvironment env)
        {
            // Get path to the project
            basePath = env.ContentRootPath;
        }

        // GET api/TimeZone/GetCountryTime/Denmark
        [HttpGet("{country}")]
        public ActionResult<int> GetCountryTime(string country)
        {
            try
            {
                return TimeHelper.GetCountryTimeUTC(country, false, basePath);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        // GET api/TimeZone/GetCountryISO/dk
        [HttpGet("{country}")]
        public ActionResult<int> GetCountryISO(string country)
        {
            try
            {
                return TimeHelper.GetCountryTimeUTC(country, true, basePath);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}
