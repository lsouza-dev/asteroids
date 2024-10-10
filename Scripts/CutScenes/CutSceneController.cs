using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutSceneController : MonoBehaviour
{
    [Header("List Variables")]
    [SerializeField] List<VideoClip> clipList;
    [SerializeField] List<Sprite> imagens;
    [SerializeField] TMP_Text playText;
    [Header("UI Variables")]
    [SerializeField] Image image;
    [SerializeField] RawImage rawImage;
    VideoPlayer videoPlayer;
    [Header("Numeric Variables")]
    [SerializeField] int videoIndex;
    [SerializeField] int imageIndex;
    [SerializeField] private float transitionTime;

    [Header("Bool Variables")]
    [SerializeField] bool playGame;
    [SerializeField] bool videoTransition;

    
    
    // Start is called before the first frame update

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        //image = GameObject.FindGameObjectWithTag("Canva").GetComponentInChildren<Image>();
    }
    void Start()
    {
        videoPlayer.SetDirectAudioVolume((ushort)videoIndex, .5f);
        videoPlayer.clip = clipList[videoIndex];
        videoIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        if (transitionTime > 0f) transitionTime -= Time.deltaTime;
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transitionTime = 1f;
                StartCoroutine(ContentTransition(videoIndex));
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && playGame)
        {
            SceneManager.LoadScene("Game");
        }
    }

    private IEnumerator ContentTransition(int indexParaTrocaAntesDoTempo)
    {
        
        yield return new WaitForSeconds(1f);
        
            if (videoIndex <= clipList.Count - 1 && transitionTime <= 0)
            {
                transitionTime = .5f;
                videoTransition = true;
                videoPlayer.clip = clipList[indexParaTrocaAntesDoTempo];
                videoPlayer.time = 0;
                videoIndex++;
            }
       
         
       
        if (videoIndex == clipList.Count)
        {
            playText.text = "Press Space to play!";
            playGame = true;
        }
     
    }
}
    