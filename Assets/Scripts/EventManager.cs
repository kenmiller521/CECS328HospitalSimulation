using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {
    private bool _inMainMenu;
    private MinHeap H;
    public InputField mainMenuInputField,nonMainMenuInputField;
    private string userInput,nonMainMenuUserInput;
    public Text menuText,outputText;
    const string CONST_MENU_TEXT = "Enter a choice: \n1: Create a new heap H. Displays H\n2: Enter single element into heap H. Displays H\n3: Pop an elemeny from H. Displays H\n4: Do simulation\n5: Repeat simulation with developed algorithm";
    public GameObject[] nodeArray;
    // Use this for initialization
	void Start () {
        _inMainMenu = true;
        nonMainMenuUserInput = null;
        H = new MinHeap();
        nodeArray = GameObject.FindGameObjectsWithTag("Node");
        swapElements();
        foreach (GameObject obj in nodeArray)
            obj.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(new Vector3(-0.5f, -0.5f, 0), new Vector3(100.5f, -0.5f, 0));
        //Gizmos.DrawLine(new Vector3(100.5f, -0.5f, 0), new Vector3(100.5f, 100.5f, 0));
        //Gizmos.DrawLine(new Vector3(100.5f, 100.5f, 0), new Vector3(-0.5f, 100.5f, 0));
        //Gizmos.DrawLine(new Vector3(-.5f, 100.5f, 0), new Vector3(-.5f, -0.5f, 0));
        for(int i = 0; i <= 101; i++)
        {
            Gizmos.DrawLine(new Vector3(i-0.5f, -0.5f, 0), new Vector3(i-0.5f, 100.5f, 0));
        }
        for(int i = 0; i <= 101; i++)
        {
            Gizmos.DrawLine(new Vector3(-0.5f, i-0.5f, 0), new Vector3(100.5f, i-0.5f, 0));
        }
    }
    public void InputFieldValueChange()
    {
        userInput = mainMenuInputField.text;
            StartCoroutine("MainMenu");
       // Debug.Log(inputField.text);
    }
    public void nonMainMenuInputFieldValueChange()
    {
        nonMainMenuUserInput = nonMainMenuInputField.text;
    }
    IEnumerator MainMenu()
    {
        foreach (GameObject obj in nodeArray)
            obj.SetActive(false);
        mainMenuInputField.gameObject.SetActive(false);
        nonMainMenuInputField.gameObject.SetActive(true);
        switch(userInput)
        {
            case "1":
                menuText.text = CONST_MENU_TEXT + "\n\nYou chose to create a new heap. Either type 'empty' or build an empty heap or enter a comma-delimited array of integers";
                nonMainMenuInputField.placeholder.GetComponent<Text>().text = "Enter your array or type 'empty' to build an empty heap";
                while (nonMainMenuUserInput == null)
                    yield return new WaitForSeconds(0.1f);
                if (nonMainMenuUserInput.ToUpper() == "EMPTY")
                {
                    //Debug.Log("CREATING EMPTY HEAP");
                    H = new MinHeap();
                    H.printHeap();
                    printHeapToBranchUI();
                }
                else
                {
                    string s = nonMainMenuUserInput;
                    string[] values = s.Split(',');
                    int[] intValues = new int[values.Length+1];
                    for (int i = 0; i < values.Length; i++)
                    {
                        intValues[i + 1] = Convert.ToInt32(values[i]);
                    }
                    intValues[0] = Int32.MaxValue;
                    H = new MinHeap(intValues.Length-1, intValues);
                    H.buildHeap();
                    H.printHeap();
                    outputText.text = H.ToString();
                    printHeapToBranchUI();
                }
                //yield return new WaitForSeconds(2);
                break;
            case "2":
                break;
            case "3":
                break;
            case "4":
                break;
        }
        //reset things back
        menuText.text = CONST_MENU_TEXT;
        nonMainMenuUserInput = null;
        mainMenuInputField.gameObject.SetActive(true);
        nonMainMenuInputField.gameObject.SetActive(false);
        yield return null;
    }
    void printHeapToBranchUI()
    {
        int[] a = H.getArray();
        for(int i = 1; i < a.Length; i++)
        {
            nodeArray[i-1].SetActive(true);
            nodeArray[i-1].GetComponentInChildren<Text>().text = a[i].ToString();
        }
    }
    void swapElements()
    {
        GameObject temp;
        for(int i = 0; i < nodeArray.Length/2; i++)
        {
            temp = nodeArray[i];           
            nodeArray[i] = nodeArray[nodeArray.Length - i-1];
            nodeArray[nodeArray.Length - i-1] = temp;
        }
    }
}