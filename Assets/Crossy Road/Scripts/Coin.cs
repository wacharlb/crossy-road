using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public GameObject coin = null;
    public AudioClip audioClip = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player picked up coin!");

            // Update the coin value in the Manager
            Manager.instance.UpdateCoinCount(coinValue);

            //this.GetComponent<AudioSource>().PlayOneShot(audioClip);
            this.GetComponent<AudioSource>().PlayOneShot(audioClip, 10);
            coin.SetActive(false);

            Destroy(this.gameObject, audioClip.length);
        }
    }
}
