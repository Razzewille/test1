using UnityEngine;
using System.Collections;

public abstract class TDTower : MonoBehaviour {

	public enum Type
	{
		eBasic = 0,
		eUber  = 1
	}

	// Use this for initialization
	void Start () {
		m_restTime = Time.time;
		gameObject.renderer.material.color = getColor();
	}
	
	// Update is called once per frame
	void Update () {
		float currentTime = Time.time;
		float respawn = getRestoration();
		if (currentTime < (m_restTime + respawn))
			return;
		GameObject [] aAllEnemies = TDWorld.getWorld().getAllEnemies();
		double recDist = -1;
		GameObject recObject = null;
		foreach(GameObject thisObject in aAllEnemies)
		{
			float dist = (transform.position - thisObject.transform.position).magnitude;
			float efficientRadius = getEfficientRadius();
		   	if (dist < efficientRadius)
			{
				TDEnemy enemy = TDWorld.getWorld().getTDEnemy(thisObject);
				if (!TDWorld.getWorld().m_strategy.shouldShootAt(enemy, getTowerDamage()))
					continue;
				if ((recDist < 0) || (recDist > dist))
				{
					recDist = dist;
					recObject = thisObject;
				}
			}
		}
		if (recObject == null)
			return;
		TDEnemy recEnemy = TDWorld.getWorld().getTDEnemy(recObject);
		TDWorld.getWorld().m_strategy.shootingAt(recEnemy, getTowerDamage());
		GameObject rocket = TDWorld.getWorld().addRocket(type(), transform.position);
		TDRocket rocketScript = (TDRocket) rocket.GetComponent("TDRocket");
		rocketScript.m_target = recObject;
		m_restTime = Time.time;
	}

	public abstract Type type();

	public abstract uint getTowerDamage();
	public abstract float getRocketSpeed();
	public abstract float getRestoration();
	public abstract float getEfficientRadius();
	protected abstract Color getColor();
	float m_restTime;
}
