using UnityEngine;
using System.Collections;

public class TDWorld
{
	GameObject getPlayer()
	{
		return null;
	}

	GameObject [] getAllEnemies()
	{
		return null;
	}

	GameObject [] getAllTowers()
	{
		return null;
	}

	GameObject [] getAllObstacles()
	{
		return null;
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
	
	bool addEnemy(Vector3 pos)
	{
		return false;
	}

	bool addTower(Vector3 pos)
	{
		return false;
	}

	bool addObstacle(Vector3 pos)
	{
		return false;
	}

	public static TDWorld s_world = new TDWorld();
	public TDConfiguration m_configuration;
	public TDPlayer m_player;
	TDGrid m_grid;
}
