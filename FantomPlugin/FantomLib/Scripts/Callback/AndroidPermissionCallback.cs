#if UNITY_EDITOR || UNITY_ANDROID

using UnityEngine;

namespace FantomLib
{
    public class AndroidPermissionCallback : AndroidJavaProxy
    {
        public delegate void OnResultCallback(string result);

        private readonly OnResultCallback onResultCallback;
        // private readonly FantomCallbackHelper callbackHelper;
        public AndroidPermissionCallback(OnResultCallback onResultCallback) : base(AndroidPlugin.ANDROID_PACKAGE + ".AndroidPermissionCallback")
        {
            this.onResultCallback = onResultCallback;
            // this.callbackHelper = new GameObject("FantomCallbackHelper").AddComponent<FantomCallbackHelper>();
        }

        public void onResult(string res)
        {
            FantomCallbackHelper.Instance.CallOnMainThread(() =>
            {
                CallResultCallback(res);
            });

        }
        private void CallResultCallback(string res)
        {
            try
            {
                if (onResultCallback != null)
                    onResultCallback(res);
            }
            finally
            {
            }
        }


    }
}
#endif