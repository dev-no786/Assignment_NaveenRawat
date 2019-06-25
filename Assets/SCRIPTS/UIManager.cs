using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    GameObject[] pauseObjects;
    GameObject[] finishObjects;
    
    // Use this for initialization
	int h_score=0;
	public Text highscore;
    void Start()
    {
        Time.timeScale = 1;

        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");            //gets all objects with tag ShowOnPause
        finishObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");          //gets all objects with tag ShowOnFinish

		
        hidePaused();
        hideFinished();

    }

    // Update is called once per frame
    void Update()
    {

        //uses the esc button to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			
            if (Time.timeScale == 1 && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().gameOver == false)
            {
                print("paused");
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0 && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().gameOver == false)
            {
                print("key press");
                Time.timeScale = 1;
                hidePaused();
            }
        }

        //shows finish gameobjects if player is dead and timescale = 0
        if (Time.timeScale == 0 && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().gameOver == true)
        {
            showFinished();
        }
    }


    //Reloads the Level
    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
			print("paused");
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
			print("resumed");
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //shows objects with ShowOnFinish tag
    public void showFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnFinish tag
    public void hideFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(false);
        }
    }

    //loads inputted level
    public void LoadLevel()
    {
        Application.LoadLevel("start");
    }

	void SetHighScore()
	{
		print(h_score);
		highscore.text=h_score.ToString();
	}
}
