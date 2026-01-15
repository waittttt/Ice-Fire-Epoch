using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarDisplayManager : MonoBehaviour
{
    public GameObject starPrefab;
    public Transform starContainer;
    private AudioSource audioSource;
    public AudioClip getSound; // 跳跃音效
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void AddStar(){
        PlaySound();
        if(starPrefab!=null&&starContainer!=null){
            GameObject newStar=Instantiate(starPrefab,starContainer);
            newStar.transform.localScale=Vector3.one;
            Debug.Log("Display Star!!!");
        }
    }
    // Start is called before the first frame update
    private void PlaySound(){
        Debug.Log("getSound:"+getSound+",audioSource:"+audioSource);
        if(getSound!=null&&audioSource!=null){
            audioSource.clip = getSound;
            audioSource.loop=false;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
