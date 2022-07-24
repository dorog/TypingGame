using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject[] submenus;

    private int _currentVisibleSubmenu = 0;

    public void ActiveNextSubmenu()
    {
        if(submenus != null)
        {
            if (IsCurrentSubmenuAvailable())
            {
                submenus[_currentVisibleSubmenu].SetActive(false);
                _currentVisibleSubmenu++;

                if(IsCurrentSubmenuAvailable())
                {
                    submenus[_currentVisibleSubmenu].SetActive(true);
                }
            }
        }
    }

    private bool IsCurrentSubmenuAvailable()
    {
        return _currentVisibleSubmenu < submenus.Length;
    }

    public void ResetSubmenus()
    {
        if(submenus != null && submenus.Length > 0)
        {
            submenus[0].SetActive(true);
            for(int i = 1; i < submenus.Length; i++)
            {
                submenus[i].SetActive(false);
            }

            _currentVisibleSubmenu = 0;
        }
    }
}
