using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

public class ModuleCard : BaseModule 
{
	public ModuleCard()
	{
		AutoRegister = true;

		_dicCards = new Dictionary<int, ModuleCardItem>();
	}

	private Dictionary<int,ModuleCardItem> _dicCards;

	/// <summary>
	/// 根据id获取一张card
	/// </summary>
	/// <returns>The card item by identifier.</returns>
	/// <param name="cardId">Card identifier.</param>
	public ModuleCardItem GetCardItemById(int cardId)
	{
		ModuleCardItem card = null;
		_dicCards.TryGetValue(cardId,out card);

		return card;
	}


	public void UpdateCard(int cardId,int level,int stat,int cntCard,E_CardPos cardPos)
	{
		ModuleCardItem card = null;
		if(!_dicCards.ContainsKey(cardId))
		{
			card = new ModuleCardItem();
			_dicCards.Add(cardId,card);
		}
		else
		{
			card = _dicCards[cardId];
		}

		//赋值
		card.CardId = cardId;
		card.Level = level;

		card.CardPos = cardPos;
		//todo
	}

	/// <summary>
	/// 获取某个位置的卡牌
	/// </summary>
	/// <returns>The battle card.</returns>
	/// <param name="pos">Position.</param>
	public ModuleCardItem GetBattleCard(E_CardPos pos)
	{
		foreach(var kv in _dicCards)
		{
			if(kv.Value.CardPos == pos)
			{
				return kv.Value;
			}
		}

		return null;
	}

	public List<ModuleCardItem> GetUnBattleCard()
	{
		List<ModuleCardItem> listRtn = new List<ModuleCardItem>();

		foreach(var kv in _dicCards)
		{
			if(kv.Value.CardPos == E_CardPos.None)
			{
				listRtn.Add(kv.Value);
			}
		}

		return listRtn;

	}
}


public enum E_CardColor
{
	Blue,
	Orange,
	Purple,
	Legend,
}

public enum E_CardPos
{
	/// <summary>
	/// 未出战
	/// </summary>
	None,
	Pos1,
	Pos2,
	Pos3,
}

public class ModuleCardItem
{
	public int CardId{get;set;}

	/// <summary>
	/// 等级
	/// </summary>
	/// <value>The level.</value>
	public int Level{get;set;}

	/// <summary>
	/// 阶数
	/// </summary>
	/// <value>The star.</value>
	public int Star{get;set;}

	public E_CardColor CardColor{get;set;}

	/// <summary>
	/// 下一阶升级需要的卡牌数量
	/// </summary>
	/// <value>The next star need card count.</value>
	public int NextStarNeedCardCnt{get;set;}

	/// <summary>
	/// 当前卡牌数量
	/// </summary>
	/// <value>The current card count.</value>
	public int CurrentCardCnt{get;set;}



	public float _cd = 10f;
	/// <summary>
	/// 当前是否可升级
	/// </summary>
	/// <value><c>true</c> if this instance is can level up; otherwise, <c>false</c>.</value>
	public bool IsCanLevelUp
	{
		get
		{
			return _cd <= 0;
		}
	}

	/// <summary>
	/// 当前是否可以进阶
	/// </summary>
	/// <value><c>true</c> if this instance is can star up; otherwise, <c>false</c>.</value>
	public bool IsCanStarUp
	{
		get
		{
			return false;
		}
	}

	public E_CardPos CardPos{get;set;}
}
