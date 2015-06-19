using Radish.Demo.Model;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace Radish.Demo.Controllers
{
    public class PetsController : ApiController
    {
        private const string JSON_PET_WITHOUT_ID = @"{""AnimalType"":""Cat"",""Name"":""Lisa"",""Breed"":""Turkish Angora"",""Age"":2,""Color"":""White""}";
        private const string JSON_PET_WITH_ID = @"{""Id"":1, ""AnimalType"":""Cat"",""Name"":""Lisa"",""Breed"":""Turkish Angora"",""Age"":2,""Color"":""White""}";
        private const string JSON_PETS_LIST = @"[{""Id"":1,""AnimalType"":""Cat"",""Name"":""Lisa"",""Breed"":""Turkish Angora"",""Age"":2,""Color"":""White"",""Gender"":2,""OwnerId"":2},{""Id"":2,""AnimalType"":""Dog"",""Name"":""Chuck"",""Breed"":""Spaniel"",""Age"":5,""Color"":""Brown"",""Gender"":1,""OwnerId"":1},{""Id"":3,""AnimalType"":""Parrot"",""Name"":""Steely"",""Breed"":""African grey"",""Age"":3,""Color"":""Grey"",""Gender"":1,""OwnerId"":1},{""Id"":4,""AnimalType"":""Guinea pig"",""Name"":""Fasty"",""Breed"":""Texel"",""Age"":1,""Color"":""Red"",""Gender"":2,""OwnerId"":3},{""Id"":5,""AnimalType"":""Cat"",""Name"":""Hamlet"",""Breed"":""Siamese"",""Age"":3,""Color"":""Colorpoint"",""Gender"":1,""OwnerId"":4},{""Id"":6,""AnimalType"":""Dog"",""Name"":""Guardi"",""Breed"":""Caucasian Shepherd"",""Age"":5,""Color"":""White"",""Gender"":2,""OwnerId"":4}]";
        private const string JSON_PETS_OF_PERSON = @"[{""Id"":2,""AnimalType"":""Dog"",""Name"":""Chuck"",""Breed"":""Spaniel"",""Age"":5,""Color"":""Brown"",""Gender"":1,""OwnerId"":1},{""Id"":3,""AnimalType"":""Parrot"",""Name"":""Steely"",""Breed"":""African grey"",""Age"":3,""Color"":""Grey"",""Gender"":1,""OwnerId"":1}]";

        #region Radish
        [Order("pets", 1)]
        [Method("GET", "/api/pets/", "pets-get")]
        [MethodTitle("Get list of all pets")]
        [MethodDescription("Returns list of all pets in the database.")]
        [ResponseBodyDescription("Json-serialized list of Pets objects")]
        [ResponseBodyExample(JSON_PETS_LIST)]
        [ResponseCode(200, "OK. On successfull result.")]
        #endregion
        [HttpGet]
        [Route("pets")]
        [ResponseType(typeof(List<Pet>))]
        public IHttpActionResult GetAllPets()
        {
            List<Pet> pets = DataBase.Instance.GetAllPets();
            return Ok(pets);
        }

        #region Radish
        [Order("pets", 2)]
        [Method("GET", "/api/pets/<id>", "pet-get-by-id")]
        [MethodTitle("Get pet")]
        [MethodDescription("Returns Pet object by specified id.")]
        [RequestParameter("id", "Integer", "Id of the Pet")]
        [ResponseBodyDescription("Json-serialized Pet object")]
        [ResponseBodyExample(JSON_PET_WITH_ID)]
        [ResponseCode(200, "OK. On successful result.")]
        [ResponseCode(404, "Not Found. When pet with specified id was not found.")]
        #endregion
        [HttpGet]
        [Route("pets/{petId:long}")]
        [ResponseType(typeof(Pet))]
        public IHttpActionResult GetPet(int petId)
        {
            Pet pet = DataBase.Instance.GetPet(petId);
            if (pet == null)
            {
                return this.NotFound(String.Format("Pet with id = {0} not found.", petId));
            }
            return Ok(pet);
        }

        #region Radish
        [Order("pets", 3)]
        [Method("GET", "/api/persons/<id>/pets", "get-pets-for-person")]
        [MethodTitle("Get pets for person")]
        [MethodDescription("Get all pets for the person with specified id.")]
        [RequestParameter("id", "Integer", "Id of the Person")]
        [ResponseBodyDescription("Json-serialized list of pets for the specified person")]
        [ResponseBodyExample(JSON_PETS_OF_PERSON)]
        [ResponseCode(200, "OK. On successful result.")]
        [ResponseCode(404, "Not Found. When person with specified Id was not found.")]
        #endregion
        [HttpGet]
        [Route("persons/{personId:long}/pets")]
        [ResponseType(typeof(List<Pet>))]
        public IHttpActionResult GetPetsForPerson(long personId)
        {
            Person person = DataBase.Instance.GetPerson(personId);
            if (person == null)
            {
                return this.NotFound(String.Format("Person with id = {0} not found.", personId));
            }
            return Ok(person.Pets);
        }

        #region Radish
        [Order("pets", 4)]
        [Method("POST", "/api/persons/<id>/pets/", "pets-add")]
        [MethodTitle("Add new pet to person")]
        [MethodDescription("Adds new Pet to the Person with specified id.")]
        [RequestParameter("id", "Integer", "Id of the person")]
        [RequestBodyDescription("Json-serialized Pet object")]
        [RequestBodyExample(JSON_PET_WITHOUT_ID, DataFormat.Json)]
        [ResponseBodyDescription("Json-serialized Pet object with assigned id")]
        [ResponseBodyExample(JSON_PET_WITH_ID)]
        [ResponseCode(200, "OK. When pet was successfully added.")]
        [ResponseCode(400, "Bad request. When incorrect data were passed.")]
        [ResponseCode(400, "Bad request. When passed pet object already contains assigned Id.")]
        [ResponseCode(404, "Not Found. When person with specified Id was not found.")]
        #endregion
        [HttpPost]
        [Route("persons/{personId}/pets")]
        [ResponseType(typeof(Pet))]
        public IHttpActionResult AddPet([FromBody] Pet pet, long personId)
        {
            if (pet == null)
            {
                return BadRequest("Incorrect JSON data were passed.");
            }

            if (pet.Id != 0)
            {
                return BadRequest("Unable to add pet with already assigned id.");
            }

            if (DataBase.Instance.GetPerson(personId) == null)
            {
                return this.NotFound(String.Format("Person with id = {0} not found", personId));
            }

            Pet added = DataBase.Instance.AddPet(pet);
            return Ok<Pet>(added);
        }

        #region Radish
        [Order("pets", 5)]
        [Method("PUT", "/api/pets/<id>", "pets-update-by-id")]
        [MethodTitle("Update pet")]
        [MethodDescription("Updates pet with specified id.")]
        [RequestParameter("id", "Integer", "Id of the Pet")]
        [RequestBodyDescription("Json-serialized Pet object to be updated.")]
        [RequestBodyExample(JSON_PET_WITH_ID, DataFormat.Json)]
        [ResponseBodyDescription("Updated Json-serialized Pet object.")]
        [ResponseBodyExample(JSON_PET_WITH_ID)]
        [ResponseCode(200, "OK. On successful update")]
        [ResponseCode(400, "Bad request. When incorrect data were passed.")]
        [ResponseCode(404, "Not Found. When pet with specified Id was not found.")]
        [ResponseCode(404, "Not Found. When person with specified Id was not found.")]
        #endregion
        [HttpPut]
        [Route("pets/{petId:long}")]
        [ResponseType(typeof(Pet))]
        public IHttpActionResult UpdatePet([FromBody] Pet pet, long petId)
        {
            if (pet == null)
            {
                return BadRequest("Incorrect JSON data were passed.");
            }

            Pet petInDb = DataBase.Instance.GetPet(pet.Id);
            if (petInDb == null)
            {
                return this.NotFound(String.Format("Pet with id = {0} not found.", pet.Id));
            }

            Person personInDb = DataBase.Instance.GetPerson(pet.OwnerId);
            if (personInDb == null)
            {
                return this.NotFound(String.Format("Person with id = {0} not found.", pet.OwnerId));
            }

            Pet updatedPet = DataBase.Instance.UpdatePet(pet);
            
            return Ok(updatedPet);
        }

        #region Radish
        [Order("pets", 6)]
        [Method("DELETE", "/api/pets/<id>", "delete-pet-by-id")]
        [MethodTitle("Delete pet")]
        [MethodDescription("Deletes pet with specified id.")]
        [RequestParameter("id", "Integer", "Id of the Pet")]
        [ResponseCode(200, "OK. When pet was deleted successful.")]
        [ResponseCode(404, "Not Found. When pet with specified Id was not found.")] 
        #endregion
        [HttpDelete]
        [Route("pets/{petId:long}")]
        public IHttpActionResult DeletePet(long petId)
        {
            Pet pet = DataBase.Instance.GetPet(petId);
            if (pet == null)
            {
                return this.NotFound(String.Format("Pet with id = {0} not found.", petId));
            }
            DataBase.Instance.DeletePet(petId);
            return Ok();
        }
    }
}
