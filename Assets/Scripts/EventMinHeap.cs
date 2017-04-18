using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMinHeap{
    List<EventScript> eventArray;
    int load;

    public EventMinHeap()
    {
        load = 0;
        eventArray = new List<EventScript>();
    }
    public EventMinHeap(int l)
    {
        load = l;
        //array = new int[load];
        eventArray = new List<EventScript>(load);
    }
    //public MinHeap(int l,int[] a)
    //{
    //    load = l;
    //    array = a;
    //}
    public EventMinHeap(int l, List<EventScript> a)
    {
        load = l;
        eventArray = a;
    }
    public EventScript pop()
    {
        EventScript data = eventArray[1];
        eventArray[1] = eventArray[load--];
        percolateDown(1);

        eventArray.RemoveAt(load + 1);
        return data;
    }
    //Percolate down the tree the object located at array[index].
    public void percolateDown(int index)
    {
        int child;
        EventScript tmp = eventArray[index];
        for (; (child = 2 * index) <= load; index = child)
        {
            if (child != load && eventArray[child + 1].CompareTo(eventArray[child]) < 0)
                child++;
            if (eventArray[child].CompareTo(tmp) < 0)
                eventArray[index] = eventArray[child];
            else
                break;
        }
        eventArray[index] = tmp;
    }
    public void buildHeap()
    {
        for (int i = load / 2; i > 0; i--)
            percolateDown(i);
    }
    public void insert(EventScript obj)
    {
        //percolate up
        load++;
        eventArray.Add(obj);
        int hole = load; //number of elements in heap
        for (; hole > 1 && obj.CompareTo(eventArray[hole / 2]) < 0; hole /= 2)
            eventArray[hole] = eventArray[hole / 2];
        eventArray[hole] = obj;

    }

    //public int[] getArray()
    //{
    //    return array;
    //}
    public List<EventScript> getArray()
    {
        return eventArray;
    }
    public int getLoad()
    {
        return load;
    }
    public void printHeap()
    {
        if (load != 0)
        {
            for (int i = 0; i < load + 1; i++)
            {
                if (eventArray[i].getVictmNumber() == int.MaxValue)
                    Debug.Log("null");
                else
                    Debug.Log(eventArray[i]);
            }
        }
        else
            Debug.Log("The Heap is empty.");

    }
    public override string ToString()
    {
        string str = "";
        if (load != 0)
        {
            for (int i = 0; i < load + 1; i++)
            {
                if (eventArray[i].getVictmNumber() != int.MaxValue)
                    str = str + eventArray[i].ToString() + ",";
                //Debug.Log(array[i]);
            }
        }
        else
            return "EMPTY";
        return str;
    }
    
}
