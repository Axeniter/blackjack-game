using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Dealer dealer;
    [SerializeField] private List<GameObject> deck;
    [SerializeField] private GameObject chip;
    [SerializeField] private Vector3 chipPlace;
    private List<GameObject> curentCards = new();
    private List<GameObject> curentChips = new();

    
    public int Bet { get; set; }

    public void GenerateChips(int chips)
    {
        float delay = 2 / chips;
        StartCoroutine(ChipSpawn(delay,chips));
    }

    private IEnumerator ChipSpawn(float delay, int chips)
    {
        int count = 0;
        while (count < chips)
        {
            count++;
            Instantiate(chip, chipPlace,Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
    }

    public void GameStart()
    {
        GiveCard(player);
        GiveCard(player);
    }

    public void Start()
    {
        GameStart();
    }

    public void GiveCard(Person person)
    {
        GameObject card = deck[Random.Range(0, deck.Count)];
        curentCards.Add(card);
        Quaternion rotate = Quaternion.Euler(90f, 0,0);
        Vector3 pos = person.CardPlace;
        pos.x += person.Cards.Count * 0.3f;
        Instantiate(card, pos, rotate);
        person.CardTake(card.GetComponent<Card>());
    }
}
