using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreen : MonoBehaviour {
    /** Images used
     * https://commons.wikimedia.org/wiki/File:Bulcolic_Mc_Mansions_Leesburg_(4949100182).jpg
     * https://commons.wikimedia.org/wiki/File:Times_Square_-_panoramio_(26).jpg
     * https://commons.wikimedia.org/wiki/File:Friday_Night_Dancing,_July_15th,_2016_(28055553520).jpg
     * https://commons.wikimedia.org/wiki/File:Chaparral_Supercell_2.JPG
     * https://commons.wikimedia.org/wiki/File:Peacock_Inachis_io.jpg
     * https://commons.wikimedia.org/wiki/File:2010_Winter_Olympics_-_Curling_-_Women_-_SWE-RUS_b.jpg
     * https://commons.wikimedia.org/wiki/File:Park_Heremastate._Brug_over_vijver.JPG
     **/

    private string[] fileNames = {"butterfly", "curling", "dancing", "house", "park", "thunder", "times sq", "shibuya"};
    private Texture[] newsReel;
    private Renderer render;

    // Use this for initialization
    void Start () {
        
        int length = fileNames.Length;
        newsReel = new Texture[length];
        render = GetComponent<Renderer>();

        for (int i = 0; i < length; i++) 
            newsReel[i] = Resources.Load<Texture>("Screen Textures/" + fileNames[i]) as Texture;

        IEnumerator display;
        display = Cycle(5);
        StartCoroutine(display);
    }

    /** Cycle
     *  Cycles through "news" photos on a timer 
     **/
    private IEnumerator Cycle(int seconds) {
        int length = fileNames.Length;
        while (true) { 
            render.materials[1].mainTexture = newsReel[(int)Random.Range(0, length)];
            yield return new WaitForSecondsRealtime(seconds);
        }
    }
}
