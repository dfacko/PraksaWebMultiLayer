using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Models;
using WebAppService;

namespace WebApp.Controllers {
	public class AppController : ApiController
    {


        // GET: api/App
            [HttpGet]
            public async Task<IHttpActionResult> OsobeReadAsync()
            {

             List<OsobaRest> popis = new List<OsobaRest>() { };
             List<Osoba> listFromDb = new List<Osoba>();
             List<Task> tasks = new List<Task>();
            


            WebAppServices person = new WebAppServices();
            AsyncHelp Help = new AsyncHelp();

             listFromDb = await person.ReadAsync();


            foreach (Osoba osoba in listFromDb)
            {

                tasks.Add(Task.Run(async ()=>popis.Add(await Help.HelpAsync(osoba))));  

                /*
                OsobaRest osobaRest = IMapper.Map<Osoba, OsobaRest>(osoba);   
				OsobaRest osobaRest = iMapper.Map<Osoba, OsobaRest>(osoba);
                osobaRest.Job = await person.InsertJobAsync(osoba.Posao_id);
				popis.Add(osobaRest);
                */
                // mislim da bi se moglo dodat u jedan taskList sve ovo unutar foreach tako da se ne ceka da se doda u popis jedna osoba, pa onda druga,
                // nego da je samo dodavanje osobe jedan task a mi samo  pokrecemo taskove za svaku osobu iz pocetne liste, tako ce se osobe brze dodavat tj kao paralelno

                popis.Add( await Help.HelpAsync(osoba));                                    
            }


                Task.WaitAll(tasks.ToArray());
                return Ok(popis);
			}
            

        
     
            
            [HttpPost]
            public async Task<IHttpActionResult> InsertPersonAsync([FromBody] OsobaRest person)
            {
           
            if (person == null) {
                return BadRequest();
                }

            WebAppServices newPerson = new WebAppServices();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<OsobaRest, Osoba>();
            });


            IMapper iMapper = config.CreateMapper();


            Osoba osoba = iMapper.Map<OsobaRest, Osoba>(person);
            

            //Osoba PersonToAdd = new Osoba(person.Name, person.Surname, person.Age);

            await newPerson.AddAsync(osoba);

            
                   
            return Ok();
            }

         [HttpPost]
            public async Task<IHttpActionResult> AddPersonToJobAsync(int personId,int jobId)
            {

            WebAppServices person = new WebAppServices();
            int status=await person.AddToJobAsync(personId, jobId);
            if(status == 1) {
                return Ok();
			}

            return NotFound();
            }


            

            [HttpPut]
            public async Task<IHttpActionResult> PutAsync(int id=0,string newName="",string newSurname="",int newAge=-1)
            {

            
            WebAppServices person = new WebAppServices();
            int  status = await person.EditAsync(id, newName,newSurname,newAge);
            

            if (status==0) { 
                return NotFound(); 
                }
			
                return Ok();
			

            
            }
        
            
            [HttpDelete]
            public async Task<IHttpActionResult> DeleteAsync([FromBody]int id)
            {
            WebAppServices person = new WebAppServices();

            int status= await person.DeleteAsync(id);

            if (status==0) { 
                return NotFound(); 
                }
			
                return Ok();

            }
                
            
    }
}
