using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager2 : MonoBehaviour
{
    public GameObject Manager;
    public GameObject Canvas;
    public GameObject DialoguePanel;
    public GameObject TextBox;
    public GameObject ScrollListContent;
    public Button[] Choices;
    public Dialogue dialogue;

    private GameObject CurrentPanel;
    public GameObject ContinueButton;
    public static int currentNode = 0;

    // Start is called before the first frame update
    void Start()
    {
        Program p = new Program();

        dialogue = p.LoadFile("Dialogue/sample");

        // Add Listeners
        Choices[0].onClick.AddListener(choiceOption01);
        Choices[1].onClick.AddListener(choiceOption02);
        Choices[2].onClick.AddListener(choiceOption03);

        TextBox.GetComponent<Text>().text = dialogue.nodes[currentNode].text;

        // Start Choices as Inactive
        for (int i = 0; i < 3; i++)
            Choices[i].gameObject.SetActive(false);

        for (int i = 0; i < dialogue.nodes[currentNode].options.Count; i++)
        {
            Choices[i].gameObject.SetActive(true);
            Choices[i].GetComponent<Button>().GetComponentInChildren<Text>().text = dialogue.nodes[currentNode].options[i].text;
        }
    }

    // Do this if user clicks 1st option
    public void choiceOption01()
    {
        // This is where we currently are in the dialogue
        currentNode = dialogue.nodes[currentNode].options[0].destId;

        // If the current node is -1, end dialogue.
        if (currentNode == -1)
        {
            CurrentPanel.SetActive(false);
            return;
        }

        // Initialize new Dialogue
        CurrentPanel = Instantiate(DialoguePanel);
        CurrentPanel.SetActive(true);

        // Set the parent to Canvas
        CurrentPanel.transform.SetParent(ScrollListContent.transform, false);

        // Get text box of new dialogue
        Text newTextBox = CurrentPanel.GetComponentInChildren<Text>();

        // Add text of currentNode to new Dialogue
        newTextBox.GetComponent<Text>().text = dialogue.nodes[currentNode].text;


        // Remove Listeners
        for(int i = 0; i < 3; i++)
        {
            Choices[i].onClick.RemoveAllListeners();
        }

        // Reset Choices to new Dialogue
        Choices = CurrentPanel.GetComponentsInChildren<Button>();
        Choices[0].onClick.AddListener(choiceOption01);
        Choices[1].onClick.AddListener(choiceOption02);
        Choices[2].onClick.AddListener(choiceOption03);

        // Start the New Buttons as Inactive
        for (int i = 0; i < 3; i++)
            Choices[i].gameObject.SetActive(false);

        // Set Text to New Buttons
        for (int i = 0; i < dialogue.nodes[currentNode].options.Count; i++)
        {
            Choices[i].gameObject.SetActive(true);
            Choices[i].GetComponent<Button>().GetComponentInChildren<Text>().text = dialogue.nodes[currentNode].options[i].text;
        }

        GameObject.Find("ScrollList").GetComponent<ScrollRect>().velocity = new Vector2(0f, 1000f);

    }

    // Do this if user clicks 2nd option
    public void choiceOption02()
    {
        // This is where we currently are in the dialogue
        currentNode = dialogue.nodes[currentNode].options[1].destId;

        // If the current node is -1, end dialogue.
        if (currentNode == -1)
        {
            CurrentPanel.SetActive(false);
            return;
        }

        // Initialize new Dialogue
        CurrentPanel = Instantiate(DialoguePanel);
        CurrentPanel.SetActive(true);

        // Set the parent to Canvas
        CurrentPanel.transform.SetParent(ScrollListContent.transform, false);

        // Get text box of new dialogue
        Text newTextBox = CurrentPanel.GetComponentInChildren<Text>();

        // Add text of currentNode to new Dialogue
        newTextBox.GetComponent<Text>().text = dialogue.nodes[currentNode].text;


        // Remove Listeners
        for (int i = 0; i < 3; i++)
        {
            Choices[i].onClick.RemoveAllListeners();
        }

        // Reset Choices to new Dialogue
        Choices = CurrentPanel.GetComponentsInChildren<Button>();
        Choices[0].onClick.AddListener(choiceOption01);
        Choices[1].onClick.AddListener(choiceOption02);
        Choices[2].onClick.AddListener(choiceOption03);

        // Start the New Buttons as Inactive
        for (int i = 0; i < 3; i++)
            Choices[i].gameObject.SetActive(false);

        // Set Text to New Buttons
        for (int i = 0; i < dialogue.nodes[currentNode].options.Count; i++)
        {
            Choices[i].gameObject.SetActive(true);
            Choices[i].GetComponent<Button>().GetComponentInChildren<Text>().text = dialogue.nodes[currentNode].options[i].text;
        }



        GameObject.Find("ScrollList").GetComponent<ScrollRect>().velocity = new Vector2(0f, 1000f);
    }

    // Do this if user clicks 3rd option
    public void choiceOption03()
    {
        // This is where we currently are in the dialogue
        currentNode = dialogue.nodes[currentNode].options[2].destId;

        // If the current node is -1, end dialogue.
        if (currentNode == -1)
        {
            CurrentPanel.SetActive(false);
            return;
        }

        // Initialize new Dialogue
        CurrentPanel = Instantiate(DialoguePanel);
        CurrentPanel.SetActive(true);

        // Set the parent to Canvas
        CurrentPanel.transform.SetParent(ScrollListContent.transform, false);

        // Get text box of new dialogue
        Text newTextBox = CurrentPanel.GetComponentInChildren<Text>();

        // Add text of currentNode to new Dialogue
        newTextBox.GetComponent<Text>().text = dialogue.nodes[currentNode].text;


        // Remove Listeners
        for (int i = 0; i < 3; i++)
        {
            Choices[i].onClick.RemoveAllListeners();
        }

        // Reset Choices to new Dialogue
        Choices = CurrentPanel.GetComponentsInChildren<Button>();
        Choices[0].onClick.AddListener(choiceOption01);
        Choices[1].onClick.AddListener(choiceOption02);
        Choices[2].onClick.AddListener(choiceOption03);

        // Start the New Buttons as Inactive
        for (int i = 0; i < 3; i++)
            Choices[i].gameObject.SetActive(false);

        // Set Text to New Buttons
        for (int i = 0; i < dialogue.nodes[currentNode].options.Count; i++)
        {
            Choices[i].gameObject.SetActive(true);
            Choices[i].GetComponent<Button>().GetComponentInChildren<Text>().text = dialogue.nodes[currentNode].options[i].text;
        }

        // This is your script David
        // DialogueSizer();

        GameObject.Find("ScrollList").GetComponent<ScrollRect>().velocity = new Vector2(0f, 1000f);

    }
}
