﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public int money = 200;
    public int health = 3;
    public bool gameOn;

    public Text gameovertext;

    public Text moneyText;
    public Text healthText;

	// Use this for initialization
	void Start () {
        string hptext = "Élet: " + health;
        healthText.text = hptext;

        string mtext = "Pénz:" + money;
        moneyText.text = mtext;

        gameOn = true;
    }
	
	// Update is called once per frame
	void Update () {
		


	}

    public void DamagePlayer()
    {
        health--;

        string hptext = "Élet: " + health;
        healthText.text = hptext;


        if (health <= 0)
        {
            GameOver();
        }
    }

    public void SetMoney(int amt)
    {
        money = amt;

        string mtext = "Pénz:" + money;
        moneyText.text = mtext;

    }
    public void GameOver()
    {
        gameovertext.gameObject.SetActive(true);
        gameOn = false;
    }
}
