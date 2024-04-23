using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STIN_Weather.Services;

namespace STIN_Weather.Endpoints;
[ApiController]
[Route("forecast")]


[Authorize]
public class WeatherController : Controller
{
    private readonly LocationSelectionService _LocationSelectionService;
    public WeatherController (LocationSelectionService _LocationSelectionService)
    {
        this._LocationSelectionService = _LocationSelectionService;
    }
    // GET: forecast
    [HttpGet(Name = "GetForecast")]
    public async Task<ActionResult> Get(int id)
    {
        string response = await _LocationSelectionService.FromId(id);
        return Content(response);
    }

}
