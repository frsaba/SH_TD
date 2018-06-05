﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public Enemy target;
    public Vector2 dest;
    public int damage;

    public GameMaster gm;


	// Use this for initialization
	void Start () {
		gm = GameObject.FindObjectOfType<GameMaster>();
    }
	
	// Update is called once per frame
	void Update () {

        if(gm.gameOn == false)
        {
            return;
        }

        Vector2 pos = transform.position;

        pos = Vector2.MoveTowards(pos, dest, speed * Time.deltaTime);

        transform.position = pos;

        if (Vector2.Distance(pos,dest) < 0.01f)
        {
            Explode();
        }
	}

    void Explode()
    {
        if (target != null)
        {
            target.TakeDamage(damage);
            target.tag = "Untagged";
        }

        FindObjectOfType<Lines>().Flush();
        Destroy(gameObject);
    }
}
