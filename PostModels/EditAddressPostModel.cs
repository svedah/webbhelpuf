using System.ComponentModel.DataAnnotations;
using webbhelpuf.Shared;

namespace webbhelpuf.PostModels;

public class EditCustomerPostModel
{
    [Required]
    public Guid cartid { get; set; }
    [Required]
    public required string firstname { get; set; }
    [Required]
    public required string lastname { get; set; }
    [Required]
    public required string streetname { get; set; }
    [Required]
    public required string streetno { get; set; }
    [Required]
    public required string zipcode { get; set; }
    [Required]
    public required string city { get; set; }
    [Required]
    public required string email { get; set; }
    public string? phone { get; set; }
    public string? addinfo { get; set; }

}
