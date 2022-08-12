using BuberBreadkfast.WebApi.Models;

namespace BuberBreadkfast.WebApi.Services;

public class BreakfastService : IBreakfastService
{
    public static readonly Dictionary<Guid, Breakfast> _breakfasts = new();

    public void CreateBreakfast(Breakfast breakfast)
    {
        _breakfasts.Add(breakfast.Id, breakfast);
    }

    public void DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);
    }

    public Breakfast GetBreakfast(Guid id)
    {
        return _breakfasts[id];
    }

    public void UpsertBreakfast(Breakfast breakfast)
    {
        _breakfasts[breakfast.Id] = breakfast;
    }
}