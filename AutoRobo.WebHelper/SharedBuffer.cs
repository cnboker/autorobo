using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Util
{
    public class SharedBuffer<T>
    {
        static private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SharedBuffer<T>));
        private Stack<T> buffer;
        private int capacity;
        private object lockObj = new object();

        /**
         * Construct a new shared buffer.
         * @param capacity The maximum capacity of the shared buffer.
         */
        public SharedBuffer(int capacity)
        {
            this.capacity = capacity;
            buffer = new Stack<T>(capacity);
        }

        public int Length
        {
            get
            {
                return buffer.Count;
            }
        }
        /**
         * Add a new object to a shared buffer or wait if no room is available.
         * @param object The object to be added to the buffer.
         */
        public void Produce(T obj)
        {
            try
            {                
                Monitor.Enter(buffer);
                while (buffer.Count == capacity)
                {
                    Monitor.Wait(buffer);
                }
                buffer.Push(obj);
            }
            finally
            {
                Monitor.PulseAll(buffer);
                Monitor.Exit(buffer);
            }
            logger.Info("Produce Invoke, Buffer Count:" + buffer.Count);
        }

        /// <summary>
        /// Block util quene pushed items
        /// </summary>
        public void Block()
        {
            try
            {
                Monitor.Enter(buffer);
                while (buffer.Count == 0)
                {
                    Monitor.Wait(buffer);
                }
            }
            finally
            {
                Monitor.Exit(buffer);
            }
        }

        /**
         * Remove the first object waiting in the buffer or wait until there is one.
         * @return The first object in the buffer.
         */
        public T Consume()
        {
            try
            {
                
                Monitor.Enter(buffer);
                while (buffer.Count == 0)
                {
                    Monitor.Wait(buffer);
                }
                T obj = buffer.Pop();
                logger.Info("Consume invocke, Buffer Count:" + buffer.Count);
                return obj;
            }
            finally
            {
                Monitor.PulseAll(buffer);
                Monitor.Exit(buffer);
            }
        }

        /**
         * Construct a string that represents a shared buffer.
         * @return The string representation of a shared buffer.
         */
        public String Contains()
        {
            try
            {
                Monitor.Enter(buffer);
                String s = "The shared buffer currently contains the following:\n";
                foreach (T obj in buffer)
                {
                    s += obj + "\n";
                }
                return s;

            }
            finally
            {
                Monitor.Exit(buffer);
            }
        }
    }
}
