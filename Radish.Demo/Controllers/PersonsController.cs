using Radish.Demo.Model;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace Radish.Demo.Controllers
{
    public class PersonsController : ApiController
    {
        private const string JSON_PERSON_WITHOUT_ID = @"{""FirstName"":""Bob"",""LastName"":""Brown"",""Age"":35,""Pets"":[],""Gender"":1}";
        private const string JSON_PERSON_WITH_ID = @"{""Id"":2,""FirstName"":""Bob"",""LastName"":""Brown"",""Age"":35,""Pets"":[],""Gender"":1}";
        private const string JSON_PERSONS_LIST = @"[{""Id"":1,""FirstName"":""Alice"",""LastName"":""Appleseed"",""Age"":20,""Pets"":[],""Gender"":2},{""Id"":2,""FirstName"":""Bob"",""LastName"":""Brown"",""Age"":35,""Pets"":[],""Gender"":1},{""Id"":3,""FirstName"":""Chris"",""LastName"":""Campbell"",""Age"":27,""Pets"":[],""Gender"":1},{""Id"":4,""FirstName"":""Diana"",""LastName"":""Doll"",""Age"":64,""Pets"":[],""Gender"":2}]";

        #region Radish
        [Order("persons", 1)]
        [Method("GET", "/api/persons/", "persons-get")]
        [MethodTitle("Get all persons")]
        [ResponseBodyDescription("Json-serialized list of Persons objects")]
        [ResponseBodyExample(JSON_PERSONS_LIST)]
        [ResponseCode(200, "OK. On successfull result.")]
        #endregion
        [HttpGet]
        [Route("persons")]
        [ResponseType(typeof(List<Person>))]
        public IHttpActionResult GetAllPersons()
        {
            List<Person> persons = DataBase.Instance.GetAllPersons();
            return Ok(persons);
        }

        #region Radish
        [Order("persons", 2)]
        [Method("GET", "/api/persons/<id>", "person-get-by-id")]
        [MethodTitle("Get person")]
        [MethodDescription("Returns Person object by specified id.")]
        [RequestParameter("id", "Integer", "Id of the Person")]
        [ResponseBodyDescription("Json-serialized Person object")]
        [ResponseBodyExample(JSON_PERSON_WITH_ID)]
        [ResponseCode(200, "OK. On successful result.")]
        [ResponseCode(404, "Not Found. When person with specified id was not found.")]
        #endregion
        [HttpGet]
        [Route("persons/{personId:long}")]
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetPerson(int personId)
        {
            Person person = DataBase.Instance.GetPerson(personId);
            if (person == null)
            {
                return this.NotFound(String.Format("Person with id = {0} not found.", personId));
            }
            return Ok(person);
        }

        #region Radish
        [Order("persons", 3)]
        [Method("POST", "/api/persons", "persons-add")]
        [MethodTitle("Add new person")]
        [MethodDescription("Adds new Person.")]
        [RequestBodyDescription("Json-serialized Person object")]
        [RequestBodyExample(JSON_PERSON_WITHOUT_ID, DataFormat.Json)]
        [ResponseBodyDescription("Json-serialized Person object with assigned id")]
        [ResponseBodyExample(JSON_PERSON_WITH_ID)]
        [ResponseCode(200, "OK. When person was successfully added.")]
        [ResponseCode(400, "Bad request. When incorrect data were passed.")]
        [ResponseCode(400, "Bad request. When passed Person object already contains assigned Id.")]
        #endregion
        [HttpPost]
        [Route("persons")]
        [ResponseType(typeof(Person))]
        public IHttpActionResult AddPerson([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest("Incorrect JSON data were passed.");
            }

            if (person.Id != 0)
            {
                return BadRequest("Unable to add person with already assigned id.");
            }

            Person added = DataBase.Instance.AddPerson(person);
            return Ok<Person>(added);
        }

        #region Radish
        [Order("persons", 4)]
        [Method("PUT", "/api/persons/<id>", "persons-update-by-id")]
        [MethodTitle("Update person")]
        [MethodDescription("Updates person with specified id.")]
        [RequestParameter("id", "Integer", "Id of the Person")]
        [RequestBodyDescription("Json-serialized Person object to be updated.")]
        [RequestBodyExample(JSON_PERSON_WITH_ID, DataFormat.Json)]
        [ResponseBodyDescription("Updated Json-serialized Person object.")]
        [ResponseBodyExample(JSON_PERSON_WITH_ID)]
        [ResponseCode(200, "OK. On successful update")]
        [ResponseCode(400, "Bad request. When incorrect data were passed.")]
        [ResponseCode(404, "Not Found. When person with specified Id was not found.")]
        #endregion
        [HttpPut]
        [Route("persons/{petId:long}")]
        [ResponseType(typeof(Person))]
        public IHttpActionResult UpdatePerson([FromBody] Person person, long personId)
        {
            if (person == null)
            {
                return BadRequest("Incorrect JSON data were passed.");
            }

            Person personInDb = DataBase.Instance.GetPerson(person.Id);
            if (personInDb == null)
            {
                return this.NotFound(String.Format("Person with id = {0} not found.", person.Id));
            }

            Person updatedPerson = DataBase.Instance.UpdatePerson(person);

            return Ok(updatedPerson);
        }

        #region Radish
        [Order("persons", 5)]
        [Method("DELETE", "/api/persons/<id>", "delete-person-by-id")]
        [MethodTitle("Delete person")]
        [MethodDescription("Deletes person with specified id.")]
        [RequestParameter("id", "Integer", "Id of the Person")]
        [ResponseCode(200, "OK. When person was deleted successful.")]
        [ResponseCode(404, "Not Found. When person with specified Id was not found.")]
        #endregion
        [HttpDelete]
        [Route("persons/{personId:long}")]
        public IHttpActionResult DeletePerson(long personId)
        {
            Person pet = DataBase.Instance.GetPerson(personId);
            if (pet == null)
            {
                return this.NotFound(String.Format("Person with id = {0} not found.", personId));
            }
            DataBase.Instance.DeletePerson(personId);
            return Ok();
        }
    }
}
