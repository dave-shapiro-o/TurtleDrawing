using System;
using System.Collections.Generic;
using static System.Console;
using System.Threading;


namespace TurtleDrawing
{
    class Drawer
    {
        static Orientation turtleOrientation;
        static int PosY;
        static int PosX;
        static int endPosY;
        static int endPosX;

        const int size = 25; // area = size x size
        const int speed = 50; // The smaller, the faster (ms per element)

        static string[,] output;
        static bool isPenDown;

        enum Orientation
        {
            Up, Left, Down, Right
        }

        internal static void Draw(Queue<string> commands)
        {
            output = new string[size, size];
            PosX = size / 2;
            PosY = size / 2;
            turtleOrientation = Orientation.Up;

            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.White;

            InitializeArray();
            while (commands.Count > 0)
            {
                RunCommand(commands.Dequeue());
            }
        }

        private static void InitializeArray()
        {
            for (int y = 0; y < size; ++y)
            {
                for (int x = 0; x < size; ++x)
                {
                    output[y, x] = "  ";
                }
            }
        }

        private static void RunCommand(string command)
        {
            string[] subCommands = command.Split(" ");

            switch (subCommands[0])
            {
                case "left":
                    turtleOrientation =
                        turtleOrientation == Orientation.Right ? Orientation.Up : (Orientation)((int)turtleOrientation + 1);
                    break;
                case "right":
                    turtleOrientation =
                        turtleOrientation == Orientation.Up ? Orientation.Right : (Orientation)((int)turtleOrientation - 1);
                    break;
                case "pen":
                    if (subCommands[1].Equals("down"))
                        isPenDown = true;
                    if (subCommands[1].Equals("up"))
                        isPenDown = false;
                    break;
                case "f":
                    Move(int.Parse(subCommands[1]), true);
                    break;
                case "b":
                    Move(int.Parse(subCommands[1]), false);
                    break;
            }
        }

        private static void Move(int distance, bool goingForward)
        {
            endPosY = PosY;
            endPosX = PosX;

            switch (turtleOrientation)
            {
                case Orientation.Up:
                    if (goingForward) { distance *= -1; }
                    MoveVertical(distance);
                    break;

                case Orientation.Down:
                    if (!goingForward) { distance *= -1; }
                    MoveVertical(distance);
                    break;

                case Orientation.Left:
                    if (goingForward) { distance *= -1; }
                    MoveHorizontal(distance);
                    break;

                case Orientation.Right:
                    if (!goingForward) { distance *= -1; }
                    MoveHorizontal(distance);
                    break;
            }
            PosX = endPosX;
            PosY = endPosY;
        }

        private static void MoveVertical(int distance)
        {
            if (PosY + distance < 0) { endPosY = 0; }
            else if (PosY + distance >= size) { endPosY = size - 1; }
            else { endPosY += distance; }
            if (isPenDown) { DrawVertical(); }
        }

        private static void MoveHorizontal(int distance)
        {
            if (PosX + distance >= size) { endPosX = size - 1; }
            else if (PosX + distance < 0) { endPosX = 0; }
            else { endPosX += distance; }
            if (isPenDown) { DrawHorizontal(); }
        }

        private static void DrawHorizontal()
        {
            if (endPosX > PosX)
            {
                for (int x = PosX; x <= endPosX; ++x)
                {
                    output[PosY, x] = "* ";
                    Render();
                }
            }
            else
            {
                for (int x = PosX; x >= endPosX; --x)
                {
                    output[PosY, x] = "* ";
                    Render();
                }
            }
        }

        private static void DrawVertical()
        {
            if (endPosY > PosY)
            {
                for (int y = PosY; y <= endPosY; ++y)
                {
                    output[y, PosX] = "* ";
                    Render();
                }
            }
            else
            {
                for (int y = PosY; y >= endPosY; --y)
                {
                    output[y, PosX] = "* ";
                    Render();
                }
            }
        }

        public static void Render()
        {
            Clear();
            Write("\n\n\n");
            for (int y = 0; y < size; ++y)
            {
                for (int x = 0; x < size; ++x)
                {
                    Write(output[y, x]);
                }
                WriteLine();
            }
            Thread.Sleep(speed);
        }

        internal static void ResetConsoleColours()
        {
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;
        }
    }
}
