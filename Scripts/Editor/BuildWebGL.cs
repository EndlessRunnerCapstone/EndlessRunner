//place this script in the Editor folder within Assets.
using UnityEditor;
using UnityEngine;


//to be used on the command line:
//$ Unity -quit -batchmode -executeMethod WebGLBuilder.build

public class WebGLBuilder : MonoBehaviour
{
    public static void build()
    {
        string[] scenes = { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/Level01.unity" };
        BuildPipeline.BuildPlayer(scenes, "WebGL", BuildTarget.WebGL, BuildOptions.None);
    }
}