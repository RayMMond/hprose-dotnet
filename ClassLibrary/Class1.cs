using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hprose.RPC;

namespace ClassLibrary
{
	public interface IRunnerLoader
	{
		IRunner Load(string file);
	}

	public interface IRunner : INotifyPropertyChanged
	{
		int Value { get; set; }

		void Run();
	}

	public class RunnerLoader : IRunnerLoader
	{
		public IRunner Load(string file)
		{
			var a = Assembly.LoadFile(file);
			//foreach (var type in a.GetTypes())
			//{
			//	if (!type.GetInterfaces().Contains(typeof(IRunner))) continue;
			//	var jsp = new JsonSerializationProxy(type);

			//	return jsp.GetTransparentProxy() as IRunner;
			//}

			return null;
		}
	}

	public static class AppDomainInitialization
	{
		public static void AppDomainInitializer(string[] args)
		{
			Console.WriteLine(Thread.GetDomainID());

			Service.Register<CrossAppDomainHandler, CrossAppDomainServer>("cad");
			Client.Register<CrossAppDomainTransport>("cad");

			var server = new CrossAppDomainServer();
			var service = new Service();
			service.AddInstanceMethods(new RunnerLoader())
				.Bind(server);

			//MockServer server = new MockServer("test2");
			//var service = new Service();
			//service.AddMethod("Hello", this)
			//	.AddMethod("Sum", this)
			//	.Add<string>(OnewayCall)
			//	.Bind(server);
			//var client = new Client("mock://test2");
			//var log = new Log();
			//client.Use(log.IOHandler).Use(log.InvokeHandler);
			//var proxy = client.UseService<ITestInterface>();
			//var result = await proxy.Hello("world").ConfigureAwait(false);
			//Assert.AreEqual("Hello world", result);
			//Assert.AreEqual(3, proxy.Sum(1, 2));
			//proxy.OnewayCall("Oneway Sync");
			//await proxy.OnewayCallAsync("Oneway Async").ConfigureAwait(false);
			//server.Close();
		}
	}

	public class CrossAppDomainTransport : ITransport
	{
		public static string[] Schemes { get; } = new string[] { "cad" };
		public async Task<Stream> Transport(Stream request, Context context)
		{
			throw new NotImplementedException();
		}

		public async Task Abort()
		{
			throw new NotImplementedException();
		}
	}

	public class CrossAppDomainServer
	{
		private readonly int _domainId;

		public CrossAppDomainServer()
		{
			_domainId = Thread.GetDomainID();
		}
	}

	public class CrossAppDomainHandler : IHandler<CrossAppDomainServer>
	{
		private CrossAppDomainServer _server;
		public Service Service { get; }

		public CrossAppDomainHandler(Service service)
		{
			Service = service ?? throw new ArgumentNullException(nameof(service));
		}

		public async Task Bind(CrossAppDomainServer server)
		{
			_server = server;
		}
	}
}
