using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppDomainToolkit;
using ClassLibrary;
using Hprose.RPC;
using Hprose.RPC.Plugins.Log;

namespace ConsoleRPC
{
	class Program
	{
		static void Main(string[] args)
		{
			Service.Register<CrossAppDomainHandler, CrossAppDomainServer>("cad");
			Client.Register<CrossAppDomainTransport>("cad");

			var context = AppDomainContext.Create(new AppDomainSetup
			{
				ApplicationBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test"),
				AppDomainInitializer = AppDomainInitialization.AppDomainInitializer
			});

			var client = new Client($"cad://{context.Domain.Id}");
			var log = new Log();
			client.Use(log.IOHandler).Use(log.InvokeHandler);
			var proxy = client.UseService<IRunnerLoader>();
			var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test", "Impl.dll");
			var runner = proxy.Load(path);

			//var remote = Remote<RunnerLoader>.CreateProxy(context.Domain);
			//var runner = remote.RemoteObject.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test", "Impl1.dll"));


			runner.PropertyChanged += RunnerOnPropertyChanged;
			runner.Run();

			Console.Read();
		}

		private static void RunnerOnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
