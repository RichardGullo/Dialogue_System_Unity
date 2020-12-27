using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Program
{
    // Global File Reference Variable
    public static StreamReader sr;

    // Globals
    static String[] lines;
    static String line;
    static int lineNumber;

    public Dialogue LoadFile(string filename)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(filename);

        lines = textAsset.text.Split('\n');
        line = null;
        lineNumber = 0;

        Dialogue dialogueList = new Dialogue();

        while (lineNumber < lines.Length)
        {
            line = lines[lineNumber];

            // Continue if line begins with any of these characters
            if (line == null || line == "" || line[0] == '\r' || line[0] == '\n' || line[0] == '#')
            {
                lineNumber++;
                continue;
            }

            DialogueNode dialogue = parseDialogueNode();

            dialogueList.addNode(dialogue.nodeId, dialogue);
        }

        return dialogueList;
    }


    // Function used to Build your DialogueNode
    public DialogueNode parseDialogueNode()
    {
        bool hasStatements = true;

        DialogueNode node = new DialogueNode();

        
        while (hasStatements)
        {
            string data = lines[lineNumber++];

            string prefix = ExtractUpToDelimeter(data, ':');

            // Parse Dialogue Node data
            if (prefix.ToLower() == "nodeid")
            {
                node.nodeId = parseId(data, ':');
            }
            // Dialogue Text
            else if (prefix.ToLower() == "text")
            {
                node.text = parseText(data, ':');
            }
            // Parse Option Data
            else if (prefix.ToLower() == "options")
            {
                OptionNode op;

                // Number of options
                int optionCount = parseId(data, ':');

                for (int i = 0; i < optionCount; i++)
                {

                    op = new OptionNode();
                    data = lines[lineNumber++];
                    prefix = ExtractUpToDelimeter(data, ':');

                    // Keep looping until you come across destId field
                    while (prefix.ToLower() != "destid")
                    {
                        // Text
                        if (prefix.ToLower() == "text")
                        {
                            op.text = parseText(data, ':');
                        }
                        else
                        {
                            Console.WriteLine("Error in options");
                        }

                        data = lines[lineNumber++];
                        prefix = ExtractUpToDelimeter(data, ':');
                    }


                    int destId = parseId(data, ':');

                    op.destId = destId;

                    node.addOption(op);

                }

                // End of Dialogue Node block
                hasStatements = false;
            }
            else
            {
                // Perfect place to throw an error and end program.
                Console.WriteLine("Not a field");
            }
        }

        return node;
    }

    // Function to extract text from file (uses statement)
    public static string parseText(string statement, char delimeter)
    {
        // String to hold the data we actually want.
        StringBuilder buffer = new StringBuilder();

        // String that holds the modified raw text
        StringBuilder temp = new StringBuilder();

        // Holds the actual raw text
        String data;

        // We use this to jump to to the delimeter in our .Substring function
        int index;

        // Grab line from text file
        data = statement;

        // Jump to relevant delimeter
        index = data.IndexOf(delimeter);

        // Store the modified raw text in data
        data = data.Substring(index+1);

        // Append it to string builder so that we can manipulate it
        temp.Append(data);

        // Only append to relevant characters to our buffer
        for (int i = (temp[0] == ' ') ? 1 : 0; i < temp.Length; i++)
        {
            if (temp[i] == '\n' || temp[i] == '\r' || temp[i] == ':')
                continue;

            buffer.Append(temp[i]);
        }

        return buffer.ToString();
    }


    // Function to extract ids from text file (uses statement)
    public static int parseId(string statement, char delimeter)
    {
        // String to hold the data we actually want.
        StringBuilder buffer = new StringBuilder();

        // String that holds the modified raw text
        StringBuilder temp = new StringBuilder();

        // Holds the actual raw text
        string data;

        // We use this to jump to to the delimeter in our .Substring function
        int index;

        // Grab line from text file
        data = statement;

        // Jump to relevant delimeter
        index = data.IndexOf(delimeter);

        // Store the modified raw to text in data
        data = data.Substring(index);

        // Append it to string builder so that we can manipulate it
        temp.Append(data);

        //Debug.Log("Parsing ID, line: " + temp.ToString());

        // Only append to our buffer if it is a number
        for (int i = 0; i < temp.Length; i++)
        {
            if (Char.IsDigit(temp[i]) || temp[i] == '-')
            {
                buffer.Append(temp[i]);
            }
        }

        return Int32.Parse(buffer.ToString());
    }

    // Function that extracts text up to a specified delimeter
    public string ExtractUpToDelimeter(string statement, char delimeter)
    {
        // String to hold the data we actually want.
        StringBuilder buffer = new StringBuilder();

        // String that holds the modified raw text
        StringBuilder temp = new StringBuilder();

        // Holds the actual raw text
        String data = statement;

        // We use this to jump to to the delimeter in our .Substring function
        int index;

        // Jump to relevant delimeter
        index = data.IndexOf(delimeter);

        // Store the modified raw text in data
        data = data.Substring(0, index);

        // Append it to string builder so that we can manipulate it
        temp.Append(data);

        // Only append to relevant characters to our buffer
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i] == '\n' || temp[i] == '\r' || temp[i] == ':' || temp[i] == ' ')
                continue;

            buffer.Append(temp[i]);
        }

        return buffer.ToString();
    }


    // Function to extract ids from text file
    public static int parseId(String str)
    {
        // String to hold the data we actually want.
        StringBuilder buffer = new StringBuilder();

        // String that holds the modified raw text
        StringBuilder temp = new StringBuilder();

        // Holds the actual raw text
        String data;

        // We use this to jump to to the delimeter in our .Substring function
        int index;

        // Grab line from text file
        data = str;

        // Jump to relevant delimeter
        index = data.IndexOf(":");

        // Store the modified raw to text in data
        data = data.Substring(index);

        // Append it to string builder so that we can manipulate it
        temp.Append(data);

        // Only append to our buffer if it is a number
        for (int j = 0; j < temp.Length; j++)
        {
            if (Char.IsDigit(temp[j]) || temp[j] == '-')
                buffer.Append(temp[j]);
        }

        if (lineNumber < lines.Length)
            line = lines[lineNumber++];

        return Int32.Parse(buffer.ToString());
    }

    // Function to extract text from file
    public static string parseText(String str)
    {
        // String to hold the data we actually want.
        StringBuilder buffer = new StringBuilder();

        // String that holds the modified raw text
        StringBuilder temp = new StringBuilder();

        // Holds the actual raw text
        String data;

        // We use this to jump to to the delimeter in our .Substring function
        int index;

        // Grab line from text file
        data = str;

        // Jump to relevant delimeter
        index = data.IndexOf(":");

        // Store the modified raw to text in data
        data = data.Substring(index);

        // Append it to string builder so that we can manipulate it
        temp.Append(data);

        // Only append to our buffer if it is
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i] == '\n' || temp[i] == '\r' || temp[i] == ':')
                continue;

            buffer.Append(temp[i]);
        }

        if (lineNumber < lines.Length)
            line = lines[lineNumber++];

        return buffer.ToString();
    }

    // Not used at the moment but could be useful in future
    public static void consumeFeeds()
    {
        if (sr.Peek() >= 0)
            return;

        if ((char)sr.Peek() == '\r')
            sr.Read();

        if ((char)sr.Peek() == '\n')
            sr.Read();
    }
}

