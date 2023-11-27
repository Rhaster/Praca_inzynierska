using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class Konverter : TileBase
{
    public Sprite sprite;

    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = sprite;
        tileData.colliderType = Tile.ColliderType.Grid; // Ustawienie typu kolizji, mo�esz dostosowa� w zale�no�ci od potrzeb
    }
}
