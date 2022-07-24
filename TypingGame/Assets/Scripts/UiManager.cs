using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private StartMenu menus;
    [SerializeField]
    private GameObject game;
    [SerializeField]
    private ResultMenu result;

    [SerializeField]
    private SpammingManager spammingManager;

    private GameObject _previousMenu;

    private void Awake()
    {
        _previousMenu = menus.gameObject;
    }

    public void StartGame(GameSetting gameSetting)
    {
        spammingManager.SetGameSettings(gameSetting);
        SetAcitivty(game);
    }

    public void RestartGame()
    {
        SetAcitivty(game);
    }

    public void ShowResult(int destroyedCount, int missedCount)
    {
        _previousMenu.SetActive(false);

        result.UpdatePoints(destroyedCount, missedCount);
        SetAcitivty(result.gameObject);
    }

    public void ShowMenu()
    {
        menus.ResetSubmenus();
        SetAcitivty(menus.gameObject);
    }

    private void SetAcitivty(GameObject newActiveMenu)
    {
        _previousMenu.gameObject.SetActive(false);
        newActiveMenu.SetActive(true);

        _previousMenu = newActiveMenu;
    }
}
