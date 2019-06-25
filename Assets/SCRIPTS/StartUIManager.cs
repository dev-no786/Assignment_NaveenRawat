using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartUIManager : MonoBehaviour {

    int h_score = 0;
    public Text highscore;
	// Use this for initialization
	void Start () {

		//reset the highschore each time the game resets
        //PlayerPrefs.SetInt("High_Score", 0);
        h_score = PlayerPrefs.GetInt("High_Score", 0);

	}
	
	// Update is called once per frame
	void Update () {

        print(h_score);
		h_score=PlayerPrefs.GetInt("High_Score");
        highscore.text = h_score.ToString();
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Play()
    {
        Application.LoadLevel("game");
    }

}
