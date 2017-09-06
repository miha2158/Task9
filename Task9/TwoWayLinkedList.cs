using System;

namespace Task9
{
    class TwoWayLinkedList
    {
        public int Data { get; private set; } = 0;
        public TwoWayLinkedList next { get; private set; } = null;
        public TwoWayLinkedList previous { get; private set; } = null;

    }
}