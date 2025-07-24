namespace Infrastructure.Extensions
{
    using UnityEngine;

    [DefaultExecutionOrder(-1000)]
    public class CutoutFix : MonoBehaviour
    {
        void Awake()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
                {
                    int sdkInt = version.GetStatic<int>("SDK_INT");

                    if (sdkInt >= 35)
                    {
                        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                        {
                            using (var window = activity.Call<AndroidJavaObject>("getWindow"))
                            {
                                using (var layoutParams = window.Call<AndroidJavaObject>("getAttributes"))
                                {
                                    int LAYOUT_IN_DISPLAY_CUTOUT_MODE_NEVER = 2;
                                    layoutParams.Set("layoutInDisplayCutoutMode", LAYOUT_IN_DISPLAY_CUTOUT_MODE_NEVER);
                                    window.Call("setAttributes", layoutParams);
                                }
                            }
                        }));
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("AndroidCutoutFix failed: " + e);
        }
#endif
        }
    }
}