using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject buildingCreationObject; 
    
    public void CreateBuilding(GameObject turretPrefab)
    {
        BuildingCreation buildingCreation = buildingCreationObject.GetComponent<BuildingCreation>();
        if (buildingCreation != null)
        {
            buildingCreation.BuildTurret(turretPrefab);
        }
    }
}