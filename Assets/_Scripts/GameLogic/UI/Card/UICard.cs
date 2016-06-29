using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

public class UICard : BaseUI 
{
	private ModuleCard _module;
	private ModuleCard module
	{
		get
		{
			if(_module == null)
			{
				_module = ModuleMgr.Instance.Get<ModuleCard>();
			}

			return _module;
		}
	}

	private Card_Indi _cardBattle1;

    /// <summary>
    /// 优先级在OnAwake前
    /// </summary>
    protected override void OnInit()
    {
        base.OnInit();
        
        
    }

    protected override void OnAwake()
	{
		
        //调整父物体
        GameObject temp = GameObject.Find("Sprite_Cards");

        gameObject.transform.SetParent(temp.transform, false);

        //拿控件
        _cardBattle1 = transform.Find("SV_Cards/S_Shelf_Fighting/C_Using_Card1").GetComponent<Card_Indi>();


        //设置出战的
  //      ModuleCardItem card = module.GetBattleCard(E_CardPos.Pos1);
		//if(card != null)
		//{
		//	_cardBattle1.SetData(card);
		//}
		//else
		//{
		//	_cardBattle1.gameObject.SetActive(false);
		//}

//		card = module.GetBattleCard(E_CardPos.Pos2);
//		if(card != null)
//		{
//			_cardBattle1.SetData(card);
//		}
//		else
//		{
//			_cardBattle1.gameObject.SetActive(false);
//		}
//
//		card = module.GetBattleCard(E_CardPos.Pos3);
//		if(card != null)
//		{
//			_cardBattle1.SetData(card);
//		}
//		else
//		{
//			_cardBattle1.gameObject.SetActive(false);
//		}

		//设置未出战的
		//List<ModuleCardItem> listUnBattleCard = module.GetUnBattleCard();
		//GameObject goCard = Resources.Load<GameObject>("");
		//Card_Indi cardIndi;
		//for(int i = 0;i < listUnBattleCard.Count;i++)
		//{
		//	//
		//	cardIndi = Instantiate<GameObject>(goCard).GetComponent<Card_Indi>();
		//	cardIndi.SetData(listUnBattleCard[i]);

		//	//cardIndi.transform.SetParent(,false);
		//}

		//goCard = null;


	}


}
