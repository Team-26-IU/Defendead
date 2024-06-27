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
                Vector2 panelOffset = new Vector2(150f, -50f);
                
                Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
        Vector2 diffPosition = new Vector2(0f, 100f);
        Vector2 towerPosition = buildPlace.transform.position;
        Quaternion buildRotation = buildPlace.transform.rotation;
        GameObject turretInstance = Instantiate(turretPrefab, towerPosition + diffPosition, buildRotation);
        turretInstance.transform.SetParent(_towerContainer.transform);
        buildPlace.SetActive(false);
        buildMenu.SetActive(false);
    }

    public void CancelBuild()
    {
        buildMenu.SetActive(false);
    }
}