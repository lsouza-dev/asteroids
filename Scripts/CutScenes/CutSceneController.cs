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
    int turno = 1;

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
        videoPlayer.clip = clipList[videoIndex];
        videoIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transitionTime = 1;
            StartCoroutine(ContentTransition(videoIndex));
        }
     
        if(transitionTime > 0) transitionTime -= Time.deltaTime;

        if(videoTransition && transitionTime <= 0)
        {
            GameObject imgGO = image.gameObject;
            imgGO.SetActive(false);
            videoTransition = false;
        }else if (Input.GetKeyDown(KeyCode.Space) && playGame)
        {
            SceneManager.LoadScene("Game");
        }
    }

    private IEnumerator ContentTransition(int indexParaTrocaAntesDoTempo)
    {
        
        yield return new WaitForSeconds(.5f);
        GameObject img = image.gameObject;

        if (turno % 2 == 0)
        {
            if (videoIndex <= clipList.Count - 1)
            {
                transitionTime = .5f;
                videoTransition = true;
                videoPlayer.clip = clipList[indexParaTrocaAntesDoTempo];
                videoPlayer.time = 0;
                videoIndex++;
            }
        }
        else
        {
            if (imageIndex <= imagens.Count - 1)
            {
                image.sprite = imagens[imageIndex];
                img.SetActive(true);
                imageIndex++;
            }
        }
        if (videoIndex == clipList.Count && imageIndex == imagens.Count)
        {
            playText.text = "Press Space to play!";
            playGame = true;
        }

        print($"Img Index: {imageIndex} \tImg Count: {imagens.Count}\nVideo Index: {videoIndex} \tVideo Count: {clipList.Count}");
        turno++;
    }
}
    