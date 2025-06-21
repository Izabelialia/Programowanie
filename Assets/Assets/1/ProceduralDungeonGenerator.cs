using System.Collections.Generic;
using UnityEngine;

public class ProceduralDungeonGenerator : MonoBehaviour
{
    [Header("Prefaby podstawowe")]
    public GameObject floorTile;
    public GameObject wallTile;
    public GameObject corridorTile;
    public GameObject[] bridgePrefabs;

    [Header("Dekoracje")]
    public GameObject[] wallDecorations; // np. pochodnie
    public GameObject[] roomDecorations; // np. sto³y, skrzynie

    [Header("Drzwi")]
    public GameObject entranceDoorPrefab;
    public GameObject exitDoorPrefab;

    [Header("Ustawienia")]
    public int roomCount = 15;
    public Vector2Int roomSizeRange = new Vector2Int(4, 8);
    public float tileSize = 2f;

    private List<BoundsInt> roomBounds = new List<BoundsInt>();

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        Vector2Int currentPos = Vector2Int.zero;

        for (int i = 0; i < roomCount; i++)
        {
            // losowy rozmiar pokoju
            int w = Random.Range(roomSizeRange.x, roomSizeRange.y);
            int h = Random.Range(roomSizeRange.x, roomSizeRange.y);
            BoundsInt bounds = new BoundsInt(currentPos.x, currentPos.y, 0, w, h, 1);
            roomBounds.Add(bounds);

            GenerateRoom(bounds);

            if (i > 0)
            {
                Vector2Int prevCenter = GetCenter(roomBounds[i - 1]);
                Vector2Int currCenter = GetCenter(bounds);
                GenerateCorridor(prevCenter, currCenter);
            }

            currentPos += new Vector2Int(w + 6, Random.Range(-3, 3)); // odstêp + lekka losowoœæ
        }

        PlaceEntranceAndExit();
    }

    void GenerateRoom(BoundsInt bounds)
    {
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3 pos = new Vector3(x * tileSize, 0, y * tileSize);
                Instantiate(floorTile, pos, Quaternion.identity, transform);

                // œciany - na obrze¿ach pokoju
                if (x == bounds.xMin || x == bounds.xMax - 1 || y == bounds.yMin || y == bounds.yMax - 1)
                {
                    Instantiate(wallTile, pos + Vector3.up, Quaternion.identity, transform);
                    if (Random.value < 0.2f && wallDecorations.Length > 0)
                    {
                        GameObject deco = wallDecorations[Random.Range(0, wallDecorations.Length)];
                        Instantiate(deco, pos + Vector3.up * 1.5f, Quaternion.identity, transform);
                    }
                }
                else
                {
                    // dekoracja œrodka pokoju
                    if (Random.value < 0.05f && roomDecorations.Length > 0)
                    {
                        GameObject deco = roomDecorations[Random.Range(0, roomDecorations.Length)];
                        Instantiate(deco, pos + Vector3.up * 0.5f, Quaternion.Euler(0, Random.Range(0, 360), 0), transform);
                    }
                }
            }
        }
    }

    void GenerateCorridor(Vector2Int from, Vector2Int to)
    {
        Vector2Int pos = from;

        while (pos.x != to.x)
        {
            pos.x += (to.x > pos.x) ? 1 : -1;
            Vector3 worldPos = new Vector3(pos.x * tileSize, 0, pos.y * tileSize);
            Instantiate(corridorTile, worldPos, Quaternion.identity, transform);
        }
        while (pos.y != to.y)
        {
            pos.y += (to.y > pos.y) ? 1 : -1;
            Vector3 worldPos = new Vector3(pos.x * tileSize, 0, pos.y * tileSize);
            Instantiate(corridorTile, worldPos, Quaternion.identity, transform);
        }
    }

    Vector2Int GetCenter(BoundsInt bounds)
    {
        return new Vector2Int(
            bounds.xMin + bounds.size.x / 2,
            bounds.yMin + bounds.size.y / 2
        );
    }

    void PlaceEntranceAndExit()
    {
        if (entranceDoorPrefab != null && roomBounds.Count > 0)
        {
            Vector2Int entrancePos = GetCenter(roomBounds[0]);
            Vector3 entranceWorld = new Vector3(entrancePos.x * tileSize, 0, entrancePos.y * tileSize);
            Instantiate(entranceDoorPrefab, entranceWorld + Vector3.up, Quaternion.identity, transform);
        }

        if (exitDoorPrefab != null && roomBounds.Count > 1)
        {
            Vector2Int exitPos = GetCenter(roomBounds[roomBounds.Count - 1]);
            Vector3 exitWorld = new Vector3(exitPos.x * tileSize, 0, exitPos.y * tileSize);
            Instantiate(exitDoorPrefab, exitWorld + Vector3.up, Quaternion.identity, transform);
        }
    }
}
