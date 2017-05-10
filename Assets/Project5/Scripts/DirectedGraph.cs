﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedGraph : MonoBehaviour {
    private List<DirectedNode> nodes;
    private int size; //number of edges for this graph

    public DirectedGraph()
    {
        nodes = new List<DirectedNode>();
        size = 0;
    }

    public int getGraphSize()
    {
        return size;
    }
    public void insertNode(DirectedNode node)
    {
        nodes.Add(node);
        size++;
    }
    public void printGraph()
    {
        foreach(DirectedNode dn in nodes)
        {
            Debug.Log(dn.getNodeName());
        }
    }
    public DirectedNode getNode(string nodeName)
    {
        foreach(DirectedNode dn in nodes)
        {
            if (dn.getNodeName() == nodeName)
                return dn;
        }
        return null;
    }
    public void setDirectedNodeChildren(List<GameObject> nodesList)
    {
        if (nodesList.Count % 2 == 0)
        {
            for (int i = 0; i < nodesList.Count; i++)
            {
                DirectedNode dn = getNode(nodesList[i].name.ToUpper());
                //Debug.Log(getNode(nodesList[i + 1].name.ToUpper()).getNodeName() + " is child of " + dn.getNodeName());
                dn.addChild(getNode(nodesList[i + 1].name.ToUpper()));
                i++;
            }
        }
        else
        {
            for (int i = 0; i < nodesList.Count-1; i++)
            {
                DirectedNode dn = getNode(nodesList[i].name.ToUpper());
                //Debug.Log(getNode(nodesList[i + 1].name.ToUpper()).getNodeName() + " is child of " + dn.getNodeName());
                dn.addChild(getNode(nodesList[i + 1].name.ToUpper()));
                i++;
            }
        }
    }
    public void setDirectedNodeParent(List<GameObject> nodesList)
    { 
        if(nodesList.Count%2==0)
        {
            for (int i = 0; i < nodesList.Count; i++)
            {
                DirectedNode dn = getNode(nodesList[i].name.ToUpper());
                //Debug.Log(getNode(nodesList[i + 1].name.ToUpper()).getNodeName() + " is parent of " + dn.getNodeName());
                dn.addParent(getNode(nodesList[i + 1].name.ToUpper()));
                i++;


            }
        }
        else
        {
            for (int i = 1; i < nodesList.Count; i++)
            {
                DirectedNode dn = getNode(nodesList[i].name.ToUpper());
                //Debug.Log(getNode(nodesList[i + 1].name.ToUpper()).getNodeName() + " is parent of " + dn.getNodeName());
                dn.addParent(getNode(nodesList[i + 1].name.ToUpper()));
                i++;
            }
        }
        
        
        
    }
    public void printNodesChildList()
    {
        foreach (DirectedNode dn in nodes)
            dn.printChildList();
    }
    public void printNodesParentList()
    {
        foreach (DirectedNode dn in nodes)
            dn.printParentList();
    }
    public string breadthFirstTraversal()
    {
        string str = null;
        int counter = 0;
        Queue<DirectedNode> queue = new Queue<DirectedNode>();
        nodes[0].setVisited(true);
        queue.Enqueue(nodes[0]);
        while(queue.Count != 0)
        {
            //Remove the vertex at the beginning of the queue
            DirectedNode currentNode = queue.Dequeue();
            //Get the children vertices of the newly dequeued node
            List<DirectedNode> nodeList = currentNode.getChildList();
            //go through list
            foreach (DirectedNode dn in nodeList)
            {
                //If the node has NOT been visited
                if (dn.isVisisted() == false)
                {
                    //set it to have been visisted and add it to the queue
                    dn.setVisited(true);
                    dn.distance = counter;
                    queue.Enqueue(dn);
                    int count = 0;
                    str += "DISTANCE FROM " + dn.getNodeName() + " TO A IS " + (dn.traceBack(nodes[0], ref count)) + "\n";
                }
            }
        }
        return str;
    }
    public string breadthFirstTraversalOption2()
    {
        string str = "DID NOT FIND NODE E";
        int counter = 0;
        Queue<DirectedNode> queue = new Queue<DirectedNode>();
        DirectedNode nodeToFind = getNode("E");
        DirectedNode nodeToStart = getNode("B");
        nodeToStart.setVisited(true);
        queue.Enqueue(nodeToStart);
        while (queue.Count != 0)
        {
            //Remove the vertex at the beginning of the queue
            DirectedNode currentNode = queue.Dequeue();
            //Get the children vertices of the newly dequeued node
            List<DirectedNode> nodeList = currentNode.getChildList();
            //go through list
            foreach (DirectedNode dn in nodeList)
            {
                //If the node has NOT been visited
                if (dn.isVisisted() == false)
                {
                    //set it to have been visisted and add it to the queue
                    dn.setVisited(true);
                    dn.distance = counter;
                    queue.Enqueue(dn);
                    if(dn.getNodeName() == nodeToFind.getNodeName())
                    {
                        str = "FOUND NODE E";
                    }
                    //int count = 0;
                    //str += "DISTANCE FROM " + dn.getNodeName() + " TO A IS " + (dn.traceBack(nodes[0], ref count)) + "\n";
                }
            }
        }
        return str;
    }
    public string outputUnconnectedNodes()
    {
        string str = null;
        foreach(DirectedNode dn in nodes)
        {
            if(dn.isVisisted() == false)
                str+= "DISTANCE FROM " + dn.getNodeName() + " TO A IS INF \n";
        }
        return str;
    }
    public void clearLists()
    {
        foreach (DirectedNode dn in nodes)
            dn.clearLists();
    }
}
