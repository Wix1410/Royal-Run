using UnityEngine;

public class Coin : Pickup
{

    [SerializeField] int coinsValue = 100;

    ScoreMenager scoreMenager;

    public void Init(ScoreMenager scoreMenager)
    {
        this.scoreMenager = scoreMenager;
    }

    protected override void OnPickup()
    {
        scoreMenager.IncreaseScore(coinsValue);
    }
}
