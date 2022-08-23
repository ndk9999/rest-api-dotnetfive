using BuberBreadkfast.Contracts.Breakfast.Common;

namespace BuberBreadkfast.Contracts.Breakfast;

// public record CreateBreakfastRequest(
//     [MinLength(3), MaxLength(50)] string Name,
//     string Description,
//     [Future] DateTime StartDateTime,
//     [Future] DateTime EndDateTime,
//     List<string> Savory,
//     List<string> Sweet
// );

public record CreateBreakfastRequest(
    BreakfastDetails BreakfastDetails,
    List<string> Savory,
    List<string> Sweet
);