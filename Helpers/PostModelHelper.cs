using System.ComponentModel.DataAnnotations;
using webbhelpuf.PostModels;
using webbhelpuf.Shared;

namespace webbhelpuf.Helpers;

static public class PostModelHelper
{
    static public bool IsValid(AddToCartPostModel input)
    {
        bool validGuid = input.id != Guid.Empty;
        bool validAmount = input.amount > 0 && input.amount <= 100;

        return validGuid && validAmount;
    }
}