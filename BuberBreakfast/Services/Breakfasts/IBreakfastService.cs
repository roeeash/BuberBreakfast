using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;
public interface IBreakfastService
{
    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast);
    public ErrorOr<Breakfast> GetBreakfast(Guid Id);
    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast);
    public ErrorOr<Deleted> DeleteBreakfast(Guid id);

}