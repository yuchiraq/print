using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Printing;

using Image = System.Drawing.Image;
using Point = System.Drawing.Point;

namespace TPExpCon
{
	public static class Printer
	{
		private static PrintDocument PrintDocument { get; set; }
	
		private static List<Image> Images { get; set; }
		private static Point Position { get; set; }

		static Printer()
		{
			var printerName = GetPrinters().FirstOrDefault(printer => printer.Contains("Val"));
			PrintDocument = new PrintDocument
			{
				PrinterSettings = { PrinterName = printerName },
				PrintController = new StandardPrintController()
			};
			PrintDocument.PrintPage += Pd_PrintPage;
		}
		/// <summary>
		/// Get list of installed printers
		/// </summary>
		/// <returns>List of installed printers</returns>
		public static IEnumerable<string> GetPrinters() => new LocalPrintServer()
				.GetPrintQueues()
				.Select(p => p.Name);

		/// <summary>
		/// Print codes one-by-one (1 code - 1 job for printer)
		/// </summary>
		/// <param name="codes">List of codes</param>
		/// <param name="position">Position on page</param>
		public static void PrintOneByOne(List<string> codes, Point position, Point size)
		{
			if (codes.Count == 1)
			{
				var bitmap = Barcodes.GetDataMatrixBitmap(codes[0], size);
				bitmap.Save("New.bmp");
			}
			Position = position;
			Images = codes.Select(code => BitmapToImage(Barcodes.GetDataMatrixBitmap(code, size))).ToList();
			var count = codes.Count();


			IEnumerable<string> printers = Printer.GetPrinters();
			int i = 1;
			foreach (string printer in printers)
			{
				Console.Write("\t{0} - {1}\n", i, printer);
				i++;
			};
			Console.Write("Введите номер принтера для печати >> ");
			string n = Console.ReadLine();
			i = Convert.ToInt32(n);
			int j = 1;
			foreach (string printer in printers)
			{
				if (j == i)
				{
					Printer.PrintDocument.PrinterSettings.PrinterName = printer;
					break;
				}
				j++;
			}


			for (i = 0; i < count; i++)
			{
				Console.Write("Press <Space> to print: {0}\n", codes[i]);

				while (Console.ReadKey().Key != ConsoleKey.Spacebar) { }
				PrintDocument.Print(); 
			}
		}

		private static Image BitmapToImage(Bitmap codeBitmap)
		{
			using var imageStream = new MemoryStream();
			codeBitmap.Save(imageStream, ImageFormat.Png);
			return Image.FromStream(imageStream);
		}
		/// <summary>
		/// Add image to printer job
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Pd_PrintPage(object sender, PrintPageEventArgs e)
		{
			var image = Images.ElementAt(0);
			e.Graphics.DrawImage(image, Position);
			Images.RemoveAt(0);
		}
		private static void MultiPrint(object sender, PrintPageEventArgs e)
		{
			foreach (var image in Images)
			{
				e.Graphics.DrawImage(image, Position);
			}
		}
	}
}
