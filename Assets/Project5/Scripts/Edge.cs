using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour {
    public string startVertex, endVertex;
    public Edge(string start, string end)
    {
        startVertex = start;
        endVertex = end;
    }
    public string getStartVertex()
    {
        return startVertex;
    }
    public string getEndVertex()
    {
        return endVertex;
    }
    public override string ToString()
    {
        return "(" + startVertex + "," + endVertex + ")";
    }
}
