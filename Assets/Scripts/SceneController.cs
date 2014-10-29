using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SceneController : MonoBehaviour {

//	public GameObject b;
//	List<GameObject> bullets;

	public GameObject o;
	List<GameObject> objects;

	public GameObject t;
	List<GameObject> towers;

	public GameObject towerLockObject;
	public GameObject tower;

	float radius = 1000;
	float spawnTimer = 0;
	float timerLimit = 1;
	float distance = 0;
	float shootTimer = 0;

	int cycleCounter = 0;
	int objectNumber = 0;
	int maxHealth = 100;
	int[] objectHealth;

	string info;

	public GUIStyle infoStyle;

	public Material enemy;
	public Material enemyTarget;

	void OnGUI()
	{
		GUI.Label (new Rect (0,0,100,50), info, infoStyle);
		//GUI.Label(new Rect(0, 0, Screen.width,0), blockCounterTotal.ToString(), scoreStyle);
		//GUI.Label(new Rect(0, 0, Screen.width, 0),milisecondString, timeStyle);
	}

	void Start () {
		objectHealth = new int[20];
		objects = new List<GameObject>();

		for(int i = 0; i<20; i++)
		{
			GameObject obj = (GameObject)Instantiate(o);
			obj.SetActive(false);
			objects.Add(obj);
			objectHealth[i] = 100; 
		}
		towerLockObject = null;
	}
	
	// Update is called once per frame
	void Update () {
		info = spawnTimer.ToString ();
		spawnTimer+=1*Time.deltaTime;
		if(spawnTimer > timerLimit)
		{
			spawnTimer = 0;
			cycleCounter++;
			if(cycleCounter < 8)
			{
				Spawn();
			}
			else
			{
				towerLockObject = null;
				if(timerLimit == 3)
				{
					timerLimit = 1;
					cycleCounter = 0;
				}
				else
					timerLimit = 3;
			}
		}
		//Debug.Log (Vector3.Distance (objects[0].transform.position, tower.transform.position));
		radius = 50;
		for(int i = 0; i < 20;i++)
		{
			if(objects[i].activeInHierarchy)
			{
				objects[i].renderer.material = enemy;
				float temp = Vector3.Distance (objects[i].transform.position, tower.transform.position);
				//Debug.Log(temp.ToString());
				if(temp < radius)
				{
					radius = temp;
					objectNumber = i;
					distance = temp;
					//Debug.Log("LOWEST: " + temp.ToString() + "OBJECT: "+objectNumber);
				}
			}
		}
		if(distance <6)
		{
			towerLockObject = objects[objectNumber];
			//objects[objectNumber].renderer.material = enemyTarget;
			//Debug.Log (objectHealth[objectNumber].ToString());
			shootTimer+=1*Time.deltaTime;
			if(shootTimer > .05f){
				objectHealth[objectNumber] -= 1;
				shootTimer = 0;
			}
			if(objectHealth[objectNumber] <= 0)
				objects[objectNumber].SetActive(false);
		}
		else
		{
			towerLockObject = null;
		}
	}
	public void Spawn()
	{
		for(int i = 0; i < 20;i++)
		{
			if(!objects[i].activeInHierarchy)
			{
				objects[i].transform.position = new Vector3(-7.2f, 1.77f, -3.20f);
				objectHealth[i] = 100;
				objects[i].SetActive(true);
				return;
			}
		}
	}
}
