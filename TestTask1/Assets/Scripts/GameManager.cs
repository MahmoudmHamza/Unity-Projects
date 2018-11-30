using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;

	[SerializeField] GameObject cube;
	[SerializeField] GameObject sphere;
	[SerializeField] GameObject cone;
	//[SerializeField] GameObject spawnPoint;

	private List<GameObject> Shapes = new List<GameObject>();

	public GameObject newShape;

	private bool coneCreated = false;
	private bool sphereCreated = false;
	private bool cubeCreated = false;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		cube.SetActive(true);
		cubeCreated = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void cubeShow(){
		cube.SetActive (true);
		cone.SetActive (false);
		sphere.SetActive (false);
		cubeCreated = true;
	 	coneCreated = false;
		sphereCreated = false;
	}

	public void shpereShow(){
		cube.SetActive (false);
		cone.SetActive (false);
		sphere.SetActive (true);
		cubeCreated = false;
		coneCreated = false;
		sphereCreated = true;
	}

	public void coneShow(){
		cube.SetActive (false);
		cone.SetActive (true);
		sphere.SetActive (false);
		cubeCreated = false;
		coneCreated = true;
		sphereCreated = false;
	}

	public void colorShape(){
		if (cubeCreated) {
			float R = Random.Range (0.0f, 100.0f);
			float G = Random.Range (0.0f, 100.0f);
			float B = Random.Range (0.0f, 100.0f);

			cube.GetComponent<Renderer>().material.color = new Color (R, B, G);

		} else if (sphereCreated) {
			float R = Random.Range (0.0f, 100.0f);
			float G = Random.Range (0.0f, 100.0f);
			float B = Random.Range (0.0f, 100.0f);

			sphere.GetComponent<Renderer>().material.color = new Color (R, B, G);

		} else if (coneCreated) {
			float R = Random.Range (0.0f, 100.0f);
			float G = Random.Range (0.0f, 100.0f);
			float B = Random.Range (0.0f, 100.0f);

			cone.GetComponent<Renderer>().material.color = new Color (R, B, G);
		}
	}

	public void createShape(){
		if (cubeCreated) {
			
			newShape = Instantiate (cube) as GameObject;
			RegisterShape (newShape);
			// getting random coordinates
			float x = Random.Range (50.0f, 280.0f);
			float y = Random.Range (0.0f, 50.0f);
			float z = Random.Range (0.0f, 450.0f);
			newShape.transform.position = new Vector3 (x, y, z);

		} else if (sphereCreated) {
			
			newShape = Instantiate (sphere) as GameObject;
			RegisterShape (newShape);
			// getting random coordinates
			float x = Random.Range (50.0f, 280.0f);
			float y = Random.Range (0.0f, 50.0f);
			float z = Random.Range (0.0f, 450.0f);
			newShape.transform.position = new Vector3 (x, y, z);
			
		} else if (coneCreated) {
			
			newShape = Instantiate (cone) as GameObject;
			RegisterShape (newShape);
			// getting random coordinates
			float x = Random.Range (50.0f, 280.0f);
			float y = Random.Range (0.0f, 50.0f);
			float z = Random.Range (0.0f, 450.0f);
			newShape.transform.position = new Vector3 (x, y, z);
			
		}
	}

	public void RegisterShape(GameObject shape){
		Shapes.Add (shape);
	}

	/*public void unRegisterShape(GameObject shape){
		Shapes.Remove (shape);
		Destroy (shape.gameObject);
	}*/

	/*public void randLocation(){
		float x = Random.Range [50.0f, 280.0f];
		float y = Random.Range [0.0f, 50.0f];
		float z = Random.Range [0.0f, 450.0f];

			
	}*/

}
