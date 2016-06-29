using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace FightNew
{
	public class PlayerBase : MonoBehaviour 
	{
//		public enum E_AttackStatus
//		{
//			None,
//			Begin,
//			End,
//		}


//		public E_AttackStatus EAttackStatus = E_AttackStatus.None;

		public bool IsNormalAttackPlaying = false;
		public bool IsSkillAttackPlaying = false;

		[SerializeField]
		private Transform TransSkillPos;
//		[SerializeField]
//		private GameObject GoSkillEffect;
		[HideInInspector]
		public bool IsOpp;

		protected StateMachine _fsm = new StateMachine();

		/// <summary>
		/// 角色状态机
		/// </summary>
		/// <value>The FS.</value>
		public StateMachine FSM
		{
			get
			{
				return _fsm;
			}
		}

		private Animator _animPlayer;
		public Animator AnimPlayer
		{
			get
			{
				if(_animPlayer == null)
				{
					_animPlayer = GetComponent<Animator>();
				}

				return _animPlayer; 
			}
		}

		public void Play(string clipName)
		{
			AnimPlayer.CrossFade(clipName,0,0,0);
//			if(AnimPlayer.GetCurrentAnimatorStateInfo(0).IsName(clipName))
//			{
////				Debug.Log("is attack");
//				AnimPlayer.CrossFade(clipName,0,0,0);
//			}
//			else
//			{
////				Debug.Log("is not attack");
//				AnimPlayer.CrossFade(clipName,0.3f);
//			}

//			AnimPlayer.Play(clipName);
//			Debug.Log("play:" + this.name +" anim:" + clipName);
		}

		public virtual void SwithchState(uint stateId,object param1 = null,object param2 = null,bool isForce = false)
		{
			uint oldStateId = 0;
			if(_fsm.GetCurrentState != null)
				oldStateId = _fsm.GetCurrentState.GetStateId();

			//在攻击不播放受击动画
			if(stateId == StateDef.Hurt && oldStateId == StateDef.Attack)
				return;

			//输赢结算后，不播放动画
			if(oldStateId == StateDef.Win || oldStateId == StateDef.Dead)
				return;

//			Debug.Log("播放动画：" + stateId);
			_fsm.SwitchState(stateId,param1,param2,isForce);

//			if(_fsm.SwitchState(stateId,param1,param2))
//			{
//				if(stateId == StateDef.Attack)
//					FightMgr.Instance.EffectCount++;
//			}
		}

		void Awake()
		{
			OnAwake();
		}

		// Update is called once per frame
		private bool _hasAddLastAttack = false;
		void Update () 
		{
			_fsm.OnUpdate();
			OnUpdate();
//			Debug.Log(this.GetType());
			if(FightMgr.Instance.limitationRoutineIsOver && ListSkillData.Count > 0)
			{
				//清除非最后一击的攻击队列
				SkillData skillDataLast = null;
				for(int i = 0;i < ListSkillData.Count;i++)
				{
					if(!ListSkillData[i].IsLastAttack)
					{
						DebugUtil.Info("移除非最后一击的伤害");
						//ListSkillData[i].CurrentHp += ListSkillData[i].Damage;
//						ListSkillData[i].Damage = 0;
//						if(i != 
						ListSkillData.RemoveAt(i);
						i--;
					}
					else
					{
						skillDataLast = ListSkillData[i];
					}
				}

				if(!_hasAddLastAttack)
				{
					if(skillDataLast != null
						&& FightMgr.Instance.GameResult == (int)Result.FIGHT_WIN && this.GetType() == typeof(OppPlayer))
					{
						_hasAddLastAttack = true;
						skillDataLast.Damage = 0;
						ListSkillData.Insert(0,skillDataLast);
					}

					if(skillDataLast != null
						&& FightMgr.Instance.GameResult == (int)Result.FIGHT_LOSE && this.GetType() == typeof(MyPlayer))
					{
						_hasAddLastAttack = true;
						skillDataLast.Damage = 0;
						ListSkillData.Insert(0,skillDataLast);
					}
				}
			}

			if(ListSkillData.Count > 0)
			{
				SkillData skillData = ListSkillData[0];

				if(skillData.SkillType != SkillTable.SkillType.BinFaSkill && skillData.SkillType != SkillTable.SkillType.GongZhuSkill)
				{
					//普通攻击，不打断
					if(!IsNormalAttackPlaying && !IsSkillAttackPlaying)
					{
						IsNormalAttackPlaying = true;
						//等上一攻击结束
//						if(skillData.SkillType == SkillTable.SkillType.GongZhuNormal
//							|| skillData.SkillType == SkillTable.SkillType.GongZhuRandomNormal)
//						{
							DebugUtil.Info("<color=orange>攻击+1</color>" + this.name + ",IsLast:" + skillData.IsLastAttack + ",Hp:" + skillData.CurrentHp + "count:" + ListSkillData.Count + ",damage:" + skillData.Damage );
//						}
						ListSkillData.RemoveAt(0);
						SwithchState(StateDef.Attack,skillData,null,true);

					}
				}
				else if(!IsSkillAttackPlaying)
				{
					if(IsNormalAttackPlaying)
					{
						IsNormalAttackPlaying = false;
						//如果有普功还未释放，叠加伤害，普功移除队列
						SkillData skillDataPre = ((StateBase)_fsm.GetCurrentState).GetSkillData();
						skillData.Damage += skillDataPre.Damage;
					}

//					Debug.Log("type:" + skillData.SkillType);
//					Debug.Break();

					IsSkillAttackPlaying = true;
					//技能攻击
					DebugUtil.Info("<color=orange>技能攻击+1</color>" + this.name + ",IsLast:" + skillData.IsLastAttack + ",Hp:" + skillData.CurrentHp + "count:" + ListSkillData.Count + ",damage:" + skillData.Damage);
					ListSkillData.RemoveAt(0);
					SwithchState(StateDef.Attack,skillData,null,true);
				}
			}
			else if(IsNormalAttackPlaying == false && IsSkillAttackPlaying == false && _fsm.CurrentStateId != StateDef.Hurt)
			{
				//攻击播放完，切idle，如果在hurt，不切换，hurt播完自动会切换
//				EAttackStatus = E_AttackStatus.None;
				SwithchState(StateDef.Idle);
			}
		}

		protected virtual void OnAwake()
		{
		}

		protected virtual void OnUpdate()
		{
		}

		/// <summary>
		/// 角色动画播放结束
		/// </summary>
		/// <param name="clipName">Clip name.</param>
		public virtual void AnimationEventEnd(string clipName)
		{
//			Debug.Log("角色动画播放结束：" + clipName);
			if(_fsm.GetCurrentState != null)
			{
				StateBase state = _fsm.GetCurrentState as StateBase;
				state.AnimationEventEnd(clipName);
			}
			else
			{
				//Debugger.LogError("AnimationEventEnd:currentState is null");
                DebugUtil.Error("AnimationEventEnd:currentState is null");
            }
		}


		public void UseSkill(PlayerBase target,int targetBossId,int currentHp,SkillTable.SkillType skillType,int damage,bool isLaskSkill,int skillId)
		{
//			if(skillId != 0)
//			{
//				Debug.LogError("UseSkill:" + skillId);
//			}

			bool useSkill = true;
			if(damage <= 0)
			{
				SkillModule skill = SkillModuleMgr.Instance.GetSkill(skillId);
				if(skill.IsNoHarmSkill) 
					useSkill = false;
			}
			if(!useSkill)
			{
				//伤害为0，且不需要特效，直接触发技能
				FightMgr.Instance.PlaySkillEffect(skillId,target.IsOpp);
			}
			else
			{
				GameObject projectile = null;
				if(skillId == 1002)
				{
					projectile = FightMgr.Instance.LoadAndInstantiate(SkillTable.GetSkill(SkillTable.SkillType.BinFen));
				}
				else
				{
					projectile = FightMgr.Instance.LoadAndInstantiate(SkillTable.GetSkill(skillType));//Instantiate(GoSkillEffect, TransSkillPos.position, Quaternion.identity) as GameObject;
				}
				projectile.transform.position = TransSkillPos.position;
	//			projectile.transform.LookAt(target.transform.position);
				var vecDiff = target.transform.position - TransSkillPos.position;
				var roatation = Quaternion.FromToRotation(TransSkillPos.up, vecDiff);
				projectile.transform.rotation = roatation;

				var pos = projectile.transform.position;
				pos.z = -0.2f;
				projectile.transform.position = pos;

				projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.up * 700);

				var myProject = projectile.GetComponent<MyProjectileScript>();
				myProject.impactNormal = target.transform.position.normalized;
				myProject.GoTarget = target;

				myProject.CurrentHp = currentHp;
				myProject.TargetBossId = targetBossId;
				myProject.Damage = damage;
				myProject.IsLastSkill = isLaskSkill;
				myProject.SkillId = skillId;
			}


//			Debug.Log("<color=green>useSkill</color> :" + skillType + ",target:" + target.IsOpp);


//			GameObject goSkill = Instantiate<GameObject>(GoSkillEffect);
//			SkillEffect skill = goSkill.GetComponent<SkillEffect>();
//			skill.transform.SetParent(TransSkillPos,false);
//			skill.Target = target;
//			skill.CurrentHp = currentHp;
//			skill.TargetBossId = targetBossId;
		}

		/// <summary>
		/// 所有待播放的攻击动作
		/// </summary>
		protected List<SkillData> ListSkillData = new List<SkillData>();

//		public bool HasAnimation
//		{
//			get
//			{
//				return ListSkillData.Count > 0;
//			}
//		}

		public void AddAttackAction(SkillData skillData)
		{
//			return;

			if(skillData.IsSkill || skillData.IsLastAttack)
			{
				//合并前面的技能
				for(int i = 0;i < ListSkillData.Count;i++)
				{
					if(skillData.BossId == ListSkillData[i].BossId)// && !ListSkillData[i].IsSkill)
					{
//						skillData.CurrentHp = ListSkillData[i].CurrentHp;
//						DebugUtil.Info("合并hp：" + skillData.CurrentHp);

						DebugUtil.Info("合并damage：" + skillData.Damage + "+" + ListSkillData[i].Damage +",IsSkill:" + skillData.IsSkill +",islast:" + skillData.IsLastAttack);
						skillData.Damage += ListSkillData[i].Damage;

						ListSkillData.RemoveAt(i);
						i--;
					}
				}
			}

			ListSkillData.Add(skillData);
		}
	}
}
