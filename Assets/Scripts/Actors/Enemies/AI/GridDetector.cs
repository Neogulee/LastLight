using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GridDetector: MonoBehaviour
{
    public List<Tilemap> tilemaps = new();

    public bool is_tile(int y, int x)
    {
        foreach (var tilemap in tilemaps)
            if (tilemap.GetTile(new Vector3Int(x, y, 0)) != null)
                return true;
        return false;
    }

    public Vector2Int get_cell(Vector2 pos)
    {
        var cell = tilemaps[0].LocalToCell(pos);
        return new Vector2Int(cell.y, cell.x);
    }
}