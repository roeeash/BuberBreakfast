using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Models;

public class Breakfast
{
    //enfoerce input correctnes
    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;

    public const int MinDescriptionLength = 5;
    public const int MaxDescriptionLength = 350;

    public const int MinDateTimeHour = 7;
    public const int MaxDateTimeHour = 10;

    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public DateTime LastModifiedDateTime { get; }
    public List<string> Savory { get; }
    public List<string> Sweet { get; }

    private Breakfast(
        Guid id,
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        DateTime lastModifiedDateTime,
        List<string> savory,
        List<string> sweet)

    //enforce invariants on Breakfast object
    {
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
        Savory = savory;
        Sweet = sweet;
    }

    public static ErrorOr<Breakfast> Create(
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        List<string> savory,
        List<string> sweet,
        Guid? id = null
    )
    {
        //list of possible errors
        List<Error> errors = new();

        //if invalid name
        if (name.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.Breakfast.InvalidName);
        }

        //if invalid description
        if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
        {
            errors.Add(Errors.Breakfast.InvalidDescription);
        }

        //check start date is before end date, and that they are on the same day
        if (startDateTime.Hour < MinDateTimeHour
        || endDateTime.Hour > MaxDateTimeHour)
        {
            errors.Add(Errors.Breakfast.InvalidDuration);
        }

        if (startDateTime.Date != endDateTime.Date
        || startDateTime >= endDateTime)
        {
            errors.Add(Errors.Breakfast.InvalidDateTimeGap);
        }

        //if list legnth is more than 0
        if (errors.Count > 0)
        {
            return errors;
        }

        return new Breakfast(
            id ?? Guid.NewGuid(),
            name,
            description,
            startDateTime,
            endDateTime,
            DateTime.UtcNow,
            savory,
            sweet
        );

    }

    public static ErrorOr<Breakfast> From(CreateBreakfastRequest request)
    {
        return Create(
                 request.Name,
                 request.Description,
                 request.StartDateTime,
                 request.EndDateTime,
                 request.Savory,
                 request.Sweet
             );
    }

    public static ErrorOr<Breakfast> From(Guid id, UpsertBreakfastRequest request)
    {
        return Create(
                 request.Name,
                 request.Description,
                 request.StartDateTime,
                 request.EndDateTime,
                 request.Savory,
                 request.Sweet,
                 id
             );
    }


}