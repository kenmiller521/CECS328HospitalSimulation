using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinHeap{
    List<int> array;
    //int[] array = null;
    int load;

    public MinHeap()
    {
        load = 0;
        //array = new int[load];
        array = new List<int>();
    }
    public MinHeap(int l)
    {
        load = l;
        //array = new int[load];
        array = new List<int>(load);
    }
    //public MinHeap(int l,int[] a)
    //{
    //    load = l;
    //    array = a;
    //}
    public MinHeap(int l, List<int> a)
    {
        load = l;
        array = a;
    }
    public int pop()
    {
        int data = array[1];
        array[1] = array[load--];
        percolateDown(1);

        array.RemoveAt(load+1);
        return data;
    }
    //Percolate down the tree the object located at array[index].
    public void percolateDown(int index)
    {
        int child;
        int tmp = array[index];
        for (; (child = 2 * index) <= load; index = child)
        {
            if (child != load && array[child + 1].CompareTo(array[child]) < 0)
                child++;
            if (array[child].CompareTo(tmp) < 0)
                array[index] = array[child];
            else
                break;
        }
        array[index] = tmp;
    }
    public void buildHeap()
    {
        for (int i = load / 2; i > 0; i--)
            percolateDown(i);
    }
    public void insert(int obj)
    {
        //percolate up
        load++;
        array.Add(obj);
        int hole = load; //number of elements in heap
        for (; hole > 1 && obj.CompareTo(array[hole / 2]) < 0; hole /= 2)
            array[hole] = array[hole / 2];
        array[hole] = obj;

    }

    //public int[] getArray()
    //{
    //    return array;
    //}
    public List<int> getArray()
    {
        return array;
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
                if (array[i] == int.MaxValue)
                    Debug.Log("null");
                else
                    Debug.Log(array[i]);
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
                if (array[i] != int.MaxValue)
                    str = str + array[i].ToString() + ",";
                //Debug.Log(array[i]);
            }
        }
        else
            return "EMPTY";
        return str;
    }
}
