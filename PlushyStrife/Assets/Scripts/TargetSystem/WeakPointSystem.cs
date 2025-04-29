using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace TargetSystem
{
    public class WeakPointSystem : MonoBehaviour
    {
        [Header("Config")]

        [SerializeField]
        private List<WeakPoint> weakPoints;

        [SerializeField]
        private float newWeakPointDelay;

        [SerializeField]
        private UnityEvent onWeakPointHit;

        private WeakPoint currentWeakPoint;

        public void StartWeakPointChain()
        {
            ChooseNewWeakPoint();
        }

        public void StopWeakPoints()
        {
            if (currentWeakPoint != null)
            {
                currentWeakPoint.SetNotVulnerable();
            }
        }

        private void ChooseNewWeakPoint()
        {
            WeakPoint randomWeakpoint = weakPoints[Random.Range(0, weakPoints.Count)];
            randomWeakpoint.SetVulnerable();
            currentWeakPoint = randomWeakpoint;
            randomWeakpoint.OnHit.AddListener(OnWeakPointHit);
        }

        private void OnWeakPointHit()
        {
            onWeakPointHit?.Invoke();

            currentWeakPoint.OnHit.RemoveListener(OnWeakPointHit);
            currentWeakPoint = null;

            // Choose a new weak point
            QueueNewWeakPoint().Forget();
        }

        private async UniTaskVoid QueueNewWeakPoint()
        {
            await UniTask.Delay((int)(newWeakPointDelay * 1000));
            ChooseNewWeakPoint();
        }

        public void HitWeakPoint(int index)
        {
            if (index < 0 || index >= weakPoints.Count)
            {
                Debug.LogError($"Invalid weak point index: {index}");
                return;
            }

            weakPoints[index].HitPoint();
        }

        public void HitWeakUpper(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                HitWeakPoint(0);
            }
        }

        public void HitWeakMiddle(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                HitWeakPoint(1);
            }
        }

        public void HitWeakLower(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                HitWeakPoint(2);
            }
        }
    }
}