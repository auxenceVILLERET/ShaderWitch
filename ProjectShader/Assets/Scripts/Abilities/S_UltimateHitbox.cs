using UnityEngine;

public class S_UltimateHitbox : MonoBehaviour
{
    [SerializeField] private SphereCollider _sphere;
    [SerializeField] private float _padding = 0.5f;
    //[SerializeField] private Transform _visualSphere;
    [SerializeField] private float maxRadius = 8f;
    [SerializeField] private float growSpeed = 10f;
    [SerializeField] private float _lifeTime = 15f;

    private void Awake()
    {
        _sphere.radius = 0.2f;
        Destroy(gameObject, _lifeTime);
    }
    private void Update()
    {
        _sphere.radius = Mathf.Min(_sphere.radius + growSpeed * Time.deltaTime, maxRadius);

        //_visualSphere.localScale = Vector3.one * (_sphere.radius * 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        S_MushroomGlow glow = other.GetComponentInParent<S_MushroomGlow>();

        if (glow != null)
            glow.EnablePermanentGlow();
    }
}