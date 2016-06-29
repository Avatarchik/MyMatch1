using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace FightNew
{
	public class LineMgr : Singleton<LineMgr> 
	{
		/// <summary>
		/// 每个方向个数
		/// </summary>
		private int _length = 11;

		private Queue<GameObject> _queuePool;

		private Dictionary<Point,GameObject> _dicInUse;
		/// <summary>
		/// 选中的line
		/// </summary>
		private List<IHelpCard> _listLinePoint = new List<IHelpCard>();

		public override void Init()
		{
			_queuePool = new Queue<GameObject>();
			_dicInUse = new Dictionary<Point, GameObject>();
		}

		private void CreateOne(int exploreIndex,Point point,Side side,ref List<Slot> list,bool isMain,Point pointFrom)
		{
			if(!_dicInUse.ContainsKey(point))
			{
				GameObject go = GetOneGo(isMain);
				go.transform.localPosition = new Vector3((point.X - FightMgr.Instance.Width * 0.5f + 0.5f) * 70,(point.Y - FightMgr.Instance.Height * 0.5f + 0.5f) * 70);
				_dicInUse.Add(point,go);

				if(point == pointFrom)
				{
					Slot slot = FightMgr.Instance.FindSlot(point.X,point.Y);
					if(slot != null && !list.Contains(slot))
					{
						if(slot.Card != null)
							_listLinePoint.Add(slot.Card.helpCard);
						
						TotalPowupCount++;
						slot.RemoveIndex = exploreIndex;
						list.Add(slot);

						if(slot.Card != null && slot.Card.IsLineCard)
						{
							slot.Card.helpCard.AddSide(side);

							if(slot.Card.CardType == E_CardType.CrossLine)
							{
								slot.Card.helpCard.AddSide(Utils.RotateSide(side,2));
								slot.Card.helpCard.AddSide(Utils.RotateSide(side,4));
								slot.Card.helpCard.AddSide(Utils.RotateSide(side,6));
							}
						}
					}
				}
				else
				{
					Slot slot = FightMgr.Instance.FindSlot(point.X,point.Y);
					if(slot != null && !list.Contains(slot))
					{
						slot.RemoveIndex = exploreIndex;
						exploreIndex++;
						list.Add(slot);

						if(slot.Block == null && slot.Card != null)
						{
							if(slot.Card.CardType == E_CardType.OneLine)
							{
								_listLinePoint.Add(slot.Card.helpCard);
								TotalPowupCount++;
								CreateLine(exploreIndex,point,E_CardType.OneLine,Utils.RotateSide(side,2),ref list,false);
								CreateLine(exploreIndex,point,E_CardType.OneLine,Utils.RotateSide(side,6),ref list,false);

								if(slot.Card != null && slot.Card.CardType == E_CardType.OneLine)
								{
									slot.Card.helpCard.AddSide(Utils.RotateSide(side,2));
									slot.Card.helpCard.AddSide(Utils.RotateSide(side,6));
								}
							}
							else if(slot.Card.CardType == E_CardType.CrossLine)
							{
								_listLinePoint.Add(slot.Card.helpCard);
								TotalPowupCount++;
//								CreateLine(point,E_CardType.OneLine,s,ref list,false);
								CreateLine(exploreIndex,point,E_CardType.OneLine,Utils.RotateSide(side,2),ref list,false);
								CreateLine(exploreIndex,point,E_CardType.OneLine,Utils.RotateSide(side,4),ref list,false);
								CreateLine(exploreIndex,point,E_CardType.OneLine,Utils.RotateSide(side,6),ref list,false);

								if(slot.Card != null && slot.Card.CardType == E_CardType.CrossLine)
								{
									slot.Card.helpCard.AddSide(side);
									slot.Card.helpCard.AddSide(Utils.RotateSide(side,2));
									slot.Card.helpCard.AddSide(Utils.RotateSide(side,4));
									slot.Card.helpCard.AddSide(Utils.RotateSide(side,6));
								}
							}
							else if(slot.Card.CardType == E_CardType.ThreeLine)
							{
								_listLinePoint.Add(slot.Card.helpCard);
								TotalPowupCount++;
								Side side1 = Utils.RotateSide(side,2);
								Side side2 = Utils.RotateSide(side,6);

								CreateLine(exploreIndex,point,E_CardType.OneLine,side1,ref list,false);
								CreateLine(exploreIndex,point,E_CardType.OneLine,side2,ref list,false);

								Point p1 = new Point(point.X,point.Y);
								Point p2 = new Point(point.X,point.Y);
								if(side == Side.Left || side == Side.Right)
								{
									p1.X -= 1;
									p2.X += 1;
								}
								else
								{
									p1.Y -= 1;
									p2.Y += 1;
								}
									
								CreateLine(exploreIndex,p1,E_CardType.OneLine,side1,ref list,false);
								CreateLine(exploreIndex,p1,E_CardType.OneLine,side2,ref list,false);

								CreateLine(exploreIndex,p2,E_CardType.OneLine,side1,ref list,false);
								CreateLine(exploreIndex,p2,E_CardType.OneLine,side2,ref list,false);

								if(slot.Card != null && slot.Card.CardType == E_CardType.ThreeLine)
								{
									slot.Card.helpCard.AddSide(side1);
									slot.Card.helpCard.AddSide(side2);
								}
							}
						}
					}
				}
			}
			else if(isMain)
			{
				//已使用，判断main，改图片
				GameObject go = _dicInUse[point];
				go.GetComponent<UISprite>().spriteName = isMain ? "wbg-3" : "wbg-4";
			}
		}

		public Card IsCheckTypeOk(Slot slot)
		{
			if(slot == null || slot.Card == null || slot.Block != null)
				return null;

			Card card = slot.Card;
			if(card.CardType != E_CardType.OneLine && card.CardType != E_CardType.CrossLine && card.CardType != E_CardType.ThreeLine)
				return null;

			return card;
		}

		/// <summary>
		/// 累计特殊效果数量
		/// </summary>
		public int TotalPowupCount = 0;

		public void CreateLine(int exploreIndex,Point pointFrom,E_CardType type,Side side,ref List<Slot> list,bool isMain = true)
		{
//			Slot slot = FightMgr.Instance.FindSlot(pointFrom.X,pointFrom.Y);
//			if(slot == null || slot.Card == null || slot.Block != null)
//			{
//				//直接生成一个
//				//createType = type;
//			}
//			else
//			{
//				Card card = slot.Card;
//				if(card.CardType != E_CardType.OneLine && card.CardType != E_CardType.CrossLine && card.CardType != E_CardType.ThreeLine)
//					return;
//			}
			
			int centerX = pointFrom.X;
			int centerY = pointFrom.Y;

			if(type == E_CardType.OneLine)
			{
				//生成一个显示区域
				for(int i = 0;i < _length;i++)
				{
					Point point = new Point(centerX + i * Utils.SideOffsetX(side),centerY + i * Utils.SideOffsetY(side));
					CreateOne(exploreIndex,point,side,ref list,isMain,pointFrom);
					exploreIndex++;
				}
			}
			else if(type == E_CardType.CrossLine)
			{
				Side side2 = Utils.RotateSide(side,2);
				Side side3 = Utils.RotateSide(side,4);
				Side side4 = Utils.RotateSide(side,6);
				for(int i = 0;i < _length;i++)
				{
					Point point = new Point(centerX + i * Utils.SideOffsetX(side),centerY + i * Utils.SideOffsetY(side));
					CreateOne(exploreIndex,point,side,ref list,isMain,pointFrom);

					Point point2 = new Point(centerX + i * Utils.SideOffsetX(side2),centerY + i * Utils.SideOffsetY(side2));
					CreateOne(exploreIndex,point2,side2,ref list,isMain,pointFrom);

					Point point3 = new Point(centerX + i * Utils.SideOffsetX(side3),centerY + i * Utils.SideOffsetY(side3));
					CreateOne(exploreIndex,point3,side3,ref list,isMain,pointFrom);

					Point point4 = new Point(centerX + i * Utils.SideOffsetX(side4),centerY + i * Utils.SideOffsetY(side4));
					CreateOne(exploreIndex,point4,side4,ref list,isMain,pointFrom);

					exploreIndex++;
				}
			}
			else if(type == E_CardType.ThreeLine)
			{
				for(int i = 0;i < _length;i++)
				{
					Point point = new Point(centerX + i * Utils.SideOffsetX(side),centerY + i * Utils.SideOffsetY(side));
					CreateOne(exploreIndex,point,side,ref list,isMain,pointFrom);
					Point p1 = new Point(point.X,point.Y);
					Point p2 = new Point(point.X,point.Y);
					if(side == Side.Left || side == Side.Right)
					{
						p1.Y -= 1;
						p2.Y += 1;
					}
					else
					{
						p1.X -= 1;
						p2.X += 1;
					}
					CreateOne(exploreIndex,p1,side,ref list,isMain,pointFrom);
					CreateOne(exploreIndex,p2,side,ref list,isMain,pointFrom);

					exploreIndex++;
				}
			}
		}


		private GameObject GetOneGo(bool isMain)
		{
			GameObject go;

			if(_queuePool.Count > 0)
			{
				go = _queuePool.Dequeue();
				go.SetActive(true);
			}
			else
			{
				go = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_LineMask);
				go.transform.SetParent(FieldMgr.SlotRoot,false);
			}

			go.GetComponent<UISprite>().spriteName = isMain ? "wbg-3" : "wbg-4";

			return go;
		}

		/// <summary>
		/// 清除显示的底图
		/// </summary>
		public void RemoveLine()
		{
			if(_dicInUse.Count <= 0) return;

			List<GameObject> list = new List<GameObject>(_dicInUse.Values);
			for(int i = 0;i < list.Count;i++)
			{
				_queuePool.Enqueue(list[i]);
				list[i].SetActive(false);
			}

			_dicInUse.Clear();


		}

		/// <summary>
		/// 清除特效块的方向
		/// </summary>
		public void RemvoeFlyOperation()
		{
			for(int i = 0;i < _listLinePoint.Count;i++)
			{
				_listLinePoint[i].ClearSide();
			}
			_listLinePoint.Clear();
		}

		public void Clear()
		{
			if(_dicInUse != null)
				_dicInUse.Clear();

			if(_queuePool != null)
				_queuePool.Clear();
		}


		public void CreateCombineLine(int exploreIndex,Card cardFrom,Card cardTo,Side side,ref List<Slot> list,bool isMain = true)
		{
			Point pointTo = cardTo.Slot.Point;
			Point pointFrom = cardFrom.Slot.Point;

//			int centerFromX = pointFrom.X;
//			int centerFromY = pointFrom.Y;
//			int centerToX = pointTo.X;
//			int centerToY = pointTo.Y;

			//添加from到消除队列
			if(!_dicInUse.ContainsKey(pointFrom))
			{
				GameObject go = GetOneGo(isMain);
				go.transform.localPosition = new Vector3((pointFrom.X - FightMgr.Instance.Width * 0.5f + 0.5f) * 70,(pointFrom.Y - FightMgr.Instance.Height * 0.5f + 0.5f) * 70);
				_dicInUse.Add(pointFrom,go);

				Slot slot = FightMgr.Instance.FindSlot(pointFrom.X,pointFrom.Y);
				if(slot != null && !list.Contains(slot))
				{
					if(slot.Card != null)
						_listLinePoint.Add(slot.Card.helpCard);

					TotalPowupCount++;
					slot.RemoveIndex = exploreIndex;
					list.Add(slot);
				}
			}

			//添加from到消除队列
			if(!_dicInUse.ContainsKey(pointTo))
			{
				GameObject go = GetOneGo(isMain);
				go.transform.localPosition = new Vector3((pointTo.X - FightMgr.Instance.Width * 0.5f + 0.5f) * 70,(pointTo.Y - FightMgr.Instance.Height * 0.5f + 0.5f) * 70);
				_dicInUse.Add(pointTo,go);

				Slot slot = FightMgr.Instance.FindSlot(pointTo.X,pointTo.Y);
				if(slot != null && !list.Contains(slot))
				{
					if(slot.Card != null)
						_listLinePoint.Add(slot.Card.helpCard);

					TotalPowupCount++;
					slot.RemoveIndex = exploreIndex;
					list.Add(slot);
				}
			}

			if(cardFrom.CardType == E_CardType.OneLine && cardTo.CardType == E_CardType.OneLine)
			{
				Side side2 = Utils.RotateSide(side,2);
				Side side4 = Utils.RotateSide(side,4);
				Side side6 = Utils.RotateSide(side,6);

				cardFrom.helpCard.AddSide(side);
				cardFrom.helpCard.AddSide(side2);
				cardFrom.helpCard.AddSide(side4);
				cardFrom.helpCard.AddSide(side6);
				cardTo.helpCard.AddSide(side2);
				cardTo.helpCard.AddSide(side6);

				//4消与4消交换
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side2,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side4,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side6,ref list,isMain);

				CreateLine(exploreIndex+1,pointTo,E_CardType.OneLine,side2,ref list,isMain);
				CreateLine(exploreIndex+1,pointTo,E_CardType.OneLine,side6,ref list,isMain);

			}
			else if(cardFrom.CardType == E_CardType.OneLine && cardTo.CardType == E_CardType.ThreeLine)
			{
				Side side2 = Utils.RotateSide(side,2);
				Side side4 = Utils.RotateSide(side,4);
				Side side6 = Utils.RotateSide(side,6);

				cardFrom.helpCard.AddSide(side2);
				cardFrom.helpCard.AddSide(side6);
				cardTo.helpCard.AddSide(side);
				cardTo.helpCard.AddSide(side4);

				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side2,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side6,ref list,isMain);

				//三线
				CreateLine(exploreIndex+1,pointTo,E_CardType.OneLine,side,ref list,isMain);
				CreateLine(exploreIndex+1,pointTo,E_CardType.OneLine,side4,ref list,isMain);
				Point p1 = new Point(pointTo.X,pointTo.Y);
				Point p2 = new Point(pointTo.X,pointTo.Y);
				if(side == Side.Left || side == Side.Right)
				{
					p1.Y -= 1;
					p2.Y += 1;
				}
				else
				{
					p1.X -= 1;
					p2.X += 1;
				}

				CreateLine(exploreIndex+1,p1,E_CardType.OneLine,side,ref list,isMain);
				CreateLine(exploreIndex+1,p1,E_CardType.OneLine,side4,ref list,isMain);

				CreateLine(exploreIndex+1,p2,E_CardType.OneLine,side,ref list,isMain);
				CreateLine(exploreIndex+1,p2,E_CardType.OneLine,side4,ref list,isMain);
			}
			else if(cardFrom.CardType == E_CardType.ThreeLine && cardTo.CardType == E_CardType.OneLine)
			{
				Side side2 = Utils.RotateSide(side,2);
				Side side4 = Utils.RotateSide(side,4);
				Side side6 = Utils.RotateSide(side,6);

				cardFrom.helpCard.AddSide(side);
				cardFrom.helpCard.AddSide(side4);
				cardTo.helpCard.AddSide(side2);
				cardTo.helpCard.AddSide(side6);

				//三线
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side4,ref list,isMain);
				Point p1 = new Point(pointFrom.X,pointFrom.Y);
				Point p2 = new Point(pointFrom.X,pointFrom.Y);
				if(side == Side.Left || side == Side.Right)
				{
					p1.Y -= 1;
					p2.Y += 1;
				}
				else
				{
					p1.X -= 1;
					p2.X += 1;
				}

				CreateLine(exploreIndex,p1,E_CardType.OneLine,side,ref list,isMain);
				CreateLine(exploreIndex,p1,E_CardType.OneLine,side4,ref list,isMain);

				CreateLine(exploreIndex,p2,E_CardType.OneLine,side,ref list,isMain);
				CreateLine(exploreIndex,p2,E_CardType.OneLine,side4,ref list,isMain);

				//单线
				CreateLine(exploreIndex+1,pointTo,E_CardType.OneLine,side2,ref list,isMain);
				CreateLine(exploreIndex+1,pointTo,E_CardType.OneLine,side6,ref list,isMain);
			}
			else if((cardFrom.CardType == E_CardType.OneLine && cardTo.CardType == E_CardType.CrossLine)
				|| (cardFrom.CardType == E_CardType.CrossLine && cardTo.CardType == E_CardType.OneLine))
			{
				//4消与直角5消交换
				Side side1 = Utils.RotateSide(side,1);
				Side side2 = Utils.RotateSide(side,2);
				Side side3 = Utils.RotateSide(side,3);
				Side side4 = Utils.RotateSide(side,4);
				Side side5 = Utils.RotateSide(side,5);
				Side side6 = Utils.RotateSide(side,6);
				Side side7 = Utils.RotateSide(side,7);

				cardFrom.helpCard.AddSide(side);
				cardFrom.helpCard.AddSide(side1);
				cardFrom.helpCard.AddSide(side2);
				cardFrom.helpCard.AddSide(side3);
				cardFrom.helpCard.AddSide(side4);
				cardFrom.helpCard.AddSide(side5);
				cardFrom.helpCard.AddSide(side6);
				cardFrom.helpCard.AddSide(side7);

				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side1,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side2,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side3,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side4,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side5,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side6,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side7,ref list,isMain);
			}
			else if((cardFrom.CardType == E_CardType.CrossLine && cardTo.CardType == E_CardType.ThreeLine)
				|| (cardFrom.CardType == E_CardType.ThreeLine && cardTo.CardType == E_CardType.CrossLine))
			{
				Side side2 = Utils.RotateSide(side,2);
				Side side4 = Utils.RotateSide(side,4);
				Side side6 = Utils.RotateSide(side,6);

				//以三线为基准点
				Card cardThree = (cardTo.CardType == E_CardType.ThreeLine ? cardTo : cardFrom);
				Point point = (cardTo.CardType == E_CardType.ThreeLine ? pointTo : pointFrom);

				cardThree.helpCard.AddSide(side);
				cardThree.helpCard.AddSide(side2);
				cardThree.helpCard.AddSide(side4);
				cardThree.helpCard.AddSide(side6);

				CreateLine(exploreIndex,point,E_CardType.OneLine,side,ref list,isMain);
				CreateLine(exploreIndex,point,E_CardType.OneLine,side2,ref list,isMain);
				CreateLine(exploreIndex,point,E_CardType.OneLine,side4,ref list,isMain);
				CreateLine(exploreIndex,point,E_CardType.OneLine,side6,ref list,isMain);

				Point p1 = new Point(point.X-1,point.Y + 1);
				Point p2 = new Point(point.X+1,point.Y + 1);
				Point p3 = new Point(point.X-1,point.Y - 1);
				Point p4 = new Point(point.X+1,point.Y - 1);

				CreateLine(exploreIndex,p1,E_CardType.OneLine,side2,ref list,isMain);
				CreateLine(exploreIndex,p1,E_CardType.OneLine,side4,ref list,isMain);
				CreateLine(exploreIndex,p2,E_CardType.OneLine,side4,ref list,isMain);
				CreateLine(exploreIndex,p2,E_CardType.OneLine,side6,ref list,isMain);

				CreateLine(exploreIndex,p3,E_CardType.OneLine,side,ref list,isMain);
				CreateLine(exploreIndex,p3,E_CardType.OneLine,side2,ref list,isMain);
				CreateLine(exploreIndex,p4,E_CardType.OneLine,side,ref list,isMain);
				CreateLine(exploreIndex,p4,E_CardType.OneLine,side6,ref list,isMain);
			}
			else if(cardFrom.CardType == E_CardType.CrossLine && cardTo.CardType == E_CardType.CrossLine)
			{
				//直角5消与直角5消交换
				Side side1 = Utils.RotateSide(side,1);
				Side side2 = Utils.RotateSide(side,2);
				Side side3 = Utils.RotateSide(side,3);
				Side side4 = Utils.RotateSide(side,4);
				Side side5 = Utils.RotateSide(side,5);
				Side side6 = Utils.RotateSide(side,6);
				Side side7 = Utils.RotateSide(side,7);

				cardFrom.helpCard.AddSide(side);
				cardFrom.helpCard.AddSide(side1);
				cardFrom.helpCard.AddSide(side2);
				cardFrom.helpCard.AddSide(side3);
				cardFrom.helpCard.AddSide(side4);
				cardFrom.helpCard.AddSide(side5);
				cardFrom.helpCard.AddSide(side6);
				cardFrom.helpCard.AddSide(side7);

				cardTo.helpCard.AddSide(side2);
				cardTo.helpCard.AddSide(side6);

				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side1,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side2,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side3,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side4,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side5,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side6,ref list,isMain);
				CreateLine(exploreIndex,pointFrom,E_CardType.OneLine,side7,ref list,isMain);

				CreateLine(exploreIndex+1,pointTo,E_CardType.OneLine,side2,ref list,isMain);
				CreateLine(exploreIndex+1,pointTo,E_CardType.OneLine,side6,ref list,isMain);
			}
			else if(cardFrom.CardType == E_CardType.ThreeLine && cardTo.CardType == E_CardType.ThreeLine)
			{
				for(int i = 0;i < 9;i++)
				{
					for(int j = 0;j < 9;j++)
					{
						Point point = new Point(i,j);

						if(!_dicInUse.ContainsKey(point))
						{
							GameObject go = GetOneGo(isMain);
							go.transform.localPosition = new Vector3((point.X - FightMgr.Instance.Width * 0.5f + 0.5f) * 70,(point.Y - FightMgr.Instance.Height * 0.5f + 0.5f) * 70);
							_dicInUse.Add(point,go);

							Slot slot = FightMgr.Instance.FindSlot(point.X,point.Y);
							if(slot != null && !list.Contains(slot))
							{
								if(slot.Card != null)
									_listLinePoint.Add(slot.Card.helpCard);

								slot.RemoveIndex = exploreIndex;
								list.Add(slot);
							}
						}
					}
				}
			}
		} 
	}
}
