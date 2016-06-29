using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace FightNew
{
	public class UIOppFace : MonoBehaviour
	{
		private GameObject _goFaceShow;
		private GameObject _goTalkExpand;

		public const string OnShowFaceEvent = "OnShowFaceEvent";

		private UILabel _labelTalk;

		private void Awake()
		{
			_goFaceShow = transform.Find("FaceShow").gameObject;
			_goTalkExpand = transform.Find("FaceShow/TalkExpand").gameObject;
			_labelTalk = transform.Find("FaceShow/TalkExpand/Label").GetComponent<UILabel>();

		}

		void OnEnable()
		{
			EventDispatcher.AddListener<int>(OnShowFaceEvent,OnShowFace);
		}

		void OnDisable()
		{
			EventDispatcher.RemoveListener<int>(OnShowFaceEvent,OnShowFace);
		}

		private void OnShowFace(int faceId)
		{
			StartCoroutine(ShowHideFaceTalk(faceId));
		}

		//显示并隐藏
		private IEnumerator ShowHideFaceTalk(int faceId)
		{
			_labelTalk.text = FightMgr.GetFaceTxt(faceId);
			float timer = 0;
			_goFaceShow.transform.localScale = Vector3.zero;
			_goFaceShow.GetComponent<UISprite>().spriteName = FightMgr.GetFaceSpriteName(faceId);
			_goFaceShow.SetActive(true);
			while(timer <= 0.3f)
			{
				timer += Time.deltaTime;
				_goFaceShow.transform.localScale = Vector3.Lerp(Vector3.zero,Vector3.one,timer * 3.3f);
				yield return 0;
			}
			_goFaceShow.transform.localScale = Vector3.one;

//			yield return new WaitForSeconds(0.3f);
			_goTalkExpand.SetActive(true);

			yield return new WaitForSeconds(2f);

			_goTalkExpand.SetActive(false);
			_goFaceShow.SetActive(false);
		}
	}
}