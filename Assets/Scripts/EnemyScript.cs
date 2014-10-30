using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	Vector3 endPos;

	public float maxHealth = 100;
	public float health = 100;
	float speed = 5;
	float spawnTimer = 0;

	int wayPointCounter = 0;


	public bool target = false;

	public Material enemy;
	public Material enemyTarget;

	public GameObject c;
	public GameObject healthBar;

	SceneController sc;
	
	void OnEnable()
	{
		wayPointCounter = 0;
		endPos = new Vector3(2.8f, 1.54f, -3.40f);
		maxHealth+=10;
		health = maxHealth;
		healthBar.transform.localScale = new Vector3(.1f, .14f, .009f);
	}
	void Start () 
	{
		c = GameObject.Find("Camera");
		sc = c.GetComponent<SceneController>();

		endPos = new Vector3(2.8f, 1.54f, -3.40f);
	}
	public void ApplyDamage(float damage)
	{
		health-=damage;
	}
	void Update () 
	{
		float h = health/maxHealth;

		healthBar.transform.localScale = new Vector3(h/10, .14f, .009f);

		if(health <=0){
			gameObject.SetActive(false);
			sc.killCounter++;
		}

		transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);

		if(wayPointCounter == 0){
			if(transform.position == endPos)
			{
				wayPointCounter++;
				endPos = new Vector3(2.8f, 1.54f, 1.36f);
			}
		}
		if(wayPointCounter == 1){
			if(transform.position == endPos)
			{
				wayPointCounter++;
				endPos = new Vector3(14.45f, 1.54f, 1.36f);
			}
		}
		if(wayPointCounter == 2){
			if(transform.position == endPos)
			{
				sc.se++;
				gameObject.SetActive(false);
			}
		}
	}
}
