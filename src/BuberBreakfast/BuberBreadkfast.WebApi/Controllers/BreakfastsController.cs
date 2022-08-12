using BuberBreadkfast.Contracts.Breakfast;
using BuberBreadkfast.WebApi.Models;
using BuberBreadkfast.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreadkfast.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BreakfastsController : ControllerBase
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var breakfast = new Breakfast(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );

        // Save breakfast to database
        _breakfastService.CreateBreakfast(breakfast);

        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );

        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new {id = breakfast.Id},
            value: response
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        var breakfast = _breakfastService.GetBreakfast(id);

        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        var breakfast = new Breakfast(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );

        // Save breakfast to database
        _breakfastService.UpsertBreakfast(breakfast);

        // var response = new BreakfastResponse(
        //     breakfast.Id,
        //     breakfast.Name,
        //     breakfast.Description,
        //     breakfast.StartDateTime,
        //     breakfast.EndDateTime,
        //     breakfast.LastModifiedDateTime,
        //     breakfast.Savory,
        //     breakfast.Sweet
        // );

        // TODO: Return 201 if a new breakfast was created

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        _breakfastService.DeleteBreakfast(id);
        return NoContent();
    }
}