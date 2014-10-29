using UnityEngine;
using System.Collections;

public class TowerScript : MonoBehaviour {
	//public GameObject obj;
	//public Transform from;
	//public Transform to;

	public float rotateSpeed = 8f;

	public float attackRange = 6;
	public float attackDamage = 2;

	//values that will be set in the Inspector
	public Transform Target;
	public float RotationSpeed;
	float shootTimer = 0;
	public GameObject p;
	SceneController sc;
	
	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;
	public string tagCheck;
	public GameObject objectToLockOnto;
	Vector3 objectPosition;
	public Material enemyTarget;
	public Material enemy;

	float lowest = 0;
	int objectNumber = 0;
	void Start () 
	{
		p = GameObject.Find("Camera");
		sc = p.GetComponent<SceneController>();
	}

	//float distanceSqr = (transform.position - tr.position).sqrMagnitude;

	void getObjects(Vector3 center, float radius) {
		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		int i = 0;
		lowest = radius;
		while (i < hitColliders.Length) {
			float distanceSqr = (transform.position - hitColliders[i].transform.position).sqrMagnitude;
			if(distanceSqr < lowest)
			{
				lowest = distanceSqr;
				objectNumber = i;
				hitColliders[i].gameObject.SendMessage("ApplyDamage", attackDamage);
				objectToLockOnto = hitColliders[i].gameObject;
				//transform.LookAt(hitColliders[i].gameObject.transform.position);
			}
			else
				objectToLockOnto = null;
			//hitColliders[i].gameObject.renderer.material = enemyTarget;
			i++;
		}
	}
	void Update () {
		shootTimer+=1*Time.deltaTime;
		if(shootTimer >=.04f)
		{
			getObjects(transform.position, attackRange);
			shootTimer = 0;
		}
		//objectToLockOnto = sc.towerLockObject;
		//if(objectToLockOnto != null)
		//shoot();

		transform.LookAt(objectToLockOnto.transform.position);
		Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;

		Debug.DrawRay(transform.position, forward, Color.green);
		Debug.DrawRay(transform.position, objectToLockOnto.transform.position - transform.position);
//		RaycastHit[] hits;
//		RaycastHit hitUse = new RaycastHit();
//		bool foundHit = false;
//		//hits = Physics.RaycastAll(transform.position, transform.forward);
//		hits = Physics.SphereCastAll(transform.position, 100.5f, transform.position);
//		//Collider[] hitColliders = Physics.OverlapSphere(transform.position, 100);
//		//int i2 = 0;
//		//while (i2 < hitColliders.Length) {
//		//hitColliders[i2].SendMessage("AddDamage");
//		//	i2++;
//		//}
//		float shortestDist;
//		shortestDist = Mathf.Infinity;
//
//		for( int i = 0; i < hits.Length; i++)
//		{
//			if(hits[i].transform.tag == tagCheck)
//				if(Vector3.Distance (transform.position, hits[i].point) < shortestDist)
//				{
//					hitUse = hits[i];
//					shortestDist = Vector3.Distance (transform.position, hits[i].point);
//					foundHit = true;
//				}
//		}
//		if(foundHit)
//		{
//			//transform.position = hitUse.point;
//			_lookRotation = Quaternion.LookRotation(objectToLockOnto);
//		}
		//_lookRotation = Quaternion.LookRotation(objectPosition);
		//find the vector pointing from our position to the target
		//_direction = (Target.position - transform.position).normalized;
		
		//create the rotation we need to be in to look at the target
		//_lookRotation = Quaternion.LookRotation(_direction);
		
		//rotate us over time according to speed until we are in the required rotation
		//transform.rotation = Quaternion.Lerp(transform.rotation, _lookRotation, Time.deltaTime * 20);
	}
	void shoot()
	{

//		shootTimer+=1*Time.deltaTime;
//		if(shootTimer >=.04f)
//		{
//			if(l.intensity == 0)
//				l.intensity = 5.2f;
//			else
//				l.intensity = 0;
//			shootTimer = 0;
//		}
	}
}
