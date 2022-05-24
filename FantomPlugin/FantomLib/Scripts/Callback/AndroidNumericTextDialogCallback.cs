#if UNITY_EDITOR || UNITY_ANDROID

using UnityEngine;

namespace FantomLib
{
    public class AndroidNumericTextDialogCallback : AndroidJavaProxy
    {
        public delegate void ResultCallback(string dateTime);

		private readonly ResultCallback resultCallback;
		private readonly FantomCallbackHelper callbackHelper;
        public AndroidNumericTextDialogCallback(ResultCallback resultCallback) : base(AndroidPlugin.ANDROID_PACKAGE + ".AndroidNumericTextDialogCallback") { 
            this.resultCallback = resultCallback;
            this.callbackHelper = new GameObject( "FantomCallbackHelper" ).AddComponent<FantomCallbackHelper>();
        }
        
        public void onResult(string dateTime)
        {
			callbackHelper.CallOnMainThread( () => {
              CallResultCallback(dateTime);
            });

        }
		private void CallResultCallback(string dateTime)
		{
			try
			{
              if (resultCallback != null)
                    resultCallback(dateTime);
			}
			finally
			{
				Object.Destroy( callbackHelper.gameObject );
			}
		}


    }
}
#endif