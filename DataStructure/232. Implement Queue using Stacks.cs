// # Implement a first in first out (FIFO) queue using only two stacks. 
// # The implemented queue should support all the functions of a normal queue (push, peek, pop, and empty).
// # Implement the MyQueue class:
//     # void push(int x) Pushes element x to the back of the queue.
//     # int pop() Removes the element from the front of the queue and returns it.
//     # int peek() Returns the element at the front of the queue.
//     # boolean empty() Returns true if the queue is empty, false otherwise.


public class MyQueue {
    Stack<int> stack1;
    Stack<int> stack2;

    public MyQueue() {
        stack1 = new Stack<int>();
        stack2 = new Stack<int>();
        
    }
    
    public void Push(int x) {
        stack1.Push(x);
    }
    
    public int Pop() {
        Peek();
        return stack2.Pop();
    }
    
    public int Peek() {
        if (stack2.Count == 0)
        {
            while (stack1.Count > 0)
            {
                stack2.Push(stack1.Pop());
            }    
        }
        return stack2.Peek();
    }
    
    public bool Empty() {
        if (stack1.Count == 0 && stack2.Count == 0)
        {
            return true;
        }
        return false;
    }
}

/**
 * Your MyQueue object will be instantiated and called as such:
 * MyQueue obj = new MyQueue();
 * obj.Push(x);
 * int param_2 = obj.Pop();
 * int param_3 = obj.Peek();
 * bool param_4 = obj.Empty();
 */

