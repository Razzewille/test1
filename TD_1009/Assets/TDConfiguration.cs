using UnityEngine;
using System.Collections;

public class TDConfiguration
{
	public TDConfiguration()
	{
		m_playerHP             = 10;
		m_towerDamage          = 3;
		m_enemyHP              = 10;
		m_killReward           = 20;
		m_enemySpeed           = 3f;
		m_rocketSpeed          = 6f;
		m_towerRestoration 	   = 1f;
		m_uberTowerRestoration = 0.1f;
		readFromResource();
	}

	void readFromResource()
	{
	}

	public uint m_playerHP;

	public uint m_towerDamage;
	public uint m_enemyHP;
	public uint m_killReward;

	public float m_enemySpeed; // units/sec
	public float m_rocketSpeed; // units/sec

	public float m_towerRestoration; // sec
	public float m_uberTowerRestoration; // sec
}
