using ManageMe.Api.Controllers.DTOs.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ManageMe.Api.Controllers;

[Authorize]
public class HealthController : ManageMeController
{
    [HttpGet]
    public ActionResult CollectData()
    {
        var process = Process.GetCurrentProcess();

        var metrics = new
        {
            Status = "Healthy",
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
        };

        return Ok(BaseApiResponse.WithData(metrics.Status, new Dictionary<string, dynamic>()
        {
            { "status", metrics }
        }));
    }
}
