using UnityEngine;
using UnityEngine.UI;

namespace TGE_SDK {
    public class TGE_SDK_GradientScroll : MonoBehaviour
    {
        public Gradient myGradient;
        public float strobeDuration = 5f;
        public Text go;

        public void FixedUpdate()
        {
            float t = Mathf.PingPong(Time.time / strobeDuration, 1f);
            go.color = myGradient.Evaluate(t);
        }
    }
}
