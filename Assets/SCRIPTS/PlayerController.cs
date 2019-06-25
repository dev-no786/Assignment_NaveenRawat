using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public PlayerDirection direction;
	UIManager uimanager;
	public float step_length = 0.2f;

	public float movement_frequency = 0.05f;

	public float counter;
	private bool move;

	public GameObject tailPrefab;

	private List<Vector2> delta_position;

	private List<Rigidbody2D> nodes;

	private Rigidbody2D main_body;
	private Rigidbody2D head_body;
	private Transform tr;

	private bool ate;
	public bool gameOver=false;
	private int count = 0;
	private int Highscore;
	public Text countText;
	public Text overText;
	// Use this for initialization
	void Start () {

		tr = transform;
		main_body = GetComponent<Rigidbody2D> ();

		InitSnakeNodes ();
		InitPlayer ();

		delta_position = new List<Vector2> () {

			new Vector2 (-step_length, 0f),		//-x.. left
			new Vector2 (0f, step_length),		//y.. up
			new Vector2 (step_length, 0f),		//x.. right
			new Vector2 (0f, -step_length)		//-y.. dowmn

		};
        Highscore = PlayerPrefs.GetInt("High_Score");
		SetCountText();

	}
	
	// Update is called once per frame
	void Update () {
		CheckMovementFrequency ();
	}

	void FixedUpdate() {

		if (move) {
			move = false;
			Move ();

		}
	}

	void InitSnakeNodes()
	{
		nodes = new List<Rigidbody2D> ();

		nodes.Add (tr.GetChild (0).GetComponent<Rigidbody2D> ());
		nodes.Add (tr.GetChild (1).GetComponent<Rigidbody2D> ());
		nodes.Add (tr.GetChild (2).GetComponent<Rigidbody2D> ());

		head_body = nodes [0];

	}

	void SetDirection()
	{
		int dirRandom = 0;
		direction = (PlayerDirection)dirRandom;

	}

	void InitPlayer()
	{
		SetDirection();
		switch (direction) {

		case PlayerDirection.DOWN:
				
			nodes [1].position = head_body.position + new Vector2(0f, Metrics.NODE);
			nodes [2].position = head_body.position + new Vector2(0f, Metrics.NODE * 2f);
			break;	

		case PlayerDirection.LEFT:
			nodes [1].position = head_body.position + new Vector2(Metrics.NODE, 0f);
			nodes [2].position = head_body.position + new Vector2(Metrics.NODE * 2f, 0f);
			break;

		case PlayerDirection.RIGHT:
			nodes [1].position = head_body.position - new Vector2(Metrics.NODE, 0f);
			nodes [2].position = head_body.position - new Vector2(Metrics.NODE * 2f, 0f);
			break;

		case PlayerDirection.UP:
			nodes [1].position = head_body.position - new Vector2 (Metrics.NODE, 0f);
			nodes [2].position = head_body.position - new Vector2 (0f, Metrics.NODE * 2f);
			break;

		}

	}

	void Move()
	{
		Vector2 dpostion = delta_position [(int)direction];

		Vector2 parentPos = head_body.position;
		Vector2 prevPosition;

		main_body.position = main_body.position + dpostion;
		head_body.position = head_body.position + dpostion;

		for (int i = 1; i < nodes.Count; i++) {
			prevPosition = nodes [i].position;

			nodes [i].position = parentPos;
			parentPos = prevPosition;
		}

		if (ate) {
			
			ate = false;
			GameObject newNode = Instantiate (tailPrefab, nodes [nodes.Count - 1].position, Quaternion.identity);

			newNode.transform.SetParent (transform, true);
			nodes.Add (newNode.GetComponent<Rigidbody2D> ());			

		}


	}


	void CheckMovementFrequency()
	{

		counter += Time.deltaTime;

		if (counter >= movement_frequency) {

			counter = 0;
			move = true;
		}

	}

	public void SetInputDirection(PlayerDirection dir)
	{
		if (dir == PlayerDirection.UP && direction == PlayerDirection.DOWN ||
		   dir == PlayerDirection.DOWN && direction == PlayerDirection.UP ||
		   dir == PlayerDirection.LEFT && direction == PlayerDirection.RIGHT ||
		   dir == PlayerDirection.RIGHT && direction == PlayerDirection.LEFT) {
			return;
		}

		direction = dir;
		//Debug.Log ("dir " + direction);
		ForceMove ();
	}

	void ForceMove()
	{
		counter = 0;
		move = false;
		Move ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.CompareTag ("item")) {

			other.gameObject.SetActive (false);
			ate = true;

			count = count + GameObject.FindGameObjectWithTag ("Respawn").GetComponent<SpawnFood> ().score;


			SetCountText ();
			GameObject.FindGameObjectWithTag ("Respawn").GetComponent<SpawnFood> ().Spawn ();

		}

		if (other.gameObject.CompareTag ("Tail") || other.gameObject.CompareTag ("Wall")) {
			
			print ("Snake has touched the tail!!");
			
			Time.timeScale = 0f;
			gameOver=true;
			overText.text = "Game Over\nScore : "+count.ToString();

            
            if (count > Highscore)
            {
                PlayerPrefs.SetInt("High_Score", count);
                overText.text +=" New HighScore!!";
            }
		}
	}

	void SetCountText()
	{
		// Update the text field of our 'countText' variable
		countText.text = "Score : " + count.ToString ();
	}

}
