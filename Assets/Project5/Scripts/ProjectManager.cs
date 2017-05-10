using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectManager : MonoBehaviour {
    public Text mainText,outputText;
    public InputField inputField,otherInputField;
    public GameObject[] nodes;
    public List<GameObject> connectingNodes;
    public List<GameObject> uniqueNodes;
    public GameObject lineRendererObject;
    private DirectedGraph dg;
    private int userInput;
    private string otherUserInput;
    private string[] tokens;
	// Use this for initialization
	void Start () {
        //StartCoroutine(MainMenu());
        connectingNodes = new List<GameObject>();
        nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject obj in nodes)
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
                //dg.breadthFirstTraversal();
                outputText.text = dg.breadthFirstTraversal();
                inputField.gameObject.SetActive(true);
                otherInputField.gameObject.SetActive(false);
                break;
            case 2:
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
}
