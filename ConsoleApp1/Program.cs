using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO.Compression;
using System.IO;
using Ionic.Zip;
using Autofac;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new App();
            app.Main();
        }
    }

    public class App
    {
        public State CurrentState = null;
        public IContainer Container = AutofacInit();
        protected Type StartState = typeof(DownloadState);

        public void Main()
        {
            CurrentState = (State)Activator.CreateInstance(StartState,this);
            do
            {
                CurrentState.TransitionState();
            } while (CurrentState.GetType() != StartState);

            Console.WriteLine("The end");
            Console.ReadLine();
        }

        static IContainer AutofacInit()
        {
            var builder = new ContainerBuilder();

            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(executingAssembly)
                .AsSelf()
                .AsImplementedInterfaces();

            // Register individual components
            builder.RegisterType<ErrorMessageToFile>()
                   .As<ErrorMessage>();

            builder.RegisterType<Factory>()
                   .AsSelf();

            return builder.Build();
        }

    }
}
