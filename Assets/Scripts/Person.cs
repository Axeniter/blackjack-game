using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Person : MonoBehaviour
{
    public List<Card> Cards { get; set; } = new List<Card>();
    [SerializeField] private Vector3 cardPlace;
    public Vector3 CardPlace => cardPlace;
    private int points;
    public UnityEvent OnBust;

    private void CountPoints()
    {
        points = 0;
        List<Card> aces = new List<Card>();
        foreach (Card card in Cards)
        {
            if (card.IsAce)
            {
                aces.Add(card);
            }
            else
            {
                points += card.Value;
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
        CountPoints();
    }
}
