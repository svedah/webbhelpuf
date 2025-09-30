namespace webbhelpuf.Data.Models;

public class Image
{
    public required Guid Id { get; set; }
    public required string Filename { get; set; }
    public required string AltText { get; set; }
}