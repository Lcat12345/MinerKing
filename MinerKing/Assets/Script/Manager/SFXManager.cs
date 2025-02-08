using UnityEngine;
using System.Collections;

public enum SoundKey
{
    Coin, Gem, Footstep, Pickaxe
}

public class SFXManager : MonoBehaviour
{
    [Header("Effect Sounds")]
    private AudioClip[] coinSounds;
    private AudioClip[] gemSounds;
    private AudioClip[] footstepSounds;
    private AudioClip pickaxeSound;
    private Coroutine footstepCoroutine = null; // 실행 중인 코루틴 저장
    private int currentIndex;   // 발자국 소리용

    public AudioSource audioSource;
    public AudioSource footstepAudioSource;
    public AudioSource loopAudioSource;

    private void Awake()
    {
        coinSounds = new AudioClip[6];
        for (int i = 0; i < 6; ++i)
        {
            coinSounds[i] = Resources.Load<AudioClip>("Sounds/Coin" + (i + 1));
        }

        gemSounds = new AudioClip[7];
        for (int i = 0; i < 7; ++i)
        {
            gemSounds[i] = Resources.Load<AudioClip>("Sounds/Gem" + (i + 1));
        }

        footstepSounds = new AudioClip[4];
        for (int i = 0; i < 4; ++i)
        {
            footstepSounds[i] = Resources.Load<AudioClip>("Sounds/Footstep" + (i + 1));
        }

        pickaxeSound = Resources.Load<AudioClip>("Sounds/pickaxe2");
        loopAudioSource.clip = pickaxeSound;
    }

    public void StopSFX(SoundKey key)
    {
        AudioClip clip = null;

        if (key == SoundKey.Pickaxe)
        {
            clip = pickaxeSound;
            if (clip != null)
            {
                loopAudioSource.Stop();
            }
            else
            {
                Debug.LogWarning("SFX not found for key: " + key);
            }
            return;
        }
        else if (key == SoundKey.Footstep)
        {
            if (footstepCoroutine != null)
            {
                StopCoroutine(footstepCoroutine); // 기존 코루틴 정지
                footstepCoroutine = null;
            }

            footstepAudioSource.Stop(); // 기존 음악 정지
            if (footstepSounds.Length == 0)
            {
                Debug.LogWarning("SFX not found for key: " + key);
                return;
            }
            return;
        }
    }

    public void PlaySFX(SoundKey key)
    {
        AudioClip clip = null;

        switch (key)
        {
        case SoundKey.Coin:
            if (coinSounds.Length > 0)
                clip = coinSounds[Random.Range(0, coinSounds.Length - 1)];
            break;

        case SoundKey.Gem:
            if (gemSounds.Length > 0)
                clip = gemSounds[Random.Range(0, gemSounds.Length - 1)];
            break;

        case SoundKey.Footstep:
            if (footstepCoroutine != null)
            {
                StopCoroutine(footstepCoroutine); // 기존 코루틴 정지
                footstepCoroutine = null;
            }

            footstepAudioSource.Stop(); // 기존 음악 정지
            if (footstepSounds.Length == 0)
            {
                Debug.LogWarning("SFX not found for key: " + key);
                return;
            }
            currentIndex = Random.Range(0, footstepSounds.Length - 1);
            footstepCoroutine = StartCoroutine(PlaySFXContinuously());
            return;

        case SoundKey.Pickaxe:
            clip = pickaxeSound;
            break;
        }

        if (clip != null)
        {
            if (key == SoundKey.Pickaxe)
            {
                loopAudioSource.Play();
            }
            else
            {
                audioSource.PlayOneShot(clip);
            }
        }
        else
        {
            Debug.LogWarning("SFX not found for key: " + key);
        }
    }

    IEnumerator PlaySFXContinuously()
    {
        while (true)
        {
            if (!footstepAudioSource.isPlaying) // 현재 재생 중인지 확인
            {
                footstepAudioSource.clip = footstepSounds[currentIndex];
                footstepAudioSource.Play();

                currentIndex = Random.Range(0, footstepSounds.Length - 1);
            }
            yield return null; // 다음 프레임까지 대기
        }
    }
}
