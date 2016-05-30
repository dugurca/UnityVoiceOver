using UnityEngine;

namespace Assets.Project.Scripts
{
    public class RecScaler : MonoBehaviour
    {
        public SoundRecorder Recorder;
        public AudioSource AudSource;
        public RectTransform TransBase;
        public RectTransform Trans;
        private float _py;
        private Vector2 _startSize;

        void Start()
        {
            _py = TransBase.rect.height;
            _startSize = new Vector2(1, _py);
        }

        void Update()
        {
            if (Recorder.IsRecording)
            {
                var xPos = TransBase.rect.width/30f*(Time.time - Recorder.RecordingStartTime);
                var v2 = new Vector2(xPos, _py);
                Trans.sizeDelta = v2;
            }
            else if (AudSource.isPlaying)
            {
                var xPos = (TransBase.rect.width/AudSource.clip.length)*AudSource.time;
                var v2 = new Vector2(xPos, _py);
                Trans.sizeDelta = v2;
            }
            else
            {
                Trans.sizeDelta = _startSize;
            }
        }
    }
}
