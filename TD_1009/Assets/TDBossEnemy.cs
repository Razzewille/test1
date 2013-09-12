using UnityEngine;
using System.Collections;

public class TDBossEnemy : TDEnemy
{

	protected override uint getStartHP()
	{
		return TDWorld.getWorld().m_configuration.bossEnemyHP;
	}
	protected override float getSpeed()
	{
		return TDWorld.getWorld().m_configuration.bossEnemySpeed;
	}
	protected override Color getColor()
	{
		return new Color(1, 0, 1, 1);
	}
}
