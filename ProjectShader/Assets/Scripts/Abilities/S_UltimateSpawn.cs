using UnityEngine;
using NaughtyAttributes;

public class S_UltimateSpawn : MonoBehaviour
{
    [BoxGroup("Ultimate Part1 Settings")]
    [SerializeField] private GameObject _ultimatePrefab;
    [BoxGroup("Ultimate Part1 Settings")]
    [SerializeField] private Transform _ultimateSpawnPoint;
    [BoxGroup("Ultimate Part2 Settings")]
    [SerializeField] private GameObject _ultimatePrefab2;
    [BoxGroup("Ultimate Part2 Settings")]
    [SerializeField] private Transform _ultimateSpawnPoint2;
    [BoxGroup("Ultimate Part3 Settings")]
    [SerializeField] private GameObject _ultimatePrefab3;
    [BoxGroup("Ultimate Part3 Settings")]
    [SerializeField] private GameObject _ultimateHitboxPrefab;
    [BoxGroup("Ultimate Part3 Settings")]
    [SerializeField] private Transform _ultimateSpawnPoint3;
    [BoxGroup("Ultimate Part3 Settings")]
    [SerializeField] private Transform _ultimateSpawnPoint4;
    [BoxGroup("Ultimate Part3 Settings")]
    [SerializeField] private Transform _ultimateSpawnPoint5;
    [BoxGroup("Ultimate Part3 Settings")]
    [SerializeField] private Transform _ultimateSpawnPoint6;

    private GameObject _currentUltimatePrefab;
    private Material _currentUltimateMaterial;
    private float _alpha = 1f;
    private float _fadeSpeed = 1f;

    public void SpawnUltimate1()
    {
        if (_ultimatePrefab != null && _ultimateSpawnPoint != null)
        {
            _alpha = 1f;
            _currentUltimatePrefab = Instantiate(_ultimatePrefab, _ultimateSpawnPoint.position, _ultimateSpawnPoint.rotation);
            _currentUltimateMaterial = _currentUltimatePrefab.GetComponentInChildren<Renderer>().material;
        }
        else
        {
            Debug.LogWarning("Ultimate prefab or spawn point is not assigned.");
        }
    }

    public void SpawnUltimate2()
    {
        if (_ultimatePrefab2 != null && _ultimateSpawnPoint2 != null)
        {
            Destroy(_currentUltimatePrefab);
            _alpha = 1f;
            _fadeSpeed = 0.8f;
            _currentUltimatePrefab = Instantiate(_ultimatePrefab2, _ultimateSpawnPoint2.position, _ultimateSpawnPoint2.rotation);
            _currentUltimateMaterial = _currentUltimatePrefab.GetComponentInChildren<Renderer>().material;
        }
        else
        {
            Debug.LogWarning("Ultimate prefab or spawn point is not assigned.");
        }
    }

    public void SpawnUltimate3()
    {
        if (_ultimatePrefab3 != null && _ultimateSpawnPoint3 != null)
        {
            Instantiate(_ultimateHitboxPrefab, _ultimateSpawnPoint3.position, Quaternion.identity);
            Instantiate(_ultimatePrefab3, _ultimateSpawnPoint3.position, _ultimateSpawnPoint3.rotation);
            Instantiate(_ultimatePrefab3, _ultimateSpawnPoint4.position, _ultimateSpawnPoint4.rotation);
            Instantiate(_ultimatePrefab3, _ultimateSpawnPoint5.position, _ultimateSpawnPoint5.rotation);
            Instantiate(_ultimatePrefab3, _ultimateSpawnPoint6.position, _ultimateSpawnPoint6.rotation); 
        }
        else
        {
            Debug.LogWarning("Ultimate prefab or spawn point is not assigned.");
        }
    }

    void Update()
    {
        if (_currentUltimateMaterial != null)
        {
            _alpha -= _fadeSpeed * Time.deltaTime;
            if (_alpha <= 0)
            {
                _alpha = 0;
                Destroy(_currentUltimatePrefab);
            }
            _currentUltimateMaterial.SetFloat("_Alpha", _alpha);
        }
    }
}
