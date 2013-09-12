using UnityEngine;
using System.Collections;

public abstract class TDTower : MonoBehaviour {

	public enum Type
	{
		eBasic = 0,
		eUber  = 1
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public abstract uint getTowerDamage();
	public abstract float getRocketSpeed();
	public abstract float getRestoration();
}
