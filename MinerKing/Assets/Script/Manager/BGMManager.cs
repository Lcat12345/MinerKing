using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;
    private AudioClip[] currentBGMSet; // 현재 맵의 음악 리스트
    private int currentIndex = 0;
    private Coroutine bgmCoroutine = null; // 실행 중인 코루틴 저장

    public void PlayNewBGMSet(AudioClip[] newBGMSet)
    {
        if (bgmCoroutine != null)
        {
            StopCoroutine(bgmCoroutine); // 기존 코루틴 정지
            bgmCoroutine = null;
        }

        audioSource.Stop(); // 기존 음악 정지
        currentBGMSet = newBGMSet;
        currentIndex = 0;

        if (currentBGMSet.Length > 0)
        {
            bgmCoroutine = StartCoroutine(PlayBGMSequentially());
        }
    }

    IEnumerator PlayBGMSequentially()
    {
        while (true)
        {
            if (!audioSource.isPlaying) // 현재 재생 중인지 확인
            {
                audioSource.clip = currentBGMSet[currentIndex];
                audioSource.Play();

                currentIndex = (currentIndex + 1) % currentBGMSet.Length; // 다음 곡으로 변경
            }
            yield return null; // 다음 프레임까지 대기
        }
    }
}