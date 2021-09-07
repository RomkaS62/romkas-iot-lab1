using System;
using System.Collections.Generic;
using System.Threading;

namespace Packets
{
    public class Worker
    {
        private Queue<Action> work = new Queue<Action>();
        private bool run = true;

        public Worker()
        {
            Thread t = new Thread(Run);
            t.IsBackground = true;
            t.Start();
        }

        public void Kill()
        {
            run = false;
        }

        public void Do(Action act)
        {
            lock (work)
            {
                work.Enqueue(act);
            }
        }

        public void Run()
        {
            Action a;
            while (run)
            {
                while ((a = GetWork()) != null)
                {
                    a();
                }
                Thread.Sleep(0);
            }
        }

        private Action GetWork()
        {
            lock (work)
            {
                return (work.Count > 0)
                    ? work.Dequeue()
                    : null;
            }
        }
    }
}
