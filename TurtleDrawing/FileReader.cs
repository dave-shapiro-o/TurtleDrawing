using System;
using System.Collections.Generic;
using System.IO;

namespace TurtleDrawing
{
    class FileReader
    {
        internal static Queue<string> GetCommandsFromFile(string patternFile)
        {
            Queue<string> commands = new();
            StreamReader reader = new(patternFile);
            string command;
            do
            {
                command = reader.ReadLine();
                if (command != null)
                {
                    commands.Enqueue(command);

                }
            }
            while (command != null);

            reader.Close();
            return commands;
        }
    }
}
