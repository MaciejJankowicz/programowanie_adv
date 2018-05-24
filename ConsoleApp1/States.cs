using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Ma;

namespace ConsoleApp1
{
	public abstract class State
	{
		protected App P;
	    protected Type NextType;     

		public State(App p)
		{
			P = p;
		}

	    public virtual void TransitionState()
	    {
	        Console.WriteLine($"State enter {this.GetType().Name}");
        }

	    protected virtual void NextState()
	    {
	        State newState = (State)Activator.CreateInstance(NextType,P);
            Console.WriteLine($"State from {this.GetType().Name} to {newState.GetType().Name}");
	        P.CurrentState = newState;
	    }
	}

    public class DownloadState : State
    {
        public DownloadState(App p) : base(p)
        {
            NextType = typeof(SerializationState);
        }

        public override void TransitionState()
        {
            base.TransitionState();

            Download d;
            IFileManager fm;
            using (var scope = P.Container.BeginLifetimeScope())
            {
                fm = scope.Resolve<Factory>().CreateFileManager();
                d = scope.Resolve<DownloadFromBossa>();
            }
            d.DownloadFile();
            var x = fm.ReadFile(@"zz\PKNORLEN.mst");           

            NextState();
        }
    }

    public class SerializationState : State
    {
        public SerializationState(App p) : base(p)
        {
            NextType = typeof(DecoratorState);
        }

        public override void TransitionState()
        {
            base.TransitionState();

            ISignals s1 = Factory.CreateSignal("a");
            Operations op = new Operations(s1);
            s1.xx(new List<(string, double)>() { ("a", 32), ("b", 34), ("c", 38) });
            s1.dates = new string[] { "a", "b", "c" };

            Console.Write(Helper.Ctrt(s1));

            Stream TestFileStream = File.Create("ser");
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(TestFileStream, s1);
            TestFileStream.Close();
            s1.dates = new string[] { "h", "h", "j", "h" };

            ISignals s2;
            if (File.Exists("ser"))
            {
                TestFileStream = File.OpenRead("ser");
                BinaryFormatter deserializer = new BinaryFormatter();
                s2 = (ISignals)deserializer.Deserialize(TestFileStream);
                TestFileStream.Close();
            }

            Stream TestFileStream2 = File.Create("ser2");
            BinaryFormatter serializer2 = new BinaryFormatter();
            serializer2.Serialize(TestFileStream2, op);
            TestFileStream2.Close();
            s1.dates = new string[] { "h", "h", "j", "h" };

            Operations op2;
            if (File.Exists("ser2"))
            {
                TestFileStream2 = File.OpenRead("ser2");
                BinaryFormatter deserializer = new BinaryFormatter();
                op2 = (Operations)deserializer.Deserialize(TestFileStream2);
                TestFileStream2.Close();
            }

            NextState();
        }
    }

    public class DecoratorState : State
    {
        public DecoratorState(App p) : base(p)
        {
            NextType = typeof(MAState);
        }

        public override void TransitionState()
        {
            base.TransitionState();

            var baseCost = new Cost();
            var extCost = new Exchange(baseCost);
            var extCost2 = new X(extCost);
            Console.WriteLine($"cost: {extCost2.GetCost()} ; desc: {extCost2.GetDescription()}");

            NextState();
        }
    }

    public class MAState : State
    {
        public MAState(App p) : base(p)
        {
            NextType = typeof(DownloadState);
        }

        public override void TransitionState()
        {
            base.TransitionState();

            Console.WriteLine("MA spaaaaaaaam, press enter to start");
            var nope = Console.ReadLine();
            DirectoryInfo di = new DirectoryInfo(@"zz");
            FileInfo[] fi = di.GetFiles();
            Directory.CreateDirectory("data");
            Parallel.For(0, fi.Length, async i =>
            {
                Indicators ind = new Indicators();
                Dictionary<string, string> w = ind.Wskazniki(20, fi[i].FullName);

                await GenericFileManager.WriteAsyncToFile("data/" + fi[i].Name, w);
                if (nope != "nope")
                {
                    foreach (var item in w)
                    {
                        Console.WriteLine($"Data {item.Key} = {item.Value}");
                    }
                }
                
            });
            Console.WriteLine("spaaaaaaaam end, press enter to continue");
            Console.ReadLine();

            NextState();
        }
    }

}
