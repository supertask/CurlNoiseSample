using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public List<OneParticle> ps = new List<OneParticle>();
    public Vector3 additionalVector;
    public float emitterSize = 10f;
    public float convergence = 4f;
    public float viscosity = 5f;
    public Vector2 times;
    private float idleTime = 3f;
    public float timer = 0.0f;

    public class OneParticle {
        public Vector3 emitPos;
        public Vector3 position;
        public Vector4 velocity; //xyz = velocity, w = velocity coef
        public Vector3 life;     // x = time elapsed, y = life time, z = isActive 1 is active, -1 is disactive
        public Vector3 size;     // x = current size, y = start size, z = target size.
        public Vector4 color;
        public Vector4 startColor;
        public Vector4 endColor;
    };

    // Use this for initialization
    void Start () {
        this.ps = new List<OneParticle>();
        for (int i = 0; i < 1000; i++)
        {
            OneParticle particle = new OneParticle();
            if (particle.life.z == -1) {
                particle.life = new Vector3(0.0f, particle.life.y, 1.0f);
                particle.position = particle.emitPos * Mathf.Clamp(times.y * 0.3f, 0.0f, 1.0f);
                particle.size.x = particle.size.y;
                particle.color = particle.startColor;
            }
            this.ps.Add(p);
            GameObject anObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            anObj.transform.position;
        }
    }
	
	// Update is called once per frame
	void Update () {
        this.timer += Time.deltaTime;
        if (this.timer <= this.idleTime) return; // just for idling

        this.times = new Vector2(Time.deltaTime, this.timer);

        for (int i = 0; i < this.ps.Count; i++) {
            OneParticle particle = this.ps[i];
            if (particle.life.z == 1.0) {
                particle.life.x += times.x;
                if (particle.life.x > particle.life.y) {
                    particle.life.z = -1.0f;
                } else {
                    Vector3 velocityXYZ = (Vector3)particle.velocity; //Vector4 to Vector3
                    Vector3 force = CurlNoiseSample.CurlNoise(
                        particle.position * times.x * this.convergence
                    ) - velocityXYZ;
                    velocityXYZ += (force + additionalVector) * times.x *
                            this.viscosity * Mathf.Clamp(times.y * 0.5f, 0.0f, 1.0f);
                    particle.position += velocityXYZ * times.x;

                    particle.size.x = Mathf.Lerp(
                        particle.size.y,
                        particle.size.z,
                        Mathf.Clamp(particle.life.x / particle.life.y, 0.0f, 1.0f)
                    ) * Mathf.Clamp(times.y * 0.1f, 0.0f, 1.0f);

                    particle.color = Vector4.Lerp(
                        particle.startColor, particle.endColor,
                        Mathf.Clamp(particle.life.x / particle.life.y, 0.0f, 1.0f) *
                        Mathf.Clamp(times.y, 0.0f, 1.0f)
                    );
                }
            }
            //代入作業
            
        }
    }

}
