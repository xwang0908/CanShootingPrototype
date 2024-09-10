using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.Serialization;

public class SoundEffect : MonoBehaviour
{
    public enum ClipSelectionMode
    {
        Random,
        Sequence,
        First
    }
    
    [FormerlySerializedAs("Clip")] [Tooltip("The clips to play")] [SerializeField]
    private AudioClip[] Clips;

    [Tooltip("How to select which clip to play from the list")] [SerializeField]
    private ClipSelectionMode SelectionMode;

    [Tooltip("The minimum pitch to play the clip at")] [SerializeField]
    private float MinPitch;

    [Tooltip("The maximum pitch to play the clip at")] [SerializeField]
    private float MaxPitch;

    [Tooltip("The volume of the clip")] [SerializeField]
    private float EffectVolume = 1.0f;

    private int _soundIndex;
    
    public void Play()
    {
        float pitch = Random.Range(MinPitch, MaxPitch);

        if (SelectionMode == ClipSelectionMode.First)
        {
            AudioManager.Instance.PlayClip(Clips[0], pitch, EffectVolume);
            return;
        }
        
        if (SelectionMode == ClipSelectionMode.Random)
            _soundIndex = Random.Range(0, Clips.Length);
        
        AudioManager.Instance.PlayClip(Clips[_soundIndex], pitch, EffectVolume);

        if (SelectionMode == ClipSelectionMode.Sequence)
            _soundIndex = (_soundIndex + 1) % Clips.Length;
    }
}
