using UnityEngine;
using System.Collections;

public class TDBasicTower : TDTower
{
	public override TDTower.Type type()
	{
		return TDTower.Type.eBasic;
	}
	public override uint getTowerDamage()
	{
		return TDWorld.getWorld().m_configuration.towerDamage;
	}
	public override float getRocketSpeed()
	{
		return TDWorld.getWorld().m_configuration.towerRocketSpeed;
	}
	public override float getRestoration()
	{
		return TDWorld.getWorld().m_configuration.towerRestoration;
	}
	public override float getEfficientRadius()
	{
		return TDWorld.getWorld().m_configuration.towerEfficientRadius;
	}
	protected override Color getColor()
	{
		return new Color(0, 1, 0, 1);
	}
}
