using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/reportingStructure")]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet("{employeeId}", Name = "getReportingStructureByEmployeeId")]
        public IActionResult GetReportingStructureByEmployeeId(String employeeId)
        {
            _logger.LogDebug($"Received reporting structure get request for '{employeeId}'");

            var employee = _employeeService.GetByIdWithDirectReports(employeeId);

            if (employee == null)
                return NotFound();

            var reportingStructure = new ReportingStructure(employee);

            return Ok(reportingStructure);
        }
    }
}
