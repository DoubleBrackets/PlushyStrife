using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace TargetSystem
{
    public class WeakPoint : MonoBehaviour
    {
        [FormerlySerializedAs("onHit")]
        [SerializeField]
        public UnityEvent OnHit;

        [SerializeField]
        public UnityEvent OnBecomeVulnerable;

        [SerializeField]
        public UnityEvent OnNotVulnerable;

        private bool _isVulnerable;

        private void Awake()
        {
            SetNotVulnerable();
        }

        public void HitPoint()
        {
            if (!_isVulnerable)
            {
                return;
            }

            _isVulnerable = false;
            Debug.Log("Weak point hit!");
            SetNotVulnerable();
            OnHit?.Invoke();
        }

        public void DoHitEffect()
        {
            OnHit?.Invoke();
        }

        [ContextMenu("Set Vuln")]
        public void SetVulnerable()
        {
            if (_isVulnerable)
            {
                return;
            }

            _isVulnerable = true;
            OnBecomeVulnerable?.Invoke();
        }

        [ContextMenu("Set Not Vuln")]
        public void SetNotVulnerable()
        {
            OnNotVulnerable?.Invoke();
        }
    }
}