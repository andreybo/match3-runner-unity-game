using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Character : MonoBehaviour
{
    public static Character Instance;

	[Header("Sound")]
	AudioSource audioS;
	public AudioClip jumpSound;
	public AudioClip healthUpSound;
	public AudioClip healthDownSound;
	public AudioClip ColliderSound;
	public AudioClip CrystalSound;
	public AudioClip WaterSound;
	public AudioClip swipe;
	public AudioClip Combo;
    
    public AudioClip coinsSound;
	
    void Awake(){
        Instance = this;
    }

	private void Start()
    {
		audioS = GetComponent<AudioSource>();
    }

    public void JumpSound()
    {
		if (audioS.enabled)
		{
			audioS.pitch = Random.Range(0.8f, 1.2f);
			audioS.clip = jumpSound;
			audioS.PlayOneShot(jumpSound);
		}
	}

	public void SoundHealthUp()
	{
		audioS.PlayOneShot(healthUpSound);
	}
	public void SoundHealthDown()
	{
		audioS.PlayOneShot(healthDownSound);
	}
	public void SoundCollision()
	{
		audioS.PlayOneShot(ColliderSound);
	}
	public void SoundCrystal()
	{
		audioS.PlayOneShot(CrystalSound);
	}
	public void GoToWater()
	{
		audioS.PlayOneShot(WaterSound);
	}

    public void CoinsSound()
    {
        audioS.PlayOneShot(coinsSound);
    }
}
