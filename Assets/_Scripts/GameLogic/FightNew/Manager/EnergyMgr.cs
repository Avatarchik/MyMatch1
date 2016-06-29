using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace FightNew
{
	public class EnergyMgr : Singleton<EnergyMgr>
	{
		public int PrizeAnimationCount = 0;

		private Queue<FlyingBezier> _poolsEnergy = new Queue<FlyingBezier>();

		private Queue<FlyingBezier> _poolsPrize = new Queue<FlyingBezier>();
		private Queue<FlyingBezier> _poolsJiangBei = new Queue<FlyingBezier>();
		private Queue<FlyingBezier> _poolsCoin = new Queue<FlyingBezier>();

		public void AddEnergySmall(Transform posParent,E_AddEnergyType addType)
		{
//			if(!FightMgr.Instance.isPlaying) return;

			if(FightMgr.Instance.limitationRoutineIsOver)
				//游戏结束了
				return;
			
			int count = GetEnergyCount(addType);
			float delay = 0f;
			List<FlyingBezier> list = new List<FlyingBezier>();

			for(int i = 0;i < count;i++)
			{
				FlyingBezier obj = null;

				if(_poolsEnergy.Count > 0)
				{
					obj = _poolsEnergy.Dequeue();
				}
				else
				{
					obj = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Particle_EnergySmall).GetComponent<EnergySmall>();// ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(FightDefine.ParticleCardCrush);
				}
				list.Add(obj);

				obj.transform.SetParent(posParent,false);
				obj.transform.localPosition = new Vector3(Random.Range(-26f,26f),Random.Range(-26f,26f),0);
				obj.transform.SetParent(FieldMgr.MyBossPosEffect);
				obj.gameObject.SetActive(false);
				obj.Speed = 0.8f;
				(obj as EnergySmall).PlayEnergySmall(delay,_poolsEnergy,list,addType,OnFinishEnergySmall);
				delay += 0.1f;
			}

//			FightMgr.Instance.StartCoroutine(AddEnergy(addType,list));
		}

		public void AddPrize(int count,Transform posParent,Transform targetPosParent,float totalDealy)
		{
			float delay = 0f;
			List<FlyingBezier> list = new List<FlyingBezier>();

			if(count > 0)
				PrizeAnimationCount++;

			for(int i = 0;i < count;i++)
			{
				FlyingBezier obj = null;

				if(_poolsPrize.Count > 0)
				{
					obj = _poolsPrize.Dequeue();
				}
				else
				{
					obj = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Particle_PrizeFlying).GetComponent<PrizeFlying>();// ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(FightDefine.ParticleCardCrush);
				}


				list.Add(obj);

				obj.transform.SetParent(posParent,false);
				obj.transform.localPosition = new Vector3(Random.Range(-10f,10f),Random.Range(-10f,10f),0);
				obj.transform.SetParent(targetPosParent);
				obj.gameObject.SetActive(false);
				obj.Speed = 0.4f;
				(obj as PrizeFlying).PlayPrize(delay,_poolsPrize,list,OnPrizeAnimationFinish,totalDealy);
				delay += 0.1f;
			}
		}

		public void AddJiangBei(int count,Transform posParent,Transform targetPosParent,float totalDealy)
		{
			float delay = 0.5f;
			List<FlyingBezier> list = new List<FlyingBezier>();

			if(count > 0)
				PrizeAnimationCount++;

			for(int i = 0;i < count;i++)
			{
				FlyingBezier obj = null;

				if(_poolsJiangBei.Count > 0)
				{
					obj = _poolsJiangBei.Dequeue();
				}
				else
				{
					obj = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Particle_JiangBeiFlying).GetComponent<PrizeFlying>();// ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(FightDefine.ParticleCardCrush);
				}
				list.Add(obj);

				obj.transform.SetParent(posParent,false);
				obj.transform.localPosition = new Vector3(Random.Range(-10f,10f),Random.Range(-10f,10f),0);
				obj.transform.SetParent(targetPosParent);
				obj.gameObject.SetActive(false);
				obj.Speed = 0.64f;
				(obj as PrizeFlying).PlayPrize(delay,_poolsJiangBei,list,OnPrizeAnimationFinish,totalDealy);
				delay += 0.1f;
			}
		}

		public void AddCoin(int count,Transform posParent,Transform targetPosParent,float totalDealy)
		{
			float delay = 0.7f;
			List<FlyingBezier> list = new List<FlyingBezier>();

			if(count > 0)
				PrizeAnimationCount++;

			for(int i = 0;i < count;i++)
			{
				FlyingBezier obj = null;

				if(_poolsCoin.Count > 0)
				{
					obj = _poolsCoin.Dequeue();
				}
				else
				{
					obj = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Particle_CoinFlying).GetComponent<PrizeFlying>();// ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(FightDefine.ParticleCardCrush);
				}
				list.Add(obj);

				obj.transform.SetParent(posParent,false);
				obj.transform.localPosition = new Vector3(Random.Range(-10f,10f),Random.Range(-10f,10f),0);
				obj.transform.SetParent(targetPosParent);
				obj.gameObject.SetActive(false);
				obj.Speed = 0.56f;
				(obj as PrizeFlying).PlayPrize(delay,_poolsCoin,list,OnPrizeAnimationFinish,totalDealy);
				delay += 0.1f;
			}
		}

		private void OnPrizeAnimationFinish()
		{
			PrizeAnimationCount--;
		}

		public void Clear()
		{
			if(_poolsEnergy != null)
				_poolsEnergy.Clear();

			if(_poolsPrize != null)
				_poolsPrize.Clear();

			if(_poolsJiangBei != null)
				_poolsJiangBei.Clear();

			if(_poolsCoin != null)
				_poolsCoin.Clear();
		}

		private void OnFinishEnergySmall(E_AddEnergyType addType)
		{
			FightMgr.Instance.AddEnergy(addType);
		}

		private int GetEnergyCount(E_AddEnergyType addType)
		{
			int rtnCount = 0;

			if(addType == E_AddEnergyType.Normal)
			{
				rtnCount = Mathf.CeilToInt(BossData.NormalAddEnergy * 10);
			}
			else if(addType == E_AddEnergyType.Powup)
			{
				rtnCount = Mathf.CeilToInt(BossData.PowupAddEnergy * 10);
			}
			else if(addType == E_AddEnergyType.Stone)
			{
				rtnCount = Mathf.CeilToInt(BossData.EnergyStone * 10);;
			}
			else if(addType == E_AddEnergyType.Recovery)
			{
				rtnCount = Mathf.CeilToInt(BossData.RecoverEnergy * 10);
			}

//			rtnCount *= 1;
			return Mathf.Min(rtnCount,10);

		}
	}
}