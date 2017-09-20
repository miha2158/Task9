using System;

namespace Task9
{
    public class TwoWayLinkedList
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
        public int Length
        {
            get
            {
                if (next == null)
                    return 1;
                return next.Length + 1;
            }
        }

        public TwoWayLinkedList() { }
        public TwoWayLinkedList(int n, int StartNum = 1)
        {
            Data = StartNum;
            if (n <= 1)
                return;
            next = new TwoWayLinkedList(n - 1, StartNum + 1);
            next.previous = this;
        }

        public int Search(int data)
        {
            if (data == Data)
                return 0;
            if (next == null)
                return -1;
            return next.Search(data) + 1;
        }
        public void Delete(int index)
        {
            if (next == null)
                throw new IndexOutOfRangeException();

            switch (index)
            {
                case 0:
                    Data = next.Data;
                    next = next.next;
                    try
                    {
                        next.previous = this;
                    }
                    catch (NullReferenceException) { }
                    catch (ArgumentNullException) { }
                    return;

                case 1:
                    next = next.next;
                    try
                    {
                        next.previous = this;
                    }
                    catch (NullReferenceException) { }
                    catch (ArgumentNullException) { }
                    break;

                default:
                    next.Delete(index - 1);
                    break;
            }
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}