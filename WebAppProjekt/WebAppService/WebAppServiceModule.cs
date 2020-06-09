using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using WebAppService.Common;

namespace WebAppService {
	 public class WebAppServiceModule : Module {
		protected override void Load(ContainerBuilder builder) {
			builder.RegisterType<WebAppServices>().As<IWebAppService>().InstancePerDependency();

		}
	}
}
