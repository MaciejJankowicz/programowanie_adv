using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	public class Factory
	{
		protected ErrorMessage erm;
		public Factory(ErrorMessage erm)
		{
			this.erm = erm;
		}

		public IFileManager CreateFileManager()
		{
			return new GenericFileManager(erm);
		}

        public static ISignals CreateSignal(string type)

        {

            ISignals sg;

            switch (type)

            {

                case "m": return new MavSignals(); break;

                case "a": return new MacdSignals(); break;

                default: return new RsiSignals(); break;

            }

        }
    }
}
