using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Models;
using WebAppService;

namespace WebApp {


	
	public  class AsyncHelp {

	
		public async Task<OsobaRest> HelpAsync(Osoba osoba) {

			WebAppServices person = new WebAppServices();

			var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Osoba, OsobaRest>();
            });

            
            IMapper iMapper = config.CreateMapper();

				//OsobaRest osobaRest = IMapper.Map<Osoba, OsobaRest>(osoba);   
				OsobaRest osobaRest = iMapper.Map<Osoba, OsobaRest>(osoba);
                osobaRest.Job = await person.InsertJobAsync(osoba.Posao_id);
				//popis.Add(osobaRest);

			return osobaRest;
		}

		
	}
}