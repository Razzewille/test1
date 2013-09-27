using UnityEngine;
using System.Collections;

public class TDGargoyle : TDEnemy {

	public override uint getStartHP()
	{
		return TDWorld.getWorld().m_configuration.enemyGargoyleHP;
	}
	public override float getStartSpeed()
	{
		return TDWorld.getWorld().m_configuration.enemyGargoyleSpeed;
	}
	protected override bool canFly()
	{
		return true;
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
