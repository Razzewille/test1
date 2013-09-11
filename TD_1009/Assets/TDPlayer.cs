using UnityEngine;
using System.Collections;

public class TDPlayer 
{
	public TDPlayer() {m_health = 10; m_money = 0;}

	bool isAlive() {return m_health > 0;}

	void heal(uint hp) {m_health += (int)hp;}

	void damage(uint hp) {m_health -= (int)hp;}

	bool affords(uint cost) {return m_money > cost;}

	void reward(uint money) {m_money += (int)money;}

	void expense(uint price) {m_money -= (int)price;}

	int m_health;
	int m_money;
}
