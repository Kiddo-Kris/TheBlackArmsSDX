//Moshiro ist nen hurensohn
using UnityEngine;
using UnityEngine.UI;

namespace theblackarmsSDX {
    public class theblackarmsSDX_GradientScroll : MonoBehaviour
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
