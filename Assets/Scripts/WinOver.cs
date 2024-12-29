using UnityEngine;
using UnityEngine.UI;

public class WinOver : MonoBehaviour
{
    [SerializeField] float resultText;
    [SerializeField] float rulesText;
    [SerializeField] GameObject[] Values;


    public void WriteEnd(float current, float target)
    {
        resultText = current;
        rulesText = target;
        for (int i = 0; i < Values.Length; i++)
        {
            Values[i].GetComponent<Values>().current = (int)current;
            Values[i].GetComponent<Values>().target = (int)target;
        }
    }
}
