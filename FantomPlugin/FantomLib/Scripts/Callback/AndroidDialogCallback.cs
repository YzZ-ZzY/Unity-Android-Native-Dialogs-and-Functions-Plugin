#if UNITY_EDITOR || UNITY_ANDROID

using UnityEngine;

namespace FantomLib
{
    public class AndroidDialogCallback : AndroidJavaProxy
    {
        public delegate void OnNegativeCallback();
        public delegate void OnPositiveCallback();

        private readonly OnPositiveCallback onPositiveCallback;
        private readonly OnNegativeCallback onNegativeCallback;
        private readonly FantomCallbackHelper callbackHelper;
        public AndroidDialogCallback(OnPositiveCallback onPositiveCallback, OnNegativeCallback onNegativeCallback = null) : base(AndroidPlugin.ANDROID_PACKAGE + ".AndroidDialogCallback")
        {
            this.onPositiveCallback = onPositiveCallback;
            this.onNegativeCallback = onNegativeCallback;
            this.callbackHelper = new GameObject("FantomCallbackHelper").AddComponent<FantomCallbackHelper>();
        }

        public void onPositive()
        {
            callbackHelper.CallOnMainThread(() =>
            {
                PositiveCallback();
            });

        }
        private void PositiveCallback()
        {

            try
            {
                if (onPositiveCallback != null)
                    onPositiveCallback();
            }
            finally
            {
                Object.Destroy(callbackHelper.gameObject);
            }
        }

        public void onNegative()
        {

            callbackHelper.CallOnMainThread(() =>
            {
                NegativeCallback();
            });

        }
        private void NegativeCallback()
        {
            try
            {
                if (onNegativeCallback != null)
                    onNegativeCallback();
            }
            finally
            {
                Object.Destroy(callbackHelper.gameObject);
            }
        }
    }
}
#endif