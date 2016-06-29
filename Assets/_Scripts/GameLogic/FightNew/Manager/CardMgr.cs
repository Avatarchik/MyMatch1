/*
 * 
 * 文件名(File Name)：             CardManager
 *
 * 作者(Author)：                  #AuthorName#
 *
 * 创建时间(CreateTime):           2016/03/29 11:27:37
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace FightNew
{
	public class CardMgr : Singleton<CardMgr>
	{
		private Dictionary<E_CardType,Queue<Card>> _dicCardPool;

		public override void Init()
		{
			_dicCardPool = new Dictionary<E_CardType, Queue<Card>>();
		}

		#region 获取/归还
		public Card GetOneCard(E_CardType cardType)
		{
			Card card = null;

			if(_dicCardPool.ContainsKey(cardType))
			{
				var queue = _dicCardPool[cardType];
				if(queue.Count > 0)
				{
					card = queue.Dequeue();
				}
			}

			if(card == null)
			{
				//生成一个新的
				GameObject goPrefab;
				if(cardType == E_CardType.Stone)
					goPrefab = FightMgr.Instance.LoadAndInstantiate(string.Format(FightDefine.Prefab_Stone));
				else
					goPrefab = FightMgr.Instance.LoadAndInstantiate(string.Format(FightDefine.Format_SimpleCardKey,cardType.ToString()));

				card = goPrefab.GetComponent<Card>();
				card.CardType = cardType;
				card.name = cardType.ToString();
			}

			card.Init();
			return card;
		}


		public void ReturnCard(Card card)
		{
			card.Reset();

			if(!_dicCardPool.ContainsKey(card.CardType))
			{
				_dicCardPool.Add(card.CardType,new Queue<Card>());
			}

			_dicCardPool[card.CardType].Enqueue(card);
		}
		#endregion

		public void DestroyAllCard()
		{

			Card card = null;
			foreach(var queue in _dicCardPool.Values)
			{
				while(queue.Count > 0)
				{
					card = queue.Dequeue();

					if(card != null && card.gameObject != null)
						MonoBehaviour.Destroy(card.gameObject);
				}
			}
		}
	}
}
