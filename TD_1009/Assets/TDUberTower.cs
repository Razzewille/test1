using UnityEngine;
using System.Collections;

public class TDUberTower : TDTower
{
	public override TDTower.Type type()
	{
		return TDTower.Type.eUber;
	}
	public override uint getTowerDamage()
	{
		return TDWorld.getWorld().m_configuration.uberTowerDamage;
	}
	public override float getRocketSpeed()
	{
		return TDWorld.getWorld().m_configuration.uberTowerRocketSpeed;
	}
	public override float getRestoration()
	{
		return TDWorld.getWorld().m_configuration.uberTowerRestoration;
	}
	public override float getEfficientRadius()
	{
		return TDWorld.getWorld().m_configuration.uberTowerEfficientRadius;
	}
	protected override Color getColor()
	{
		return new Color(1, 0.7f, 0, 1);
	}
}
