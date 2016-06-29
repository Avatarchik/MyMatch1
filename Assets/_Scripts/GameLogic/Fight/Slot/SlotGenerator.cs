/*
 * 
 * 文件名(File Name)：             SlotGenerator
 *
 * 作者(Author)：                  Yangzj
 *
 * 创建时间(CreateTime):           2016/03/31 16:25:24
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace Fight
{
//	public class SlotGeneratorManager : Singleton<SlotGeneratorManager>
//	{
//		private Queue<SlotGenerator> _queue;
//		private List<SlotGenerator> _list;
//		public List<SlotGenerator> List
//		{
//			get
//			{
//				return _list;
//			}
//		}
//
//		public override void Init()
//		{
//			_queue = new Queue<SlotGenerator>();
//			_list = new List<SlotGenerator>();
//		}
//
//		protected override void OnReleaseValue()
//		{
//			_queue.Clear();
//			_list.Clear();
//		}
//
//		protected override void OnAppQuit()
//		{
//			_queue = null;
//			_list = null;
//		}
//
//		public SlotGenerator GetOne(Slot slot)
//		{
//			SlotGenerator slotGenerator = null;
//
//			if(_queue.Count > 0)
//			{
//				slotGenerator = _queue.Dequeue();
//				slotGenerator.Slot = slot;
//			}
//			else
//			{
//				slotGenerator = slot.GetOrAddComponent<SlotGenerator>();
//			}
//
//			return slotGenerator;
//		}
//
//		public void Return(SlotGenerator slotGenerator)
//		{
//			slotGenerator.Reset();
//
//			if(_list.Contains(slotGenerator))
//				_list.Remove(slotGenerator);
//
//			_queue.Enqueue(slotGenerator);
//		}
//
//		public void ReturnAll()
//		{
//			SlotGenerator[] arr = _list.ToArray();
//			for(int i = 0;i < arr.Length;i++)
//			{
//				Return(arr[i]);
//			}
//		}
//	}
		
	public class SlotGenerator : MonoBehaviour
	{
		public void Reset()
		{
			this.Slot.IsGenerator = false;
			this.Slot = null;
			slotForChip = null;
		}

		float lastTime = -10;
		float delay = 0.15f; // delay between the generations

		public Slot Slot{get;set;}
		public SlotForCard slotForChip;

		void Awake ()
		{
			Slot = GetComponent<Slot>();
			Slot.IsGenerator = true;
			slotForChip = GetComponent<SlotForCard>(); 
		}

		public void SetSlot(Slot slot)
		{
			this.Slot = slot;
			this.Slot.IsGenerator = true;
			slotForChip = GetComponent<SlotForCard>(); 
		}

		public bool IsInUse
		{
			get
			{
				return this.Slot != null;
			}
		}

		public void Update ()
		{
			if(this.Slot == null) return;

			if (!SessionControl.Instance.enabled) return;

			if (!SessionControl.Instance.CanIGravity ()) return; // Generation is possible only in case of mode "gravity"

			if (Slot.GetChip()) return; // Generation is impossible, if slot already contains chip

			if (Slot.Block != null) return; // Generation is impossible, if the slot is blocked

			if (lastTime + delay > Time.time) return; // limit of frequency generation

			lastTime = Time.time;

			Vector3 spawnOffset = new Vector3(
				Utils.SideOffsetX(Utils.MirrorSide(Slot.slotGravity.gravityDirection)),
				Utils.SideOffsetY(Utils.MirrorSide(Slot.slotGravity.gravityDirection)),
				0) * 0.4f;

			//			if (LevelProfile.CurrentLevelProfile.target == E_FieldTarget.SugarDrop && SessionAssistant.main.creatingSugarDropsCount > 0) {
			//				if (SugarChip.live_count == 0 || SessionAssistant.main.GetResource() <= 0.4f + 0.6f * SessionAssistant.main.creatingSugarDropsCount / LevelProfile.CurrentLevelProfile.targetSugarDropsCount) {
			//					SessionAssistant.main.creatingSugarDropsCount--;
			//					FieldAssistant.main.GetSugarChip(slot.x, slot.y, transform.position + spawnOffset); // creating new sugar chip
			//					return;
			//				}
			//			}

			if (Random.value > LevelProfile.CurrentLevelProfile.StonePortion)
				FieldAssistant.Instance.GetNewSimpleChip(Slot.Row, Slot.Col, Slot.transform.position + spawnOffset); // creating new chip
			else
				FieldAssistant.Instance.GetNewStone(Slot.Row, Slot.Col, Slot.transform.position + spawnOffset); // creating new stone
		}
	}
}
