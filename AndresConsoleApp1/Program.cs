using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndresConsoleApp1
{
	class Factorizer
	{
		static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Please provide path to input file."); // Return this if no argument is given.
				return;
			}
			if (args.Length > 1)
			{
				Console.WriteLine("One file path argument only please. If path contains spaces, wrap it in quotes.");
				return;
			}
			if (!(System.IO.File.Exists(args[0]))) // At this point I have exactly one argument so it will be at index 0.
			{
				Console.WriteLine("File not found at specified path."); // Return this if file is not found at path specified in the argument.
				return;
			}
			int counter = 0;
			string line;

			// Read the file and display it line by line.
			System.IO.StreamReader file = new System.IO.StreamReader(args[0]); // Instantiates StreamReader with valid path from argument and assigns to "file"
			while ((line = file.ReadLine()) != null) // Assigns, one by one, each line in the read file to variable "line". Next block executes if line is not null.
			{
				Console.WriteLine(line);
				bool checkIfInteger = int.TryParse(line, out int number); // Seems int is shorthand for Int32 so I'll just use that as the limit.
				if (!(checkIfInteger) || number < 1) // I'm only going to consider positive integers. Probably can't have commas in them because I don't think that will pass the TryParse line above.
				{
					Console.WriteLine(line + " is not an integer greater than 1" + Environment.NewLine);
					counter++;
					continue;
				}
				if (Convert.ToInt32(line) == 1) // Seems like they should have a "ToInt" as shorthand since int exists.
				{
					Console.WriteLine("The loneliest number? Yes. Prime? No." + Environment.NewLine); // I could just make the above check "< 2" instead of 1 but then I wouldn't get to make this corny joke!
					counter++;
					continue;
				}
				Factorizer factorizeWorker = new Factorizer(); // Seems I need an instance of this class to call its method. OOP gone crazy?
				List<int> factors = factorizeWorker.Factorize(Convert.ToInt32(line)); // Lines from the file appear to be put into strings so have to cast to long before calling Factorize.
				if (factors.Count == 1)
				{
					Console.WriteLine(line + " is prime." + Environment.NewLine); // If factorize returned a list containing just one thing (which will be the original number passed), that means the number is prime so I'll just print that.
				}
				else
				{
					Console.WriteLine((String.Join(",", factors)) + Environment.NewLine); // Print results list as comma delimited and add extra linebreak to space out the output from each line in the file.
				}
				counter++;
			}
			file.Close(); // I think I have to close this for garbage collection purposes? I also saw that using the keyword "using" with file may dispose of it automatically. Memory management and garbage collection were not things I dealt with in the higher level languages I studied!
		}

		private List<int> Factorize(int num) // This nice algorithm came from Rod Stephens. Had to change it a bit to make it work. He allows its use and requests a shout out in the comments if used. Hi Rod! I enjoy algorithms and was pretty good at them but haven't looked at them in months, busy reviewing my old IT knowledge.
		{
			List<int> result = new List<int>();
			while (num % 2 == 0) // Get all the 2's possible first
			{
				result.Add(2);
				num /= 2;
			}
			// I think that starting with 3 and going up works because any higher numbers that aren't prime will have been taken care of by an earlier iteration which is prime. Definitely need to practice these more.
			// Take out other primes.
			int factor = 3;
			while (factor * factor <= num) // Go until the square of variable "factor" becomes larger than the number being factored
			{
				if (num % factor == 0)
				{
					// This is a factor.
					result.Add(factor);
					num /= factor;
				}
				else
				{
					// Go to the next odd number.
					factor += 2;
				}
			}

			// If num is not 1, then whatever is left is prime.
			if (num > 1) result.Add(num);

			return result;
		}
	}
}
