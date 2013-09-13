using UnityEngine;
using System.Collections;

public class TDGrid
{
	public TDGrid()
	{
		m_ncx = m_ncy = 1;
		reallocate();
		m_length = m_width = 1f;
		m_startX = m_startY = 0;
	}

	public bool initialize(uint nbCellsX, uint nbCellsY,
		float startX, float startY, float length, float width)
	{
		m_ncx = nbCellsX;
		m_ncy = nbCellsY;
		m_startX = startX;
		m_startY = startY;
		m_length = length;
		m_width  = width;
		reallocate();
		recalcGrid();
		return true;
	}

	public uint nbCellsX
	{
		get {return m_ncx;}
		set {m_ncx = value; reallocate(); recalcGrid();}
	}
	public uint nbCellsY
	{
		get {return m_ncy;}
		set {m_ncy = value; reallocate(); recalcGrid();}
	}

	public float length
	{
		get {return m_length;}
		set {m_length = value; recalcGrid();}
	}

	public float width
	{
		get {return m_width;}
		set {m_width = value; recalcGrid();}
	}

	enum CellState {eFree = 0, eBusy};
	public struct Cell
	{
		Cell(uint i, uint j) {m_i = i; m_j = j; pos = new Vector2();}
		public Vector2 pos;
		public uint m_i, m_j;
	}

	CellState cellState(Cell cell)
	{
		return m_aCells[cell.m_i, cell.m_j];
	}

	Vector2 getCenter(Cell cell)
	{
		return new Vector2(m_gridX*((float)(cell.m_i) + 0.5f), m_gridY*((float)(cell.m_j) + 0.5f));
	}

	Cell getCell(Vector2 pos)
	{
		Cell cell = new Cell();
		pos.x += 0.5f - m_startX;
		if (pos.x < 0f)
			cell.m_i = 0;
		else			
			cell.m_i = (uint)(pos.x/m_gridX);
		pos.y += 0.5f - m_startY;
		if (pos.y < 0f)
			cell.m_j = 0;
		else			
			cell.m_j = (uint)(pos.y/m_gridX);
		return cell;
	}

	uint getCellY(float coordY)
	{
		coordY += 0.5f - m_startY;
		if (coordY < 0f)
			return 0;
		return (uint)(coordY/m_gridY);
	}
	
    // Registers one more object with grid assuming it takes one cell
	void addObject(Vector2 pos)
	{
		Cell cell = getCell(pos);
		m_aCells[cell.m_i, cell.m_j] = CellState.eBusy;
	}
	
	bool buildPath(Vector2 startPos, Vector2 endPos, out Cell[] path)
	{
		path = null;
		return false;
	}

	void reallocate()
	{
		m_aCells = new CellState[m_ncx, m_ncy];
		for (uint i=0; i<m_ncx; i++)
			for (uint j=0; j<m_ncy; j++)
				m_aCells[i, j] = CellState.eFree;
	}

	void recalcGrid()
	{
		m_gridX = m_length/(float)m_ncy;
		m_gridY = m_length/(float)m_ncy;
	}

	CellState[,] m_aCells;

	uint m_ncx;
	uint m_ncy;

	float m_length;
	float m_width;

	public float m_startX;
	public float m_startY;
	
	public float m_gridX;
	public float m_gridY;
}
