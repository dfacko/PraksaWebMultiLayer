using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Models;
using WebAppService;

namespace WebApp.Controllers
{
    public class AppController : ApiController
    {


        // GET: api/App
            [HttpGet]
            public IHttpActionResult OsobeRead()
            {
             List<OsobaRest> popis = new List<OsobaRest>() { };
             List<Osoba> listFromDb = new List<Osoba>() { };

            OsobaReadService person = new OsobaReadService();
             listFromDb=person.Read();

            /*var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Osoba, OsobaRest>();
            }); 

            IMapper iMapper = config.CreateMapper();
            var source = new Osoba();
            var destination = iMapper.Map<AuthorModel, AuthorDTO>(source);*/

            foreach (var osoba in listFromDb) {
                popis.Add(new OsobaRest(osoba.Name, osoba.Surname, osoba.Age));

			    }

                return Ok(popis);
			}
            

        
     
            
            [HttpPost]
            public IHttpActionResult InsertPerson([FromBody] OsobaRest person)
            {
           
            if (person == null) {
                return BadRequest();
                }



            OsobaAddService newPerson = new OsobaAddService();

            Osoba ToAdd = new Osoba(person.Name, person.Surname, person.Age);
            newPerson.Add(ToAdd);

            
                   
            return Ok();
            }

         [HttpPost]
            public IHttpActionResult AddPersonToJob(int personId,int jobId)
            {

            OsobaToJobService person = new OsobaToJobService();
            int status=person.AddToJob(personId, jobId);
            if(status == 1) {
                return Ok();
			}

            return NotFound();
            }


            

            [HttpPut]
            public IHttpActionResult Put(int id=0,string newName="",string newSurname="",int newAge=-1)
            {

            
            OsobaEditService person = new OsobaEditService();
            int  status = person.Edit(id, newName,newSurname,newAge);
            

            if (status==0) { 
                return NotFound(); 
                }
			
                return Ok();
			

            
            }
        
            
            [HttpDelete]
            public IHttpActionResult Delete([FromBody]int id)
            {
            OsobaDeleteService person = new OsobaDeleteService();

            int status=person.Delete(id);

            if (status==0) { 
                return NotFound(); 
                }
			
                return Ok();

            }
                
            
    }
}
