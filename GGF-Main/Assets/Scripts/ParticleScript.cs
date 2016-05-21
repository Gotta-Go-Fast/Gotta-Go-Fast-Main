using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {

    public Player player;

    public ParticleSystem particleDoublejump;
    public ParticleSystem particleSpeedboost;

    private void Awake()
    {
        particleDoublejump = GameObject.Find("ParticleDoublejump").GetComponent<ParticleSystem>();
        particleSpeedboost = GameObject.Find("ParticleSpeedboost").GetComponent<ParticleSystem>();

        particleDoublejump.gameObject.SetActive(false);
        particleSpeedboost.gameObject.SetActive(false);

        player = this.GetComponentInParent<Player>();

        player.FindParticleScript(this);
    }

    public void Doublejump()
    {
        Debug.Log("JumpParticle");
        //particleDoublejump.gameObject.SetActive(true);
    }

    public void Speedboost()
    {
        Debug.Log("BoostParticle");
        //particleSpeedboost.gameObject.SetActive(true);
    }
}
