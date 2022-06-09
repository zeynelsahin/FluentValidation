// See https://aka.ms/new-console-template for more information

using System.Data;
using FluentValidation;

CustomerValidator validator = new CustomerValidator();

Customer customer = new Customer() { Orders = new List<Order>() { new Order() { Total = 1 ,Name = "Zeynel"} } };
 var result = validator.Validate(customer);


Console.WriteLine(result.ToString());


public class Customer 
{
    public List<Order> Orders { get; set; }
}

public class Order 
{
    public double Total { get; set; }
    public string Name { get; set; }
}

class OrderValidator: AbstractValidator<Order>
{
    public OrderValidator()
    {
        // RuleFor(order => order.Total).GreaterThan(5);
        RuleFor(order => order.Total).NotEqual(1).WithMessage("ADadsdasdasdasdasd");
    }

    private bool StartWithA(string arg)
    {
        return false;
    }
}

class CustomerValidator: AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        // RuleForEach(customer => customer.Orders).SetValidator(new OrderValidator());
        RuleForEach(customer => customer.Orders).ChildRules(orders =>
        {
            orders.RuleFor(order => order.Name).NotEqual("Zeynel");
        });
        RuleForEach(customer => customer.Orders).Where(order => order.Name == "Zeynel");
        // RuleForEach(customer => customer.Orders).SetValidator();
        RuleForEach(customer => customer.Orders).Where(order => order.Name != null).SetValidator(new OrderValidator()); 
        
        //Kural birleştirme

        RuleFor(customer => customer.Orders).Must(list => list.Count <= 10).WithMessage("No more then 10 orders are allowed");
        RuleForEach(customer => customer.Orders).Must(order => order.Total > 0).WithMessage("Orders must have a total of more than 0");

        
        //İki Satır 
        RuleFor(customer => customer.Orders).Must(list => list.Count <= 10).WithMessage("No more 10 orders are allowed").ForEach(orderRule =>
        {
            orderRule.Must(order => order.Total > 0).WithMessage("Orders must have a total of more than 0");
        });
        
    }
}