// See https://aka.ms/new-console-template for more information

using System.Data;
using System.Threading.Channels;
using FluentValidation;

PersonValidator validator = new PersonValidator();
Person person = new Person();
var result = validator.Validate(person); 
Console.WriteLine(result.ToString());

class Person
{
    public List<string> AdressLines { get; set; } = new List<string>();
}

class PersonValidator: AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleForEach(person => person.AdressLines).NotEmpty().WithMessage("Address {CollectionIndex} is reqired");
    }
}