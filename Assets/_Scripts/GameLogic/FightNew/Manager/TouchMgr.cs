/*
 * 
 * 文件名(File Name)：             TouchControl
 *
 * 作者(Author)：                  #AuthorName#
 *
 * 创建时间(CreateTime):           2016/03/29 10:35:35
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace FightNew
{
	public class TouchMgr : Singleton<TouchMgr> 
	{
		#region 操作成功通知回调
		public System.Action<Slot> AfterMoveHandler;
		#endregion

		private GameObject _selectedGoSlot = null;
		private bool _isLineSlot = false;
		/// <summary>
		/// 是否开始移动
		/// </summary>
		private bool _isStartMoving = false;
		/// <summary>
		/// 移动累计
		/// </summary>
		private Vector2 _moveVec;

		/// <summary>
		/// 移动长度
		/// </summary>
		private float _checkMoveLength;

		public delegate bool SwapDele(Slot slot,Side side);
		/// <summary>
		/// 交换委托
		/// </summary>
		public SwapDele swap;

		public override void Init()
		{
			_moveVec = Vector2.zero;
			_checkMoveLength = Screen.height * 0.05f * Screen.height * 0.05f;
//			DebugUtil.Info("screen height:" + Screen.height + " check length:" + _checkMoveLength);
			swap = FightMgr.Instance.Swap;
		}

		public void AddTouchControl(Slot slot)
		{
			GameObject goSlot = slot.gameObject;
			//监听事件
			UIEventListener.Get(goSlot).onDragStart = OnMyDragStart;//开始
			UIEventListener.Get(goSlot).onDrag = OnMyDrag;
//			UIEventListener.Get(goSlot).onDragOver = OnMyDragOver;//覆盖
			UIEventListener.Get(goSlot).onDragEnd = OnMyDragEnd;//结束

//			UIEventListener.Get(goSlot).onClick = OnMyClick;
			UIEventListener.Get(goSlot).onPress = OnMyPress;

		}

		public void RemoveTouchControl(Slot slot)
		{
			GameObject goSlot = slot.gameObject;
			//监听事件
			UIEventListener.Get(goSlot).onDragStart = null;//开始
			UIEventListener.Get(goSlot).onDrag = null;
//			UIEventListener.Get(goSlot).onDragOver = null;//覆盖
			UIEventListener.Get(goSlot).onDragEnd = null;//结束
		}


		#region 移动检测
		private bool _isCombinePowup = false;
		private Card _cardFrom;
		private Card _cardTo;

		private Side _lastSide = Side.Null;

		public void OnMyDrag(GameObject go,Vector2 delta)
		{
			if(!_isStartMoving) return;

			_moveVec += delta;
			float moveX = _moveVec.x * _moveVec.x;
			float moveY = _moveVec.y * _moveVec.y;
			if((moveX + moveY) > _checkMoveLength)
			{
				//有效移动

				//算方向
				Side side;
				if(moveX > moveY)
				{
					//左右
					side = _moveVec.x > 0 ? Side.Right : Side.Left;
				}
				else
				{
					//上下
					side = _moveVec.y > 0 ? Side.Top : Side.Bottom;
				}

				if(_lastSide == side)
					return;
				_lastSide = side;

				Slot s = go.GetComponent<Slot>();

				hasMoved = true;

				if(s.Card != null && _isLineSlot)
				{
					if(FightMgr.Instance.CanIWait() && !FightMgr.Instance.IsDoSkillToSlot)
					{
						//特殊效果拖拽
						if (!FightMgr.Instance.isPlaying || !FightMgr.Instance.IsFighting
							|| !AnimationMgr.Instance.CanTouch() || s.Card.destroying) 
						{
							s.Card.SetBorder(false);
							return;
						}
						else
						{
							if(_listLineSlot == null)
								_listLineSlot = new List<Slot>();
							else
								_listLineSlot.Clear();

							LineMgr.Instance.RemoveLine();
							LineMgr.Instance.RemvoeFlyOperation();

							ClearCombinePowupEffect();

							//特效合并检查
							_isCombinePowup = false;
							Slot slotTo = s[side];

							_cardFrom = LineMgr.Instance.IsCheckTypeOk(s);
							_cardTo = LineMgr.Instance.IsCheckTypeOk(slotTo);

							if(_cardFrom != null && _cardTo != null)
								_isCombinePowup = true;

							if(_isCombinePowup)
							{
								//特效合并
								ChangeCombinePowerupPos();

								slotTo.Card.SetBorder(true);
								s.Card.SetBorder(false);

								LineMgr.Instance.TotalPowupCount = 0;
								LineMgr.Instance.CreateCombineLine(0,_cardFrom,_cardTo,side,ref _listLineSlot);
							}
							else
							{
								//单个特效
								if(_cardFrom != null)
								{
									LineMgr.Instance.TotalPowupCount = 0;
									LineMgr.Instance.CreateLine(0,s.Point,_cardFrom.CardType,side,ref _listLineSlot);
								}
							}
						}
					}
					else
					{
						hasMoved = false;
						_lastSide = Side.Null;
					}
				}
				else
				{
					//结束此次检测
					EndMoveCheck();

					//交换
	//				DebugUtil.Info("move side:" + side);
					if(!FightMgr.Instance.IsDoSkillToSlot && swap != null && !swap(s, side))
					{
						if(s != null && s.Card != null)
							s.Card.SetBorder(false);
					}
				}
			}
			else
			{
				if(hasMoved && _isLineSlot)
				{
					LineMgr.Instance.RemoveLine();
					LineMgr.Instance.RemvoeFlyOperation();
					hasMoved = false;
					_lastSide = Side.Null;

					ChangeCombinePowerupPos();
					ClearCombinePowupEffect();
					if(_cardFrom != null)
					{
						_cardFrom.SetBorder(true);
					}

				}
			}
		}

		private void ChangeCombinePowerupPos()
		{
			if(_cardFrom != null && _cardTo != null)
			{
				Slot slotA = _cardFrom.Slot;
				Slot slotB = _cardTo.Slot;

				slotA.SetChip(_cardTo);
				slotB.SetChip(_cardFrom);
			}
		}

		private void ClearCombinePowupEffect()
		{
			if(_isCombinePowup)
			{
				//上次特效合并取消
				if(_cardFrom != null)
				{
					_cardFrom.SetBorder(false);
				}

				if(_cardTo != null)
					_cardTo.SetBorder(false);
			}
		}

//		public void OnMyDragOver(GameObject go)
//		{
//			
//		}

		public void OnMyDragEnd(object go)
		{
			//移动太短，结束
			EndMoveCheck();
		}

		private bool hasMoved = false;
		public void OnMyDragStart(GameObject go)
		{
			if(_isStartMoving || FightMgr.Instance.IsIceBlcok || FightMgr.Instance.IsDoSkillToSlot) return;
		
//			DebugUtil.Debug("OnDragStart");

			//开始移动
			_isStartMoving = true;
			_selectedGoSlot = go;
		}

		private void EndMoveCheck()
		{
			_isStartMoving = false;
			_moveVec = Vector2.zero;
			_lastSide = Side.Null;
		}

		private List<Slot> _listLineSlot;
		/// <summary>
		/// boss位置是否选中
		/// </summary>
		private bool _isBossPosSel = false;
		private bool _isPress = false;
		private void OnMyPress(GameObject go,bool state)
		{
//			DebugUtil.Debug("OnMyPress:" + state + " go:" + go.name);

			EventDispatcher.TriggerEvent(UIMyFace.OnHideFaceEvent);
			if(_isStartMoving || FightMgr.Instance.IsIceBlcok || FightMgr.Instance.IsDoSkillToSlot) return;

//			if(state)
//			{
//				if(_listLineSlot == null)
//					_listLineSlot = new List<Slot>();
//				else
//					_listLineSlot.Clear();
//				
//				Slot slot = go.GetComponent<Slot>();
//				Card card = LineMgr.Instance.IsCheckTypeOk(slot);
//				if(card != null)
//					LineMgr.Instance.CreateLine(slot.Point,card.CardType,Utils.straightSides[Random.Range(0,Utils.straightSides.Length)],ref _listLineSlot);
//			}
//			else
//			{
//				FightMgr.Instance.RemoveLineSlot(_listLineSlot);
//				LineMgr.Instance.RemoveLine();
//			}

			if(FightMgr.Instance.limitationRoutineIsOver)
				return;

			if(_isPress == state) 
				return;
			else
				_isPress = state;
			
			Slot sSel = go.GetComponent<Slot>();
			if(sSel.IsBoss)
			{
				//选中boss位置
				_isBossPosSel = true;
//				if(!state)
//				{
//					//显示技能选择面板
//					FightMgr.Instance.ShowSkillPanel(true);
//				}
			}
			else
			{
				_isBossPosSel = false;
				if(state)
				{
					_isLineSlot = false;
					//按下
					_selectedGoSlot = go;
							_cardFrom = sSel.GetChip();
					if(_cardFrom != null)
					{
						_cardFrom.SetBorder(true);
						_isLineSlot = _cardFrom.IsLineCard;
					}

					hasMoved = false;
				}
				else if(!hasMoved)
				{
					//抬起
					//没有移动过，释放
					if(_cardFrom != null)
					{
						_cardFrom.SetBorder(false);
					}

					ClearCombinePowupEffect();
					_lastSide = Side.Null;
				}
				else
				{
					if(_isLineSlot)
					{
						if(_isCombinePowup)
						{
							ClearCombinePowupEffect();

//							if(FightMgr.Instance.movesCount > 0)
//							{
								FightMgr.Instance.swapEvent++;
//								FightMgr.Instance.movesCount -= 1;
								FightMgr.Instance.EventCounter ();
								//算普通攻击
								FightMgr.Instance._continueMatchCount += LineMgr.Instance.TotalPowupCount;
								FightMgr.Instance.RemoveLineSlot(_listLineSlot);
								if(AfterMoveHandler != null)
									AfterMoveHandler(go.GetComponent<Slot>());
//							}
//							else
//							{
//								ChangeCombinePowerupPos();
//								FightMgr.Instance.ShowMsg(FightDefine.E_NoteMsgType.NoMoves);
//							}
						}
						else
						{
							if(_cardFrom != null)
								_cardFrom.SetBorder(false);

//							if(FightMgr.Instance.movesCount > 0)
//							{
								FightMgr.Instance.swapEvent++;
//								FightMgr.Instance.movesCount -= 1;
								FightMgr.Instance.EventCounter ();
								//算普通攻击
								FightMgr.Instance._continueMatchCount += LineMgr.Instance.TotalPowupCount;
								FightMgr.Instance.RemoveLineSlot(_listLineSlot);
								if(AfterMoveHandler != null)
									AfterMoveHandler(go.GetComponent<Slot>());
//							}
//							else
//							{
//								FightMgr.Instance.ShowMsg(FightDefine.E_NoteMsgType.NoMoves);
//							}
						}

						LineMgr.Instance.RemoveLine();
					}

					_lastSide = Side.Null;
				}
			}
		}
		#endregion
	}
}
