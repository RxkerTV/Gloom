using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Continue : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Continuebutton()
    {
        LookMode.Instance.PauseMenuOn=false;
        LookMode.Instance.vol.profile = LookMode.Instance.standard;
        UI.Instance.PauseMenu.SetActive(false);
    }
}
