using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using STIN_Weather.Data;
using STIN_Weather.Services;
using System.Security.Claims;
using STIN_Weather.WeatherReportUtils;

namespace STIN_Weather.Endpoints;

[ApiController]
[Route("forecast")]
[Authorize]
public class WeatherController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly WeatherApi api;

    public WeatherController(UserManager<ApplicationUser> userManager,WeatherApi api)
    {
        _userManager = userManager;
        this.api = api;
    }

    // GET: forecast
    [HttpGet(Name = "GetForecast")]
    public async Task<IActionResult> Get(int id, bool historic = false)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        try
        {
            var applicationUser = await _userManager.FindByIdAsync(userId);
            var _LocationSelectionService = new LocationSelectionService(applicationUser,api);
            string response = await _LocationSelectionService.FromId(id, historic);
            return Content(response);
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
