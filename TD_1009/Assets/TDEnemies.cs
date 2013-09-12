using UnityEngine;
using System.Collections;

public class TDBasicEnemy : TDEnemy
{

	protected override uint getStartHP()
	{
		return TDWorld.s_world.m_configuration.enemyHP;
	}
	protected override float getSpeed()
	{
		return TDWorld.s_world.m_configuration.enemySpeed;
	}
}

public class TDBossEnemy : TDEnemy
{

	protected override uint getStartHP()
	{
		return TDWorld.s_world.m_configuration.bossEnemyHP;
	}
	protected override float getSpeed()
	{
		return TDWorld.s_world.m_configuration.bossEnemySpeed;
	}
}
