using TMPro;
using UnityEngine;

public class ChipsShower : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text chipsText;
    [SerializeField] private TMP_Text betText;
    private int currentBet;
    void OnEnable()
    {
        currentBet = 0;
        ChangeChipsText();
    }
    
    public void ChangeChipsText()
    {
        chipsText.text = "Фишки: " + player.Chips;
    }
    public void ChangeBetText(int bet)
    {
        currentBet += bet;
        betText.text = "Ставка: " + currentBet;
    }
}
