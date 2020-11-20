using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MyTimer
{
    public delegate void Deleg();
    class MyTimer
    {
        static readonly private object l = new object();

        public int interval;
        private bool running = false;
        private bool fin = false;
        Deleg f;

        public MyTimer(Deleg Func)
        {
            this.f = Func;
            Thread t = new Thread(loop);
            t.IsBackground = true;      //NECESARIO para que acabe o fío cando acabe o main!!!
            t.Start();
        }

        public void loop()
        {
            while (!fin)
            {
                lock (l)
                {
                    if (!running)
                    {
                        Monitor.Wait(l);                            
                    }
                    f();
                    Thread.Sleep(interval);
                }
            }
        }

        public void run()
        {
            lock (l)
            {
                running = true;
                Monitor.Pulse(l);
            }
        }

        public void pause()
        {
            lock (l)
            {
                running = false;
            }
        }        
    }
}
