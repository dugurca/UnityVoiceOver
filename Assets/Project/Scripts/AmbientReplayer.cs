using System.Collections;
using UnityEngine;

namespace Assets.Project.Scripts
{
    public class AmbientReplayer : MonoBehaviour
    {
        public SoundRecorder Recorder;
        public AudioSource AmbientAudioSource;

        private readonly WaitForEndOfFrame _waitForFrame = new WaitForEndOfFrame();

        public void ReplayAmbient()
        {
            float startTime = Recorder.AmbientStartTime;
            float endTime = Recorder.AmbientEndTime;

            StartCoroutine(AmbientPlayback(startTime, endTime));
        }

        private IEnumerator AmbientPlayback(float startTime, float endTime)
        {
            AmbientAudioSource.time = startTime;
            AmbientAudioSource.Play();
            while (AmbientAudioSource.time < endTime)
            {
                yield return _waitForFrame;
            }
            AmbientAudioSource.Stop();
        }
    }
}
