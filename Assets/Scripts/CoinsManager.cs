using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;

public class CoinsManager : MonoBehaviour
{
	//References
	[Header("UI references")]
	//[SerializeField] TMP_Text coinUIText;
	[SerializeField] GameObject animatedCoinPrefab;

	[Space]
	[Header("Available coins : (coins to pool)")]
	[SerializeField] int maxCoins;
	Queue<GameObject> coinsQueue = new Queue<GameObject>();


	[Space]
	[Header("Animation settings")]
	[SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
	[SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;

	[SerializeField] Ease easeType;
	[SerializeField] float spread;
	public GameObject target;
	public Vector3 targetPosition;


	private int _c = 0;

	public int Coins
	{
		get { return _c; }
		set
		{
			_c = value;
		}
	}

	void Start()
	{
		PrepareCoins();
	}

    public void getTarget()
	{
		targetPosition = target.transform.position;
	}

	void Update(){
		getTarget();
	}


    void PrepareCoins()
	{
		GameObject coin;
		for (int i = 0; i < maxCoins; i++)
		{
			coin = Instantiate(animatedCoinPrefab);
			coin.transform.parent = transform;
			coin.SetActive(false);
			coinsQueue.Enqueue(coin);
		}
	}

    void AnimateCoins(Vector3 collectedCoinPosition, int coinAmount)
    {
        for (int _ = 0; _ < coinAmount; _++)
        {
            if (coinsQueue.Count > 0)
            {
                GameObject coin = coinsQueue.Dequeue();
                coin.SetActive(true);

                coin.transform.position = collectedCoinPosition + new Vector3(Random.Range(-spread, spread), 0f, 0f);

                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                coin.transform.DOMove(targetPosition, duration)
                    .SetEase(easeType)
                    .OnComplete(() =>
                    {
                        coin.SetActive(false);
                        coinsQueue.Enqueue(coin);

                        Coins++;
                    });
            }
        }
    }

	public void AddCoins(Vector3 collectedCoinPosition, int amount)
	{
		AnimateCoins(collectedCoinPosition, amount);
	}
}