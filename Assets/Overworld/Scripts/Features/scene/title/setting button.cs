using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class settingbutton : MonoBehaviour
{
    // Start is called before the first frame update
    public void setting_button()
    {
        SceneManager.LoadScene("setting");
    }
}
