using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyInfoPopup : MonoBehaviour
{
    public TextMeshProUGUI NameTxt;
    public TextMeshProUGUI GradeTxt;
    public TextMeshProUGUI SpeedTxt;
    public TextMeshProUGUI HealthTxt;

    public void SetTxt(EnemyData data)
    {
        NameTxt.text = data.Name;
        GradeTxt.text = data.Grade;
        SpeedTxt.text = data.Speed.ToString();
        HealthTxt.text = data.Health.ToString();
    }
}
