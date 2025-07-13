using TMPro;
using UnityEngine;

public class ChipsShower : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text chipsText;
    [SerializeField] private TMP_Text betText;
    void OnEnable()
    {
        ChangeChipsText();
    }
    
    public void ChangeChipsText()
    {
        chipsText.text = "Фишки: " + player.Chips;
    }
    public void ChangeBetText(int bet)
    {
        betText.text = "Ставка: " + bet;
    }
}
