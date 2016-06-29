using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace FightNew
{
	public partial class FightMgr
	{
		private bool _isDoSkillToSlot = false;
		/// <summary>
		/// 是否有木块在飞向自己的棋盘
		/// </summary>
		/// <value><c>true</c> if is do skill to slot; otherwise, <c>false</c>.</value>
		public bool IsDoSkillToSlot
		{
			get
			{
				return _isDoSkillToSlot;
			}
		}

		private Queue<FlyingBezier> _poolsBlock = new Queue<FlyingBezier>();
		private Queue<FlyingBezier> _poolsBlockWeed = new Queue<FlyingBezier>();
		private GameObject _goBossOpp = null;
		private GameObject goBossOpp
		{
			get
			{
				if(_goBossOpp == null)
				{
					_goBossOpp = _energyMgr.transform.Find("GoOppBoss").gameObject;
					_goBossOpp.transform.position = FightMgr.Instance.CurrentOppBoss.transform.position;
				}

				return _goBossOpp;
			}
		}

		public void TriggerSkill(int skillId)
		{
			SkillModule skill = SkillModuleMgr.Instance.GetSkill(skillId);
			if(skill == null)
				DebugUtil.Error("未找到skill,id = " + skillId);

			BossData bossData = ModuleFight.CurrentMyBoss;
			//暂时屏蔽
			if(skill.NeedEnergy > bossData.Energy)
			{
				ShowMsg(FightDefine.E_NoteMsgType.NotEnoughEnergy);
				return;
			}

			ModuleFight.UseEnergy(skill.NeedEnergy);
			ModuleFight.SkillAttackBoss(skill);

		}

		/// <summary>
		/// 当前boss的能量
		/// </summary>
		/// <value>The current boss energy.</value>
		public int CurrentBossEnergy
		{
			get
			{
				#if FightTest
				return 9;
				#else
				if(ModuleFight.CurrentMyBoss != null)
				{
					return (int)ModuleFight.CurrentMyBoss.Energy;
				}
				else
				{
					return 9;
				}
				#endif
			}
		}


		/// <summary>
		/// 显示技能面板
		/// </summary>
		/// <param name="isShow">If set to <c>true</c> is show.</param>
		public void ShowSkillPanel(bool isShow)
		{
			energyMgr.ShowSkill(isShow);
		}

		/// <summary>
		/// 播放技能效果
		/// </summary>
		/// <param name="skill">Skill.</param>
		public void PlaySkillEffect(int skillId,bool isMyAttack)
		{
//			isMyAttack = !isMyAttack;

			SkillModule skill = SkillModuleMgr.Instance.GetSkill(skillId);
			if(skill == null)
				DebugUtil.Error("未找到skill,id = " + skillId);

			if(skill.SkillTypeDetail == E_SkillTypeDetail.AddMoves)
			{
				if(isMyAttack)
				{
					//增加自己步数
					int addMoves = int.Parse(skill.EffectNum);
					EventDispatcher.TriggerEvent<int,bool>(UIFight.AddMoves,addMoves,true);
				}
				else
				{
					ShowMsg(FightDefine.E_NoteMsgType.OppUseAddMoves);
				}
			}
			else if(skill.SkillTypeDetail == E_SkillTypeDetail.AddBlocks)
			{
				if(isMyAttack)
				{
					//自己飞向对方
					List<FlyingBezier> list = new List<FlyingBezier>();
					int count = int.Parse(skill.EffectNum);
					for(int i = 0;i < count;i++)
					{
						FlyingBezier obj = null;
						Queue<FlyingBezier> pool = null;
						bool isBlock = true;//Random.Range(1,1000) <= 750;
						if(isBlock)
						{
							//木块
							if(_poolsBlock.Count > 0)
							{
								obj = _poolsBlock.Dequeue();
							}
							else
							{
								obj = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Flying_Block).GetComponent<BlockFlyToOpp>();// ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(FightDefine.ParticleCardCrush);
							}

							pool = _poolsBlock;
						}
//						else
//						{
//							//杂草
//							if(_poolsBlockWeed.Count > 0)
//							{
//								obj = _poolsBlockWeed.Dequeue();
//							}
//							else
//							{
//								obj = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Flying_Weed).GetComponent<BlockFlyToOpp>();// ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(FightDefine.ParticleCardCrush);
//							}
//
//							pool = _poolsBlockWeed;
//						}
						list.Add(obj);

						obj.transform.SetParent(_energyMgr.transform,false);
						obj.transform.localPosition = new Vector3(Random.Range(-46f,46f),Random.Range(-46f,46f),0);
						obj.transform.SetParent(goBossOpp.transform);
						obj.gameObject.SetActive(false);
						obj.Speed = 0.8f;

						(obj as BlockFlyToOpp).Play(i * 0.3f,pool,list,true,null,null,null);
					}
				}
				else
				{
					//对方往我棋盘飞
					_FlyBlockToMyCnt = int.Parse(skill.EffectNum);
				}
			}
			else if(skill.SkillTypeDetail == E_SkillTypeDetail.ForbidAttack)
			{
				if(isMyAttack)
				{
					//不管
				}
				else
				{
					IsIceBlcok = true;
					//获取冰封路径
					var dicSlot = _filedMgr.FindIcePath();

					//停止原来协程
					if(_iceBlock != null)
						StopCoroutine(_iceBlock);

					//开始冰封
					_iceBlock = StartCoroutine(ReIceBlockTimer(dicSlot,float.Parse(skill.EffectNum)));

					//恢复冰封协程
					StartCoroutine(IceSlot(dicSlot));

					ShowMsg(FightDefine.E_NoteMsgType.ShowIceNotice,skill.Effect);
				}
			}
		}

		private Coroutine _iceBlock;


		IEnumerator IceSlot(Dictionary<int, List<Slot>> dicSlot)
		{
			DebugUtil.Info("<color=orange>开始冰封棋盘</color>:" + dicSlot.Count);

			int index = 0;
			while(dicSlot.ContainsKey(index))
			{
				var list = dicSlot[index];
				//Debug.Log("========= index = " + index + " ============");
				for(int i = 0;i < list.Count;i++)
				{
					list[i].PlayIceBlock(true);
				}

				index++;

				yield return new WaitForSeconds(0.2f);
			}
		}

		/// <summary>
		/// 一段时间后解封棋盘
		/// </summary>
		/// <returns>The ice block timer.</returns>
		/// <param name="dealy">Dealy.</param>
		IEnumerator ReIceBlockTimer(Dictionary<int, List<Slot>> dicSlot,float dealy)
		{
			yield return new WaitForSeconds(dealy);

			int index = 0;
			while(dicSlot.ContainsKey(index))
			{
				var list = dicSlot[index];
				//Debug.Log("========= index = " + index + " ============");
				for(int i = 0;i < list.Count;i++)
				{
					list[i].PlayIceBlock(false);
				}

				index++;
			}

			IsIceBlcok = false;
			_iceBlock = null;

			DebugUtil.Info("<color=orange> stop ice block</color>");
		}

		/// <summary>
		/// 是否冻住棋盘
		/// </summary>
		public bool IsIceBlcok = false;

		/// <summary>
		/// 设置障碍的个数
		/// </summary>
		private int _FlyBlockToMyCnt = 0;

		/// <summary>
		/// 这一关是否提示了能量满了 给新手引导
		/// </summary>
		public bool IsTriggerEnergyForTutorial = false;

		private void DoFlyBlockToMyself()
		{
			_isDoSkillToSlot = true;

			var listTarget = _filedMgr.FindFlyBlockTargetSlots(_FlyBlockToMyCnt);

			List<FlyingBezier> list = new List<FlyingBezier>();
			for(int i = 0;i < listTarget.Count;i++)
			{
				FlyingBezier obj = null;
				Queue<FlyingBezier> pool = null;
				bool isBlock = true;//Random.Range(1,1000) <= 750;
				if(isBlock)
				{
					//木块
					if(_poolsBlock.Count > 0)
					{
						obj = _poolsBlock.Dequeue();
					}
					else
					{
						obj = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Flying_Block).GetComponent<BlockFlyToOpp>();// ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(FightDefine.ParticleCardCrush);
					}

					pool = _poolsBlock;
				}

				obj.transform.SetParent(goBossOpp.transform,false);
				obj.transform.localPosition = new Vector3(Random.Range(-46f,46f),Random.Range(-46f,46f),0);
				obj.transform.SetParent(listTarget[i].transform);
				obj.gameObject.SetActive(false);
				obj.Speed = 0.8f;
				(obj as BlockFlyToOpp).Play(i * 0.3f,pool,list,false,
					()=>
						{
							SlotGravity.Reshading();
							_isDoSkillToSlot = false;
							EventCounter();
						},
					listTarget[i],
					(targetSlot)=>
						{
						_filedMgr.CreateOneBlock(targetSlot);
						});
			}

			_FlyBlockToMyCnt = 0;
		}

		private void ClearSkillVal()
		{
			_poolsBlock.Clear();

			_poolsBlockWeed.Clear();

			_FlyBlockToMyCnt = 0;

			_isDoSkillToSlot = false;

			IsIceBlcok = false;

			IsTriggerEnergyForTutorial = false;

		}

		/// <summary>
		/// 表情图片
		/// </summary>
		private static string[] _spriteFace = new string[4]{"fplan-12","fplan-13","fplan-10","fplan-11"};
		private static string[] _txtFace = new string[4]{"来，打我吖！","嘿嘿，厉害吧!","打得不错！","哎呀，失误了！"};
		/// <summary>
		/// 根据表情id获取表情图片
		/// </summary>
		/// <returns>The face sprite name.</returns>
		/// <param name="faceId">Face identifier.</param>
		public static string GetFaceSpriteName(int faceId)
		{
			if(faceId < _spriteFace.Length)
			{
				return _spriteFace[faceId];
			}
			else
			{
				return _spriteFace[0];
			}
		}

		public static string GetFaceTxt(int faceId)
		{
			if(faceId < _txtFace.Length)
			{
				return _txtFace[faceId];
			}
			else
			{
				return _txtFace[0];
			}
		}


		/// <summary>
		/// 播放技能效果
		/// </summary>
		/// <param name="skill">Skill.</param>
		public void PlaySkillEffectTest()
		{
			//			isMyAttack = !isMyAttack;

			int skillId = 1002;
			bool isMyAttack = false;

			IsIceBlcok = true;
			//获取冰封路径
			var dicSlot = _filedMgr.FindIcePath();

			//停止原来协程
			if(_iceBlock != null)
				StopCoroutine(_iceBlock);

			//开始冰封
			_iceBlock = StartCoroutine(ReIceBlockTimer(dicSlot,3f));

			//恢复冰封协程
			StartCoroutine(IceSlot(dicSlot));

			ShowMsg(FightDefine.E_NoteMsgType.ShowIceNotice,3f);
		}

	}
}