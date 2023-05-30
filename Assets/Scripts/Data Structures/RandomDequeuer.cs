using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random; // distinguishes between System's and UnityEngine's Random class

public class RandomDequeuer<T>
{
    // Fields/Variables
    private Queue<T> dequeueList;
    
    // Constructors
    public RandomDequeuer(List<T> items)
    {
        //Shuffles list
        ShuffleList(ref items);
        
        // Create a queue from the shuffled list
        dequeueList = new Queue<T>(items);
    }

    // Methods
    private void ShuffleList(ref List<T> items)
    {
        // Fisher-Yates shuffle algorithm
        Random rnd = new Random();
        for (int i = items.Count - 1; i >= 0; i--)
        {
            int k = rnd.Next(i + 1);
            T temp = items[i];
            items[i] = items[k];
            items[k] = temp;
        }
    }

    public T Dequeue()
    {
        // If dequeueList is empty
        if (dequeueList.Count == 0)
            return default(T); // null for reference types, 0 for value types, etc.,

        // Dequeue a random item
        return dequeueList.Dequeue();
    }
}