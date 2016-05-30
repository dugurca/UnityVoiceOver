using UnityEngine;

namespace Assets.Project.Scripts
{
    public class Scaler : MonoBehaviour
    {
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
            if (AudSource != null && AudSource.isPlaying)
            {
                var v2 = new Vector2((TransBase.rect.width/AudSource.clip.length)*AudSource.time, _py);
                Trans.sizeDelta = v2;
            }
            else
            {
                Trans.sizeDelta = _startSize;
            }
        }
    }
}
