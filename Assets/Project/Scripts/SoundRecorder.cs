using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Project.Scripts
{
    public class SoundRecorder : MonoBehaviour
    {
        private const int MaxRecordTime = 30;
        public AudioSource AudSource;
        public AudioSource AmbientAudioSource;

        public UnityEvent StartRecording;
        public UnityEvent StopRecording;

        public float[] Samples { get; private set; }
        public bool IsRecording { get; private set; }
        public float RecordingStartTime { get; private set; }
        public float RecordingEndTime { get; private set; }
        public float AmbientStartTime { get; private set; }
        public float AmbientEndTime { get; private set; }

        public void SetRecording(bool toggle)
        {
            IsRecording = toggle;
        }

        private void Update()
        {
            if (Microphone.IsRecording("Built-in Microphone"))
            {
                if (Time.time - RecordingStartTime > MaxRecordTime)
                {
                    OnStopRecording();
                }
                else
                {
                    AudSource.clip.GetData(Samples, 0);
                }
            }
        }

        public void OnStartRecording()
        {
            if (IsRecording) return;
            StartRecording.Invoke();
            AmbientAudioSource.time = 0;
            AudSource.clip = Microphone.Start("Built-in Microphone", false, MaxRecordTime, 44100);
            Debug.LogWarning("channels: " + AudSource.clip.channels);
            Debug.LogWarning("samples: " + AudSource.clip.samples);
            Debug.LogWarning("frequency: " + AudSource.clip.frequency);
            
            Samples = new float[AudSource.clip.samples*AudSource.clip.channels];
            RecordingStartTime = Time.time;
            AmbientStartTime = AmbientAudioSource.time;
        }

        public void OnStopRecording()
        {
            if (!IsRecording) return;
            StopRecording.Invoke();
            Microphone.End("Built-in Microphone");
            RecordingEndTime = Time.time;
            AmbientEndTime = AmbientAudioSource.time;
            float recTime = RecordingEndTime - RecordingStartTime;
            if (recTime > MaxRecordTime) recTime = MaxRecordTime;
            var ac = AudSource.clip;
            var chan = ac.channels;
            var freq = ac.frequency;

            var samplesList = Samples.ToList().GetRange(0, (int)(recTime*freq*chan));
            Samples = samplesList.ToArray();
            AudSource.clip = AudioClip.Create("Recording", (int)(recTime*freq), chan, freq, false);
            AudSource.clip.SetData(Samples, 0);
        }
    }
}
