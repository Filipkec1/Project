using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Models.Entities;
using Project.Core.Requests;
using Project.Core.Results;
using Project.Core.Services;
using Project.Infrastructure.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Project.Web.Controllers
{
    /// <summary>
    /// Defines person api controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PersonApiController : ControllerBase
    {
        private IPersonService personService;

        /// <summary>
        /// Initialize a new instance of <see cref="PersonApiController"/> class.
        /// </summary>
        /// <param name="service">Person Controller service.</param>
        public PersonApiController(IPersonService service)
        {
            personService = service;
        }

        /// <summary>
        /// Delete <see cref="Person"/>.
        /// </summary>
        /// <param name="id">Clinet id</param>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await personService.Delete(id);
            return NoContent();
        }

        /// <summary>
        /// Get <see cref="Person"/> by id.
        /// </summary>
        /// <param name="id">Person id in the database.</param>
        /// <returns><see cref="PersonResult"/> of the searched for <see cref="Person"/>.</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [Produces(typeof(PersonResult))]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            PersonResult personResult = await personService.GetById(id);
            return Ok(personResult);
        }

        [HttpGet]
        [Route("")]
        [Produces(typeof(IEnumerable<PersonResult>))]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<PersonResult> peopleList = await personService.GetAll();
            return Ok(peopleList);
        }

        /// <summary>
        /// Check if <paramref name="oib"/> is being used in the database.
        /// </summary>
        /// <param name="oib">Oib that is going to be check if it exists.</param>
        /// <returns>
        /// Return true if it does not exist in the database.
        /// Return false if it does exist in the database.
        /// </returns>
        [HttpGet]
        [Route("CheckOib/{oib}")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> CheckIfOibUnique([FromRoute] string oib)
        {
            bool isUnique = await personService.CheckIfOibUnique(oib);
            return Ok(isUnique);
        }

        /// <summary>
        /// Create new <see cref="Person"/>.
        /// </summary>
        /// <param name="request"><see cref="PersonCreateRequest"/></param>
        [HttpPost]
        [Route("")]
        [Produces(typeof(PersonResult))]
        public async Task<IActionResult> Post([FromBody] PersonCreateRequest request)
        {
            PersonResult personResult = await personService.Create(request);
            return CreatedAtAction(nameof(Get), new { id = personResult.Id }, personResult);
        }

        /// <summary>
        /// Update <see cref="PersonController"/>.
        /// </summary>
        /// <param name="request"><see cref="PersonUpdateRequest"/></param>
        [HttpPut]
        [Produces(typeof(NoContentResult))]
        public async Task<IActionResult> Put([FromBody] PersonUpdateRequest request)
        {
            await personService.Update(request);
            return NoContent();
        }

        /// <summary>
        /// Read selected text file and converts the data to objects that are saved to the database.
        /// </summary>
        /// <param name="uploadFileRequests">Class that contains an <see cref="IFormFile"/> for the text file.</param>
        /// <returns><see cref="CreatedResult"/></returns>
        [HttpPost]
        [Route("Upload")]
        [Produces(typeof(CreatedResult))]
        public async Task<IActionResult> UploadTextFile([FromForm] PersonTextFileUploadRequest uploadFileRequests)
        {
            MemoryStream memoryStream = IFormFileToMemoryStream(uploadFileRequests.TextFile);
            PersonTextParser textParser = new PersonTextParser(memoryStream);

            IEnumerable<Person> textData = textParser.Read();
            await personService.AddTextData(textData);

            return Created(uploadFileRequests.TextFile.FileName, null);
        }

        /// <summary>
        /// Creates new <see cref="MemoryStream"/> from <see cref="IFormFile"/>.
        /// </summary>
        /// <param name="f"><see cref="IFormFile"/> that is going to be used in the new <see cref="MemoryStream"/>.</param>
        /// <returns>New <see cref="MemoryStream"/>.</returns>
        private MemoryStream IFormFileToMemoryStream(IFormFile f)
        {
            MemoryStream memoryStream = new MemoryStream();
            f.CopyTo(memoryStream);
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}
