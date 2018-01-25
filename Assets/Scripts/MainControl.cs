using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControl : MonoBehaviour
{
    public GameObject bulletPrefab;

    public GameObject player;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Fire(Vector3 pos, Vector3 dir)
    {
        GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
        bullet.GetComponent<BulletControl>().SetDirection(dir);
    }
}
