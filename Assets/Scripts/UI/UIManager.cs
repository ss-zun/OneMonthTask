using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public EnemyInfoPopup enemyInfoPopup;

    public GameObject HpBar;
    public Image HpFillBar;

    public void ShowEnemyInfoPopup(EnemyData data)
    {
        enemyInfoPopup.gameObject.SetActive(true);
        enemyInfoPopup.SetTxt(data);
    }
}