using UnityEngine;

public class TowerSelectionUI : MonoBehaviour
{
    public static GameObject SelectedTowerPrefab;

    public void selectTower(GameObject towerPrefab)
    {
        if ( towerPrefab == SelectedTowerPrefab )
        {
            SelectedTowerPrefab = null;
            return;
        }
        
        if ( towerPrefab.GetComponent<Tower>().towerPrice <= CoinManager.instance.coins )
        {
            SelectedTowerPrefab = towerPrefab;
        }
    }
}
