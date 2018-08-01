//place this script in the Editor folder within Assets.
using UnityEditor;
using UnityEngine;


//to be used on the command line:
//$ Unity -quit -batchmode -executeMethod WebGLBuilder.build

public class WebGLBuilder : MonoBehaviour
{
    public static void build()
    {
        string[] scenes = { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/WaitingForPlayer.unity", "Assets/Scenes/Level01.unity",
            "Assets/Scenes/Level02.unity", "Assets/Scenes/Level03.unity", "Assets/Scenes/Level04.unity", "Assets/Scenes/LoadingScreen1.unity",
        "Assets/Scenes/LoadingScreen2.unity", "Assets/Scenes/LoadingScreen3.unity", "Assets/Scenes/LoadingScreen4.unity"};
        BuildPipeline.BuildPlayer(scenes, "WebGL", BuildTarget.WebGL, BuildOptions.None);
    }
}