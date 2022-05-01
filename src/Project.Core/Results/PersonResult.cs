using Project.Core.Models.Entities;
using System;

namespace Project.Core.Results
{
    /// <summary>
    /// Defines person result class.
    /// </summary>
    public class PersonResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public string Oib { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="PersonResult"/>.
        /// </summary>
        /// <param name="person">Person.</param>
        public PersonResult(Person person)
        {
            Id = person.Id;
            Name = person.Name;
            Surname = person.Surname;
            City = person.City;
            Address = person.Address;
            TelephoneNumber = person.TelephoneNumber;
            Email = person.Email;
            Oib = person.Oib;
        }
    }
}
