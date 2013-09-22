using UnityEngine;
using System.Collections;

public class TDBasicEnemy : TDEnemy
{
	public override TDEnemy.Type type()
	{
		return TDEnemy.Type.eBasic;
	}
	public override uint getStartHP()
	{
		return TDWorld.getWorld().m_configuration.enemyHP;
	}
	public override float getStartSpeed()
	{
		return TDWorld.getWorld().m_configuration.enemySpeed;
	}
	public override Color getColor()
	{
		return new Color(0, 1, 1, 1);
	}
	protected override bool canFly()
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
}

