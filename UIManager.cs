using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public Text cannonAmount;
    public Text trapAmount;
    public Text monster1_Amount;
    public Text monster2_Amount;
    public Text monster3_Amount;
    public Text monster4_Amount;
    public Text monster5_Amount;
    public Text monster6_Amount;
    public Text pillerAmount;
    public Text portalAmount;
    public Text iceAmount;
    public Text dropdownAmount;
    public Text stoneAmount;

    void Start()
    {
        ShowText();
    }

    public void ShowText()
    {
        Debug.Log("cnstepNumber :" + Scene.cnstepNumber);
        Debug.Log("tpstepNumber :" + Scene.tpstepNumber);
        cannonAmount.text = Scene.cnstepNumber.ToString();
        trapAmount.text = Scene.tpstepNumber.ToString();
        iceAmount.text = Scene.icestepNumber.ToString();
        dropdownAmount.text = Scene.ddNumber.ToString();
        stoneAmount.text = Scene.stNumber.ToString();
        monster1_Amount.text = Scene.mon1_leftNumber.ToString();
        monster2_Amount.text = Scene.mon2_leftNumber.ToString();
        monster3_Amount.text = Scene.mon3_leftNumber.ToString();
        monster4_Amount.text = Scene.mon4_leftNumber.ToString();
        monster5_Amount.text = Scene.mon5_leftNumber.ToString();
        monster6_Amount.text = Scene.mon6_leftNumber.ToString();
        pillerAmount.text = "Path Length : " + Scene.stepNumber.ToString() + "/" + Scene.groundNumber.ToString();
        portalAmount.text = Scene.ptNumber.ToString();
    }
}
