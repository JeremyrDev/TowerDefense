using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	Vector3 endPos;
	float speed = 5;
	int wayPointCounter = 0;

	public float health = 100;
	public bool target = false;
	float spawnTimer = 0;
	public float maxHealth = 100;
	public Material enemy;
	public Material enemyTarget;
	void OnEnable()
	{
		wayPointCounter = 0;
		endPos = new Vector3(2.8f, 1.54f, -3.40f);
		health = maxHealth;
	}
	void Start () 
	{
		endPos = new Vector3(2.8f, 1.54f, -3.40f);
	}
	public void ApplyDamage(float damage)
	{
		health-=damage;
	}
	void Update () 
	{
		if(health <=0)
			gameObject.SetActive(false);

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
				gameObject.SetActive(false);
			}
		}
	}
}
