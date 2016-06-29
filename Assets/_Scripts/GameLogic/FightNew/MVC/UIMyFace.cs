using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace FightNew
{
	public class UIMyFace : MonoBehaviour
	{
		private UIButton _btnFace;
		private GameObject _goExpand;

		private GameObject _goFaceShow;
		private GameObject _goTalkExpand;
		/// <summary>
		/// 是否正在显示对话
		/// </summary>
		private bool _isShowTalk = false;

		public const string OnHideFaceEvent = "OnHideFaceEvent";
		public const string OnFaceBtnClickEvent = "OnFaceBtnClickEvent";

		private UILabel _labelTalk;

		private void Awake()
		{
			_btnFace = transform.Find("BtnFace").GetComponent<UIButton>();
			_btnFace.onClick.Add(new EventDelegate(OnBtnFaceClick));

			_goExpand = transform.Find("Expand").gameObject;

			_goFaceShow = transform.Find("FaceShow").gameObject;
			_goTalkExpand = transform.Find("FaceShow/TalkExpand").gameObject;

			_labelTalk = transform.Find("FaceShow/TalkExpand/Label").GetComponent<UILabel>();

		}

		void OnEnable()
		{
			EventDispatcher.AddListener<int>(OnFaceBtnClickEvent,OnFaceBtnClick);
			EventDispatcher.AddListener(OnHideFaceEvent,OnHideFace);
			EventDispatcher.AddListener(FightDefine.Event_GameOver,OnWinOrLoseUIShow);
		}

		void OnDisable()
		{
			EventDispatcher.RemoveListener<int>(OnFaceBtnClickEvent,OnFaceBtnClick);
			EventDispatcher.RemoveListener(OnHideFaceEvent,OnHideFace);
			EventDispatcher.RemoveListener(FightDefine.Event_GameOver,OnWinOrLoseUIShow);
		}

		private void OnBtnFaceClick()
		{
			_goExpand.SetActive(!_goExpand.activeSelf);
		}

		private void OnFaceBtnClick(int faceId)
		{
			//DebugUtil.Info("face id:" + faceId);

			//隐藏按钮
			OnHideFace();

			_btnFace.gameObject.SetActive(false);
			_isShowTalk = true;
			//显示
			StartCoroutine(ShowHideFaceTalk(faceId));
			Util.CallMethod("Network", "ClientFacing", faceId);
		}

		private void OnHideFace()
		{
			if(_isShowTalk) return;

			_goExpand.SetActive(false);
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

			//test
//			EventDispatcher.TriggerEvent<int>(UIOppFace.OnShowFaceEvent,faceId);

			yield return new WaitForSeconds(2f);

			_goTalkExpand.SetActive(false);
			_goFaceShow.SetActive(false);
			_btnFace.gameObject.SetActive(true);

			_isShowTalk = false;
		}

		private void OnWinOrLoseUIShow()
		{
			BaseUI winOrLoose = UIMgr.Instance.GetUIByType(E_UIType.UIWinOrLosePanel);
			this.transform.SetParent(winOrLoose.transform);
			this.GetComponent<UIPanel>().depth = winOrLoose.GetComponent<UIPanel>().depth + 1;
		}
	}
}