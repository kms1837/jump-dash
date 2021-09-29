using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private TitleScene.Menu menuNumber;

    [SerializeField]
    private TitleScene scene;

    public void OnPointerEnter(PointerEventData eventData) {
        scene.setMenuSelete((int)menuNumber);
    }
}
