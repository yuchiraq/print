using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace TPExpCon
{
	public static class Codes
	{
		/// <summary>
		/// How much codes codes read each time. Default - 50
		/// </summary>
		public static int CodesChunkSize { get; set; } = 10;

		/// <summary>
		/// Length of one code
		/// </summary>
		private static int CodeLength { get; } = 33;
		/// <summary>
		/// Get IEnumerable with 50 readed from file codes
		/// </summary>
		/// <param name="codesFile">File with codes to read</param>
		/// <returns></returns>
		public static IEnumerable<string> GetCodesFromFile(string codesFile)
		{
			var lines = new List<string>();

			try
			{
				using var codesFileStreamReader = new StreamReader(codesFile);
				for (int i = 0; i < CodesChunkSize; i++)
				{
					var code = codesFileStreamReader.ReadLine();
					var modifiedCode = ReplaceGsSymbols(code);
					lines.Add(modifiedCode);
				}
			}
			catch (FileNotFoundException)
			{
				throw new FileNotFoundException("No such file with codes");
			}
			return lines;
		}
		/// <summary>
		/// Return one code on the <param name="number"/> position
		/// </summary>
		/// <param name="codesFile">File with codes to read</param>
		/// <param name="number">Number of code</param>
		/// <returns></returns>
		public static string GetOneCode(string codesFile, int number=0)
		{
			string line;
			try
			{
				using var codesStreamReader = new StreamReader(codesFile);

				//_codesStream ??= new FileStream(codesFile, FileMode.Open);
				//var offset = number * CodeLength;
				//_codesStream.Seek(offset, SeekOrigin.Begin);
				for (int i = 1; i < number; i++) codesStreamReader.ReadLine();
				var code = codesStreamReader.ReadLine();
				var modifiedCode = ReplaceGsSymbols(code);
				line = modifiedCode;
			}
			catch
			{
				throw;
			}
			return line;
		}
		/// <summary>
		/// Replace GS1 symbol by (char)29 and append code by it
		/// </summary>
		/// <param name="code">code to replace symbols</param>
		public static string ReplaceGsSymbols(string code) => $"{(char)29}{code.Replace('', (char)29)}";
	}
}
