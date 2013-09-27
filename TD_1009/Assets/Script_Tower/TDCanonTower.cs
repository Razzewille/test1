using UnityEngine;
using System.Collections;

public class TDCanonTower : TDTower
{
	public override float getRestoration()
	{
		return TDWorld.getWorld().m_configuration.towerCanonRestoration;
	}
	public override float getEfficientRadius()
	{
		return TDWorld.getWorld().m_configuration.towerCanonRadius;
	}
	public override TDProjectile createProjectile()
	{
		GameObject projectile = (GameObject) Instantiate(m_prefabArrow, gameObject.transform.position, Quaternion.identity);
		return (TDProjectile) projectile.GetComponent<TDProjectile>();
	}
	public override TDDamage getTowerDamage()
	{
		return new TDDamage(TDDamage.Type.ePhysical, TDWorld.getWorld().m_configuration.towerCanonPhysicalDamage, 0);
	}
	public GameObject m_prefabArrow;
}
