using UnityEngine;
using System.Collections;

public class TDArrow : TDProjectile {

	public override float speed()
	{
		return TDWorld.getWorld().m_configuration.towerArcherProjectileSpeed;
	}
	public override void moveToTarget()
	{
		Vector3 dir = m_target.transform.position - transform.position;
		dir.Normalize();
		dir *= speed()*Time.deltaTime;
		transform.Translate(dir);
	}
	public override void onTargetReached()
	{
		if (m_target != null)
		{
			m_target.receiveDamage(m_damage);
		}
	}
}
