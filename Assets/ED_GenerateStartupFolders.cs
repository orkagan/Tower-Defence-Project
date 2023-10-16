using UnityEngine;
using UnityEditor;
using System.Linq;

public class ED_GenerateStartupFolders : MonoBehaviour
{
    private const string parentFolderName = "Game";
    private static readonly string parentFolderPath = "Assets/" + parentFolderName;

    private const string parentFolderCreationLog = "Parent folder created at: ";
    private const string parentFolderExistsWarning = "Parent folder already exists at: ";
    private const string folderCreationLog = "Folder created at: ";
    private const string folderExistsWarning = "Folder already exists at: ";

    [MenuItem("Startup/Generate Startup Folders")]
    static void CreateCommonProjectFolders()
    {
        if (!AssetDatabase.IsValidFolder(parentFolderPath))
        {
            AssetDatabase.CreateFolder("Assets", parentFolderName);
            Debug.Log(parentFolderCreationLog + parentFolderPath);
        }
        else
        {
            Debug.LogWarning(parentFolderExistsWarning + parentFolderPath);
        }

        string[] folderNames = new string[]
        {
            "Animations",
            "Audio",
            "Documentation",
            "Editor",
            "Fonts",
            "Materials",
            "PhysicsMaterials",
            "Plugins",
            "Prefabs",
            "Resources",
            "Scenes",
            "Scripts",
            "Shaders",
            "Textures",
            "UI"
        };

        folderNames = folderNames.OrderBy(name => name).ToArray();

        foreach (string folderName in folderNames)
        {
            string path = parentFolderPath + "/" + folderName;

            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder(parentFolderPath, folderName);
                Debug.Log(folderCreationLog + path);
            }
            else
            {
                Debug.LogWarning(folderExistsWarning + path);
            }
        }
    }
}
