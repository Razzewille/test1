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
		updatePath();
		m_currentPathCell = 0;
	}

	// Update is called once per frame
	void Update () {
		TDWorld world = TDWorld.getWorld();
		GameObject player = world.getPlayer();
		
		if (null == m_path)
			return;

		if (m_currentPathCell >= m_path.Length - 1)
			return;
		
		Vector3 nextCellPos = world.from2dTo3d(world.m_grid.getCenter(m_path[m_currentPathCell + 1]));
		Vector3 dir = nextCellPos - transform.position;
		dir.y = 0;

		Bounds b = gameObject.renderer.bounds;
		Vector3 deltab = b.max - b.min;
		deltab.y = 0;
		float delta = 0.5f*deltab.magnitude;

		Bounds pb = player.renderer.bounds;
		Vector3 pdeltab = pb.max - pb.min;
		pdeltab.y = 0;
		float pdelta = 0.5f*pdeltab.magnitude;

// 		if (dir.magnitude < delta + pdelta)
// 		{
// 			TDPlayer tdPlayer = TDWorld.getWorld().getTDPlayer();
// 			tdPlayer.receiveDamage(1);
// 			Destroy(gameObject);
// 			return;
// 		}
		Vector3 otherDir = dir;
		if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
		{
			dir.z = 0;
			otherDir.x = 0;
		}
		else
		{
			dir.x = 0;
			otherDir.z = 0;
		}
		dir.Normalize();
		otherDir.Normalize();
		Vector3 nextPos = transform.position + (1.0f + getSpeed()*Time.deltaTime)*dir;
		if (TDGrid.CellState.eBusy == TDWorld.getWorld().positionState(nextPos))
		{
			Vector3 nextPos2 = transform.position + (1.0f + getSpeed()*Time.deltaTime)*otherDir;
			if (TDGrid.CellState.eBusy != TDWorld.getWorld().positionState(nextPos2))
			{
				dir = otherDir;
			}
		}
		dir *= getSpeed()*Time.deltaTime;

		transform.Translate(dir);

		if ( (world.from3dTo2d(transform.position) - world.from3dTo2d(nextCellPos)).magnitude < 0.5 )
			++m_currentPathCell;
		
		updateHealthBar();
	}

	void updateHealthBar()
	{
		if (m_healthBar == null)
			return;
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

	void updatePath()
	{
		TDWorld world = TDWorld.getWorld();
		TDGrid grid = world.m_grid;
		TDGrid.Cell startCell = grid.getCell(world.from3dTo2d(gameObject.transform.position));
		TDGrid.Cell endCell = grid.getCell(world.from3dTo2d(world.getPlayer().transform.position));
		bool pathExists = grid.buildPath(startCell, endCell, out m_path);
		if (!pathExists)
		{
			//error
		}
	}

	public abstract Type type();

	public abstract uint getStartHP();
	public abstract float getSpeed();
	public abstract Color getColor();
	
	int m_currentPathCell;
	TDGrid.Cell[] m_path;
	
	int m_maxHP;
	int m_HP;
	GameObject m_healthBar;
}
