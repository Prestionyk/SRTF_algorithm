using System;
using System.Collections.Generic;
using System.Threading;

namespace AlgorithmSRTF
{
    class Program
    {

        public static int QTime = 0;
        public static int AdditionalProcess = 15;

        //////////////////////////////////////////////////////////////////////////////////////
        //Queue of process
        public static List<Process> queue = new()
        {
            new Process(10),
            new Process(8),
            new Process(6)
        };

        static void Main(string[] args)
        {
            Console.SetBufferSize(5000, Console.BufferHeight);

            List<Process> doneTasks = new();
            List<HistoryProcess> timelineTasks = new();   
            Thread addProcess = new(Simulate);
            addProcess.Start();

            //////////////////////////////////////////////////////////////////////////////////////
            //Procesor work simulation
            Process inProcesor = null, recentSmallestInQuery = null;
            int counter = 0;
            do
            {
                if(queue.Count > 0)
                    recentSmallestInQuery = FindSmallestProcess(queue);

                    if(inProcesor == null)
                    {
                        queue.Remove(recentSmallestInQuery);
                        inProcesor = recentSmallestInQuery;
                        inProcesor.ExecutingForOneQTime();
                        queue.ForEach(Wait);
                        counter++;

                        if (!inProcesor.IfWorkLeft())
                        {
                            timelineTasks.Add(new HistoryProcess(inProcesor.Id, counter, inProcesor.color));
                            doneTasks.Add(inProcesor);
                            inProcesor = null;

                        }
                    }
                    else if(inProcesor.getWorkLeft() > recentSmallestInQuery.getWorkLeft())
                    {
                        timelineTasks.Add(new HistoryProcess(inProcesor.Id, counter, inProcesor.color));
                        counter = 0;
                        queue.Remove(recentSmallestInQuery);
                        queue.Add(inProcesor);
                        inProcesor = recentSmallestInQuery;
                    }
                    else
                    {
                        inProcesor.ExecutingForOneQTime();
                        queue.ForEach(Wait);
                        counter++;

                        if (!inProcesor.IfWorkLeft())
                        {
                            timelineTasks.Add(new HistoryProcess(inProcesor.Id, counter, inProcesor.color));
                            doneTasks.Add(inProcesor);
                            inProcesor = null;

                        }
                    }
                    
                    Thread.Sleep(500); 
                
            } while (queue.Count != 0 || AdditionalProcess > 0 || inProcesor != null);

            //////////////////////////////////////////////////////////////////////////////////////
            //Summary of simulation
            Console.WriteLine();

            int avgTime = 0;

            foreach (Process p in doneTasks)
            {
                avgTime += p.getWait();
                p.ShowWaitTime();
            }

            avgTime /= doneTasks.Count;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n\nAverage waiting time: {avgTime}");
            Console.WriteLine("\nProcess execution timeline:");

            foreach (HistoryProcess p in timelineTasks)
            {
                p.ShowRunningTimeBar();
            }

            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();
            Console.ReadKey();
            //////////////////////////////////////////////////////////////////////////////////////
            //Aditional functions
            static void Wait(Process p)
            {
                p.Wait();
            }

            Process FindSmallestProcess(List<Process> list)
            {
                List<Process> processList = list;
                Process sProcess = list[0];

                foreach (Process p in processList)
                {
                    if (p.getWorkLeft() < sProcess.getWorkLeft())
                        sProcess = p;
                }

                return sProcess;
            }

            static void Simulate()
            {
                Thread.Sleep(1000);
                Random rnd = new();
                do
                {
                    if (AdditionalProcess != 0)
                    {
                        queue.Add(new Process(rnd.Next(2) + 1));
                        AdditionalProcess--;
                        Thread.Sleep(1200);
                    }
                    else return;
                } while (true);
            }
        }
    }
}
