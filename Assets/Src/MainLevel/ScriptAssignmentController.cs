using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScriptAssignmentController : MonoBehaviour {

	ScriptPlayerControls player;
	ScriptSettingsGameplay gameplaySettings;
	ScriptPlayerHUD hud;
	private int assignmentNr = 0;
	private bool collidingWithAssignmentGiver = false;

	private Text timer_text;
	private Text trash_a_text;
	private Text trash_b_text;
	private Text trash_c_text;
	private Text trash_bio_text;

	private GameObject timeSlider;
	private GameObject energySlider;

	private GameObject canvas;
	private GameObject trash_a_prefab;
	private GameObject trash_b_prefab;
	private GameObject trash_c_prefab;

	private bool readyToDeliver = false;
	private bool calculateTime = false;
	private bool showVideo = true;


	private float time_left;
	// Use this for initialization
	private int[][] assignment = {
		new int[] {0,0,0} ,
		new int[] {0,0,0} ,
		new int[] {0,0,0} ,
		new int[] {0,0,0} ,
	};

	private List<GameObject> a_trash_images = new List<GameObject>();
	private List<GameObject> b_trash_images = new List<GameObject>();
	private List<GameObject> c_trash_images = new List<GameObject>();


	private ScriptCameraControl cameraScript;
	private ScriptArea area_1;
	private ScriptArea area_2;
	private ScriptArea area_3;
	private ScriptArea tutorial;

	private int[] player_trash = {0,0,0,0}; // 1 - a trash ; 2 - b trash ; 3 - c trash , 0 - bio waste
	private int[] time_limits = new int[3];

	void Start () {
		hud = GameObject.Find ("Root").GetComponent<ScriptPlayerHUD> ();
		canvas = GameObject.Find ("Canvas");
		player = GameObject.Find ("Player").GetComponent<ScriptPlayerControls> ();
		//timer_text = GameObject.Find("timer_text").GetComponent<Text>();

		gameplaySettings = GameObject.Find ("Root").GetComponent<ScriptSettingsGameplay> ();

		time_limits[0] = gameplaySettings.assignment_1_time;
		time_limits[1] = gameplaySettings.assignment_2_time;
		time_limits[2] = gameplaySettings.assignment_3_time;

		assignment[0][0] = gameplaySettings.assignment0_a_trash;
		assignment[0][1] = gameplaySettings.assignment0_b_trash;
		assignment[0][2] = gameplaySettings.assignment0_c_trash;

		assignment[1][0] = gameplaySettings.assignment1_a_trash;
		assignment[1][1] = gameplaySettings.assignment1_b_trash;
		assignment[1][2] = gameplaySettings.assignment1_c_trash;

		assignment[2][0] = gameplaySettings.assignment2_a_trash;
		assignment[2][1] = gameplaySettings.assignment2_b_trash;
		assignment[2][2] = gameplaySettings.assignment2_c_trash;

		assignment[3][0] = 20;
		assignment[3][1] = 0;
		assignment[3][2] = 0;

		time_left = Time.time + time_limits[0];


		trash_a_prefab = (GameObject)Resources.Load("prefabs/CanvasPrefabs/trash_a_image");
		trash_b_prefab = (GameObject)Resources.Load("prefabs/CanvasPrefabs/trash_b_image");
		trash_c_prefab = (GameObject)Resources.Load("prefabs/CanvasPrefabs/trash_c_image");

		timeSlider = (GameObject)Resources.Load ("prefabs/CanvasPrefabs/timeSlider");
		timeSlider.GetComponent<Slider>().maxValue = time_limits [0];
		timeSlider.GetComponent<Slider>().minValue = 0;

		energySlider = (GameObject)Resources.Load ("prefabs/CanvasPrefabs/energyLevel");
		energySlider.GetComponent<Slider>().maxValue = 100;
		energySlider.GetComponent<Slider>().minValue = 0;

		//timer_text.gameObject.transform.position = new Vector3 (timer_text.gameObject.transform.position.x, Screen.height / 8 * 7 , 0);

		StartCoroutine (spawnStartHUD (8));
		//StartCoroutine (showStartVideo (5));
	}


	// Update is called once per frame
	void Update () {

		if (assignment [assignmentNr] [0] <= player_trash [1] && assignment [assignmentNr] [1] <= player_trash [2] && assignment [assignmentNr] [2] <= player_trash [3]) {
			readyToDeliver = true;
		} else
			readyToDeliver = false;
		
		if (time_left <= 0 && assignmentNr < 3) {
			//changeAssignment ();
		}
			
		if (calculateTime && time_left > 0 ) {
			time_left -= Time.deltaTime;
		}
		if (timeSlider)
			timeSlider.GetComponent<Slider> ().value = time_left;
	}

	IEnumerator changeAssignment( float time , int nr )
	{
		yield return new WaitForSeconds (time);
		player.resetTrashCollected ();
		deleteImages ();

		time_left = time_limits [assignmentNr];
		timeSlider.GetComponent<Slider> ().maxValue = time_limits [assignmentNr];
		spawnImages ();

		if (nr == 0) {
			GameObject.Find("Player").gameObject.transform.position  = GameObject.Find( "TriggerArea_1_2").gameObject.transform.position;
			GameObject.Find ("Player").GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
		calculateTime = true;
		showVideo = true;

	}

	IEnumerator spawnStartHUD( float time )
	{
		yield return new WaitForSeconds (time);

		timeSlider = (GameObject)Instantiate (timeSlider, new Vector2(Screen.width/20 + timeSlider.GetComponent<RectTransform>().rect.width / 2,Screen.height /20 * 19 ), timeSlider.transform.rotation);
		timeSlider.transform.SetParent (canvas.transform, false);

		energySlider = (GameObject)Instantiate (energySlider, new Vector2(-Screen.width/20 , energySlider.GetComponent<RectTransform>().rect.height / 2 ), energySlider.transform.rotation);
		energySlider.transform.SetParent (canvas.transform, false);
		GameObject.Find ("Root").GetComponent<ScriptPlayerHUD> ().setEnergyLevel (energySlider);

		spawnImages ();
		calculateTime = true;

	}

	public void setDeliveringGoods(int giverNum) // 0 - tuto , 1 - area1 , etc.
	{
		if (readyToDeliver && assignmentNr == giverNum) {
			if ( assignmentNr < 4 )
				assignmentNr += 1;
			calculateTime = false;
			showVideo = false;

			switch (giverNum) {
			case 0:
				StartCoroutine (changeAssignment (5,giverNum));
				// add on complete hud image
				break;
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			}
		} else if ( showVideo )
			GameObject.Find ("videoTexture").GetComponent<ScriptMovieTexture> ().playVideo (""+giverNum.ToString() );

	}






	private void spawnImages()
	{

		if (assignment [assignmentNr] [0] > 0) {
			int tempDif = 0;
			for (int i = 0; i < assignment [assignmentNr] [0]; i++) {
				GameObject temp_image;
				temp_image = (GameObject)Instantiate (trash_a_prefab, new Vector2(Screen.width / 10 + tempDif,Screen.height / 10 * 9) , trash_a_prefab.transform.rotation);
				tempDif += 140;
				temp_image.transform.SetParent (canvas.transform, false);
				a_trash_images.Add (temp_image);
			}
		}

		if (assignment [assignmentNr] [1] > 0) {
			int tempDif = 0;
			for (int i = 0; i < assignment [assignmentNr] [1]; i++) {
				GameObject temp_image;
				temp_image = (GameObject)Instantiate (trash_b_prefab, new Vector2(Screen.width / 10 + tempDif,Screen.height / 10 * 7) , trash_b_prefab.transform.rotation);
				tempDif += 140;
				temp_image.transform.SetParent (canvas.transform, false);
				b_trash_images.Add (temp_image);
			}
		}

		if (assignment [assignmentNr] [2] > 0) {
			int tempDif = 0;
			for (int i = 0; i < assignment [assignmentNr] [2]; i++) {
				GameObject temp_image;
				temp_image = (GameObject)Instantiate (trash_c_prefab, new Vector2(Screen.width / 10 + tempDif,Screen.height / 10 * 5) , trash_c_prefab.transform.rotation);
				tempDif += 140;
				temp_image.transform.SetParent (canvas.transform, false);
				c_trash_images.Add (temp_image);
			}
		}

			
	}

	private void deleteImages()
	{
		for (int i = 0; i < a_trash_images.Count; i++) {
			Destroy (a_trash_images [i]);
		}
		a_trash_images.Clear();
		player_trash [1] = 0;

		for (int j = 0; j < b_trash_images.Count; j++) {
			Destroy (b_trash_images [j]);
		}
		b_trash_images.Clear();
		player_trash [2] = 0;

		for (int k = 0; k < c_trash_images.Count; k++) {
			Destroy (c_trash_images [k]);
		}
		c_trash_images.Clear();
		player_trash [3] = 0;
	}

	private void colorImage(int trashNr)
	{
		
		if (trashNr == 1 && player_trash[1] <= assignment[assignmentNr][0] ) {
			a_trash_images[ player_trash[1] - 1 ].GetComponent<Button>().interactable = true;
		}
		if (trashNr == 2 && player_trash[2] <= assignment[assignmentNr][1] ) {
			b_trash_images[ player_trash[2] - 1 ].GetComponent<Button>().interactable = true;
		}
		if (trashNr == 3 && player_trash[3] <= assignment[assignmentNr][2] ) {
			c_trash_images[ player_trash[3] - 1 ].GetComponent<Button>().interactable = true;
		}
		if (trashNr == -1 && player_trash[1] > 0 && player_trash[1] <= assignment[assignmentNr][0]) {
			a_trash_images[ player_trash[1] -1].GetComponent<Button>().interactable = false;
		}
		if (trashNr == -2 && player_trash[2] > 0 && player_trash[2] <= assignment[assignmentNr][1] ) {
			b_trash_images[ player_trash[2] -1].GetComponent<Button>().interactable = false;
		}
		if (trashNr == -3 && player_trash[3] > 0 && player_trash[3] <= assignment[assignmentNr][2]) {
			c_trash_images[ player_trash[3] -1].GetComponent<Button>().interactable = false;
		}
	}

	public void drawHud()
	{
		player_trash [0] = player.getRecycablesCollected (0);

		if ( player_trash [1] < player.getRecycablesCollected (1)) { // if player collected a trash is smaller / same as assignment a trash , and , player collected trash a is smaller than player collected trash in player class
			player_trash [1] = player.getRecycablesCollected (1);
			colorImage (1);
		}
		if (player_trash [2] < player.getRecycablesCollected (2)) { // if player collected a trash is smaller / same as assignment a trash , and , player collected trash a is smaller than player collected trash in player class
			player_trash [2] = player.getRecycablesCollected (2);
			colorImage (2);
		}
		if ( player_trash [3] < player.getRecycablesCollected (3)) { // if player collected a trash is smaller / same as assignment a trash , and , player collected trash a is smaller than player collected trash in player class
			player_trash [3] = player.getRecycablesCollected (3);
			colorImage (3);
		}
		if ( player_trash [1] > player.getRecycablesCollected (1)) { // if player collected a trash is smaller / same as assignment a trash , and , player collected trash a is smaller than player collected trash in player class
			colorImage (-1);
			player_trash [1] = player.getRecycablesCollected (1);

		}
		if (player_trash [2] > player.getRecycablesCollected (2)) { // if player collected a trash is smaller / same as assignment a trash , and , player collected trash a is smaller than player collected trash in player class
			colorImage (-2);
			player_trash [2] = player.getRecycablesCollected (2);

		}
		if ( player_trash [3] > player.getRecycablesCollected (3)) { // if player collected a trash is smaller / same as assignment a trash , and , player collected trash a is smaller than player collected trash in player class
			colorImage (-3);
			player_trash [3] = player.getRecycablesCollected (3);

		}

	}


	public int getAssignmentNr()
	{
		return assignmentNr;
	}
}
