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

    public bool is_tile(Vector2Int pos)
    {
        foreach (var tilemap in tilemaps)
            if (tilemap.GetTile(new Vector3Int(pos.x, pos.y, 0)) != null)
                return true;
        return false;
    }

    public Vector2Int get_cell(Vector2 pos)
    {
        var cell = tilemaps[0].WorldToCell(pos);
        return new Vector2Int(cell.x, cell.y);
    }
}