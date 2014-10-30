using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SceneController : MonoBehaviour {

	public GameObject e;
	List<GameObject> enemies;

	public GameObject t;
	List<GameObject> towers;

	public GameObject t2;
	List<GameObject> towers2;

	GameObject spawnObject;

	public GameObject pause;
	public GameObject selectedTower;
	public GameObject Towers;
	public GameObject Towers2;
	public GameObject Enemies;

	float spawnTimer = 0;
	float timerLimit = .2f;
	float fpsTimer = 0;

	public int killCounter = 0;
	public int se = 0;
	int numberOfTowers = 5;
	int numberOfTowers2 = 5;
	int numberOfEnemies = 20;
	int activeTowers = 0;
	int activeTowers2 = 0;
	int cycleCounter = 0;
	int objectNumber = 0;
	int cycleLimit = 15;
	int fpsCounter = 0;

	bool gotTower = false;
	bool spawnClick = false;

	string info;

	Vector3 spawnPosition;

	public GUIStyle infoStyle;

	public Material TowerMat;
	public Material TowerSelectedMat;
	public Material TowerMat2;
	public Material TowerSelectedMat2;

	//If fps drops below thresholds, degrade overall game quality in quality settings
	void OnGUI()
	{
		GUI.Label (new Rect (0,0,100,50), info, infoStyle);
	}

	void Start () 
	{
		enemies = new List<GameObject>();
		towers = new List<GameObject>();
		towers2 = new List<GameObject>();

		for(int i = 0; i<numberOfTowers; i++)
		{
			GameObject obj = (GameObject)Instantiate(t);
			obj.SetActive(false);
			obj.name = "Tower"+i;
			obj.transform.SetParent(Towers.transform);
			towers.Add(obj); 
		}
		for(int i = 0; i<numberOfTowers2; i++)
		{
			GameObject obj = (GameObject)Instantiate(t2);
			obj.SetActive(false);
			obj.name = "Tower"+i;
			obj.transform.SetParent(Towers2.transform);
			towers2.Add(obj); 
		}
		for(int i = 0; i<numberOfEnemies; i++)
		{
			GameObject obj = (GameObject)Instantiate(e);
			obj.SetActive(false);
			obj.name = "Enemy"+i;
			obj.transform.SetParent(Enemies.transform);
			enemies.Add(obj); 
		}
		spawnObject = t;
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			if(pause.activeSelf)
			{
				pause.SetActive(false);
				Time.timeScale = 1;
			}
			else
			{
				pause.SetActive(true);
				Time.timeScale = 0;
			}
		}

		fpsCounter++;
		fpsTimer+=1*Time.deltaTime;
		if(fpsTimer >= 1)
		{
			info = spawnTimer.ToString ("f2")+"\n"+cycleCounter + "\n"+fpsCounter + "\n" + killCounter+"\n"+se;
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
				if(hit.collider.tag == "t" || hit.collider.tag == "t2")
				{
					selectedTower = hit.collider.gameObject;
				}
				else if(hit.collider.tag == "Ground")
				{
					spawnPosition = new Vector3(ray.origin.x, 1.54f, ray.origin.z);
					if(!gotTower)
						if(spawnObject == t)
							spawnTower(0);
						else if(spawnObject == t2)
							spawnTower (1);
						else
							spawnEnemy();
					if(activeTowers > 5)
						alterFlash(false);
					else
						alterFlash(true);
				}
				else if(hit.collider.tag == "SpawnEnemy")
				{
					spawnObject = e;
				}
				else if(hit.collider.tag == "SpawnTower")
				{
					spawnObject = t;
				}
				else if(hit.collider.tag == "SpawnTower2")
				{
					spawnObject = t2;
				}
				Debug.Log(hit.collider.tag);
			}
			if(selectedTower.tag == "t")
				selectedTower.renderer.material = TowerSelectedMat;
			if(selectedTower.tag == "t2")
				selectedTower.renderer.material = TowerSelectedMat2;
			spawnPosition = new Vector3(ray.origin.x, 1.54f, ray.origin.z);
			selectedTower.transform.position = spawnPosition;
		}
		else
			spawnClick = false;
		if(Input.GetMouseButtonUp(1))
		{
			spawnObject = null;
			if(selectedTower.tag == "t")
				selectedTower.renderer.material = TowerMat;
			if(selectedTower.tag == "t2")
				selectedTower.renderer.material = TowerMat2;
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
			if(!enemies[i].activeInHierarchy)
			{
				if(spawnObject == e && spawnClick)
					enemies[i].transform.position = spawnPosition;
				else
					enemies[i].transform.position = new Vector3(-7.2f, 1.54f, -3.40f);
				enemies[i].SetActive(true);
				return;
			}
			counter++;
			if(counter == numberOfEnemies)
			{
				GameObject obj = (GameObject)Instantiate(e);
				if(spawnObject == e)
					enemies[i].transform.position = spawnPosition;
				else
					enemies[i].transform.position = new Vector3(-7.2f, 1.54f, -3.40f);
				obj.SetActive(true);
				obj.name = "Enemy"+i;
				obj.transform.SetParent(Enemies.transform);
				enemies.Add(obj);
				numberOfEnemies++;
				return;
			}
		}
	}
	public void spawnTower(int type)
	{
		spawnObject = null;
		if(type == 0)
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
		if(type == 1)
		{
			activeTowers2 = 0;
			gotTower = true;
			int counter = 0;
			for(int i = 0; i < numberOfTowers2;i++)
			{
				if(!towers2[i].activeInHierarchy)
				{
					activeTowers2++;
					//towers[i].transform.position = new Vector3(-7.2f, 1.54f, -3.40f);
					selectedTower = towers2[i];
					towers2[i].SetActive(true);
					return;
				}
				counter++;
				if(counter == numberOfTowers2)
				{
					GameObject obj = (GameObject)Instantiate(t2);
					//obj.transform.position = new Vector3(-7.2f, 1.54f, -3.40f);
					obj.SetActive(true);
					obj.name = "Tower"+numberOfTowers2;
					obj.transform.SetParent(Towers2.transform);
					towers2.Add(obj);
					selectedTower = towers2[numberOfTowers2];
					numberOfTowers2++;
					return;
				}
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
