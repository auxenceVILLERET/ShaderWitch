using UnityEngine;
using NaughtyAttributes;
public class FootprintEventRelay : MonoBehaviour
{
    [BoxGroup("Footprint Event Relay")]
    [SerializeField] private S_Move moveScript;

    public void SpawnFootprint()
    {
        if (moveScript == null)
        {
            Debug.LogWarning("FootprintEventRelay : référence à S_Move manquante.", this);
            return;
        }

        moveScript.SpawnFootprint();
    }
}