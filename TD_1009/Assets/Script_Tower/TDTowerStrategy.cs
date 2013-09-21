using UnityEngine;
using System.Collections.Generic;

public class TDTowerStrategy
{
	public bool shouldShootAt(TDEnemy enemy, uint potentialDamage)
	{
		if (m_followedEnemies == null)
			m_followedEnemies = new Dictionary<TDEnemy, uint>();
		if (!m_followedEnemies.ContainsKey(enemy))
		{
			m_followedEnemies[enemy] = 0;
			enemy.OnEventDestroy += destroyCallback;
			return true;
		}
		uint followedDamage = m_followedEnemies[enemy];
		if (followedDamage > enemy.getStartHP())
			return false;
		return true;
	}

	public void shootingAt(TDEnemy enemy, uint potentialDamage)
	{
		m_followedEnemies[enemy] += potentialDamage;
	}

	void destroyCallback(TDEnemy enemy)
	{
		enemy.OnEventDestroy -= destroyCallback;
		if (m_followedEnemies.ContainsKey(enemy))
		{
			m_followedEnemies.Remove(enemy);
		}
	}
	
	Dictionary<TDEnemy, uint> m_followedEnemies;
}
