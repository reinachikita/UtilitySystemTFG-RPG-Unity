using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HUDSystemNotBattle : MonoBehaviour
{

    public TextMeshProUGUI textAnxiety, textSIP, textName; //UI out of battle

    // Start is called before the first frame update
    public void startUI()
    {
        textAnxiety.text = GameManager.instance.currentAnxiety.ToString();
        textSIP.text = GameManager.instance.currentSip.ToString();
        textName.text = "Elisa";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
