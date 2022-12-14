using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Source : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgm;
    public Slider backVolume;

    private float backVol = 1f;

    public void Start()
    {
        /*
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = bgm; //오디오에 bgm이라는 파일 연결

        audioSource.volume = 1.0f; //0.0f ~ 1.0f사이의 숫자로 볼륨을 조절
        audioSource.loop = true; //반복 여부
        audioSource.mute = false; //오디오 음소거

        audioSource.Play(); //오디오 재생
        audioSource.Stop(); //오디오 멈추기

        audioSource.playOnAwake = true;
        //활성화시 해당씬 실행시 바로 사운드 재생이 시작됩니다.
        //비활성화시 Play()명령을 통해서만 재생됩니다.

        audioSource.priority = 0;
        //씬안에 모든 오디오소스중 현재 오디오 소스의 우선순위를 정한다.
        // 0 : 최우선, 256 : 최하, 128 : 기본값
        */
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        backVolume.value = backVol;
        audioSource.volume = backVolume.value;
    }

    void Update()
    {
        SoundSlider();
    }

    public void SoundSlider()
    {
        audioSource.volume = backVolume.value;

        backVol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);
    }
}
