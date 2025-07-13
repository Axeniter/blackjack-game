using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Person : MonoBehaviour
{
    public class Hand
    {
        public List<GameObject> CardsObjects { get; set; }
        public List<Card> Cards { get; set; }
        public int Bet {  get; set; }

        public Hand(int bet)
        {
            CardsObjects = new List<GameObject>();
            Cards = new List<Card>();
            Bet = bet;
        }
    }
    public List<Hand> Hands { get; set; }
    public List<Card> Cards { get; set; } = new List<Card>();
    [SerializeField] private Vector3 cardPlace;
    public Vector3 CardPlace => cardPlace;
    protected int points;
    public UnityEvent<Person> OnBust;
    public UnityEvent<int> OnPointsChange;
    public UnityEvent<Person> OnCardTake;
    public UnityEvent<int> OnStand;

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
        OnPointsChange?.Invoke(points);
        if (points > 21)
        {
            OnBust?.Invoke(this);
        }
    }

    public void CardTake()
    {
        OnCardTake?.Invoke(this);
        CountPoints();
    }
}
