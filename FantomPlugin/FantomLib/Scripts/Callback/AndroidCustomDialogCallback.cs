#if UNITY_EDITOR || UNITY_ANDROID

using UnityEngine;

namespace FantomLib
{
    public class AndroidCustomDialogCallback : AndroidJavaProxy
    {
        public delegate void OnResultCallback(string res);
        public delegate void OnChangeCallback(string res);
        public delegate void OnCancelCallback(string res);

        private readonly OnResultCallback onResultCallback;
        private readonly OnChangeCallback onChangeCallback;
        private readonly OnCancelCallback onCancelCallback;

        // private readonly FantomCallbackHelper callbackHelper;
        public AndroidCustomDialogCallback(OnResultCallback onResultCallback, OnChangeCallback onChangeCallback = null, OnCancelCallback onCancelCallback = null) : base(AndroidPlugin.ANDROID_PACKAGE + ".AndroidCustomDialogCallback")
        {
            this.onResultCallback = onResultCallback;
            this.onChangeCallback = onChangeCallback;
            this.onCancelCallback = onCancelCallback;
            // this.callbackHelper = new GameObject("FantomCallbackHelper").AddComponent<FantomCallbackHelper>();
        }

        public void onResult(string res)
        {
            FantomCallbackHelper.Instance.CallOnMainThread(() =>
            {
                CallResultCallback(res);
            });
        }

        public void onCancel(string res)
        {
            FantomCallbackHelper.Instance.CallOnMainThread(() =>
            {
                CallCancelCallback(res);
            });
        }
        public void onChange(string res)
        {
            FantomCallbackHelper.Instance.CallOnMainThread(() =>
            {
                CallChangeCallback(res);
            });

        }

        private void CallChangeCallback(string res)
        {
            
            try
            {
                if (onChangeCallback != null)
                    onChangeCallback(res);
                }
            finally
            {
  
            }
        }

        private void CallCancelCallback(string res)
        {
            try
            {
                if (onCancelCallback != null)
                    onCancelCallback(res);
            }
            finally
            {

            }
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