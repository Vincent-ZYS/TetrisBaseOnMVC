using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip cursorAudioClip;
    public AudioClip fallAudioClip;
    public AudioClip leftRightMoveClip;
    public AudioClip tetrisRowEliminatedClip;
    public AudioClip gameOverClip;

    private AudioSource audioSource;
    private bool isMute = false;
    public bool IsMute { set { isMute = value; } }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCursorSound()
    {
        PlayAudio(cursorAudioClip);
    }

    public void PlayDropSound()
    {
        PlayAudio(fallAudioClip);
    }

    public void PlayLeftRightMoveSound()
    {
        PlayAudio(leftRightMoveClip);
    }

    public void PlayTetrisRowEliminatedSound()
    {
        PlayAudio(tetrisRowEliminatedClip);
    }

    public void PlayGameOverSound()
    {
        PlayAudio(gameOverClip);
    }

    private void PlayAudio(AudioClip adClip)
    {
        if (isMute) { return; }
        audioSource.clip = adClip;
        audioSource.Play();
    }

    public bool MuteAudio()
    {
        return isMute = !isMute;
    }
}
