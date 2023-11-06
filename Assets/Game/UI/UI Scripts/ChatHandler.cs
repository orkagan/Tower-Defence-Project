using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatHandler : MonoBehaviour
{
    #region Singleton
    public static ChatHandler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public Text chat;
    [SerializeField] List<string> lines;

    public void CreateNewLine(string text)
    {
        lines.Add(text);
        chat.text = $"{text}\n";
    }

    private void Update()
    {
        if (lines.Count > 20)
        {
            lines.RemoveAt(lines.Count - 1);
        }
    }
}
