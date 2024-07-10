using Unity.VisualScripting;
using UnityEngine;

public class BuildingCreation : MonoBehaviour
{
    public GameObject buildMenu;
    public GameObject buildPlace;
    private GameObject _towerContainer;

    void Start()
    {
        _towerContainer = new GameObject("Towers");
        buildMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 panelOffset = new Vector2(0f, -50f);

            if (buildMenu.activeSelf)
            {
                RectTransform menuRect = buildMenu.GetComponent<RectTransform>();
                if (!RectTransformUtility.RectangleContainsScreenPoint(menuRect, mousePosition, null))
                {
                    buildMenu.SetActive(false);
                    return; 
                }
            }
            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
            if (hit.collider && hit.collider.CompareTag("BuildSite") && !buildMenu.activeSelf)
            {
                buildPlace = hit.collider.gameObject;
                buildMenu.transform.position = mousePosition + panelOffset;
                buildMenu.SetActive(true);
            }
        }
    }


    public void BuildTurret(GameObject turretPrefab)
    {
        Vector2 towerPosition = buildPlace.transform.position;
        if (turretPrefab.name == "Turret")
        {
            towerPosition += new Vector2(0, -20 );
        }
        Quaternion buildRotation = buildPlace.transform.rotation;
        GameObject turretInstance = Instantiate(turretPrefab, towerPosition, buildRotation);
        Tower towerScript = turretInstance.GetComponent<Tower>();
        turretInstance.transform.position += (Vector3)towerScript.DiffPos;
        turretInstance.transform.SetParent(_towerContainer.transform);
        buildPlace.SetActive(false);
        buildMenu.SetActive(false);
    }
}