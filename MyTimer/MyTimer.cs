using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MyTimer
{
    public delegate void MiTimer();
    class MyTimer
    {
        static readonly private object l = new object();

        public int interval;
        private bool running = false;
        private bool fin = false;
        MiTimer f;

        public MyTimer(MiTimer Func, int interv)
        {
            this.f = Func;
            interval = interv;
            Thread t = new Thread(loop);
            t.Start();
        }

        public void loop()
        {
            while (!fin)
            {
                lock (l)
                {
                    if (!fin)
                    {
                        if (running)
                        {
                            f();
                            Thread.Sleep(interval);
                        }
                        else
                        {
                            Monitor.Wait(l);
                        }
                    }
                }
            }
        }

        public void run()
        {
            lock (l)
            {
                if (!fin)
                {
                    running = true;
                    Monitor.Pulse(l);
                }
            }
        }

        public void pause()
        {
            lock (l)
            {
                if (!fin)
                {
                    running = false;
                }
            }
        }

        public bool repeat(ConsoleKey k)
        {
            if (k == ConsoleKey.D1)
            {
                fin = false;
                return true;
            }
            else
            {
                fin = true;
                running = false;
                lock (l)
                {
                    Monitor.Pulse(l);
                }
                return false;
            }
        }
    }
}
