using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager:MonoBehaviour
{
    static MusicManager instance;
    AudioSource s;
    [SerializeField]
    public TrackList tracks;
    IEnumerator PlayNext;
    IEnumerator ShowName;
    [SerializeField]
    float Seconds_to_show_name_for = 1.5f;
    [SerializeField]
    Text t;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
        Init();
        play(tracks.GetCurrentTrack());
    }

    private void Init()
    {
        if (s == null)
            s = this.GetComponent<AudioSource>();
        tracks.onTrackSet += (t) => play(t);
    }
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            tracks.nextTrack();
        if (Input.GetButtonDown("Fire2"))
            tracks.previousTrack();
    }

    void play(Track track)
    {
        if (track == null)
            Debug.LogError("trying to play null track");
        if (PlayNext != null)
            StopCoroutine(PlayNext);
        if (ShowName != null)
            StopCoroutine(ShowName);
        ShowName = showName(track);
        StartCoroutine(ShowName);
        s.clip = track.audio;
        s.Play();
        PlayNext = playnext(s.clip.length);
        StartCoroutine(PlayNext);
    }
    IEnumerator playnext(float after)
    {
        yield return new WaitForSeconds(after);
        tracks.nextTrack();
    }
    IEnumerator showName(Track track)
    {
        if (t != null)
        {
            t.text = "Song: "+track.name;
            t.enabled = true;
            yield return new WaitForSeconds(Seconds_to_show_name_for);
            t.enabled = false;
        }
    }

}
