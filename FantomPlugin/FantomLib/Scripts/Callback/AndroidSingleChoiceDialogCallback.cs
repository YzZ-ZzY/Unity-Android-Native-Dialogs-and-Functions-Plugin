#if UNITY_EDITOR || UNITY_ANDROID

using UnityEngine;

namespace FantomLib
{
    public class AndroidSingleChoiceDialogCallback : AndroidJavaProxy
    {
        public delegate void OnResultCallback(string res);
        public delegate void OnChangeCallback(string res);
        public delegate void OnCancelCallback();

        private readonly OnResultCallback onResultCallback;
        private readonly OnChangeCallback onChangeCallback;
        private readonly OnCancelCallback onCancelCallback;

        // private readonly FantomCallbackHelper callbackHelper;
        public AndroidSingleChoiceDialogCallback(OnResultCallback onResultCallback, OnChangeCallback onChangeCallback = null, OnCancelCallback onCancelCallback = null) : base(AndroidPlugin.ANDROID_PACKAGE + ".AndroidSingleChoiceDialogCallback")
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

        public void onCancel()
        {
            FantomCallbackHelper.Instance.CallOnMainThread(() =>
            {
                CallCancelCallback();
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
            Debug.Log("CallChangeCallback" + res);
            try
            {
                if (onChangeCallback != null)
                    onChangeCallback(res);
            }
            finally
            {
            }
        }

        private void CallCancelCallback()
        {
            try
            {
                if (onCancelCallback != null)
                    onCancelCallback();
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