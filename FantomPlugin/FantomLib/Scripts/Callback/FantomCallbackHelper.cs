#if UNITY_EDITOR || UNITY_ANDROID
using System.Linq;
using UnityEngine;

namespace FantomLib
{
	public class FantomCallbackHelper : MonoBehaviour
	{


        protected static FantomCallbackHelper instance;

        public static FantomCallbackHelper Instance
        {
            get
            {
				if( instance == null )
				{
					instance =  new GameObject("FantomCallbackHelper").AddComponent<FantomCallbackHelper>();
					DontDestroyOnLoad( instance.gameObject );
				}
                return instance;
            }
        }

		private System.Action mainThreadAction = null;


		private void Update()
		{
			if( mainThreadAction != null )
			{
				System.Action temp = mainThreadAction;
				mainThreadAction = null;
				temp();
			}
		}

		public void CallOnMainThread( System.Action function )
		{
			mainThreadAction = function;
		}
	}
}
#endif
