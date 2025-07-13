using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private List<GameObject> curentCards = new();
    private List<GameObject> curentChips = new();

    [SerializeField] private Player player;
    [SerializeField] private Dealer dealer;
    [SerializeField] private List<GameObject> deck;
    [SerializeField] private GameObject chip;
    [SerializeField] private Vector3 chipPlace;


    public UnityEvent OnPlayerWin;
    public UnityEvent OnDealerWin;

    public int Bet { get; set; }

    public void Bust(Person loser)
    {
        if (loser is Player)
        {
            OnDealerWin?.Invoke();
            Bet = 0;
        }
        else
        {
            OnPlayerWin?.Invoke();
            player.Chips += Bet * 2;
            Bet = 0;
        }
    }

    public void GenerateChips(int chips)
    {
        IEnumerator ChipSpawn(float delay, int chips)
        {
            int count = 0;
            while (count < chips)
            {
                curentChips.Add(Instantiate(chip, chipPlace, Quaternion.identity));
                count++;
                yield return new WaitForSeconds(delay);
            }
        }
        float delay = 2f / chips;
        StartCoroutine(ChipSpawn(delay,chips));
    }

    public IEnumerator GameStart()
    {
        dealer.CardTake();
        yield return new WaitForSeconds(0.5f);
        player.CardTake();
        yield return new WaitForSeconds(0.5f);
        player.CardTake();
        yield return new WaitForSeconds(0.5f);
    }

    public void Start()
    {
        StartCoroutine(GameStart());
    }

    public void ResetGame()
    {
        foreach (GameObject card in curentCards) Destroy(card);
        foreach (GameObject chip in curentChips) Destroy(chip);
        player.Cards.Clear();
        dealer.Cards.Clear();
        curentCards.Clear();
        curentChips.Clear();
    }

    public void GiveCard(Person person)
    {
        GameObject card = deck[Random.Range(0, deck.Count)];
        Quaternion rotate = Quaternion.Euler(90f, 0, 0);
        Vector3 pos = person.CardPlace;
        pos.x += person.Cards.Count * 0.75f;
        curentCards.Add(Instantiate(card, pos, rotate));
        person.Cards.Add(card.GetComponent<Card>());
    }
}
