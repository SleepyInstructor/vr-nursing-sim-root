using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeScreen : MonoBehaviour {
    // Total number of textures for the TV screen
    public const int NumTextures = 9;
    // Total number of different items to find 
    public const int NumItems = 6;
    // Maximum number of one type of item to retrieve
    public const int MaxToFind = 3;
    // Number of seconds permitted to find one type of object
    public const int TimeLimit = 120;

    public Text instructions;
    public Text countdown;

    // For better readability
    public const int Correct = 6;
    public const int Incorrect = 7;
    public const int Congratulations = 8; 

    // Accessing the materials of the screen
    private Renderer render;

    // Storing textures
    private string[] textureNames = { "eraser", "tape", "notepad", "saw", "medicine", "calculator", "correct", "incorrect", "congratulations"};
    private Texture[] textureArray;

    // For keeping track of player progress
    private bool[] retrievedItems;
    private bool gameOver;
    private int currentTask;
    private int itemQuantity;
    private int itemsBrought;
    private float remainingTime;

	// Use this for initialization
	private void Start () {
        render = GetComponent<Renderer>();
        remainingTime = TimeLimit;

        // defaults
        textureArray = new Texture[NumTextures];
        retrievedItems = new bool[NumItems];
        currentTask = 0; 
        itemQuantity = 0;
        itemsBrought = 0;
        gameOver = false; 

        // load textures
        for (int i = 0; i < NumTextures; i++){
            textureArray[i] = Resources.Load("Screen Textures/" + textureNames[i]) as Texture;
            if (i < NumItems) {
                retrievedItems[i] = false; // no items have been retrieved yet
            }
        }

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

    /** IsTaskComplete
    * Checks to see if the player has completed a task
    **/
    public void IsTaskComplete(string id) {
        if(!gameOver && id == textureNames[currentTask]){
            // Show result screen for 5 seconds
            //DisplayAndWait(5, true);

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
        // Show result screen for 5 seconds
        //DisplayAndWait(5, false);
    }

    /** IsGameComplete
    * Checks to see if all items have been retrieved
    **/
    private void IsGameComplete() {
        // if the timer has run out
        if (gameOver) {
            instructions.text = "Time's up!";
            return;
        }

        for (int i = 0; i < NumItems; i++)
            if (!retrievedItems[i])
                return;

        gameOver = true;
        render.materials[1].mainTexture = textureArray[Congratulations];
        instructions.text = "You did it!";
    }

    /** SetTask
    * A random generator chooses which object the player should retrieve next
    * Displays the item on the TV screen in the patient room
    **/
    private void SetTask() {
        IsGameComplete();
        if (!gameOver){
            // generates a new number until it finds an item that hasn't been retrieved yet
            do{
                // excludes the correct, incorrect, and congratulations textures
                currentTask = (int)Random.Range(0, NumItems);
            } while (retrievedItems[currentTask]);

            // determines the quantity of items the player needs to retrieve
            itemQuantity = (int)Random.Range(1, MaxToFind+1);

            // display item
            DisplayInstructions();
        }
    }

    /** DisplayInstructions
     * Displays the number of remaining items the player needs to collect on the UI
     * on the wall in the storage room, and on the TV
     **/
    private void DisplayInstructions() {
        // displays on TV
        render.materials[1].mainTexture = textureArray[currentTask];

        // displays on UI 
        instructions.text = "Find " + (itemQuantity - itemsBrought) + " " + textureNames[currentTask];
        if (itemQuantity - itemsBrought != 1)
            instructions.text += "s"; // for plurals
    }

    /** DisplayTime
     * Updates UI timer and formats minutes and seconds
     **/
    private void DisplayTime() {
        // update timer
        remainingTime -= Time.deltaTime;
        if (remainingTime > 0) {
            countdown.text = (int)(remainingTime / 60) + "min ";
            countdown.text += (int)(remainingTime % 60) + "sec";
        } else {
            countdown.text = "0min 0sec";
            gameOver = true;
            IsGameComplete();
        }
    }

    /** DisplayAndWait
    * A method to wait for a certain number of seconds
    * Doesn't work yet
    **/
    private IEnumerator DisplayAndWait(int seconds, bool isCorrect) {
        if (isCorrect) {
            render.materials[1].mainTexture = textureArray[Correct];
        } else {
            render.materials[1].mainTexture = textureArray[Incorrect];
        }
        yield return new WaitForSecondsRealtime(seconds);
        if (!isCorrect){
            render.materials[1].mainTexture = textureArray[currentTask];
        }
    }
}
