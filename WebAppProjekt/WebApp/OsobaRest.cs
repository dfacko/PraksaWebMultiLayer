using Models.Common;

namespace WebApp {
	public class OsobaRest : IOsoba {

		public string Name { get; set; }
		public int Age { get; set; }
		public string Surname { get; set; }

		//public string Job { get; set; }

		public JobRest Job {get;set;}

		public  OsobaRest(string name="",string prezime="",int age=-1)
		{
			this.Name = name;
			this.Age = age;
			this.Surname = prezime;
			Job = new JobRest();
		}
	}
}