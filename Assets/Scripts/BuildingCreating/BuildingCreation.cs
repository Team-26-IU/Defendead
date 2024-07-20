using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class BuildingCreation : MonoBehaviour
{
    public GameObject upgradeMenuScan;
    public GameObject upgradeMenuSniper;
    public GameObject upgradeMenuTurret;
    public GameObject buildMenu;
    public GameObject buildPlace;
    private GameObject _towerContainer;
    private Tower currentTower;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (GameObject.Find("Towers") == null)
        {
            _towerContainer = new GameObject("Towers");
            DontDestroyOnLoad(_towerContainer);
        }
        else
        {
            _towerContainer = GameObject.Find("Towers");
        }

        buildMenu.SetActive(false);
        upgradeMenuScan.SetActive(false);
        upgradeMenuTurret.SetActive(false);
        upgradeMenuSniper.SetActive(false);

        if (gameObject.GetComponent<Camera>() != null)
        {
            DontDestroyOnLoad(gameObject);
        }
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
            
            if (upgradeMenuTurret.activeSelf)
            {
                RectTransform menuRect = upgradeMenuTurret.GetComponent<RectTransform>();
                if (!RectTransformUtility.RectangleContainsScreenPoint(menuRect, mousePosition, null))
                {
                    upgradeMenuTurret.SetActive(false);
                    return;
                }
            }
            
            if (upgradeMenuScan.activeSelf)
            {
                RectTransform menuRect = upgradeMenuScan.GetComponent<RectTransform>();
                if (!RectTransformUtility.RectangleContainsScreenPoint(menuRect, mousePosition, null))
                {
                    upgradeMenuScan.SetActive(false);
                    return;
                }
            }
            
            if (upgradeMenuSniper.activeSelf)
            {
                RectTransform menuRect = upgradeMenuSniper.GetComponent<RectTransform>();
                if (!RectTransformUtility.RectangleContainsScreenPoint(menuRect, mousePosition, null))
                {
                    upgradeMenuSniper.SetActive(false);
                    return;
                }
            }

            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);

            if (hit.collider)
            {
                if (hit.collider.CompareTag("BuildSite") && !buildMenu.activeSelf)
                {
                    buildPlace = hit.collider.gameObject;
                    buildMenu.transform.position = mousePosition + panelOffset;
                    buildMenu.SetActive(true);
                }
                else if (hit.collider.CompareTag("ScanTower") && !upgradeMenuScan.activeSelf)
                {
                    currentTower = hit.collider.GetComponent<Tower>();
                    if (currentTower)
                    {
                        upgradeMenuScan.transform.position = mousePosition + panelOffset;
                        upgradeMenuScan.SetActive(true);
                    }
                }
                else if (hit.collider.CompareTag("TurretTower") && !upgradeMenuTurret.activeSelf)
                {
                    currentTower = hit.collider.GetComponent<Tower>();
                    if (currentTower)
                    {
                        upgradeMenuTurret.transform.position = mousePosition + panelOffset;
                        upgradeMenuTurret.SetActive(true);
                    }
                }
                else if (hit.collider.CompareTag("SniperTower") && !upgradeMenuSniper.activeSelf)
                {
                    currentTower = hit.collider.GetComponent<Tower>();
                    if (currentTower)
                    {
                        upgradeMenuSniper.transform.position = mousePosition + panelOffset;
                        upgradeMenuSniper.SetActive(true);
                    }
                }
            }
        }
    }

    public void BuildTurret(GameObject turretPrefab)
    {
        Vector2 towerPosition = buildPlace.transform.position;
        Quaternion buildRotation = buildPlace.transform.rotation;
        GameObject turretInstance = Instantiate(turretPrefab, towerPosition, buildRotation);
        Tower towerScript = turretInstance.GetComponent<Tower>();
        if (CurrencyManager.instance.Coins >= towerScript.Price)
        {
            CurrencyManager.instance.SpendCoins(towerScript.Price);
            turretInstance.transform.position += (Vector3)towerScript.DiffPos;
            turretInstance.transform.SetParent(_towerContainer.transform);
            buildPlace.SetActive(false);
            buildMenu.SetActive(false);
        }
        else
        {
            Destroy(turretInstance);
        }
    }

    public void UpgradeTurret(GameObject upgrade)
    {
        if (currentTower != null && CurrencyManager.instance.Coins >= currentTower.Upgrade)
        {
            CurrencyManager.instance.SpendCoins(currentTower.Upgrade);
            Vector3 position = currentTower.transform.position;
            Vector3 diff = new Vector3(0, 0, 0);
            Destroy(currentTower.gameObject);
            GameObject upgradedTower = null;
            upgradedTower = Instantiate(upgrade, position + diff, Quaternion.identity);

            if (upgradedTower != null)
            {
                upgradedTower.transform.SetParent(_towerContainer.transform);
                upgradeMenuScan.SetActive(false);
                upgradeMenuTurret.SetActive(false);
                upgradeMenuSniper.SetActive(false);
            }
        }
    }

    public void SellTurret()
    {
        if (currentTower != null)
        {
            CurrencyManager.instance.AddCoins(currentTower.Sell); 
            Destroy(currentTower.gameObject);
            upgradeMenuScan.SetActive(false);
            upgradeMenuTurret.SetActive(false);
            upgradeMenuSniper.SetActive(false);
            buildPlace.SetActive(true); 
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_towerContainer != null)
        {
            Destroy(_towerContainer);
        }

        if (gameObject.GetComponent<Camera>() != null)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
