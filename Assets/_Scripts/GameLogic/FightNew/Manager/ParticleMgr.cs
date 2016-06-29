/*******************************************************
 * 
 * 文件名(File Name)：             ParticleControl
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/04/28 13:44:21
 *
 *******************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;
using DG.Tweening;

namespace FightNew
{
	public class ParticleMgr : Singleton<ParticleMgr> 
	{
		private Queue<GameObject> _pools = new Queue<GameObject>();
		private Dictionary<int,Texture2D> _dicTexture = new Dictionary<int, Texture2D>();
		public override void Init()
		{
			for(int i = 1;i <= 6;i++)
			{
				#if FightTest
				var asset = Resources.Load<Texture2D>(string.Format("FightNew/Effect/CrushParticle/CrushCard{0}",i));
				_dicTexture.Add(i,asset);
				#else
					_dicTexture.Add(i,ResourceMgr.Instance.GetAsset(string.Format(FightDefine.CrushCardFormat,i)).Asset as Texture2D);
				#endif
			}
		}

		public void AddParticle(Vector3 pos,int cardType)
		{
			GameObject obj = null;

			if(_pools.Count > 0)
			{
				obj = _pools.Dequeue();
			}
			else
			{
				obj = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Particle_CardCrush);// ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(FightDefine.ParticleCardCrush);
			}

			Renderer render = obj.GetComponent<Renderer>();
//			render.sortingLayerName = "UI";
//			render.sortingOrder = 4;
			render.material.mainTexture = _dicTexture[cardType + 1];

			obj.transform.position = pos;
			obj.gameObject.SetActive(true);
			obj.GetComponent<ParticleSystem>().Play();

			FightMgr.Instance.StartCoroutine(Deactive(obj));
//			obj.transform.RunAction(CCSequence.Create(CCDelay.Create(1f),
//				CCCallFunc.Create(
//					()=>{
//					obj.SetActive(false);
//					_pools.Enqueue(obj);
//				})));
		}

		public void Clear()
		{
			if(_pools != null)
				_pools.Clear();
		}

		private IEnumerator Deactive(GameObject obj)
		{
			yield return new WaitForSeconds(1f);

			if(obj == null)
				yield break;
			
			obj.SetActive(false);
			_pools.Enqueue(obj);
		}
	}
}
