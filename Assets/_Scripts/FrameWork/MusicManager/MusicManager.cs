using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace MyFrameWork
{
    public class MusicManager : Manager 
	{
		public static MusicManager Instance
		{
			get
			{
				return AppFacade.Instance.AddManager<MusicManager>(ManagerName.Music);
			}
		}

        private AudioSource _audio;
        private Hashtable sounds = new Hashtable();

        void Awake() {
            _audio = GetComponent<AudioSource>();
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        void Add(string key, AudioClip value) {
            if (sounds[key] != null || value == null) return;
            sounds.Add(key, value);
        }

        /// <summary>
        /// ��ȡһ������
        /// </summary>
        AudioClip Get(string key) {
            if (sounds[key] == null) return null;
            return sounds[key] as AudioClip;
        }

        /// <summary>
        /// ����һ����Ƶ
        /// </summary>
        public AudioClip LoadAudioClip(string path) {
            AudioClip ac = Get(path);
            if (ac == null) {
                ac = (AudioClip)Resources.Load(path, typeof(AudioClip));
                Add(path, ac);
            }
            return ac;
        }

        /// <summary>
        /// �Ƿ񲥷ű������֣�Ĭ����1������
        /// </summary>
        /// <returns></returns>
        public bool CanPlayBackSound() {
            string key = AppConst.AppPrefix + "BackSound";
            int i = PlayerPrefs.GetInt(key, 1);
            return i == 1;
        }

        /// <summary>
        /// ���ű�������
        /// </summary>
        /// <param name="canPlay"></param>
        public void PlayBacksound(string name, bool canPlay) 
		{
			#if StopAudio
			return;
			#endif

            if (_audio.clip != null) {
                if (name.IndexOf(_audio.clip.name) > -1) {
                    if (!canPlay) {
                        _audio.Stop();
                        _audio.clip = null;
                        Util.ClearMemory();
                    }
                    return;
                }
            }
            if (canPlay) {
                _audio.loop = true;
                _audio.clip = LoadAudioClip(name);
                _audio.Play();
            } else {
                _audio.Stop();
                _audio.clip = null;
                Util.ClearMemory();
            }
        }

        /// <summary>
        /// �Ƿ񲥷���Ч,Ĭ����1������
        /// </summary>
        /// <returns></returns>
        public bool CanPlaySoundEffect() {
            string key = AppConst.AppPrefix + "SoundEffect";
            int i = PlayerPrefs.GetInt(key, 1);
            return i == 1;
        }

        /// <summary>
        /// ������Ƶ����
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="position"></param>
        public void Play(AudioClip clip, Vector3 position) {
            if (!CanPlaySoundEffect()) return;
            AudioSource.PlayClipAtPoint(clip, position); 
        }

        public void PlaySoundEff(string path)
        {
			#if StopAudio
			return;
			#endif

            if (!CanPlaySoundEffect()) return;

            AudioClip ac = Get(path);
            if (ac == null)
            {
                ac = (AudioClip)Resources.Load(path, typeof(AudioClip));
                Add(path, ac);  
            }

			if(ac == null)
			{
				DebugUtil.Error("audio is null:" + path);
				return;
			}
	
            AudioSource.PlayClipAtPoint(ac, this.transform.position);
        }
    }
}