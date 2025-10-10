using Microsoft.EntityFrameworkCore;
using webbhelpuf.Data.Models;
using webbhelpuf.Factories;
using webbhelpuf.PostModels;
using webbhelpuf.Services;
using webbhelpuf.Shared;

namespace webbhelpuf.Helpers;

static public class CustomerHelper
{
    public static Customer GetOrCreateCustomer(BeService _beService)
    {
        //should NOT create customer in db, just a dummy customer satisfy the view
        Customer output;
        Guid customerId = Guid.Empty;

        if (!SessionHelper.HasCustomerId(_beService))
        {
            customerId = Guid.NewGuid();
            SessionHelper.SetCustomerId(_beService, customerId);
        }
        else
        {
            customerId = SessionHelper.GetCustomerId(_beService);
        }

        //if no customer found, make new customer
        if (!GetCustomer(_beService, customerId, out output))
        {
            var cart = CartHelper.GetOrCreateCart(_beService);
            output.Cart = cart;
        }

        return output;
    }

    public static bool GetCustomer(BeService _beService, Guid id, out Customer customer)
    {
        bool output = _beService.DbContext.Customers.Where(e => e.Id == id).Any();
        if (output)
        {
            customer = _beService.DbContext.Customers
                        .Where(e => e.Id == id)
                        .Include(e => e.Cart)
                        .Include(e => e.CustomerInfo)
                        .Include(e => e.CustomerState)
                        .First();
        }
        else
        {
            customer = new Customer
            {
                Cart = CartHelper.GetOrCreateCart(_beService)
            };
        }
        return output;
    }

    public static void CreateCustomer(BeService _beService, EditAddressPostModel pm, ref Customer customer)
    {
        bool output = false;

        pm = Validators.EditAddressPostModelValidator.Sanitize(pm);

        CustomerInfo customerInfo = new CustomerInfo
        {
            Id = Guid.NewGuid(),
            FirstName = pm.firstname,
            LastName = pm.lastname,
            StreetName = pm.streetname,
            StreetNo = pm.streetno,
            ZipCode = pm.zipcode,
            City = pm.city,
            Email = pm.email,
            Phone = pm.phone ?? string.Empty
        };

        customer.Id = Guid.NewGuid();
        customer.CustomerInfo = customerInfo;

        //spara i db
        _beService.DbContext.CustomerInfos.Add(customerInfo);
        _beService.DbContext.Customers.Add(customer);
        _beService.DbContext.SaveChanges();

        // return output;
    }
}