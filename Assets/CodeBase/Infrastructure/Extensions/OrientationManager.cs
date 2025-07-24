using UnityEngine;

namespace Infrastructure.Extensions
{
    public class OrientationManager : MonoBehaviour
    {
        private void LateUpdate()
        {
            DeviceOrientation orientation = Input.deviceOrientation;
            if (orientation == DeviceOrientation.LandscapeLeft)
            {
                Screen.orientation = ScreenOrientation.LandscapeLeft;
            }
            else if (orientation == DeviceOrientation.LandscapeRight)
            {
                Screen.orientation = ScreenOrientation.LandscapeRight;
            }
        }
    }
}