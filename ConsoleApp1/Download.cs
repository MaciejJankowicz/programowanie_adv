using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	public abstract class Download  //Now better. To be continued next week
	{
		protected ErrorMessage erm;
		protected WebClient client = new WebClient();
		public Download(ErrorMessage erm)
		{
			this.erm = erm;
		}

		public void DownloadFile()

		{
			try
			{
				DoDownload();
			}
			catch (Exception exc)
			{
				erm.GetError(exc.Message);
			}
		}

		protected virtual void DoDownload()
		{
		}
	}



	public class DownloadFromBossa : Download
	{
		public DownloadFromBossa(ErrorMessage erm) : base(erm)
		{
		}
		protected override void DoDownload()
		{
			client.DownloadFile("http://bossa.pl/pub/metastock/mstock/mstall.zip", "z.zip");
			using (ZipFile zip = ZipFile.Read("z.zip"))
			{
				zip.ExtractAll("zz", ExtractExistingFileAction.DoNotOverwrite);
			}
		}
	}

	public class DownloadFromBankier : Download
	{
		public DownloadFromBankier(ErrorMessage erm) : base(erm)
		{
		}
		protected override void DoDownload()
		{
			client.DownloadFile("https://www.bankier.pl/inwestowanie/profile/quote.html?symbol=PKNORLEN", @"E:\ZTP\bossa\bankier.html");
		}
	}

	public abstract class ErrorMessage
	{
		public virtual void GetError(string message)
		{
		}
	}



	public class ErrorMessageToFile : ErrorMessage
	{
		public override void GetError(string message)
		{
			System.IO.File.AppendAllText(@"bossa\log.txt", message);
		}

	}



	public class ErrorMessageToUser : ErrorMessage
	{
		public override void GetError(string message)
		{
			Console.WriteLine(message);
		}
	}



	class Download0 //This class violates the Single Responsibility Principle and The Open-Closed Principle

	{

		public void DownloadFile(int source, int err)

		{



			WebClient client = new WebClient();

			try

			{

				if (source == 1)

				{

					client.DownloadFile("https://www.bankier.pl/inwestowanie/profile/quote.html?symbol=PKNORLEN", @"E:\ZTP\bossa\bankier.html");

				}

				else if (source == 2)

				{

					client.DownloadFile("http://bossa.pl/pub/metastock/mstock/mstall.zip", @"E:\ZTP\bossa\mstall.zip");

					using (ZipFile zip = ZipFile.Read(@"E:\ZTP\bossa\mstall.zip"))

					{

						zip.ExtractAll(@"E:\ZTP\bossa");

					}

				}

			}

			catch (Exception exc)

			{

				if (err == 1)

				{

					System.IO.File.AppendAllText(@"E:\ZTP\bossa\log.txt", exc.Message);

				}

				else if (err == 2)

				{

					Console.WriteLine(exc.Message);

				}

			}

		}

	}
}