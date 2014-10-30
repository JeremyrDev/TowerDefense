using UnityEngine;
using System.Collections;

public class TowerScript : MonoBehaviour {

	public float rotateSpeed = 8f;
	public float attackRange = 6;
	public float attackDamage = 1;
	public float distanceSqr = 0;
	public float RotationSpeed;
	float shootTimer = 0;
	float lowest = 0;

	int objectNumber = 0;

	bool flash = true;

	string tempString;

	public GameObject muzzleFlash;
	public GameObject objectToLockOnto = null;

	private Quaternion _lookRotation;

	private Vector3 _direction;

	void Start () 
	{
	}
	public void Flash(bool f)
	{
		if(f)
			flash = true;
		else
			flash = false;
	}
	void getObjects(Vector3 center, float radius) {
		tempString = "";
		Collider[] hits = Physics.OverlapSphere(center, radius);
		lowest = radius;

		for(int i = 0; i < hits.Length; i++)
		{
			if(hits[i].tag == "Enemy")
			{
				distanceSqr = (transform.position - hits[i].transform.position).sqrMagnitude;

				if(distanceSqr < lowest)
				{
					lowest = distanceSqr;
					objectNumber = i;
					transform.LookAt(hits[i].gameObject.transform.position);
				}
				tempString += hits[i].tag + " : ";
			}

		}
		//Debug.Log(tempString);
		objectToLockOnto = hits[objectNumber].gameObject;
		if(objectToLockOnto.tag == "Enemy")
		{
			_lookRotation = Quaternion.LookRotation(_direction);
			transform.LookAt(objectToLockOnto.transform.position);

			shootTimer+=1*Time.deltaTime;
			if(shootTimer >=.001f)
			{
				if(flash)
				{
					if(muzzleFlash.activeSelf)
						muzzleFlash.SetActive(false);
					else
						muzzleFlash.SetActive(true);
				}
				shootTimer = 0;
				objectToLockOnto.SendMessage("ApplyDamage", attackDamage);
			}
		}
	}

	void Update () {
		try{ getObjects(transform.position, attackRange); }catch
		{ 
			muzzleFlash.SetActive(false); 
		}

		//Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
		//Debug.DrawRay(transform.position, forward, Color.green);
		//Debug.DrawRay(transform.position, objectToLockOnto.transform.position - transform.position);

	}
}
