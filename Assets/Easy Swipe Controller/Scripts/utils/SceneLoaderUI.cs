using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace MOSoft.SwipeController
{
    public class SceneLoaderUI : MonoBehaviour
    {
        void OnGUI()
        {
            // Make a background box
            GUI.Box(new Rect(10, 10, 220, 240), "Change Scene");

            // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
            if (GUI.Button(new Rect(20, 40, 200, 100), "Scene 1"))
            {
                SceneManager.LoadScene(0);
            }

            // Make the second button.
            if (GUI.Button(new Rect(20, 140, 200, 100), "Scene 2"))
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}