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
using MyFrameWork;

namespace Fight
{
	public class TouchControl : Singleton<TouchControl> 
	{
		private GameObject _selectedGoSlot = null;
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

		/// <summary>
		/// 交换委托
		/// </summary>
		public System.Action<Slot, Side> swap = delegate {};

		public override void Init()
		{
			_moveVec = Vector2.zero;
			_checkMoveLength = Screen.height * 0.05f * Screen.height * 0.05f;
//			DebugUtil.Info("screen height:" + Screen.height + " check length:" + _checkMoveLength);
			swap += SlotManager.Instance.Swap;
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
					
				//结束此次检测
				EndMoveCheck();

				hasMoved = true;

				//交换
//				DebugUtil.Info("move side:" + side);
				swap(go.GetComponent<Slot>(), side);
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
			if(_isStartMoving) return;
		
//			DebugUtil.Debug("OnDragStart");

			//开始移动
			_isStartMoving = true;
			_selectedGoSlot = go;
		}

		private void EndMoveCheck()
		{
			_isStartMoving = false;
			_moveVec = Vector2.zero;
		}

		private void OnMyPress(GameObject go,bool state)
		{
			//DebugUtil.Debug("OnMyPress:" + state);

			if(_isStartMoving) return;

			if(state)
			{
				//按下
				_selectedGoSlot = go;
				Card card = go.GetComponent<Slot>().GetChip();
				if(card != null)
				{
					card.SetBorder(true);
				}

				hasMoved = false;
			}
			else if(!hasMoved)
			{
				//没有移动过，释放
				Card card = go.GetComponent<Slot>().GetChip();
				if(card != null)
				{
					card.SetBorder(false);
				}
			}

		}
		#endregion
	}
}
