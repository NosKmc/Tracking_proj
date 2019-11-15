using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWindow
{
    public int Size { get; private set; }
    public float Value { get; private set; }
    private Queue<float> window;

    public MovingWindow(int windowSize)
    {
        window = new Queue<float>();
        Size = windowSize;
    }

    public float CalcValue(float val)
    {
        if (window.Count < Size)
        {
            window.Enqueue(val);
            Value += val / Size;
            return 0.0f;
        }
        window.Enqueue(val);
        Value += (val - window.Dequeue()) / Size;
        return Value;
    }
}
