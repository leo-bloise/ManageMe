using ManageMe.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Api.Controllers;

[ApiController]
[Route("[controller]")]
[TypeFilter<ExceptionFilter>]
public abstract class ManageMeController: ControllerBase;
