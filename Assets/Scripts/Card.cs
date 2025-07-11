using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private bool isAce;

    public int Value => value;
    public bool IsAce => isAce;
}
