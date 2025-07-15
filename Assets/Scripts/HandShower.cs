using UnityEngine;
using TMPro;

public class HandShower : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text text;

    private void OnEnable()
    {
        text.text = "Рука 1:";
    }

    public void ChangeText()
    {
        if (player.CurrentHand == player.SplitedHand2)
        {
            text.text = "Рука 2:";
        }
    }    
}

