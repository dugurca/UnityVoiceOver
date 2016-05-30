using UnityEngine;

namespace Assets.Project.Scripts
{
    public class Draw : MonoBehaviour
    {
        public AudioSource AudSource;
        public SoundRecorder Recorder;
        public Material Mat;

        void OnPostRender()
        {
            if (Recorder.IsRecording || AudSource.isPlaying)
            {
                var samp = Recorder.Samples;
                var n = samp.Length;
                if (AudSource.isPlaying)
                    n = AudSource.timeSamples;
                GL.PushMatrix();
                GL.LoadOrtho();
                Mat.SetPass(0);
                GL.Begin(GL.LINES);
                GL.Color(Color.white);
                const float k = 1/6f;
                for (int i = 0; i < n; i += 100)
                {
                    if (i < samp.Length)
                    {
                        GL.Vertex(new Vector3((float) i/samp.Length, samp[i]/6f + k, 0));
                        GL.Vertex(new Vector3((float) i/samp.Length, k, 0));
                    }
                }

                GL.End();
                GL.PopMatrix();
            }
        }
    }
}








