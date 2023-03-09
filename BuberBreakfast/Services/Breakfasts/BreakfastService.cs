using BuberBreakfast.Models;
using ErrorOr;
using BuberBreakfast.ServiceErrors;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    //map id to a breakfast
    private static readonly Dictionary<Guid, Breakfast> _breakfasts = new Dictionary<Guid, Breakfast>();

    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        //add to dictionary
        _breakfasts.Add(breakfast.Id, breakfast);

        return Result.Created;

    }

    //delete
    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        //try to get breakfast by Id
        if (_breakfasts.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }
        else
            return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast)
    {
        var IsNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);

        //add to dictionary
        _breakfasts[breakfast.Id] = breakfast;


        return new UpsertedBreakfast(IsNewlyCreated);
    }
}