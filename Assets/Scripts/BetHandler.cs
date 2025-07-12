using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetHandler : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text text;

    private void OnEnable()
    {
        slider.minValue = 1;
        slider.maxValue = player.Chips;
        slider.value = 1;
        text.text = "1";
    }

    public void ChangeText(float value)
    {
        text.text = value.ToString();
    }

    public void BetButton()
    {
        player.Bet((int)slider.value);
    }
}
