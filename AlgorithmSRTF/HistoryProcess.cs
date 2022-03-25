using System;

namespace AlgorithmSRTF
{
    class HistoryProcess
    {
        public int id, doneWork;
        public ConsoleColor color;
        
        public HistoryProcess(int id, int work, ConsoleColor col)
        {
            color = col;
            this.id = id;
            doneWork = work;
        }

        public void ShowRunningTimeBar()
        {
            Console.BackgroundColor = color;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < doneWork; i++)
            {
                if (i == doneWork / 2)
                    Console.Write(id);
                else
                    Console.Write(' ');
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
