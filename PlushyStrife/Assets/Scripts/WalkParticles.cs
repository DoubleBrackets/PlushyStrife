using UnityEngine;

public class WalkParticles : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private ParticleSystem particleSystem;

    private void Update()
    {
        if (rb2d.linearVelocity.magnitude > 0.1f)
        {
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
        else
        {
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop();
            }
        }
    }
}