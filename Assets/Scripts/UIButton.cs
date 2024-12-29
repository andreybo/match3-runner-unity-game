using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameControllerButton gameControllerButton;
    [SerializeField] bool isLeftButton = false;
    void Update()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(isLeftButton){
            gameControllerButton.isLeft = true;
        }
        else{
            gameControllerButton.isRight = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(isLeftButton){
            gameControllerButton.isLeft = false;
        }
        else{
            gameControllerButton.isRight = false;
        }
    }
}
