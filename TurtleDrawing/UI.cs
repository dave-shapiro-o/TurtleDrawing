using System;
using System.Collections.Generic;
using static System.Console;

namespace TurtleDrawing
{
    class UI
    {
        internal static string ChooseFileOrManual()
        {
            return GetInput("enter 'F' to load pattern from file, or 'M' to enter commands manually:");
        }

        internal static Queue<string> GetCommands(Queue<string> commands)
        {
            string input;

            WriteLine("Enter drawing commands, 'Z' to draw:");
            do
            {
                input = ReadLine();
                commands.Enqueue(input);
            }
            while (!input.Equals("Z", StringComparison.OrdinalIgnoreCase));
            return commands;
        }

        internal static string ChooseSave()
        {
            return GetInput("Save pattern to new preset? Y / N:");
        }

        internal static void ExitMessage(string patternFileName)
        {
            WriteLine($"Completed drawing {patternFileName}. Press any key to exit");
            ReadKey();
        }

        internal static void PatternAddedMessage(string patternFile)
        {
            WriteLine($"{patternFile} added to presets.");
        }

        internal static void Message(string message)
        {
            WriteLine(message);
        }

        internal static void DisplayTryAgainMessage()
        {
            WriteLine("Unrecognised input. Please try again.");
        }

        internal static void DisplayErrorMessage(Exception ex)
        {
            WriteLine(ex.Message);
        }

        private static string GetInput(string message)
        {
            WriteLine(message);
            return ReadLine();
        }

        internal static string GetPattern(List<string> patterns)
        {
            WriteLine("Enter filename of pattern file, or one of the following preset numbers:");
            for (int i = 1; i < patterns.Count; ++i)
            {
                // Removes the ".txt" from pattern file name, for displaying preset options
                string[] _ = patterns[i].Split(".");
                string preset = _[0];
                WriteLine($"{i}: {preset}");
            }
            return ReadLine();
        }
    }
}
