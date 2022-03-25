using System;

namespace AlgorithmSRTF
{
    class Process
    {
        public static int idProcess = 0, colorId = 0;

        bool isDone = false;
        public int Id;
        int workLeft, waitTime = 0;
        public ConsoleColor color;

        public Process(int work)
        {
            Id = idProcess++;
            workLeft = work;
            color = GetNextColor();
        }
        public void ExecutingForOneQTime()
        {
            --workLeft;
            Console.ForegroundColor = color;
            Console.WriteLine($"QTime:{Program.QTime++}  \tProcess {Id} working...");

            if (workLeft == 0)
                WorkDone();
        }
        void WorkDone()
        {
            isDone = true;
            Console.ForegroundColor = color;
            Console.WriteLine($"QTime:{Program.QTime}  \tProcess {Id} done his work");
        }

        public bool IfWorkLeft()
        {
            if (isDone)
                return false;
            else
                return true;
        }
        public void Wait()
        {
            waitTime++;
        }

        public int getWait()
        {
            return waitTime;
        }
        public int getWorkLeft()
        {
            return workLeft;
        }
        public void ShowWaitTime()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nProcess {Id} waited {waitTime} QT");
            Console.BackgroundColor = color;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int i = 0; i < waitTime; i++)
                Console.Write(' ');
            Console.BackgroundColor = ConsoleColor.Black;
        }

        static ConsoleColor GetNextColor()
        {
            ConsoleColor tempColor = (ConsoleColor)(((int)Console.ForegroundColor + colorId++) % Enum.GetValues(typeof(ConsoleColor)).Length);
            if (tempColor == ConsoleColor.Black)
                tempColor = (ConsoleColor)(((int)Console.ForegroundColor + colorId++) % Enum.GetValues(typeof(ConsoleColor)).Length);

            return tempColor;
        }
    }
}
