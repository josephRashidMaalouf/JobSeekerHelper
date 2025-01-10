using System.Text.RegularExpressions;
using UserService.Domain.Exceptions;

namespace UserService.Domain.Models;

public class Email
{
    private string _value = string.Empty;

    public string Value
    {
        get => _value;
        set
        {
            if (!IsValidEmail(value))
            {
                throw new InvalidEmailException();
            }
            _value = value;
        }
    }

    //Empty constructor needed for json deserializing
    public Email()
    {
        
    }
    public Email(string email)
    {
        Value = email;
    }

    private bool IsValidEmail(string email)
    {
        var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-]{2,}(\.[a-zA-Z0-9-]{2,})*$";
        return Regex.IsMatch(email, emailPattern);
    }

    public override string ToString()
    {
        return _value;
    }

}