using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class VarLocal : MonoBehaviour
{
    public LocalizedString localizedString = new LocalizedString { TableReference = "GUI", TableEntryReference = "Current Score {1}" };
    public Text text;

    void Start()
    {
        localizedString.StringChanged += LocalizedString_StringChanged;
    }

    void LocalizedString_StringChanged(string value)
    {
        Debug.Log(value);
    }

    public void SetScore(int score)
    {
        var operation = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("GUI");
        if (operation.IsDone)
            text.text = operation.Result;
        else
            operation.Completed += t => text.text = t.Result;
    }
}
