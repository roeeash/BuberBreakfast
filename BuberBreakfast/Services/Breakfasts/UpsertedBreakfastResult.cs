
namespace BuberBreakfast.Services.Breakfasts;
//manages and organizes result for upsert - update if exists, insert otherwise

public record struct UpsertedBreakfast(bool IsNewlyCreated);