using Feel;
using UnityEngine;

namespace Camera
{
    public class ScreenShakeMono : MonoBehaviour
    {
        [SerializeField]
        private ScreenShakeSO screenShakeSO;

        [SerializeField]
        private Vector2 velocity;

        [SerializeField]
        private float strength;

        public void GenerateImpulse()
        {
            screenShakeSO.GenerateImpulse(transform.position, velocity * strength);
        }
    }
}