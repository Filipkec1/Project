using Project.Core.Exceptions;
using Project.Core.Models.Entities;
using Project.Core.Requests;
using Project.Core.Results;
using Project.Core.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Core.Services.Implementations
{

    /// <summary>
    /// Defines item service.
    /// </summary>
    public class PersonService : ServiceBase, IPersonService
    {
        /// <summary>
        /// Initilizes new instance of <see cref="PersonService"/>
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public PersonService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        /// <inheritdoc/>
        public async Task AddTextData(IEnumerable<Person> peopleList)
        {
            bool duplicateOibs = peopleList.ToList().GroupBy(p => p.Oib).Any(o => o.Count() > 1);
            if (duplicateOibs)
            {
                throw new UnprocessableEntityException($"Duplicate Oibs in the text file.");
            }

            IEnumerable<Person> peopleListDb = await unitOfWork.Person.GetAllByOib(peopleList.Select(p => p.Oib));

            List<Person> peopleListCreate = new List<Person>();
            List<Person> peopleListUpdate = new List<Person>();

            foreach(Person person in peopleList)
            {
                SetPeopleValues(person, peopleListDb, peopleListCreate, peopleListUpdate);
            }

            await unitOfWork.Person.AddRange(peopleListCreate);
            unitOfWork.Person.UpdateRange(peopleListUpdate);
            await unitOfWork.Commit();
        }

        /// <summary>
        /// Update <see cref="Person"/> from the database by searching by <paramref name="personText"/> Oib in <paramref name="peopleListDb"/> or create a new <see cref="Person"/>.
        /// </summary>
        /// <param name="personText"><see cref="Person"/> from the text file.</param>
        /// <param name="peopleListDb">List of all <see cref="Person"/>s gotten from the database.</param>
        /// <param name="peopleListCreate">List of all <see cref="Person"/>s that are going to be created.</param>
        /// <param name="peopleListUpdate">List of all <see cref="Person"/>s that are going to be updated.</param>
        private void SetPeopleValues(Person personText, IEnumerable<Person> peopleListDb, List<Person> peopleListCreate, List<Person> peopleListUpdate)
        {
            Person person = peopleListDb.FirstOrDefault(p => p.Oib == personText.Oib);
            if(person != null)
            {
                person.Name = personText.Name;
                person.Surname = personText.Surname;
                person.City = personText.City;
                person.Address = personText.Address;
                person.TelephoneNumber = personText.TelephoneNumber;
                person.Email = personText.Email;

                peopleListUpdate.Add(person);
                return;
            }

            peopleListCreate.Add(personText);
        }

        /// <inheritdoc/>
        public async Task<bool> CheckIfOibUnique(string oib)
        {
            bool isUnique = await unitOfWork.Person.CheckIfOibUnique(oib);
            return isUnique;
        }

        /// <inheritdoc/>
        public async Task<PersonResult> Create(PersonCreateRequest request)
        {
            bool isOibUnique = await unitOfWork.Person.CheckIfOibUnique(request.Oib);
            if (!isOibUnique)
            {
                throw new UnprocessableEntityException("OIB is not unique");
            }

            Person newPerson = new Person()
            {
                Name = request.Name,
                Surname = request.Surname,
                City = request.City,
                Address = request.Address,
                TelephoneNumber = request.TelephoneNumber,
                Email = request.Email,
                Oib = request.Oib
            };

            await unitOfWork.Person.Add(newPerson);
            await unitOfWork.Commit();

            return new PersonResult(newPerson);
        }

        /// <inheritdoc/>
        public async Task Delete(Guid id)
        {
            Person person = await unitOfWork.Person.GetById(id);
            if (person is null)
            {
                throw new EntityNotFoundException(typeof(Person), id);
            }

            unitOfWork.Person.Delete(person);
            await unitOfWork.Commit();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<PersonResult>> GetAll()
        {
            IEnumerable<Person> peopleList = await unitOfWork.Person.GetAll();
            return peopleList.Select(p => new PersonResult(p));
        }

        /// <inheritdoc/>
        public async Task<PersonResult> GetById(Guid id)
        {
            Person person = await unitOfWork.Person.GetById(id);
            if(person is null)
            {
                throw new EntityNotFoundException(typeof(Person), id);
            }

            return new PersonResult(person);
        }

        /// <inheritdoc/>
        public async Task Update(PersonUpdateRequest request)
        {
            Person person = await unitOfWork.Person.GetById(request.Id);
            if (person is null)
            {
                throw new EntityNotFoundException(typeof(Person), request.Id);
            }

            bool isOibUnique = await unitOfWork.Person.CheckIfOibUnique(request.Oib);
            if (!isOibUnique)
            {
                throw new UnprocessableEntityException("OIB is not unique");
            }

            person.Name = request.Name;
            person.Surname = request.Surname;
            person.City = request.City;
            person.Address = request.Address;
            person.TelephoneNumber = request.TelephoneNumber;
            person.Email = request.Email;
            person.Oib = request.Oib;

            unitOfWork.Person.Update(person);
            await unitOfWork.Commit();
        }
    }
}
