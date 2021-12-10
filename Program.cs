using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System;

namespace TPExpCon
{
	public class Program
	{
		static void Main()
		{
			///Console.Title = "$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$";
			Console.Title = "                                                                                                                                                  ...    ";
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Red;

			Console.Clear();
			Console.Write("Путь к файлу с кодами: C:\\unik\\printers\\PRINT\\codes.txt\n");
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write("\t\tДоступные принтеры:\t\t\n");
			Console.BackgroundColor = ConsoleColor.White;
			var codes = Codes.GetCodesFromFile("C:\\unik\\printers\\PRINT\\codes.txt");
			List<string> list = codes.ToList();
			
			Printer.PrintOneByOne(list, new Point(90, 150), new Point(180, 180));

		}
	}
}
