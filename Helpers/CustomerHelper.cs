using Microsoft.EntityFrameworkCore;
using webbhelpuf.Data.Models;
using webbhelpuf.Enums;
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
                        // .Include(e => e.CustomerState)
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

    public static void CreateCustomer(BeService _beService, EditCustomerPostModel pm, ref Customer customer)
    {
        bool output = false;

        pm = Validators.CustomerEditPostModelValidator.Sanitize(pm);

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
            Phone = pm.phone ?? string.Empty,
            Info = pm.addinfo ?? string.Empty
        };

        var sessioncustomer = SessionHelper.GetCustomerId(_beService);
        if (sessioncustomer.Equals(Guid.Empty))
        {
            SessionHelper.SetCustomerId(_beService, Guid.NewGuid());
        }

        customer.Id = SessionHelper.GetCustomerId(_beService);
        customer.CustomerInfo = customerInfo;
        customer.CustomerState = CustomerStateEnum.hasRegistered;

        _beService.DbContext.CustomerInfos.Add(customerInfo);
        _beService.DbContext.Customers.Add(customer);
        _beService.DbContext.SaveChanges();

        // return output;
    }

    public static void EditCustomer(BeService _beService, EditCustomerPostModel pm, ref Customer customer)
    {
        pm = Validators.CustomerEditPostModelValidator.Sanitize(pm);

        //First Name
        if (!customer.CustomerInfo.FirstName.Equals(pm.firstname))
        {
            customer.CustomerInfo.FirstName = pm.firstname;
        }

        //Last Name
        if (!customer.CustomerInfo.LastName.Equals(pm.lastname))
        {
            customer.CustomerInfo.LastName = pm.lastname;
        }

        //Street Name
        if (!customer.CustomerInfo.StreetName.Equals(pm.streetname))
        {
            customer.CustomerInfo.StreetName = pm.streetname;
        }

        //Street Number
        if (!customer.CustomerInfo.StreetNo.Equals(pm.streetno))
        {
            customer.CustomerInfo.StreetNo = pm.streetno;
        }

        //Zip Code
        if (!customer.CustomerInfo.ZipCode.Equals(pm.zipcode))
        {
            customer.CustomerInfo.ZipCode = pm.zipcode;
        }

        //City
        if (!customer.CustomerInfo.City.Equals(pm.city))
        {
            customer.CustomerInfo.City = pm.city;
        }

        //Email
        if (!customer.CustomerInfo.Email.Equals(pm.email))
        {
            customer.CustomerInfo.Email = pm.email;
        }

        //Phone
        if (!customer.CustomerInfo.Phone.Equals(pm.phone))
        {
            customer.CustomerInfo.Phone = pm.phone ?? string.Empty;
        }

        //Additional Info
        if (!customer.CustomerInfo.Info.Equals(pm.addinfo))
        {
            customer.CustomerInfo.Info = pm.addinfo ?? string.Empty;
        }

        //Save
        _beService.DbContext.CustomerInfos.Update(customer.CustomerInfo);
        _beService.DbContext.SaveChanges();

    }

}