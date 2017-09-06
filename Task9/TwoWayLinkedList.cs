using System;

namespace Task9
{
    class TwoWayLinkedList
    {
        public int Data { get; private set; } = 0;
        public TwoWayLinkedList next { get; private set; } = null;
        public TwoWayLinkedList previous { get; private set; } = null;
        public TwoWayLinkedList this[int i]
        {
            get
            {
                if (i == 0)
                    return this;
                if (next == null)
                    throw new IndexOutOfRangeException();
                return next[i - 1];
            }
            set { }
        }

        TwoWayLinkedList() { }
        TwoWayLinkedList(int n, int StartNum = 1)
        {
            Data = StartNum;
            if (n <= 0)
                return;
            previous = new TwoWayLinkedList(n - 1, StartNum + 1);
            previous.next = this;
        }

        public TwoWayLinkedList Search(int data)
        {
            if (data == Data)
            {
                return this;
            }
            if (next == null)
                throw new NullReferenceException();
            return next.Search(data);
        }
        public void Delete()
        {
            Delete(1);
        }
        public void Delete(int index)
        {
            if(next == null)
                throw new NullReferenceException();
            if (index == 1)
            {
                next = next.next;
                try
                {
                    next.previous = this;
                }
                catch (NullReferenceException) { }
                catch (ArgumentNullException) { }
            }
            else
                Delete(index - 1);
        }

    }
}