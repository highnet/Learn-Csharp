using System;
namespace Singly_Linked_List
{
    public class GenericList<E>
    {
        private GenericNode head = null;

        public GenericList()
        {
        }

        public override string ToString()
        {
            if (IsEmpty())
            {
                return "[]";
            }
            return "[" + head.ToString() + "]";
        }

        public int Count()
        {
            if (IsEmpty())
            {
                return 0;
            }
            int counter = 0;
            GenericNode scannerNode = head;
            while (scannerNode.HasNext())
            {
                scannerNode = scannerNode.next;
                counter++;
            }

            return counter+1;
            
        }

        public void RemoveFirst()
        {
            head = head.next;
        }

        public void RemoveLast()
        {
            if (this.Count() <= 2)
            {
                RemoveFirst();
                return;
            }
            GenericNode scannerNode = head;

            while (scannerNode.next.HasNext()){
                scannerNode = scannerNode.next;
            }
            scannerNode.next = null;
        }

        public void Clear()
        {
            head = null;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public void Add(E element)
        {
            if (IsEmpty())
            {
                head = new GenericNode(element, null);
            } else
            {
                head.AddToChildren(element);
            }
        }

        public bool Contains(E element)
        {
            return head != null && this.head.ChildrenContain(element);
        }

        private class GenericNode
        {
            public E element = default(E);
            public GenericNode next;

            public GenericNode(E element, GenericNode next)
            {
                this.element = element;
                this.next = next;
            }

            public override string ToString()
            {
                string str = "";
                str += this.element.ToString();
                if (HasNext())
                {
                    str += "->" + this.next.ToString();
                } else
                {
                    str += "->null";
                }
                return str;
            }

            public Boolean HasNext()
            {
                return next != null;
            }

            public void AddToChildren(E element)
            {
                if (this.HasNext())
                {
                    this.next.AddToChildren(element);
                } else
                {
                    this.next = new GenericNode(element, null);
                }
            }

            public bool ChildrenContain(E element)
            {
                return this.element.Equals(element) || (this.HasNext() && this.next.ChildrenContain(element));
            }
        }
    }
}
