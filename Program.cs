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
			Console.Title = " ";
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Clear();

			String path;
			Console.Write("Введите путь к файлу с кодами\n");
			path = Console.ReadLine();
			
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write("\n\t\tДоступные принтеры:\t\t\n");
			Console.BackgroundColor = ConsoleColor.White;
			var codes = Codes.GetCodesFromFile(path);
			List<string> list = codes.ToList();
			
			Printer.PrintOneByOne(list, new Point(90, 150), new Point(180, 180));
		}
	}
}
