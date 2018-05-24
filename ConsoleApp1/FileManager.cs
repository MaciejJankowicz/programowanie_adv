using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	 public interface IFileManager
	 {
		 ErrorMessage Err { get; set; }

         string[] ReadFile(string path);

     }


	public class GenericFileManager : IFileManager
	{			
		public ErrorMessage Err { get ; set ; }

		public GenericFileManager(ErrorMessage err)
		{
            Err = err;
		}

		public string[] ReadFile(string path)
		{
			try
			{
				return DoReadFile(path);
			}
			catch (Exception e)
			{
				Err.GetError(e.Message);
                return null;
			}
		}
		protected string[] DoReadFile(string path)
		{
			return System.IO.File.ReadAllLines(path);
		}


	    public static async Task WriteAsyncToFile(string path, Dictionary<string, string> data)
	    {
	        StreamWriter sww = new StreamWriter(path, false);
	        foreach (var item in data)
	        {
	            await sww.WriteLineAsync($"Data {item.Key} = {item.Value}");
	        }
	        sww.Close();
	    }
    }
}
