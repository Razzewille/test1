using UnityEngine;
using System.Collections;

public class TDWorld : MonoBehaviour {

	TDWorld()
	{
		m_configuration = new TDConfiguration();
	}

	// Use this for initialization
	void Start () {
		m_startTime = -1;
		Random.seed = 1;
		m_frequency = 1;
		m_created = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_startTime != (int) ((float)(m_frequency)*Time.time))
		{
			if (Random.value < 0.05)
				addEnemy(TDEnemy.Type.eBoss, new Vector3(40, 2, (int)(60.0f*Random.value) - 50));
			else
				addEnemy(TDEnemy.Type.eBasic, new Vector3(40, 2, (int)(60.0f*Random.value) - 50));
			m_startTime = (int) ((float)(m_frequency)*Time.time);
		}
		if (Input.GetMouseButtonDown(0))
		{
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(mouseRay, out hit))
			{
				if (hit.transform.gameObject.Equals(GameObject.Find("Terrain")))
				{
					if (Random.value < 0.05)
						addTower(TDTower.Type.eUber, hit.point);
					else
						addTower(TDTower.Type.eBasic, hit.point);
					m_created++;
					m_frequency = (3*m_created)/10 + 1;
				}
			}
		}
	}
	
	public static TDWorld getWorld()
	{
		GameObject [] aWorlds = GameObject.FindGameObjectsWithTag("World");
		TDWorld world = (TDWorld) (aWorlds[0].GetComponent("TDWorld"));
		return world;
	}

	public GameObject getPlayer()
	{
		GameObject [] aPlayers = GameObject.FindGameObjectsWithTag("Player");
		return aPlayers[0];
	}

	public TDPlayer getTDPlayer()
	{
		return (TDPlayer) (getPlayer().GetComponent("TDPlayer"));
	}

	public TDEnemy getTDEnemy(GameObject obj)
	{
		return (TDEnemy) obj.GetComponent("TDEnemy");
	}

	public TDRocket getTDRocket(GameObject obj)
	{
		return (TDRocket) obj.GetComponent("TDRocket");
	}

	public GameObject [] getAllEnemies()
	{
		return GameObject.FindGameObjectsWithTag("Enemy");
	}

	public GameObject [] getAllTowers()
	{
		return GameObject.FindGameObjectsWithTag("Tower");
	}

	public GameObject [] getAllObstacles()
	{
		return GameObject.FindGameObjectsWithTag("Obstacle");
	}

	bool isPositionFree(Vector3 pos)
	{
		return false;
	}
	
	public GameObject addEnemy(TDEnemy.Type type, Vector3 pos)
	{
		GameObject enemy = null;
		switch (type)
		{
			case TDEnemy.Type.eBasic:
				enemy = (GameObject) Instantiate(m_prefabBasicEnemy, pos, Quaternion.identity);
				break;
			case TDEnemy.Type.eBoss:
				enemy = (GameObject) Instantiate(m_prefabBossEnemy, pos, Quaternion.identity);
				break;
				
		}
		return enemy;
	}

	public GameObject addTower(TDTower.Type type, Vector3 pos)
	{
		GameObject tower = null;
		switch (type)
		{
			case TDTower.Type.eBasic:
				tower = (GameObject) Instantiate(m_prefabBasicTower, pos, Quaternion.identity);
				break;
			case TDTower.Type.eUber:
				tower = (GameObject) Instantiate(m_prefabUberTower, pos, Quaternion.identity);
				break;
				
		}
		return tower;
	}
	

	public GameObject addRocket(TDTower.Type towerType, Vector3 pos)
	{
		GameObject rocket = (GameObject) Instantiate(m_prefabRocket, pos, Quaternion.identity);
		TDRocket tdRocket = getTDRocket(rocket);
		switch (towerType)
		{
			case TDTower.Type.eBasic:
				tdRocket.m_damage = m_configuration.towerDamage;
				tdRocket.m_speed = m_configuration.towerRocketSpeed;
				break;
			case TDTower.Type.eUber:
				tdRocket.m_damage = m_configuration.uberTowerDamage;
				tdRocket.m_speed = m_configuration.uberTowerRocketSpeed;
				break;
				
		}
		return rocket;
	}

	public GameObject addObstacle(Vector3 pos)
	{
		return null;
	}

	public TDConfiguration m_configuration;
	TDGrid m_grid;

	public GameObject m_prefabBasicEnemy;
	public GameObject m_prefabBossEnemy;
	public GameObject m_prefabBasicTower;
	public GameObject m_prefabUberTower;
	public GameObject m_prefabRocket;

	int m_created;
	int m_frequency;
    int m_startTime;

}
