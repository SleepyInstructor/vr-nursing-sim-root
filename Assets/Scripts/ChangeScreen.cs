using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeScreen : MonoBehaviour {
    public const int NumTextures = 9;
    public const int NumItems = 6;
    public Text uiText;

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

	// Use this for initialization
	private void Start () {
        render = GetComponent<Renderer>();
        textureArray = new Texture[NumTextures];
        retrievedItems = new bool[NumItems];
        currentTask = 0; // default
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
        if (!gameOver) { // if game is continuing
            // if the player has correctly brought back an item, assign them a new task
            if (retrievedItems[currentTask])
                SetTask();
        }
    }

    /** IsTaskComplete
    * Checks to see if the player has completed a task
    **/
    public void IsTaskComplete(string id) {
        if(id == textureNames[currentTask]){
            // Show result screen for 5 seconds
            DisplayAndWait(5, true);
            retrievedItems[currentTask] = true;
        }
        // Show result screen for 5 seconds
        DisplayAndWait(5, false);
    }

    /** IsGameComplete
    * Checks to see if all items have been retrieved
    **/
    private void IsGameComplete() {
        for (int i = 0; i < NumItems; i++)
            if (!retrievedItems[i])
                return;
        gameOver = true;
        render.materials[1].mainTexture = textureArray[Congratulations];
        uiText.text = "You did it!";
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
            // only for show at the moment
            int itemQuantity = (int)Random.Range(1, 6); 

            render.materials[1].mainTexture = textureArray[currentTask];
            uiText.text = "Find " + itemQuantity + " " + textureNames[currentTask];
            if (itemQuantity != 1)
                uiText.text += "s"; // for plurals
        }
    }

    /** DisplayAndWait
    * A method to wait for a certain number of seconds
    * Doesn't work yet
    **/
    private IEnumerator DisplayAndWait(int seconds, bool isCorrect){
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
