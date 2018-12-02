using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager> {

    [SerializeField]
    private AudioSource sceneAudioSource;
    [SerializeField]
    private AudioClip lightZ1;

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        SelectSceneSound();
    }

    private void SelectSceneSound() {
        if(SceneManager.GetActiveScene().name == Constants.SCENE_1) {
            PlaySceneSound(lightZ1, true);
        }
    }

    private void PlaySceneSound(AudioClip audio, bool loop) {
        sceneAudioSource.clip = audio;
        sceneAudioSource.Play();
        sceneAudioSource.loop = loop;
    }
}
