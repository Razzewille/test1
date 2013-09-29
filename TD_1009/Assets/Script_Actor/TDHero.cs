using UnityEngine;
using System.Collections;

public class TDHero : TDActor {

	// Use this for initialization
	protected override void Start () {
		base.Start();
		m_state = State.ePatrol;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		switch (m_state)
		{
			case State.eWalk:
				walk();
				break;
			case State.eFight:
				fight();
				break;
			case State.eReturnToBase:
				returnToBase();
				break;
			case State.ePatrol:
				patrol();
				break;
		}
	}

	private void returnToBase()
	{
		
	}

	private void fight()
	{
		TDWorld world = TDWorld.getWorld();
		TDEnemy tdEnemy = world.getTDEnemy(m_target);
		if (null == tdEnemy)
		{
			m_state = State.ePatrol;
			return;
		}
		if ((m_target.transform.position - transform.position).magnitude > world.m_configuration.heroFightRadius)
		{
			if (hasPathTo(m_target))
			{
				m_state = State.eWalk;
			}
			else
			{
				m_state = State.ePatrol;
			}
			return;
		}
		TDDamage damage = new TDDamage(TDDamage.Type.ePhysical, world.m_configuration.heroPhysicalDamagePerSec*Time.deltaTime, 0f);
		damage.setTarget(tdEnemy);
		tdEnemy.receiveDamage(damage);
	}

	private void walk()
	{
		if (null == m_target)
			m_path = null;
		if (null == m_path)
		{
			m_state = State.ePatrol;
			return;
		}
		if (hasPathTo(m_target))
		{
			if (1 == m_path.Length)
			{
				m_path = null;
				m_state = State.eFight;
			}
			walkByPath();
			return;
		}
		m_state = State.ePatrol;
	}

	private void patrol()
	{
		m_path = null;
		TDWorld world = TDWorld.getWorld();
		GameObject [] aEnemies = world.getAllEnemiesUnsafe();
		foreach (GameObject enemy in aEnemies)
		{
			if ((enemy.transform.position - transform.position).magnitude < world.m_configuration.heroPatrolRadius)
			{
				if (hasPathTo(enemy))
				{
					m_state = State.eWalk;
					break;
				}
			}
		}
		
	}

	protected override void onTargetReached(GameObject obj)
	{
		TDWorld world = TDWorld.getWorld();
		GameObject player = world.getPlayer();
		if (obj == player)
		{
			return;
		}
		
		TDEnemy tdEnemy = world.getTDEnemy(obj);
		if (null != tdEnemy)
		{
			m_state = State.eFight;
			return;
		}

		if (world.isFakeTarget(obj))
		{
			DestroyObject(obj);
		}

		m_state = State.ePatrol;
	}

	protected override void onTargetDestroyed()
	{
		m_state = State.ePatrol;
	}

	public override uint getStartHP()
	{
		return TDWorld.getWorld().m_configuration.heroHP;
	}
	public override float getStartSpeed()
	{
		return TDWorld.getWorld().m_configuration.heroSpeed;
	}
	public override bool canFly()
	{
		return false;
	}
	protected override float flyHeight()
	{
		return 5.0f;
	}
	public override float getResistance(TDDamage.Type type)
	{
		return 0f;
	}

	enum State
	{
		ePatrol       = 0,
		eWalk         = 1,
		eFight        = 2,
		eReturnToBase = 3
	}
	State m_state;
	public GameObject m_prefabFakeTarget;
}
