using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip btnClick;
    public AudioClip bgm;
    public AudioClip rodThrowing;
    public AudioClip fishCatch;
    public AudioClip coin;
    public AudioClip noCoin;
    public AudioClip slotSelect;
    public AudioClip itemEquip;
    

    AudioSource audioSource;
    
    
<<<<<<< HEAD
    
=======
    // Start is called before the first frame update
>>>>>>> da68dd2e469b9e8b3aa924e7f09a8b4ef45d6a22
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void ThrowRod(){
        audioSource.clip = rodThrowing;
        audioSource.Play();
    }

    public void CatchFish(){
        audioSource.clip = fishCatch;
        audioSource.Play();
    }

    public void ClickBtn(){
        audioSource.clip = btnClick;
        audioSource.Play();
    }
    public void Coin(){
        audioSource.clip = coin;
        audioSource.Play(); 
    }

    public void NoCoin(){
        audioSource.clip = noCoin;
        audioSource.Play(); 
    }

    public void SelectSlot(){
        audioSource.clip = slotSelect;
        audioSource.Play(); 
    }

    public void EquipItem(){
        audioSource.clip = itemEquip;
        audioSource.Play(); 
    }
}
