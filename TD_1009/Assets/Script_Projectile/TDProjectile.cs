using UnityEngine;
using System.Collections;

public abstract class TDProjectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (m_target == null)
		{
			Destroy (gameObject);
			return;
		}
		moveToTarget();
		Vector3 dir = m_target.transform.position - transform.position;
		if (dir.magnitude < TDWorld.getWorld().m_configuration.hitDistance)
		{
			onTargetReached();
			Destroy(gameObject);
		}
	}

	public virtual void setTarget(TDActor target)
	{
		m_target = target;
	}

	public abstract float speed();
	public abstract void moveToTarget();
	public abstract void onTargetReached();
	
	public TDDamage m_damage;
	protected TDActor m_target;
}
