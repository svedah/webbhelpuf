using webbhelpuf.Helpers;
using webbhelpuf.PostModels;
using webbhelpuf.Services;
using webbhelpuf.Shared;

namespace webbhelpuf.Validators;

public static class EditAddressPostModelValidator
{

    public static EditAddressPostModel Sanitize(EditAddressPostModel input)
    {
        string sanitized_firstname = input.firstname.Trim();
        string sanitized_lastname = input.lastname.Trim();
        string sanitized_streetname = input.streetname.Trim();
        string sanitized_streetno = input.streetno.Trim();
        string sanitized_zipcode = input.zipcode.Trim();
        string sanitized_city = input.city.Trim();
        string sanitized_email = input.email.Trim();
        string sanitized_phone = (input.phone ?? string.Empty).Trim();

        EditAddressPostModel output = new EditAddressPostModel
        {
            firstname = sanitized_firstname,
            lastname = sanitized_lastname,
            streetname = sanitized_streetname,
            streetno = sanitized_streetno,
            zipcode = sanitized_zipcode,
            city = sanitized_city,
            email = sanitized_email,
            phone = sanitized_phone
        };

        return output;
    }

    public static bool Validate(EditAddressPostModel input)
    {
        return ValidateFirstName(input.firstname) &&
                ValidateLastName(input.lastname) &&
                ValidateStreetName(input.streetname) &&
                ValidateStreetNo(input.streetno) &&
                ValidateZipCode(input.zipcode) &&
                ValidateCity(input.city) &&
                ValidateEmail(input.email) &&
                ValidatePhone(input.phone) &&
                ValidateAdditionalInfo(input.addinfo);
    }

    private static bool ValidateFirstName(string input)
    {
        return input.Length >= 2;
    }

    private static bool ValidateLastName(string input)
    {
        return input.Length >= 2;
    }

    private static bool ValidateStreetName(string input)
    {
        return true;
    }

    private static bool ValidateStreetNo(string input)
    {
        input.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .Select(c => c.ToString())
            .Aggregate((a, b) => a + b);
        return input.Length <= 3;

    }

    private static bool ValidateZipCode(string input)
    {
        input.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .Select(c => c.ToString())
            .Aggregate((a, b) => a + b);
        return input.Length == 5;
    }

    private static bool ValidateCity(string input)
    {
        return false;
    }

    private static bool ValidateEmail(string input)
    {
        return EmailHelper.IsValidEmail(input);
    }

    private static bool ValidatePhone(string input)
    {
        return input.Length >= 10;
    }

    private static bool ValidateAdditionalInfo(string input)
    {
        return true;
    }
}
