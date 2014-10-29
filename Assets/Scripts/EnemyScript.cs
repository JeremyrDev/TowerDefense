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
		endPos = new Vector3(3.6f, 1.77f, -3.20f);
		health = maxHealth;
	}
	void Start () 
	{
		endPos = new Vector3(3.6f, 1.77f, -3.20f);
	}
	public void targeted(bool t)
	{
		target = t;
	}
	public void ApplyDamage(float damage)
	{
		Debug.Log("DAMAGE WORKED");
		health-=damage;
	}
	void Update () 
	{
		if(target)
			renderer.material = enemyTarget;
		else
			renderer.material = enemy;
		if(health <=0)
			gameObject.SetActive(false);
//		var fwd = transform.TransformDirection (Vector3.forward);
//		RaycastHit hit;
//		if (Physics.Raycast (transform.position, fwd, 10)) {
//			print (hit.point.ToString());
//
//		}
//		Debug.DrawRay(transform.position, fwd, Color.green);

		transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
		if(wayPointCounter == 0){
			if(transform.position == endPos)
			{
				wayPointCounter++;
				endPos = new Vector3(3.6f, 1.77f, 1.20f);
			}
		}
		if(wayPointCounter == 1){
			if(transform.position == endPos)
			{
				wayPointCounter++;
				endPos = new Vector3(14.5f, 1.77f, 1.20f);
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
