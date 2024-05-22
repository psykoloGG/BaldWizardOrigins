using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructibleTilemap : MonoBehaviour
{
    public Tilemap Tilemap;
    private Dictionary<Vector3Int, int> Tiles;

    private void Awake()
    {
        Tiles = new Dictionary<Vector3Int, int>();
        foreach (Vector3Int Position in Tilemap.cellBounds.allPositionsWithin)
        {
            if (!Tilemap.HasTile(Position))
            {
                continue;
            }
            
            Tiles[Position] = ((DestructibleTile)Tilemap.GetTile(Position)).Health;
        }
    }

    private void OnCollisionEnter2D(Collision2D Other)
    {
        if (!Tilemap)
        {
            return;
        }

        if (Other.gameObject.CompareTag("Bullet"))
        {
            Vector3 HitPosition = Vector3.zero;
            foreach (ContactPoint2D Hit in Other.contacts)
            {
                HitPosition.x = Hit.point.x + 0.01f * Hit.normal.x;
                HitPosition.y = Hit.point.y + 0.01f * Hit.normal.y;

                Vector3Int CellPosition = Tilemap.WorldToCell(HitPosition);

                if (!Tilemap.HasTile(CellPosition) || !Tiles.ContainsKey(CellPosition))
                {
                    return;
                }
                
                Debug.Log(Tiles[CellPosition]);
                
                if (Tiles[CellPosition] > 0)
                {
                    Tiles[CellPosition] -= Other.gameObject.GetComponent<ProjectileBehavior>().GetGun().Damage;

                    if (Tiles[CellPosition] == 0)
                    {
                        Tilemap.SetTile(CellPosition, null);
                        Tiles.Remove(CellPosition);
                    }
                }
                else
                {
                    Tilemap.SetTile(CellPosition, null);
                    Tiles.Remove(CellPosition);
                }
            }

            Destroy(Other.gameObject);
        }
    }
}