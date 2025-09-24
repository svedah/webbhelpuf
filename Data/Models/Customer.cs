using webbhelpuf.Enums;

namespace webbhelpuf.Data.Models;

public class Customer
{
    public Guid Id { get; set; }

    public required virtual Cart Cart { get; set; }
    public required virtual CustomerInfo CustomerInfo { get; set; }
    public required CustomerStateEnum CustomerState { get; set; }

}

//customerstate:
//created
//hasactivatedpayment
//hascompletedpayment
//försök göra om till true/false
