using UnityEngine;
using System.Collections;

public class TDWorld : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static TDWorld getWorld()
	{
		GameObject [] aWorlds = GameObject.FindGameObjectsWithTag("World");
		return (TDWorld) (aWorlds[0].GetComponent("TDWorld"));
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

	bool start()
	{
		m_configuration = new TDConfiguration();
		m_player = new TDPlayer();
		return true;
	}

	bool isPositionFree(Vector3 pos)
	{
		return false;
	}
	
	bool addEnemy(TDEnemy.Type type, Vector3 pos)
	{
		GameObject enemy = null;
		switch (type)
		{
			case TDEnemy.Type.eBasic:
			break;
			case TDEnemy.Type.eBoss:
			break;
				
		}
		enemy.transform.position = pos;
		return false;
	}

	bool addTower(TDTower.Type type, Vector3 pos)
	{
		return false;
	}

	bool addObstacle(Vector3 pos)
	{
		return false;
	}

	public TDConfiguration m_configuration;
	public TDPlayer m_player;
	TDGrid m_grid;

	public GameObject m_prefabBasicEnemy;
	public GameObject m_prefabBossEnemy;
	public GameObject m_prefabBasicTower;
	public GameObject m_prefabUberTower;

}
