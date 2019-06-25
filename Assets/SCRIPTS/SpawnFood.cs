//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnFood : MonoBehaviour {
	
	public GameObject foodPrefab;
	public int score=5;
	public Text streakText;
	public Transform wallTop;
	public Transform wallBottom;
	public Transform wallLeft;
	public Transform wallRight;
	public TextAsset datfile;
	
	int streak=1;
	private Color food_color,prevColor;
	SpriteRenderer food_renderer;
	// Use this for initialization
	void Start () {

		food_color = prevColor = new Color(0.5454545f,0.5454545f,0.5454545f,0.5454545f);
		Spawn ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	//Reading and proccessing string tuples from our data file
	void FileLines()
	{
		//stroing lines in array
		string[] s = datfile.text.Split ('\n');
		
		//selecting index of the array at random
		int line = Random.Range (0, s.Length);
		
		//picking up 
		int pos = s [line].IndexOf (';');

		string sub  = s[line].Substring (0, pos);
		string[] rgba = sub.Split (' ');

		
		float r = float.Parse(rgba [0]);
		float g = float.Parse(rgba [1]);
		float b = float.Parse(rgba [2]);

		food_color = new Color (r, g, b, 1.0f);
		if(food_color==prevColor){
			streak++;
			print("same color");
		}
		else
		{
            //resert the multi/streak
			streak=1;
		}
		float p = float.Parse(s [line].Substring (pos + 1));
		score = (int)p*streak;
		Debug.Log ("Color = "+r+g+b+" Score = "+score+"Streak "+streak);

	}
	public void Spawn() {

		int x = (int)Random.Range (wallLeft.position.x, wallRight.position.x);
		int y = (int)Random.Range (wallBottom.position.y, wallTop.position.y);
		FileLines();

		Instantiate (foodPrefab, new Vector2 (x, y), Quaternion.identity);
		//Debug.Log ("x "+x +"y "+ y);
		food_renderer = GameObject.FindGameObjectWithTag("item").GetComponent<SpriteRenderer> ();
		food_renderer.material.color = food_color;
		prevColor=food_color;
		streakText.color=food_color;
        streakText.text = "   STREAK X " + (streak-1).ToString();
	}


}
