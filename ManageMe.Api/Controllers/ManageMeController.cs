using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ManageMeController: ControllerBase;
