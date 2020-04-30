using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip Track;
    [SerializeField] private AudioSource Source;
    // Start is called before the first frame update
    void Start()
    {
        Source.clip = Track;
        Source.loop = true;
        Source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
