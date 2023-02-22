using UnityEngine;
using MoreMountains.NiceVibrations;

namespace Dlite.Games.Managers
{
    public class HapticManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            MMNViOS.iOSInitializeHaptics();
        }

        private void OnDisable()
        {
            MMNViOS.iOSReleaseHaptics();
        }

        public static void Haptic(HapticType hapticType)
        {
            var hapticTypeIndex = (int)hapticType;

            var mmvHaptic = (HapticTypes)hapticTypeIndex;
            MMVibrationManager.Haptic(mmvHaptic);
#if UNITY_EDITOR
            Debug.Log($"{hapticType} Triggered!");
#endif
        }
    }
}