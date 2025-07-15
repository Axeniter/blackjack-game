using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private List<GameObject> currentChips = new();
    private bool playersTurn;

    [SerializeField] private Player player;
    [SerializeField] private Dealer dealer;
    [SerializeField] private List<GameObject> deck;
    [SerializeField] private GameObject chip;
    [SerializeField] private Vector3 chipPlace;
    [SerializeField] private float delay;

    public UnityEvent<string> OnGameOver;
    public UnityEvent OnGameReset;
    public UnityEvent OnBankrupt;

    public void GenerateChips(int chips)
    {
        IEnumerator ChipSpawn(float delay, int chips)
        {
            int count = 0;
            while (count < chips)
            {
                currentChips.Add(Instantiate(chip, chipPlace, Quaternion.identity));
                count++;
                yield return new WaitForSeconds(delay);
            }
        }
        float delay = 2f / chips;
        StartCoroutine(ChipSpawn(delay,chips));
    }

    public GameObject CardSpawn(Person person, GameObject card)
    {
        Vector3 pos = person.CardPlace;
        pos.x += person.CurrentHand.Cards.Count * 0.75f;
        Quaternion rotate = Quaternion.Euler(90f, 0, 0);
        return Instantiate(card, pos, rotate);

    }
    public void GiveCard(Person person)
    {
        GameObject card = deck[Random.Range(0, deck.Count)];
        person.CurrentHand.Take(CardSpawn(person, card));
    }

    public void StartGame(int bet)
    {
        IEnumerator GivingCards()
        {
            yield return new WaitForSeconds(0.3f);
            GiveCard(dealer);
            yield return new WaitForSeconds(delay);
            GiveCard(player);
            yield return new WaitForSeconds(delay);
            GiveCard(player);
            if (player.CurrentHand.Points != 21)
            {
                player.ReadyForTurn();
            }
        }
        player.Bet(bet);
        player.CurrentHand = player.MakeHand(bet);
        dealer.CurrentHand = dealer.MakeHand();
        StartCoroutine(GivingCards());
        playersTurn = true;
    }

    public void NextTurn()
    {
        if (playersTurn)
        {
            if (!player.Splited && player.CurrentHand.Busted)
            {
                StopAllCoroutines();
                playersTurn = false;
                GameOver();
            }
            else
            {
                StopAllCoroutines();
                playersTurn = false;
                dealer.DealerTake();
            }
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (player.Splited)
        {
            if (dealer.CurrentHand.Busted)
            {
                if (!player.SplitedHand1.Busted)
                {
                    player.TakeChips(player.SplitedHand1.Bet * 2);
                    OnGameOver?.Invoke("Рука 1 - выйгрыш: " + player.SplitedHand1.Bet);
                }
                else
                {
                    OnGameOver?.Invoke("Рука 1 - Bust");
                }
                if (!player.SplitedHand2.Busted)
                {
                    player.TakeChips(player.SplitedHand2.Bet * 2);
                    OnGameOver?.Invoke("Рука 2 - выйгрыш: " + player.SplitedHand2.Bet);
                }
                else
                {
                    OnGameOver?.Invoke("Рука 2 - Bust");
                }
            }
            else
            {
                if (!player.SplitedHand1.Busted)
                {
                    if (player.SplitedHand1.Points > dealer.CurrentHand.Points)
                    {
                        player.TakeChips(player.SplitedHand1.Bet * 2);
                        OnGameOver?.Invoke("Рука 1 - выйгрыш: " + player.SplitedHand1.Bet);
                    }
                    else if (player.SplitedHand1.Points < dealer.CurrentHand.Points)
                    {
                        OnGameOver?.Invoke("Рука 1 - проигрыш: " + player.SplitedHand1.Bet);
                    }
                    else
                    {
                        player.TakeChips(player.SplitedHand1.Bet);
                        OnGameOver?.Invoke("Рука 1 - Push");
                    }
                }
                else
                {
                    OnGameOver?.Invoke("Рука 1 - Bust");
                }
                if (!player.SplitedHand2.Busted)
                {
                    if (player.SplitedHand2.Points > dealer.CurrentHand.Points)
                    {
                        player.TakeChips(player.SplitedHand2.Bet * 2);
                        OnGameOver?.Invoke("Рука 2 - выйгрыш: " + player.SplitedHand2.Bet);
                    }
                    else if (player.SplitedHand2.Points < dealer.CurrentHand.Points)
                    {
                        OnGameOver?.Invoke("Рука 2 - проигрыш: " + player.SplitedHand2.Bet);
                    }
                    else
                    {
                        player.TakeChips(player.SplitedHand2.Bet);
                        OnGameOver?.Invoke("Рука 2 - Push");
                    }
                }
                else
                {
                    OnGameOver?.Invoke("Рука 2 - Bust");
                }

            }
        }
        else
        {
            if (dealer.CurrentHand.Busted)
            {
                player.TakeChips(player.CurrentHand.Bet * 2);
                OnGameOver?.Invoke("Выйгрыш: " + player.CurrentHand.Bet);
            }
            else if (player.CurrentHand.Busted)
            {
                OnGameOver?.Invoke("Bust");
            }
            else
            {
                if (player.CurrentHand.Points > dealer.CurrentHand.Points)
                {
                    player.TakeChips(player.CurrentHand.Bet * 2);
                    OnGameOver?.Invoke("Выйгрыш: " + player.CurrentHand.Bet);
                }
                else if (player.CurrentHand.Points < dealer.CurrentHand.Points)
                {
                    OnGameOver?.Invoke("Проигрыш: " + player.CurrentHand.Bet);
                }
                else
                {
                    player.TakeChips(player.CurrentHand.Bet);
                    OnGameOver?.Invoke("Push");
                }
            }
        }
        if (player.Chips >= 1)
        {
            ResetGame();
        }
        else
        {
            OnBankrupt?.Invoke();
        }
    }
    public void ResetGame()
    {
        IEnumerator Reseting()
        {
            yield return new WaitForSeconds(1f);
            foreach (GameObject chip in currentChips)
            {
                Destroy(chip);
            }
            currentChips.Clear();

            dealer.ResetHand();
            player.ResetHand();

            OnGameReset?.Invoke();
        }
        StartCoroutine(Reseting());
    }
}
