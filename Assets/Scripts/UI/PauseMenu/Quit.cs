using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void QuitButton()
    {
        UI.Instance.PauseOptions.SetActive(false);
        UI.Instance.Mainmenu.SetActive(true);
        UI.Instance.Desktop.SetActive(true);
        UI.Instance.Cancel.SetActive(true);
    }
    public void Cancel()
    {
        LookMode.Instance.PauseMenuOn = false;
        LookMode.Instance.vol.profile = LookMode.Instance.standard;
        UI.Instance.PauseMenu.SetActive(false);
    }
}
