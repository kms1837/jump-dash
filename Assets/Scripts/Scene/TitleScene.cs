using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    const string StartGameScene = "TestScene";

    enum UIState {
        Intro,
        Menu
    }

    public enum Menu {
        Start,
        Setting,
        Quit,
        Length
    }

    [SerializeField]
    private GameObject pleaseKeyLabel = null;

    [SerializeField]
    private GameObject menuLayer = null;

    [SerializeField]
    private Transform menuButtons = null;

    [SerializeField]
    private RectTransform selectBar = null;

    private UIState state = UIState.Intro;
    private int menuSelect = 0;

    private void Awake() {
        menuLayer.SetActive(false);
    }

    void Start() {
        
    }

    void Update() {
        if (Input.anyKey && state == UIState.Intro) {
            state = UIState.Menu;
            pleaseKeyLabel.SetActive(false);
            menuLayer.SetActive(true);
        }

        if (state == UIState.Menu) {
            menuUpdate();
            selectBarUpdate();
        }
    }

    private void menuUpdate() {
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            menuSelect--;
            if (menuSelect <= -1) {
                menuSelect = (int)Menu.Length - 1;
            }
            setMenuSelete(menuSelect);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            menuSelect++;
            setMenuSelete(menuSelect % (int)Menu.Length);
        }

        if (Input.GetKeyUp(KeyCode.Return)) {
            menuAction();
        }
    }

    public void setMenuSelete(int select) {
        menuSelect = select;
        selectBarUpdate();
    }

    private void menuAction() {
        switch (menuSelect) {
            case (int)Menu.Start:
                gameStart();
                break;
            case (int)Menu.Setting:
                gameSetting();
                break;
            case (int)Menu.Quit:
                gameQuit();
                break;
        }
    }

    private void selectBarUpdate() {
        Vector3 selectButtonPos = menuButtons.GetChild(menuSelect).position;

        selectBar.position = new Vector3(selectBar.position.x, selectButtonPos.y, selectButtonPos.z);
    }

    public void gameStart() {
        SceneManager.LoadScene(StartGameScene);
    }

    public void gameSetting() {

    }

    public void gameQuit() {
        Application.Quit();
    }
}
