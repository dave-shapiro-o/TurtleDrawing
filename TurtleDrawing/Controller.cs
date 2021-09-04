using System;
using System.Collections.Generic;
using static System.Console;

namespace TurtleDrawing
{
    /*  Turtle starts in the centre of a 25 x 25 grid.
        Command Key: (press enter after each command)
        pen down 
        pen up
        left
        right
        f 5 (move forward eg 5 spaces)
        b 5 (move back eg 5 spaces)
        Z   (ends command input and starts drawing)
     */
    class Controller
    {
        static string patternFile;
        static bool isNewPattern;
        static List<string> patterns;
        static Queue<string> commands;

        static void Main(string[] args)
        {
            commands = new();
            patterns = Serialiser.DeserializePatterns();
            string mode;
            do
            {
                mode = UI.ChooseFileOrManual();
            }
            while (!mode.Equals("M", StringComparison.OrdinalIgnoreCase)
                    && !mode.Equals("F", StringComparison.OrdinalIgnoreCase));

            if (mode.Equals("M", StringComparison.OrdinalIgnoreCase)) 
            { 
                commands = UI.GetCommands(commands); 
            }
            else 
            {
                patternFile = GetPatternFile();
                commands = FileReader.GetCommands(patternFile);
            }
            
            Drawer.Draw(commands);

            if (isNewPattern)
            {
                if (UI.ChooseSave().Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    patterns.Add(patternFile);
                    UI.PatternAddedMessage(patternFile);
                }
            }

            UI.ExitMessage(patternFile);
            Serialiser.SerializePatterns();
            Drawer.ResetConsoleColours();
            Clear();
        }

        private static string GetPatternFile()
        {
            string input = UI.GetPattern(patterns);
            if (int.TryParse(input, out _))
            {
                int i = int.Parse(input);
                return patterns[i];
            }
            else
            {
                isNewPattern = true;
                return input;
            }
        }

        internal static void ErrorOccurred(Exception ex)
        {
            UI.DisplayErrorMessage(ex);
        }
    }
}
