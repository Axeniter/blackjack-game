using TMPro;
using UnityEngine;

public class ChipsShower : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text chipsText;

    private void Awake()
    {
        ChangeChipsText();
    }
    public void ChangeChipsText()
    {
        chipsText.text = "Фишки: " + player.Chips;
    }

}
