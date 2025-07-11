using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Person : MonoBehaviour
{
    public List<Card> Cards { get; set; }
    private int points;
    public UnityEvent OnBust;

    public void CountPoints()
    {
        points = 0;
        List<Card> aces = new List<Card>();
        foreach (Card card in Cards)
        {
            if (card.isAce)
            {
                aces.Add(card);
            }
            else
            {
                points += card.value;
            }
        }
        if (aces.Count > 0)
        {
            foreach (Card card in aces)
            {
                points += 1;
            }
            if (points <= 11)
            {
                points += 10;
            }
        }

        if (points > 21)
        {
            OnBust?.Invoke();
        }
    }

    public void CardTake(Card card)
    {
        Cards.Add(card);

    }
}
