using System;
using UnityEngine;

public class VoiceChatPlayer : MonoBehaviour {
    float lastTime = 0;
    double played = 0;
    double received = 0;
    int index = 0;
    float[] data;
    float playDelay = 0;
    bool shouldPlay = false;
    float lastRecvTime = 0;
    NSpeex.SpeexDecoder speexDec = new NSpeex.SpeexDecoder(NSpeex.BandMode.Narrow);

    [SerializeField]
    int playbackDelay = 2;

    // Audio source we're going to play the voice chat through
    AudioSource audioSource;

    public float LastRecvTime {
        get { return lastRecvTime; }
    }

    void Start() {
        // Create an indicator that this object is talking


        // Create a new audio source to play audio from
        GameObject soundObj = new GameObject("VoiceChatPlayer");
        soundObj.transform.SetParent(gameObject.transform);
        soundObj.transform.localPosition = Vector3.zero;
        audioSource = soundObj.AddComponent<AudioSource>();

        // Set up some good default values for how far our voice travels
        audioSource.spatialBlend = 1.0f;
        audioSource.dopplerLevel = 0.0f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.maxDistance = 200.0f;


        int size = VoiceChatSettings.Instance.Frequency * 10;
        audioSource.loop = true;
        audioSource.clip = AudioClip.Create("VoiceChat", size, 1, VoiceChatSettings.Instance.Frequency, false);
        data = new float[size];

        if (VoiceChatSettings.Instance.LocalDebug) {
            VoiceChatRecorder.Instance.NewSample += OnNewSample;
        }
    }

    void Update() {
        if (audioSource.isPlaying) {
            // Wrapped around
            if (lastTime > audioSource.time) {
                played += audioSource.clip.length;
            }

            lastTime = audioSource.time;

            // Check if we've played to far
            if (played + audioSource.time >= received) {
                Stop();
                shouldPlay = false;
            }
        } else {
            if (shouldPlay) {
                playDelay -= Time.deltaTime;

                if (playDelay <= 0) {
                    audioSource.Play();
                }
            }
        }
    }

    void Stop() {
        audioSource.Stop();
        audioSource.time = 0;
        index = 0;
        played = 0;
        received = 0;
        lastTime = 0;
    }

    public void OnNewSample(VoiceChatPacket packet) {
        // Store last packet

        // Set last time we got something
        lastRecvTime = Time.time;

        // Decompress
        float[] sample = null;
        int length = VoiceChatUtils.Decompress(speexDec, packet, out sample);

        // Add more time to received
        received += VoiceChatSettings.Instance.SampleTime;

        // Push data to buffer
        if (data == null) {
            return;
        }
        Array.Copy(sample, 0, data, index, length);

        // Increase index
        index += length;

        // Handle wrap-around
        if (index >= audioSource.clip.samples) {
            index = 0;
        }

        // Set data
        audioSource.clip.SetData(data, 0);

        // If we're not playing
        if (!audioSource.isPlaying) {
            // Set that we should be playing
            shouldPlay = true;

            // And if we have no delay set, set it.
            if (playDelay <= 0) {
                playDelay = (float)VoiceChatSettings.Instance.SampleTime * playbackDelay;
            }
        }

        VoiceChatFloatPool.Instance.Return(sample);
    }

    public AudioSource GetAudioSource() {
        return audioSource;
    }
}
