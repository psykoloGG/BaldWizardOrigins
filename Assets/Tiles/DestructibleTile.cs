using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DestructibleTile : Tile
{
    public int Health;

    public override void RefreshTile(Vector3Int Position, ITilemap Tilemap)
    {
        base.RefreshTile(Position, Tilemap);
    }

    public override void GetTileData(Vector3Int Position, ITilemap Tilemap, ref UnityEngine.Tilemaps.TileData TileData)
    {
        base.GetTileData(Position, Tilemap, ref TileData);
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/2D/Tiles/DestructibleTile")]
    public static void CreateTile()
    {
        string Path = EditorUtility.SaveFilePanelInProject("Save Destructible Tile", "New Destructible Tile", "asset",
            "Save Destructible Tile", "Assets");

        if (Path == "")
        {
            return;
        }

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DestructibleTile>(), Path);
    }
#endif
}