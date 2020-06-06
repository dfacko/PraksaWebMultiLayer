using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Management;
using AutoMapper;
using Models;
using WebAppRepo;
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

            WebAppServices person = new WebAppServices();
             listFromDb=person.Read();



            foreach (var osoba in listFromDb) {
                OsobaRest osobaRest = new OsobaRest(osoba.Name, osoba.Surname, osoba.Age);
                osobaRest.Job = person.InsertJob(osoba.Posao_id);
                popis.Add(osobaRest);
			    }

                return Ok(popis);
			}
            

        
     
            
            [HttpPost]
            public IHttpActionResult InsertPerson([FromBody] OsobaRest person)
            {
           
            if (person == null) {
                return BadRequest();
                }



            WebAppServices newPerson = new WebAppServices();

            Osoba ToAdd = new Osoba(person.Name, person.Surname, person.Age);
            newPerson.Add(ToAdd);

            
                   
            return Ok();
            }

         [HttpPost]
            public IHttpActionResult AddPersonToJob(int personId,int jobId)
            {

            WebAppServices person = new WebAppServices();
            int status=person.AddToJob(personId, jobId);
            if(status == 1) {
                return Ok();
			}

            return NotFound();
            }


            

            [HttpPut]
            public IHttpActionResult Put(int id=0,string newName="",string newSurname="",int newAge=-1)
            {

            
            WebAppServices person = new WebAppServices();
            int  status = person.Edit(id, newName,newSurname,newAge);
            

            if (status==0) { 
                return NotFound(); 
                }
			
                return Ok();
			

            
            }
        
            
            [HttpDelete]
            public IHttpActionResult Delete([FromBody]int id)
            {
            WebAppServices person = new WebAppServices();

            int status=person.Delete(id);

            if (status==0) { 
                return NotFound(); 
                }
			
                return Ok();

            }
                
            
    }
}
