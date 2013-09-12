﻿using UnityEngine;
using System.Collections;

public class TDPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		m_health = (int) TDWorld.getWorld().m_configuration.playerHP;
		m_money = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isAlive() {return m_health > 0;}

	public void heal(uint hp) {m_health += (int)hp;}

	public void receiveDamage(uint hp) {m_health -= (int)hp;}

	public bool affords(uint cost) {return m_money > cost;}

	public void reward(uint money) {m_money += (int)money;}

	public void expense(uint price) {m_money -= (int)price;}

	int m_health;
	int m_money;
}
