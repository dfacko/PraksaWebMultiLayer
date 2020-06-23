using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Models;
using WebApp.Common;
using WebAppService;
using WebAppService.Common;

namespace WebApp.Controllers {
	public class AppController : ApiController
    {


		protected IWebAppService Service { get; private set; }
        protected IMapper Mapper { get; private set; }


        public  AppController(IWebAppService service/*,Mapper mapper*/)
        {
            this.Service = service;
            //this.Mapper=mapper;
        }
		

        


        

       

		// GET: api/App
		[HttpGet]
            public async Task<IHttpActionResult> OsobeReadAsync(int currentPage=1,int recordsOnPage=3,string filterProperty="Name",string filterCondition ="",string sortOrder="asc",string sortProperty="Id")
            {

             List<OsobaRest> popis = new List<OsobaRest>() { };
             List<Osoba> listFromDb = new List<Osoba>();
             List<Task> tasks = new List<Task>();

            Paging paging = new Paging();
            Filtering filter = new Filtering();
            Sorting sort = new Sorting();

            paging.CurrentPage = currentPage;
            paging.RecordsPerPage = recordsOnPage;
            filter.filterCondition = filterCondition;
            filter.filterProperty = filterProperty;
            sort.sortOrder = sortOrder;
            sort.sortProperty = sortProperty;

            //WebAppServices person = new WebAppServices();


             listFromDb = await Service.ReadAsync(filter,sort,paging);

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Osoba, OsobaRest>();
            });


            IMapper iMapper = config.CreateMapper();

            
          

            foreach (Osoba osoba in listFromDb)
            {
                OsobaRest osobaRest = iMapper.Map<Osoba, OsobaRest>(osoba);
                osobaRest.Job = await Service.InsertJobAsync(osoba.Posao_id);
                popis.Add(osobaRest); 

                                 
            }


                //Task.WaitAll(tasks.ToArray());
                return Ok(popis);
			}
            

        
     
            
            [HttpPost]
            public async Task<IHttpActionResult> InsertPersonAsync([FromBody] OsobaRest person)
            {
           
            if (person == null) {
                return BadRequest();
                }

            

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<OsobaRest, Osoba>();
            });


            IMapper iMapper = config.CreateMapper();


            Osoba osoba = iMapper.Map<OsobaRest, Osoba>(person);
            

            //Osoba PersonToAdd = new Osoba(person.Name, person.Surname, person.Age);

            await Service.AddAsync(osoba);

            
                   
            return Ok();
            }

         [HttpPost]
            public async Task<IHttpActionResult> AddPersonToJobAsync(int personId,int jobId)
            {

            
            int status=await Service.AddToJobAsync(personId, jobId);
            if(status == 1) {
                return Ok();
			}

            return NotFound();
            }


            

            [HttpPut]
            public async Task<IHttpActionResult> PutAsync(int id=0,string newName="",string newSurname="",int newAge=-1)
            {

            
            
            int  status = await Service.EditAsync(id, newName,newSurname,newAge);
            

            if (status==0) { 
                return NotFound(); 
                }
			
                return Ok();
			

            
            }
        
            
            [HttpDelete]
            public async Task<IHttpActionResult> DeleteAsync([FromBody]int id)
            {
            

            int status= await Service.DeleteAsync(id);

            if (status==0) { 
                return NotFound(); 
                }
			
                return Ok();

            }
        /*public async Task<OsobaRest> HelpAsync(Osoba osoba) {

			

			var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Osoba, OsobaRest>();
            });

            
            IMapper iMapper = config.CreateMapper();

				//OsobaRest osobaRest = IMapper.Map<Osoba, OsobaRest>(osoba);   
				OsobaRest osobaRest = iMapper.Map<Osoba, OsobaRest>(osoba);
                osobaRest.Job = await Service.InsertJobAsync(osoba.Posao_id);
				//popis.Add(osobaRest);

			return osobaRest;
		}*/
                
            
    }
}
