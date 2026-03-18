using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class TowerPlacer : MonoBehaviour
{
    public static TowerPlacer instance;

    public Tilemap placementMap;
    public Tilemap nonPlaceableMap;
    public GameObject ghostPrefab;

    public GameObject[] towerPrefabs; // assign all tower prefabs in order in Inspector

    private HashSet<Vector3Int> occupiedTiles = new HashSet<Vector3Int>();
    private GameObject ghostInstance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        HandlePlacementHover();
        HandlePlacementClick();
    }

    void HandlePlacementHover()
    {
        if (TowerSelectionUI.SelectedTowerPrefab == null)
        {
            if (ghostInstance != null) Destroy(ghostInstance);
            return;
        }

        if (ghostInstance == null)
            ghostInstance = Instantiate(ghostPrefab);

        ghostInstance.GetComponent<SpriteRenderer>().sprite = TowerSelectionUI.SelectedTowerPrefab.GetComponent<SpriteRenderer>().sprite;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3Int cellPos = placementMap.WorldToCell(mouseWorldPos);
        Vector3 worldCenter = placementMap.GetCellCenterWorld(cellPos);
        worldCenter.z = 0;

        ghostInstance.transform.position = worldCenter + new Vector3(0, placementMap.cellSize.y * 0.25f);

        bool valid = placementMap.HasTile(cellPos) && !occupiedTiles.Contains(cellPos);
        ghostInstance.GetComponent<GhostTower>().SetValid(valid);
    }

    void HandlePlacementClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (TowerSelectionUI.SelectedTowerPrefab == null) return;
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3Int cellPos = placementMap.WorldToCell(mouseWorldPos);

        if (!placementMap.HasTile(cellPos)) return;
        if (occupiedTiles.Contains(cellPos)) return;

        PlaceTower(TowerSelectionUI.SelectedTowerPrefab, ghostInstance.transform.position, cellPos);
        CoinManager.instance.UpdateCoins(-TowerSelectionUI.SelectedTowerPrefab.GetComponent<Tower>().towerPrice);
        TowerSelectionUI.SelectedTowerPrefab = null;
    }

    // Separated out so both clicking and loading can use it
    public void PlaceTower(GameObject prefab, Vector3 position, Vector3Int cellPos)
    {
        Instantiate(prefab, position, Quaternion.identity);
        occupiedTiles.Add(cellPos);
    }

    public List<TowerSaveData> GetTowerSaveData()
    {
        List<TowerSaveData> list = new List<TowerSaveData>();

        foreach (Tower tower in FindObjectsOfType<Tower>())
        {
            int index = GetPrefabIndex(tower.gameObject);
            if (index == -1)
            {
                Debug.LogWarning("Tower prefab not found in towerPrefabs array: " + tower.gameObject.name);
                continue;
            }

            list.Add(new TowerSaveData
            {
                x = tower.transform.position.x,
                y = tower.transform.position.y,
                z = tower.transform.position.z,
                prefabIndex = index
            });
        }

        return list;
    }

    public void RestoreTowers(List<TowerSaveData> towers)
    {
        foreach (TowerSaveData t in towers)
        {
            if (t.prefabIndex < 0 || t.prefabIndex >= towerPrefabs.Length)
            {
                Debug.LogWarning("Invalid prefab index on load: " + t.prefabIndex);
                continue;
            }

            Vector3 pos = new Vector3(t.x, t.y, t.z);
            Vector3Int cellPos = placementMap.WorldToCell(pos);
            PlaceTower(towerPrefabs[t.prefabIndex], pos, cellPos);
        }
    }

    // Matches a placed tower back to its prefab by name
    int GetPrefabIndex(GameObject towerInstance)
    {
        for (int i = 0; i < towerPrefabs.Length; i++)
        {
            if (towerInstance.name.Contains(towerPrefabs[i].name))
                return i;
        }
        return -1;
    }
}