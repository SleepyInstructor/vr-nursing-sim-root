using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {
    // Total number of different items to find 
    public const int NumItems = 6;
    // Maximum number of one type of item to retrieve
    public const int MaxToFind = 3;
    // Number of seconds permitted to find one type of object
    public const int TimeLimit = 120;

    public Text instructions;
    public Text countdown;

    // Storing textures
    private string[] itemNames = { "syringe", "head support", "towel", "saw", "medicine", "gloves"};

    // For keeping track of player progress
    private bool[] retrievedItems;
    private bool gameOver;
    private int currentTask;
    private int itemQuantity;
    private int itemsBrought;
    private float remainingTime;

	// Use this for initialization
	private void Start () {
        remainingTime = TimeLimit;

        // defaults
        retrievedItems = new bool[NumItems];
        currentTask = 0; 
        itemQuantity = 0;
        itemsBrought = 0;
        gameOver = false; 

        // load textures
        for (int i = 0; i < NumItems; i++)
                retrievedItems[i] = false; // no items have been retrieved yet

        // start the game
        SetTask();
    }
	
	// Update is called once per frame
	private void Update () {
        if (!gameOver) { // if game is not over yet
            // if the player has correctly brought back an item, assign them a new task
            if (retrievedItems[currentTask])
                SetTask();

            DisplayTime();
        }
    }

    /** SetTask
    * A random generator chooses which object the player should retrieve next
    * Displays the item on the TV screen in the patient room
    **/
    private void SetTask() {
        IsGameComplete();
        if (!gameOver)
        {
            // generates a new number until it finds an item that hasn't been retrieved yet
            do {
                // excludes the correct, incorrect, and congratulations textures
                currentTask = (int)Random.Range(0, NumItems);
            } while (retrievedItems[currentTask]);

            // determines the quantity of items the player needs to retrieve
            itemQuantity = (int)Random.Range(1, MaxToFind + 1);

            // display item
            DisplayInstructions();
        }
    }

    /** IsTaskComplete
    * Checks to see if the player has completed a task
    **/
    public void IsTaskComplete(string id) {
        if(!gameOver && id == itemNames[currentTask]){

            itemsBrought++;
            // reset timer and tracker
            if (itemsBrought == itemQuantity) {
                retrievedItems[currentTask] = true;
                remainingTime = TimeLimit;
                itemsBrought = 0;
            } else {
                DisplayInstructions();
            }
        }
    }

    /** IsGameComplete
    * Checks to see if all items have been retrieved
    **/
    private void IsGameComplete() {
        // if the timer has run out
        if (gameOver) {
            instructions.text = "Time's up!";
            countdown.text = "";
            return;
        }

        for (int i = 0; i < NumItems; i++)
            if (!retrievedItems[i])
                return;

        gameOver = true;
        instructions.text = "You did it!";
        countdown.text = "";
    }

    /** DisplayInstructions
    * Displays the number of remaining items the player needs to collect on the UI
    * on the wall in the storage room
    **/
    private void DisplayInstructions() {
        // displays on UI 
        instructions.text = "I need " + (itemQuantity - itemsBrought);
        // special cases
        if (currentTask == 4) {
            if (itemQuantity - itemsBrought != 1)
                instructions.text += " brown bottles of medicine";
            else
                instructions.text += " brown bottle of medicine";
        } else if (currentTask == 5) {
            if (itemQuantity - itemsBrought != 1)
                instructions.text += " boxes of gloves";
            else
                instructions.text += " box of gloves";
        } else {
            instructions.text += " " + itemNames[currentTask];
            if (itemQuantity - itemsBrought != 1)
                instructions.text += "s"; // for plurals
        }
    }

    /** DisplayTime
     * Updates UI timer and formats minutes and seconds
     **/
    private void DisplayTime() {
        // update timer
        remainingTime -= Time.deltaTime;
        if (remainingTime > 0) {
            countdown.text = "in " + (int)(remainingTime / 60) + " min ";
            countdown.text += (int)(remainingTime % 60) + "sec";
        } else {
            gameOver = true;
            IsGameComplete();
        }
    }
}
