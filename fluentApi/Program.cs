// See https://aka.ms/new-console-template for more information

using System.Data;
using FluentValidation;
using FluentValidation.Validators;

Customer customer = new Customer(){Surname = "Zeynel",Address = new Adress()};

CustomerValidator customerValidator = new CustomerValidator();
var results = customerValidator.Validate(customer);
Console.WriteLine(results.ToString("~"));

if (!results.IsValid)
{
    foreach (var failure in results.Errors)
    {
        Console.WriteLine($"Property:{failure.PropertyName}, failed validation. Error was: {failure.ErrorMessage}");
    }
}

// customerValidator.ValidateAndThrow(customer); Exception atar
// customerValidator.Validate(customer, options =>
// {
//     options.ThrowOnFailures();
//     options.IncludeRuleSets("MyRuleSet");
//     options.IncludeProperties(x => x.Surname);
// });
class Customer
{
    public int Id { get; set; }
    public string Surname { get; set; }
    public string Forename { get; set; }
    public decimal Discount { get; set; }
    public Adress Address { get; set; }
}

class Adress
{
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string Town { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }

}

class AdressValidator: AbstractValidator<Adress>
{
    public AdressValidator()
    {
        RuleFor(adress => adress.Postcode).NotNull();
    }
}
class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.Surname).NotNull().NotEqual("foo");
        // RuleFor(customer => customer.Address).SetValidator(new AdressValidator());
        RuleFor(customer => customer.Address.Postcode).NotNull().When(customer => customer.Address != null);
        
    }
}