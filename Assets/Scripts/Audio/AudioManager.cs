using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    [SerializeField] private AudioSource SourcePrefab;
    
    void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClip(AudioClip clip)
    {
        AudioSource source = Instantiate(SourcePrefab);
        source.transform.position = Vector3.zero;

        source.pitch *= Random.Range(0.85f, 1.15f);
        source.PlayOneShot(clip);
        
        Destroy(source.gameObject, clip.length);
    }
}
