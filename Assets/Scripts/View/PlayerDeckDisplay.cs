using TMPro;
using UnityEngine;

/// <summary>
/// �������� �� ����������� ���������� ���������� ���� � ���� ������.
/// ����������� �� ������.
/// </summary>
public class PlayerDeckDisplay : MonoBehaviour
{
    public TextMeshProUGUI amountOfCards;

    private void OnEnable()
    {
        EventBus.OnPlayerDeckCardsChanged += UpdateCardsAmountText;
    }

    private void OnDisable()
    {
        EventBus.OnPlayerDeckCardsChanged -= UpdateCardsAmountText;
    }

    private void UpdateCardsAmountText()
    {
        amountOfCards.text = PlayerDeck.playerDeck.Count.ToString();
    }
}