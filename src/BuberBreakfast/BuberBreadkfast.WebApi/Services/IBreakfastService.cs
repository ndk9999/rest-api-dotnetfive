using BuberBreadkfast.WebApi.Models;
using ErrorOr;

namespace BuberBreadkfast.WebApi.Services;

public interface IBreakfastService
{
    ErrorOr<Created> CreateBreakfast(Breakfast breakfast);

    ErrorOr<Breakfast> GetBreakfast(Guid id);

    ErrorOr<UpsertedBreakfastResult> UpsertBreakfast(Breakfast breakfast);
    
    ErrorOr<Deleted> DeleteBreakfast(Guid id);
}