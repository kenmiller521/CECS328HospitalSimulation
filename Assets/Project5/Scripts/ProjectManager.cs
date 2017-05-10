using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectManager : MonoBehaviour {
    public Text mainText,outputText;
    public InputField inputField,otherInputField;
    public GameObject[] nodes,nodes2;
    public List<GameObject> connectingNodes;
    public List<GameObject> uniqueNodes;
    public GameObject lineRendererObject;
    private List<GameObject> lineRendererObjects;
    private DirectedGraph dg;
    private int userInput;
    private string otherUserInput, edgesToAddString;
    private string[] tokens;
    private List<Edge> edgesToAdd;
    private int[] intToEdgesMapArray;
    private int[] alreadyReinitializedIntArray;
	// Use this for initialization
	void Start () {
        //StartCoroutine(MainMenu());
        lineRendererObjects = new List<GameObject>();
        connectingNodes = new List<GameObject>();
        edgesToAdd = new List<Edge>();
        intToEdgesMapArray = new int[35];
        alreadyReinitializedIntArray = new int[35];
        nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject obj in nodes)
            obj.SetActive(false);
        nodes2 = GameObject.FindGameObjectsWithTag("Node2");
        foreach (GameObject obj in nodes2)
            obj.SetActive(false);
        dg = new DirectedGraph();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator MainMenu()
    {
       switch(userInput)
        {
            case 1:
                inputField.gameObject.SetActive(false);
                otherInputField.gameObject.SetActive(true);
                outputText.text = " ";
                uniqueNodes.Clear();
                connectingNodes.Clear();
                removeLineRenderers();
                dg = new DirectedGraph();
                foreach (GameObject obj in nodes)
                    obj.SetActive(false);
                foreach (GameObject obj in nodes2)
                    obj.SetActive(false);
                mainText.text = "You have chosen to input a list of directed edges.\nEnter your edges below like this:\na,b,c,d,e,f,g,h,\nThe follow edges will be made:\n(a,b),(c,d),(e,f),(g,h)\nA breadth-first traversal starting at the first node will be displayed.";
                while(otherUserInput == null)
                    yield return null;

                tokens = otherUserInput.Split(',');
                foreach(string str in tokens)
                {
                    foreach(GameObject obj in nodes)
                    {
                        if (obj.name == str.ToUpper())
                        {
                            obj.SetActive(true);
                            connectingNodes.Add(obj);
                            if(!uniqueNodes.Contains(obj))
                            {
                                uniqueNodes.Add(obj);
                                dg.insertNode(new DirectedNode(obj.name));
                            }
                        }                           
                    }
                }
                dg.printGraph();
                for (int i = 0; i < tokens.Length; i++)
                {
                    if(i+1 <tokens.Length)
                    {
                        GameObject obj = (GameObject)Instantiate(lineRendererObject, transform.position, Quaternion.identity);
                        lineRendererObjects.Add(obj);
                        LineRenderer lr = obj.GetComponent<LineRenderer>();
                        lr.positionCount = 2;
                        lr.SetPosition(0, new Vector3(connectingNodes[i].transform.position.x, connectingNodes[i].transform.position.y, 0));
                        lr.SetPosition(1, new Vector3(connectingNodes[i + 1].transform.position.x, connectingNodes[i + 1].transform.position.y, 0));
                        lr.startColor = Random.ColorHSV();
                        lr.endColor = Random.ColorHSV();
                        i++;
                    }                    
                }
                dg.setDirectedNodeChildren(connectingNodes);
                connectingNodes.Reverse();
                dg.setDirectedNodeParent(connectingNodes);
                connectingNodes.Reverse();
                outputText.text = dg.breadthFirstTraversal();
                outputText.text += dg.outputUnconnectedNodes();
                mainText.text = "Enter choice below:\n1. Enter list of directed edges\n2. Determine minimum gas pipes\n3.Quit";
                inputField.gameObject.SetActive(true);                
                otherInputField.gameObject.SetActive(false);
                otherUserInput = null;
                break;
            case 2:

                removeLineRenderers();
                foreach (GameObject obj in nodes)
                    obj.SetActive(false);
                otherUserInput = "a,f,a,h,b,m,d,l,f,e,f,g,g,a,g,j,g,l,h,c,h,i,j,b,j,d,j,m,k,c,k,d,k,h,k,j,k,m,l,c,l,i";
                edgesToAddString = "k,j,m,i,j,b,e,m,h,l,j,k,j,i,i,d,e,a,h,c,i,m,d,i,g,c,k,l,f,d,a,b,h,k,e,f,g,h,h,g,e,h,l,m,i,b,h,j,e,d,e,b,a,c,a,h,g,d,c,j,j,l,h,m,e,l,e,j,g,j";
                string[] tokens2 = edgesToAddString.Split(',');
                //Add all the eligible edges to the list
                for(int i = 0; i < tokens2.Length;i++)
                {
                    edgesToAdd.Add(new Edge(tokens2[i].ToUpper(), tokens2[i+1].ToUpper()));
                    i++;
                }
                int numberOfEdges = 35;
                //initialize int mapping array
                for (int i = 0; i < numberOfEdges; i++)
                {
                    alreadyReinitializedIntArray[i] = i + 1;
                }
                //temp string list to output to text since Debug.Log is an expencive call
                List<string> fileOutputStringList = new List<string>();
                string updatedOtherUserInput = null;
                //Go through, in increasing order, of the number of nodes in a set i.e. set of 1 nodes, set of 2 nodes, set of 3 nodes, etc.
                for(int k = 1; k <= 2; k++)
                {
                    //while there is a next combination of the k-size subset
                    while (next_combination(intToEdgesMapArray, k, numberOfEdges))
                    {
                        //reset the graph
                        //dg = new DirectedGraph();
                        //uniqueNodes.Clear();
                        connectingNodes.Clear();
                        //this sets the number of edges to be the default set of edges before the pipeline shutdown
                        updatedOtherUserInput = otherUserInput;
                        //add the set to the output string variable
                        fileOutputStringList.Add(printArraySubset(intToEdgesMapArray, k));

                        //This for loop maps the integers in the integer array to the edges array
                        for (int i = 0; i < k; i++)
                        {
                            fileOutputStringList.Add(edgesToAdd[intToEdgesMapArray[i] - 1].ToString());
                            updatedOtherUserInput += "," + edgesToAdd[intToEdgesMapArray[i] - 1].getStartVertex() + "," + edgesToAdd[intToEdgesMapArray[i] - 1].getEndVertex();
                        }

                        fileOutputStringList.Add("\n");
                        //Removes the commas from the string
                        tokens = updatedOtherUserInput.Split(',');
                        //go through the tokens and add them to the list of connecting nodes
                        foreach (string str in tokens)
                        {
                            foreach (GameObject obj in nodes2)
                            {
                                if (obj.name == str.ToUpper())
                                {
                                    obj.SetActive(true);
                                    connectingNodes.Add(obj);
                                    if (!uniqueNodes.Contains(obj))
                                    {
                                        uniqueNodes.Add(obj);
                                        dg.insertNode(new DirectedNode(obj.name));
                                    }
                                }
                            }
                        }
                        dg.setDirectedNodeChildren(connectingNodes);
                        connectingNodes.Reverse();
                        dg.setDirectedNodeParent(connectingNodes);
                        connectingNodes.Reverse();
                        outputText.text = dg.breadthFirstTraversalOption2();
                        
                    }

                    //intToEdgesMapArray = alreadyReinitializedIntArray;
                    for (int i = 0; i < numberOfEdges; i++)
                    {
                        intToEdgesMapArray[i] = i + 1;
                    }

                }
                if(outputText.text.Contains("NOT"))
                {
                    foreach (GameObject obj in nodes2)
                        obj.SetActive(false);
                }
                Debug.Log(outputText.text);
                System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\ConnectingNodesToAdd.txt", fileOutputStringList.ToArray());
                mainText.text = "Enter choice below:\n1. Enter list of directed edges\n2. Determine minimum gas pipes\n3.Quit";
                inputField.gameObject.SetActive(true);
                otherInputField.gameObject.SetActive(false);
                otherUserInput = null;
                break;
            case 3:
                break;
        }
        yield return null;
    }
    public void inputFieldChange()
    {        
        userInput = int.Parse(inputField.text);
        StartCoroutine(MainMenu());
    }
    public void otherInputFieldChange()
    {
        otherUserInput = otherInputField.text;
    }
    private void removeLineRenderers()
    {
        for(int i = 0; i < lineRendererObjects.Count; i++)
        {
            Destroy(lineRendererObjects[i]);
        }
        lineRendererObjects.Clear();
    }
    //arr[] is the array to write in
    //k is the number of subset from the set S
    //maxS is the maximum number in the array
    bool next_combination(int[] arr, int k, int maxS)
    {
        //printArraySubset(arr, k);
        int value = 0;
        for (int j = (k - 1); j >= 0; j--)
        {
            if (arr[j] + (k - j) <= maxS)
            {
                value = arr[j] + 1;
                for (int r = j; r <= (k - 1); r++)
                {
                    arr[r] = value;
                    value++;
                }
                return true;
            }
        }
        return false;
    }
    string printArraySubset(int[] arr, int k)
    {
        string str = "{";
        for (int i = 0; i < k; i++)
        {
            if (arr[i] != 0)
                str+=arr[i];
            if (i + 1 != k)
                str+=",";
        }
        return str+"}";
    }
}
