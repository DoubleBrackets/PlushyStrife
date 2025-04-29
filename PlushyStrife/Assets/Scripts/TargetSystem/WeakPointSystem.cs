using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace TargetSystem
{
    public class WeakPointSystem : MonoBehaviour
    {
        [Header("Config")]

        [SerializeField]
        private List<WeakPoint> weakPoints;

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
            currentWeakPoint.OnHit.RemoveListener(OnWeakPointHit);
            currentWeakPoint = null;

            // Choose a new weak point
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