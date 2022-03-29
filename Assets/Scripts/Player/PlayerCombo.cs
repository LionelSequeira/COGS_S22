using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    [Header("Combo")]
    public int comboLimit = 4;

    //Instance Variables
    private Queue<char> charQueue = new Queue<char>();
    private Queue<string> comboQueue = new Queue<string>();
    private PlayerController controller;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
    }

    void FixedUpdate()
    {

    }

    void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            charQueue.Enqueue('I');
            testCharQueueLimit();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            charQueue.Enqueue('O');
            testCharQueueLimit();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            charQueue.Enqueue('P');
            testCharQueueLimit();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            convertCharQueueToStringQueue();
            //printCharQueue();
            //printStringQueue();
            executeStringQueueToCombos();
        }
    }

    void convertCharQueueToStringQueue()
    {
        string temp = "";
        char ptr;

        while (charQueue.Count != 0)
        {
            ptr = charQueue.Dequeue();
            temp += ptr;

            if (temp.Length == 3 || charQueue.Count == 0 || (charQueue.Count > 0 && ptr != charQueue.Peek()))
            {
                comboQueue.Enqueue(temp);
                temp = "";
            }
        }
    }

    void executeStringQueueToCombos()
    {
        string temp = "";

        while (comboQueue.Count != 0)
        {
            temp = comboQueue.Dequeue();
            switch (temp)
            {
                case "I": 
                    controller.reduceI(5);
                    break;
                case "II":
                    controller.reduceI(20);
                    break;
                case "III":
                    controller.reduceI(50);
                    break;
                case "O":
                    controller.reduceO(5);
                    break;
                case "OO":
                    controller.reduceO(20);
                    break;
                case "OOO":
                    controller.reduceO(50);
                    break;
                case "P":
                    controller.reduceP(5);
                    break;
                case "PP":
                    controller.reduceP(20);
                    break;
                case "PPP":
                    controller.reduceP(50);
                    break;
                default: break;
            }
        }
    }

    void testCharQueueLimit()
    {
        while (charQueue.Count > comboLimit)
        {
            charQueue.Dequeue();
        }
    }

    void printStringQueue()
    {
        Debug.Log("Printing string queue");
        string[] testQueue = comboQueue.ToArray();
        foreach (string str in testQueue)
        {
            Debug.Log(str);
        }
    }

    void printCharQueue()
    {
        Debug.Log("Printing char queue");
        char[] testQueue = charQueue.ToArray();
        foreach (char letter in testQueue)
        {
            Debug.Log(letter);
        }
    }
}
