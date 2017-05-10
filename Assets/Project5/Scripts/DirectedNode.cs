using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DirectedNode : MonoBehaviour {
    private string nodeName;
    private List<DirectedNode> neighbors = new List<DirectedNode>();
    private List<DirectedNode> parents = new List<DirectedNode>();
    private List<DirectedNode> children = new List<DirectedNode>();
    private bool visited;//used in some algorithms for marking the node
    public int distance;
    public DirectedNode(string name)
    {
        neighbors = new List<DirectedNode>();
        parents = new List<DirectedNode>();
        children = new List<DirectedNode>();
        visited = false;
        nodeName = name;
    }
    public bool isVisisted()
    {
        return visited;
    }
    public void setVisited(bool v)
    {
        visited = v;
    }
    public string getNodeName()
    {
        return nodeName;
    }
    public void addChild(DirectedNode dn)
    {
        children.Add(dn);
    }
    public void addParent(DirectedNode dn)
    {
        parents.Add(dn);
    }
    public List<DirectedNode> getParentList()
    {
        return parents;
    }
    public List<DirectedNode> getChildList()
    {
        return children;
    }
    public void printChildList()
    {
        foreach (DirectedNode dn in children)
            Debug.Log(nodeName + " IS A CHILD OF " + dn.getNodeName());
    }
    public void printParentList()
    {
        foreach (DirectedNode dn in parents)
            Debug.Log(nodeName + " IS A PARENT OF " + dn.getNodeName());
    }
    public int traceBack(DirectedNode targetNode, ref int counter)
    {
        counter++;
        if(parents.Count != 0)
        {
            DirectedNode dn = parents[0];
            if (dn.getNodeName() == targetNode.getNodeName())
                return counter;
            else
                dn.traceBack(targetNode, ref counter);
        }
        
        return counter;
    }
    public void clearLists()
    {
        parents.Clear();
        children.Clear();
    }
}
