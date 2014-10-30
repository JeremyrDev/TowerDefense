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

	GameObject spawnObject;

	Vector3 spawnPosition;
	
	//public GameObject tower;
	public GameObject target;
	public GameObject selectedTower;
	public GameObject Towers;
	public GameObject Enemies;

	float spawnTimer = 0;
	float timerLimit = .2f;
	float fpsTimer = 0;

	public int killCounter = 0;
	int numberOfTowers = 5;
	int numberOfEnemies = 20;
	int activeTowers = 0;
	int cycleCounter = 0;
	int objectNumber = 0;
	int cycleLimit = 15;
	int fpsCounter = 0;

	bool gotTower = false;
	bool spawnClick = false;

	string info;

	public GUIStyle infoStyle;

	public Material TowerMat;
	public Material TowerSelectedMat;

	//If fps drops below thresholds, degrade overall game quality in quality settings
	void OnGUI()
	{
		GUI.Label (new Rect (0,0,100,50), info, infoStyle);
	}

	void Start () 
	{
		objects = new List<GameObject>();
		towers = new List<GameObject>();

		for(int i = 0; i<numberOfTowers; i++)
		{
			GameObject obj = (GameObject)Instantiate(t);
			obj.SetActive(false);
			obj.name = "Tower"+i;
			obj.transform.SetParent(Towers.transform);
			towers.Add(obj); 
		}
		for(int i = 0; i<numberOfEnemies; i++)
		{
			GameObject obj = (GameObject)Instantiate(o);
			obj.SetActive(false);
			obj.name = "Enemy"+i;
			obj.transform.SetParent(Enemies.transform);
			objects.Add(obj); 
		}
		spawnObject = t;
	}
	public void killed()
	{
		killCounter++;
	}
	void Update () 
	{
		fpsCounter++;
		fpsTimer+=1*Time.deltaTime;
		if(fpsTimer >= 1)
		{
			info = spawnTimer.ToString ("f2")+"\n"+cycleCounter + "\n"+fpsCounter + "\n" + killCounter;
			fpsCounter = 0;
			fpsTimer = 0;
		}

		if (Input.GetMouseButton(0)) {
			spawnClick = true;
			RaycastHit hit;
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);

			if (Physics.Raycast ( ray.origin, ray.direction, out hit, 1000f )) 
			{
				if(hit.collider.tag == "Tower")
				{
					selectedTower = hit.collider.gameObject;
				}
				else if(hit.collider.tag == "Ground")
				{
					spawnPosition = new Vector3(ray.origin.x, 1.54f, ray.origin.z);
					if(!gotTower)
						if(spawnObject == t)
							spawnTower();
						else
							spawnEnemy();
					if(activeTowers > 5)
						alterFlash(false);
					else
						alterFlash(true);
				}
				else if(hit.collider.tag == "SpawnEnemy")
				{
					spawnObject = o;
				}
				else if(hit.collider.tag == "SpawnTower")
				{
					spawnObject = t;
				}
				Debug.Log(hit.collider.tag);
			}
			selectedTower.renderer.material = TowerSelectedMat;
			spawnPosition = new Vector3(ray.origin.x, 1.54f, ray.origin.z);
			selectedTower.transform.position = spawnPosition;
		}
		else
			spawnClick = false;
		if(Input.GetMouseButtonUp(1))
		{
			selectedTower.renderer.material = TowerMat;
			selectedTower = null;
			gotTower = false;
		}
		if(Input.GetMouseButtonUp(2))
		{
			selectedTower.SetActive(false);
			selectedTower = null;
			if(activeTowers > 5)
				alterFlash(false);
			else
				alterFlash(true);
		}

		spawnTimer+=1*Time.deltaTime;
		if(spawnTimer > timerLimit)
		{
			spawnTimer = 0;
			cycleCounter++;
			if(cycleCounter < cycleLimit)
			{
				spawnEnemy();
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
					cycleLimit = 35;
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

	public void spawnEnemy()
	{
		int counter = 0;
		for(int i = 0; i < numberOfEnemies;i++)
		{
			if(!objects[i].activeInHierarchy)
			{
				if(spawnObject == o && spawnClick)
					objects[i].transform.position = spawnPosition;
				else
					objects[i].transform.position = new Vector3(-7.2f, 1.54f, -3.40f);
				objects[i].SetActive(true);
				return;
			}
			counter++;
			if(counter == numberOfEnemies)
			{
				GameObject obj = (GameObject)Instantiate(o);
				if(spawnObject == o)
					objects[i].transform.position = spawnPosition;
				else
					objects[i].transform.position = new Vector3(-7.2f, 1.54f, -3.40f);
				obj.SetActive(true);
				obj.name = "Enemy"+i;
				obj.transform.SetParent(Enemies.transform);
				objects.Add(obj);
				numberOfEnemies++;
				return;
			}
		}
	}
	public void spawnTower()
	{
		activeTowers = 0;
		gotTower = true;
		int counter = 0;
		for(int i = 0; i < numberOfTowers;i++)
		{
			if(!towers[i].activeInHierarchy)
			{
				activeTowers++;
				//towers[i].transform.position = new Vector3(-7.2f, 1.54f, -3.40f);
				selectedTower = towers[i];
				towers[i].SetActive(true);
				return;
			}
			counter++;
			if(counter == numberOfTowers)
			{
				GameObject obj = (GameObject)Instantiate(t);
				//obj.transform.position = new Vector3(-7.2f, 1.54f, -3.40f);
				obj.SetActive(true);
				obj.name = "Tower"+numberOfTowers;
				obj.transform.SetParent(Towers.transform);
				towers.Add(obj);
				selectedTower = towers[numberOfTowers];
				numberOfTowers++;
				return;
			}
		}
	}
	void alterFlash(bool f)
	{
		for(int i = 0; i < numberOfTowers;i++)
		{
			towers[i].SendMessage("Flash", f);
		}
	}
}
