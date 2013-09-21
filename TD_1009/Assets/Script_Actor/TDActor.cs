using UnityEngine;
using System.Collections.Generic;

public abstract class TDActor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (TDModifier m in m_aModifier)
		{
			if (m != null)
			{
				m.apply();
				if (m_HP <= 0)
				{
					die();
					break;
				}
				if (m.finished())
					m_aModifier.Remove(m);
			}
			else
				m_aModifier.Remove(m);
		}
	}

	protected abstract bool canFly();
	protected abstract float flyHeight();
	
	// Caches the path in case of success
	public bool hasPathTo(GameObject target)
	{
		return buildPath(target);
	}
	
    // Returns false if target is not reachable
	public bool walkByPath()
	{
		return false;
	}

	protected abstract void onTargetReached(GameObject obj);

	void die()
	{
		Destroy(gameObject);
	}

	public void receiveDamage(TDDamage damage)
	{
		m_aModifier.Add(damage);
	}

	public abstract uint getStartHP();
	public abstract float getSpeed();

	public void receiveDamage(TDDamage.Type type, float val)
	{
		m_HP -= (1.0f - getResistance(type))*val;
	}
	public abstract float getResistance(TDDamage.Type type);

	bool buildPath(GameObject target)
	{
		TDWorld world = TDWorld.getWorld();
		TDGrid grid = world.m_grid;
		TDGrid.Cell startCell = grid.getCell(world.from3dTo2d(gameObject.transform.position));
		TDGrid.Cell endCell = grid.getCell(world.from3dTo2d(target.transform.position));
		bool pathExists = false;
		if (canFly())
			pathExists = grid.buildAirPath(startCell, endCell, out m_path);
		else
			pathExists = grid.buildPath(startCell, endCell, out m_path);
		return pathExists;
	}

	protected List<TDModifier> m_aModifier;
	protected float m_HP;

	int m_currentPathCell;
	TDGrid.Cell[] m_path;
}
