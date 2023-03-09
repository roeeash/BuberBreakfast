using ErrorOr;

namespace BuberBreakfast.ServiceErrors;

//class for service errors
public static class Errors
{

    //errors for breakfast
    public static class Breakfast
    {

        //not found error, errorcode and decsription
        public static Error NotFound => Error.NotFound
        (
            code: "breakfast.NotFound",
            description: "Breakfast Not Found"
        );

        //invalid name
        public static Error InvalidName => Error.Validation
        (
            code: "breakfast.InvalidName",
            description: $"Breakfast name cannot be less than {Models.Breakfast.MinNameLength} " +
            $"characters long or more than {Models.Breakfast.MaxNameLength} characters long"
        );
        //invalid description
        public static Error InvalidDescription => Error.Validation
        (
            code: "breakfast.InvalidDescription",
            description: $"Breakfast description cannot be less than {Models.Breakfast.MinDescriptionLength} " +
            $"characters long or more than {Models.Breakfast.MaxDescriptionLength} characters long"
        );

        //invalid duration
        public static Error InvalidDuration => Error.Validation
        (
            code: "breakfast.InvalidDuration",
            description: $"Breakfast cannot start before {Models.Breakfast.MinDateTimeHour} " +
            $"or end after {Models.Breakfast.MaxDateTimeHour}"
        );

        //invalid duration
        public static Error InvalidDateTimeGap => Error.Validation
        (
            code: "breakfast.InvalidDateTimeGap",
            description: $"Breakfast time gap issue: either it ends before it starts," +
            $"or the times are in different dates"
        );
    }
}