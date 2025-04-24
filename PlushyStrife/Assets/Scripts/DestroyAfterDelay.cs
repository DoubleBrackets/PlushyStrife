using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    public void DelayedDestroy(float delay)
    {
        Destroy(gameObject, delay);
    }
}