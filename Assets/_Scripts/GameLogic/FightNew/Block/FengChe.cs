using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace FightNew
{
	// Destroyable blocks on playing field
	public class FengChe : BlockInterface 
	{
		public int level = 1; // Level of block. From 1 to 3. Each "BlockCrush"-call fall level by one. If it becomes zero, this block will be destroyed.
		public Color[] spritesColor = new Color[]{new Color(1,1,1),
			new Color(247/255f,94/255f,158/255f),new Color(241/255f,60/255f,37/255f),
			new Color(158/255f,70/255f,22/255f),new Color(44/255f,182/255f,228/255f),
			new Color(166/255f,224/255f,56/255f),new Color(252/255f,218/255f,72/255f)};
		
		private UISprite[] _uiSprite; // Images of blocks of different levels. The size of the array must be equal to 3
		int eventCountBorn;
	    Animation anim;
	    bool destroying = false;

		private TimerBehaviour _timer;

		public void Initialize()
		{
			slot.gravity = false;

			_nodeTrans = transform.Find("Node");

			UISprite sprite1 = transform.Find("Node/Sprite1").GetComponent<UISprite>();
			UISprite sprite2 = transform.Find("Node/Sprite2").GetComponent<UISprite>();
			UISprite sprite3 = transform.Find("Node/Sprite3").GetComponent<UISprite>();
			UISprite sprite4 = transform.Find("Node/Sprite4").GetComponent<UISprite>();
			_uiSprite = new UISprite[4]{sprite1,sprite2,sprite3,sprite4};
			eventCountBorn = FightMgr.Instance.eventCount;
	        anim = GetComponent<Animation>();

			ClearColor();

			_timer = TimerManager.GetTimer(this.gameObject);

//			OnBeginStartTime();
		}

		private Transform _nodeTrans;

		private bool _isActive = false;

		/// <summary>
		/// 经过的时间
		/// </summary>
		private uint _passedTime;

		#region implemented abstract members of BlockInterface
		
		// Crush block funtion
		override public void  BlockCrush (bool force,Transform parent,bool isDirect = false,E_CardType cardType = E_CardType.None) 
		{
	        if (destroying)
	            return;
			if (eventCountBorn == FightMgr.Instance.eventCount && !force) return;
			eventCountBorn = FightMgr.Instance.eventCount;
	//		level --;
			//FieldAssistant.Instance.field.blocks[slot.Row, slot.Col] = level;

//			if (level == 0) 
//			{
//				slot.gravity = true;
////	            slot.SetScore(1);
//	            slot.SetBlock(null);
//	            SlotGravity.Reshading();
//	            StartCoroutine(DestroyingRoutine());
//				return;
//			}

			if (cardType != E_CardType.None && !_isActive && level > 0) 
			{
				int colorIndex = (int)cardType + 1;
				if(spritesColor.Length <= colorIndex)
				{
					//超出范围
					return;
				}

				Color color = spritesColor[colorIndex];
				if(!IsActiveColor(color))
				{
					//改变颜色
					bool isAllColored = ActiveColor(color);
					if(isAllColored)
					{
						//激活风车
						_isActive = true;
						_passedTime = 0;
						StartTimer(10);

						anim["Rotate"].speed = 2f;
						anim.Play("Rotate");
						_timerSpeed = 0f;
						_targetAngle = 0;
					}
					else
					{
						//移动90度
						_targetAngle -= 90;
						_startRotate90 = true;
					}
				}

//				#if !FightTest
//				FightMgr.Instance.AttackBoss();
//				#endif


			}
		}

		private float _timerSpeed = 0f;
		private int _targetAngle = 0;
		private bool _startRotate90 = false;
		void Update()
		{
			if(_isActive)
			{
				_timerSpeed += Time.deltaTime;
				float speed = anim["Rotate"].speed;
				speed = Mathf.Lerp(speed,0.6f,Time.deltaTime);
				anim["Rotate"].speed = speed;

				if(_timerSpeed > 1f)
				{
					_timerSpeed = 0f;
				}
			}
			else if(_startRotate90 && _targetAngle != 0)
			{
				_timerSpeed += Time.deltaTime * 2f;
				float f = Mathf.Lerp(0,_targetAngle,_timerSpeed);
				Vector3 vecTarget = new Vector3(0,0,f);
				_nodeTrans.localEulerAngles = vecTarget;
				if(Mathf.Abs(_targetAngle - f) < 10)
				{
					_nodeTrans.localEulerAngles = new Vector3(0,0,_targetAngle);
					_startRotate90 = false;
					_timerSpeed = 0f;
				}
			}
		}

		public override bool CanBeCrushedByNearSlot () 
		{
			return true;
		}

		#endregion

	    IEnumerator DestroyingRoutine() 
		{
	        destroying = true;

//	        GameObject o = ContentAssistant.main.GetItem(crush_effect);
//	        o.transform.position = transform.position;

	        anim.Play("BlockDestroy");
//	        AudioAssistant.Shot("BlockCrush");
	        while (anim.isPlaying) 
			{
	            yield return 0;
	        }

	        Destroy(gameObject);
	    }

		/// <summary>
		/// 是否激活过该颜色
		/// </summary>
		/// <returns><c>true</c> if this instance is active color the specified color; otherwise, <c>false</c>.</returns>
		/// <param name="color">Color.</param>
		private bool IsActiveColor(Color color)
		{
			for(int i = 0;i < _uiSprite.Length;i++)
			{
				if(_uiSprite[i].color == color)
				{
					return true;
				}
			}

			return false;
		}

		private bool ActiveColor(Color color)
		{
			for(int i = 0;i < _uiSprite.Length;i++)
			{
				if(_uiSprite[i].color == spritesColor[0])
				{
					_uiSprite[i].color = color;
					break;
				}
			}
				
			for(int i = 0;i < _uiSprite.Length;i++)
			{
				if(_uiSprite[i].color == spritesColor[0])
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// 清除颜色
		/// </summary>
		private void ClearColor()
		{
			for(int i = 0;i < _uiSprite.Length;i++)
			{
				_uiSprite[i].color = spritesColor[0];
			}
		}

		private void StartTimer(uint leftSecs)
		{
			_timer.StartTimer(leftSecs,OnEverySecCallBack,OnFinishTimer);
		}

		private void OnEverySecCallBack(uint leftSecs)
		{
			if(!FightMgr.Instance.IsFighting || !_isActive) return;

			_passedTime++;
			if(_passedTime == 3)
			{
				anim["Rotate"].speed = 2f;
				#if !FightTest
				EnergyMgr.Instance.AddEnergySmall(this.slot.transform,E_AddEnergyType.Recovery);
				#endif

				_passedTime = 0;
			}
		}

		private void OnFinishTimer()
		{
			//if(!FightMgr.Instance.IsFighting) return;

			DebugUtil.Info("OnFinishTimer");

			_isActive = false;
			anim.Stop();
			ClearColor();
		}
	}
}