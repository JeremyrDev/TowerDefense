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
	
	public GameObject tower;
	
	float spawnTimer = 0;
	float timerLimit = .2f;

	int numberOfEnemies = 20;
	int cycleCounter = 0;
	int objectNumber = 0;
	int cycleLimit = 15;

	string info;

	public GUIStyle infoStyle;

	public Material enemy;
	public Material enemyTarget;

	void OnGUI()
	{
		GUI.Label (new Rect (0,0,100,50), info, infoStyle);
	}

	void Start () 
	{
		objects = new List<GameObject>();

		for(int i = 0; i<numberOfEnemies; i++)
		{
			GameObject obj = (GameObject)Instantiate(o);
			obj.SetActive(false);
			obj.name = "Enemy"+i;
			objects.Add(obj); 
		}
	}

	void Update () 
	{
		info = spawnTimer.ToString ("f2")+"\n"+cycleCounter;

		spawnTimer+=1*Time.deltaTime;
		if(spawnTimer > timerLimit)
		{
			spawnTimer = 0;
			cycleCounter++;
			if(cycleCounter < cycleLimit)
			{
				Spawn();
			}
			else
			{
				if(timerLimit == 2.5f)
				{
					timerLimit = 1f;
					cycleCounter = 0;
					cycleLimit = 15;
				}
				else if(timerLimit == 1)
				{
					timerLimit = .01f;
					cycleCounter = 0;
					cycleLimit = 25;
				}
				else
				{
					timerLimit = 2.5f;
					cycleCounter = 0;
					cycleLimit = 5;
				}
			}
		}
	}

	public void Spawn()
	{
		int counter = 0;
		for(int i = 0; i < numberOfEnemies;i++)
		{
			if(!objects[i].activeInHierarchy)
			{
				objects[i].transform.position = new Vector3(-7.2f, 1.54f, -3.40f);
				objects[i].SetActive(true);
				return;
			}
			counter++;
			if(counter == numberOfEnemies)
			{
				GameObject obj = (GameObject)Instantiate(o);
				obj.transform.position = new Vector3(-7.2f, 1.54f, -3.40f);
				obj.SetActive(true);
				obj.name = "Enemy"+i;
				objects.Add(obj);
				numberOfEnemies++;
				return;
			}
		}
	}
}
