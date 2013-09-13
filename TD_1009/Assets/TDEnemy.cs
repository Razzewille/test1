using UnityEngine;
using System.Collections;

public abstract class TDEnemy : MonoBehaviour {

	public enum Type
	{
		eBasic = 0,
		eBoss  = 1
	};

	public delegate void EventHandler(TDEnemy enemy);
	public event EventHandler OnEventDestroy;

	// Use this for initialization
	void Start () {
		m_maxHP = m_HP = (int) getStartHP();
		gameObject.renderer.material.color = getColor();
		GameObject enemyHealthPrefab = (GameObject) Resources.Load("EnemyHealthBarPrefab");
		m_healthBar = (GameObject) Instantiate(enemyHealthPrefab, new Vector3(0.5f, 0.5f), new Quaternion());
		updateHealthBar();
	}

	// Update is called once per frame
	void Update () {
		GameObject player = TDWorld.getWorld().getPlayer();
		Vector3 dir = player.transform.position - gameObject.transform.position;
		dir.y = 0;
		TDGrid grid = TDWorld.getWorld().m_grid;
		double gridDiag = Mathf.Sqrt(grid.m_gridX*grid.m_gridX + grid.m_gridY*grid.m_gridY);
		if (dir.magnitude < 0.5*gridDiag)
		{
			TDPlayer tdPlayer = TDWorld.getWorld().getTDPlayer();
			tdPlayer.receiveDamage(1);
			Destroy(gameObject);
			return;
		}
		dir.Normalize();
		dir *= getSpeed()*Time.deltaTime;
		Vector3 nextPos = transform.position + dir;
		if (!TDWorld.getWorld().isPositionFree(nextPos))
		{
			dir.Set(0, 0, 1);
			dir *= getSpeed()*Time.deltaTime;
			nextPos = transform.position + dir;
		}
		transform.Translate(dir);
		
		updateHealthBar();
	}

	void updateHealthBar()
	{
		Vector3 txtPos = Camera.main.WorldToViewportPoint(transform.position);
		m_healthBar.transform.position = txtPos;
		float hpLeft = ((float) m_HP)/((float) m_maxHP);
		hpLeft *= 30;
		m_healthBar.guiTexture.pixelInset = new Rect(0, 0, hpLeft, 10);
	}

	void OnDestroy()
	{
		Destroy(m_healthBar);
		if (OnEventDestroy != null)
			OnEventDestroy(this);
	}

	public void receiveDamage(uint damage)
	{
		m_HP -= (int) damage;
		if (m_HP <= 0)
		{
			Destroy(gameObject);
		}
	}

	public abstract Type type();

	public abstract uint getStartHP();
	public abstract float getSpeed();
	public abstract Color getColor();
	
	TDGrid.Cell[] m_path;
	
	int m_maxHP;
	int m_HP;
	GameObject m_healthBar;
}
