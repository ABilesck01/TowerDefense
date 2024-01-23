using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAudio : MonoBehaviour
{
    [SerializeField] private SerializableDictionary<BaseUnit.UnitState, AudioClip> unitStates;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public void OnStateChanged(BaseUnit.UnitState state)
    {
        unitStates.TryGetValue(state, out AudioClip clip);
        if (clip == null)
        {
            return;
        }

        audioSource.clip = clip;
        audioSource.Play();
    }
}
