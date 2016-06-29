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

namespace Fight
{
	public enum E_CardType
	{
		None,

		//SimpleCard
		SimpleCard1,
		SimpleCard2,
		SimpleCard3,
		SimpleCard4,
		SimpleCard5,
		SimpleCard6,

		//Block
//		Block,
	}


	public class CardManager : Singleton<CardManager>
	{
		private Dictionary<E_CardType,Queue<Card>> _dicCardPool;
//		private Dictionary<E_CardType,Queue<GameObject>> _dicGoPool;

		private List<Card> _listCard;

		public override void Init()
		{
			_listCard = new List<Card>();
			_dicCardPool = new Dictionary<E_CardType, Queue<Card>>();

			//资源加载
//			_dicCard = new Dictionary<string, GameObject>();
//			for(int i = 1;i<=6;i++)
//			{
//				string key = string.Format("Card{0}",i);
//				string path = string.Format("Fight/Card/Card{0}",i);
//				_dicCard.Add(key,Resources.Load<GameObject>(path));
//			}
		}

		#region 获取/归还
		private Card GetOneCard(E_CardType cardType)
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
				GameObject goPrefab = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(cardType.ToString());
				card = goPrefab.GetComponent<Card>();
				card.CardType = cardType;
				card.name = cardType.ToString();
			}

			_listCard.Add(card);

			card.Init();
			return card;
		}

		/// <summary>
		/// 获取一个普通的卡片
		/// </summary>
		/// <returns>The one simple card.</returns>
		/// <param name="cardId">Card identifier.</param>
		public Card GetOneSimpleCard(int cardId)
		{
			E_CardType cardType = (E_CardType)System.Enum.Parse(typeof(E_CardType),string.Format("SimpleCard{0}",cardId+1));
			return GetOneCard(cardType);
		}


		public void ReturnCard(Card card)
		{
			card.Reset();

			_listCard.Remove(card);

			if(!_dicCardPool.ContainsKey(card.CardType))
			{
				_dicCardPool.Add(card.CardType,new Queue<Card>());
			}

			_dicCardPool[card.CardType].Enqueue(card);
		}
		#endregion

		public void DestroyAllCard()
		{
			
			_listCard.Clear();
//			for(int i = 0; i < _listCard.Count;i++)
//			{
//				if(_listCard[i] != null || _listCard[i].gameObject != null)
//					MonoBehaviour.Destroy(_listCard[i].gameObject);
//			}

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
