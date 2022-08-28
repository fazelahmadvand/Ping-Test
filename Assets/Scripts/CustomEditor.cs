using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Diagnostics;

public class CustomEditor : EditorWindow
{

    private const string PATH = "CustomEditor/";


    [MenuItem(PATH + "Remove File")]
    public static void RemoveFiles()
    {
        string path = Application.dataPath;
        string[] files = Directory.GetFiles(path, "*.prefab", SearchOption.AllDirectories);

        foreach (var item in files)
        {
            File.Delete(item);
        }

    }

    [MenuItem(PATH + "Git Commit")]
    public static void GitCommit()
    {
        string git = "git";
        string add = @"add .";
        string commit = "commit -m ";
        var time = DateTime.Now.ToString();
        string push = @"push";


        Process.Start(git, add);
        Process.Start(git, commit + time);
        Process.Start(git, push);



    }





}
