 /*
 * 
 * 文件名(File Name)：             FieldAssistant
 *
 * 作者(Author)：                  #AuthorName#
 *
 * 创建时间(CreateTime):           2016/03/16 17:47:45
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace Fight
{
	public class FieldAssistant : DDOLSingleton<FieldAssistant> 
	{
		[HideInInspector]
		public MatchField[] fieldAll;
		[HideInInspector]
		public MatchField field
		{
			set
			{
				if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss1
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss1)
				{
					fieldAll[0] = value;
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss2
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss2)
				{
					fieldAll[1] = value;
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss3
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss3)
				{
					fieldAll[2] = value;
				}
				else
				{
					DebugUtil.Error("get slot fail");
				}
			}
			get
			{
				if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss1
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss1)
				{
					return fieldAll[0];
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss2
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss2)
				{
					return fieldAll[1];
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss3
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss3)
				{
					return fieldAll[2];
				}
				else
				{
					DebugUtil.Error("get slot fail");
					return null;
				}
			}
		}

		public static int Width 
		{
			get 
			{
				if (Instance.field != null)
					return Instance.field.width;
				return 0;
			}
		}

		public static int Height 
		{
			get 
			{
				if (Instance.field != null)
					return Instance.field.height;
				return 0;
			}
		}

		public void StartLevel() 
		{
			_goSlotCreateBoss1 = null;
			_goSlotCreateBoss2 = null;
			_goSlotCreateBoss3 = null;

			StartCoroutine(StartLevelRoutine());
		}

		IEnumerator StartLevelRoutine() 
		{
//			UIAssistant.main.ShowPage("Loading");

//			while (CPanel.uiAnimation > 0)
//				yield return 0;

//			ProfileAssistant.main.local_profile["live"]--;

			SessionControl.Instance.enabled = false;
//
			SessionControl.Instance.Reset();

			yield return StartCoroutine(CreateField());

			yield return StartCoroutine(PreviewScreen());

			SessionControl.Instance.enabled = true;
			SessionControl.Instance.eventCount++;

			SessionControl.Instance.StartSession();

			//todo camera
//			GameCamera.main.transform.position = new Vector3(0, 20, -10);

//			SlotManager.Instance.InitlizeSlot();
//
//			SessionControl.Instance.isPlaying = true;
//			SessionControl.Instance.movesCount = 20;
//			SessionControl.Instance.timeLeft = 100;
//
//			AnimationControl.Instance.Iteraction = true;

			yield return 0;

			#if UNITY_EDITOR
			if(!GameEntrance.Instance.IsTestFight)
			{
				if(_goSlotCreateBoss3 != null)
					_goSlotCreateBoss3.SetActive(false);

				if(_goSlotCreateBoss2 != null)
					_goSlotCreateBoss2.SetActive(false);
			}
			#else
				if(_goSlotCreateBoss3 != null)
				_goSlotCreateBoss3.SetActive(false);

				if(_goSlotCreateBoss2 != null)
				_goSlotCreateBoss2.SetActive(false);
			#endif

			EventDispatcher.TriggerEvent(UIFight.BeginStartTime);

		}

		public IEnumerator PreviewScreen()
		{
			if(FightControl.Instance.EFightStatus == E_FightStatus.PreviewScene)
			{
				GameObject goSlotAll = GameObject.Find("SlotAll");

				TweenCtrl._instance.ShowEffect(2);
				yield return new WaitForSeconds(0.3f);
				EventDispatcher.TriggerEvent<int>(UIFight.BossCreateEvent,2);
				yield return new WaitForSeconds(1f);

				yield return StartCoroutine(MoveSlot(goSlotAll,_vecSlot2 * -1));
				goSlotAll.transform.localPosition = _vecSlot2 * -1;
				TweenCtrl._instance.ShowEffect(1);
				yield return new WaitForSeconds(0.3f);
				EventDispatcher.TriggerEvent<int>(UIFight.BossCreateEvent,1);
				yield return new WaitForSeconds(1f);


//				_goSlotCreateBoss3.SetActive(false);
				yield return StartCoroutine(MoveSlot(goSlotAll,_vecSlot1 * -1));
				goSlotAll.transform.localPosition = _vecSlot1 * -1;
				TweenCtrl._instance.ShowEffect(0);
				yield return new WaitForSeconds(0.3f);
				EventDispatcher.TriggerEvent<int>(UIFight.BossCreateEvent,0);
				yield return new WaitForSeconds(1f);

//				_goSlotCreateBoss2.SetActive(false);

				FightControl.Instance.EFightStatus = E_FightStatus.FightBoss1;

//				yield return new WaitForSeconds(3f);
				UIMgr.Instance.DestroyUI(E_UIType.UIMainPanel);

				MusicManager.Instance.PlaySoundEff("Music/readyGo");
			}
		}

		public IEnumerator MoveSlot(GameObject goSlotAll,Vector3 targetPos)
		{
			float swapDuration = 0.6f;
			float time = 0f;
			float progress = 0f;
			while (progress < swapDuration) 
			{
				time = EasingFunctions.easeInOutQuad(progress / swapDuration);
				goSlotAll.transform.localPosition = Vector3.Lerp(goSlotAll.transform.localPosition, targetPos, time);

				progress += Time.deltaTime;

				yield return 0;
			}


		}

		// Field generator
		public IEnumerator CreateField ()
		{
			//Utils.waitingStatus = "Level loading";
			DebugUtil.Info("Level loading");

			//删除原先布局，稍后用对象池
			SlotManager.Instance.DestroyAllSlot();

			if(GameEntrance.Instance.IsTestFight)
			{
				FightControl.Instance.EFightStatus = E_FightStatus.LoadingBoss3;

				field = new MatchField (LevelProfile.CurrentLevelProfile.Width, LevelProfile.CurrentLevelProfile.Height);
				field.chipCount = LevelProfile.CurrentLevelProfile.CardCount;

				yield return StartCoroutine(GenerateSlots());
				_goSlotCreateBoss3.SetActive(true);

				yield return StartCoroutine(GenerateBlocks());

				SlotManager.Instance.Initialize();

				yield return StartCoroutine(GenerateWalls());

				SlotGravity.Reshading();

				//			yield return StartCoroutine(GenerateJelly());
				yield return StartCoroutine(GenerateChips());
				yield return StartCoroutine(GeneratePowerups());

				UIMgr.Instance.DestroyUI(E_UIType.UIMainPanel);

				MusicManager.Instance.PlaySoundEff("Music/readyGo");



				FightControl.Instance.EFightStatus = E_FightStatus.FightBoss3;
			}
			else
			{
				for(int i = 0;i < FightControl.Instance.ModuleFight.LevelId.Length;i++)
				{
					if(i == 0)
					{
						FightControl.Instance.EFightStatus = E_FightStatus.LoadingBoss1;
					}
					else if(i == 1)
					{
						FightControl.Instance.EFightStatus = E_FightStatus.LoadingBoss2;
					}
					else
					{
						FightControl.Instance.EFightStatus = E_FightStatus.LoadingBoss3;
					}

					field = new MatchField (LevelProfile.CurrentLevelProfile.Width, LevelProfile.CurrentLevelProfile.Height);
					field.chipCount = LevelProfile.CurrentLevelProfile.CardCount;

					yield return StartCoroutine(GenerateSlots());
					yield return StartCoroutine(GenerateBlocks());

					SlotManager.Instance.Initialize();

					yield return StartCoroutine(GenerateWalls());

					SlotGravity.Reshading();

		//			yield return StartCoroutine(GenerateJelly());
					yield return StartCoroutine(GenerateChips());
					yield return StartCoroutine(GeneratePowerups());
				}

				FightControl.Instance.EFightStatus = E_FightStatus.PreviewScene;
			}
				

//			SUBoosterButton.Generate(Slot.folder);
		}
			

		#region 卡槽
		private GameObject _goSlotCreateBoss1;
		private GameObject _goSlotCreateBoss2;
		private GameObject _goSlotCreateBoss3;

		private Vector3 _vecSlot1 = Vector3.right * (640f * Screen.width / Screen.height * 16 / 9 * -2);
		private Vector3 _vecSlot2 = Vector3.right * (640f * Screen.width / Screen.height * 16 / 9 * -1);

		public GameObject _goSlotCreate
		{
			set
			{
				if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss1 
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss1)
				{
					_goSlotCreateBoss1 = value;
					_goSlotCreateBoss1.transform.localPosition = _vecSlot1;
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss2
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss2)
				{
					_goSlotCreateBoss2 = value;
					_goSlotCreateBoss2.transform.localPosition = _vecSlot2;
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss3
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss3)
				{
					_goSlotCreateBoss3 = value;
					_goSlotCreateBoss3.transform.localPosition = Vector3.zero;
				}
				else
				{
					DebugUtil.Error("状态错误");
				}
			}
			get
			{
				if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss1 
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss1)
				{
					return _goSlotCreateBoss1;
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss2
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss2)
				{
					return _goSlotCreateBoss2;
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss3
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss3)
				{
					return _goSlotCreateBoss3;
				}
				else
				{
					DebugUtil.Error("状态错误");
					return null;
				}
			}
		}
		// Generation slots for chips
		IEnumerator GenerateSlots() 
		{
//			Utils.waitingStatus = "Slots building";
			DebugUtil.Info("Slots loading");
//			SlotManager.Instance.CreateSlotGameObject();

//			GameObject o;

			Slot s;
			SlotGenerator sGenerator;
			SlotTeleport sTeleport;
//			SlotGravity sGravity;
//			Vector3 position;


			while(_goSlotCreate == null)
			{
				yield return new WaitForSeconds(0.3f);
				string slotKey = string.Empty;
				if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss1)
				{
					slotKey = "Slot1";
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss2)
				{
					slotKey = "Slot2";
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss3)
				{
					slotKey = "Slot3";
				}
				else
				{
					continue;
				}

				_goSlotCreate = GameObject.Find(slotKey);
			}

			for (int x = 0; x < field.width; x++) 
			{
				yield return 0;//加快速度
				for (int y = 0; y < field.height; y++) 
				{
					field.slots[x, y] = LevelProfile.CurrentLevelProfile.GetSlot(x, Height - y - 1);
					field.generator[x, y] = LevelProfile.CurrentLevelProfile.GetGenerator(x, Height - y - 1);
//					field.sugarDrop[x, y] = LevelProfile.CurrentLevelProfile.GetSugarDrop(x, Height - y - 1);
					field.teleport[x, y] = LevelProfile.CurrentLevelProfile.GetTeleport(x, Height - y - 1);

					if (field.slots[x, y]) 
					{
//						position = new Vector3();
//						position.x = -_slotoffset * (0.5f * (field.width - 1) - x);
//						position.y = -_slotoffset * (0.5f * (field.height - 1) - y);
////						o = ContentAssistant.main.GetItem("SlotEmpty", position);
//						o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(Slot.SlotEmptyPrefabName);
//						o.transform.position = position;
//						o.name = string.Format(Slot.SlotGoNameFormat,x,y);
//						s = new Slot(x,y,o);
						s = SlotManager.Instance.CreateSlot(x,y);
	
						s.transform.SetParent(_goSlotCreate.transform,false);

						if (field.generator[x, y])
						{
							//s.owner.AddComponent<SlotGenerator>();
							sGenerator = s.GetOrAddComponent<SlotGenerator>();
							sGenerator.SetSlot(s);//SlotGeneratorManager.Instance.GetOne(s);// new SlotGenerator(x,y,o);
						}

						if (field.teleport[x, y] > 0) 
						{
//							DebugUtil.Log("Add teleport");
							sTeleport = s.GetOrAddComponent<SlotTeleport>();// SlotTeleportManager.Instance.GetOne(s);// new SlotTeleport(x,y,o);
							sTeleport.TargetID = field.teleport[x, y];
//							s.slotTeleport.targetID = field.teleport[x, y];
						}
							
						int gravity = LevelProfile.CurrentLevelProfile.GetGravity(x, Height - y - 1);
						switch (gravity) 
						{
							case 0:
								s.slotGravity.gravityDirection = Side.Bottom;
								break;
							case 1:
								s.slotGravity.gravityDirection = Side.Left;
								break;
							case 2:
								s.slotGravity.gravityDirection = Side.Top;
								break;
							case 3:
								s.slotGravity.gravityDirection = Side.Right;
								break;
						}

//						if (sd) {
//							switch (gravity) {
//								case 1:
//									sd.transform.Rotate(0, 0, -90);
//									break;
//								case 2:
//									sd.transform.Rotate(0, 0, 180);
//									break;
//								case 3:
//									sd.transform.Rotate(0, 0, 90);
//									break;
//							}
//						}
					}
				}
			}		
		}
		#endregion

		#region wall
		// Generation impassable walls
		IEnumerator GenerateWalls() {
//			Utils.waitingStatus = "Walls building";
			DebugUtil.Info("Walls loading");

			int x;
			int y;
			Slot near;
			Slot current;

			for (x = 0; x < field.width-1; x++)		
				for (y = 0; y < field.height; y++) 
				{
					field.wallsV[x,y] = LevelProfile.CurrentLevelProfile.GetWallV(x,Height-y-1);
					if (field.wallsV[x,y] && field.slots[x,y]) 
					{
//						Debug.LogError("has wall");

						current = SlotManager.Instance.FindSlot(x, y);
						if (current != null) 
						{
							current.SetWall(Side.Right);
							near = current[Side.Right];

							if (near != null)
								near.SetWall(Side.Left);
						}
					}
				}

			for (x = 0; x < field.width; x++)	
				for (y = 0; y < field.height-1; y++) 
				{
					field.wallsH[x,y] = LevelProfile.CurrentLevelProfile.GetWallH(x, Height-y-2);
					if (field.wallsH[x,y] && field.slots[x,y]) 
					{
//						Debug.LogError("has wall");

						current = SlotManager.Instance.FindSlot(x, y);
						if (current != null) 
						{
							current.SetWall(Side.Top);
							near = current[Side.Top];
						
							if (near != null)
								near.SetWall(Side.Bottom);
						}
					}
				}

			yield return 0;

			List<Pair> walls = new List<Pair>();
			Pair pair;
			Vector3 position;
			GameObject wall;
			foreach (Slot slot in SlotManager.Instance.DicSlotNow.Values) 
			{
//				yield return 0;

				foreach (Side side in Utils.straightSides) 
				{
					if (slot[side] != null)
						continue;
					pair = new Pair(slot.key, (slot.Row + Utils.SideOffsetX(side)) + "_" + (slot.Col + Utils.SideOffsetY(side)));
					if (walls.Contains(pair))
						continue;


//					Debug.LogError("has wall:" + slot.Key + " side:" + side);
					position = new Vector3();
					position.x = Utils.SideOffsetX(side) * 35 + 0.353f;
					position.y = Utils.SideOffsetY(side) * 35 + 0.353f;

					wall = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(Slot.WallPrefabName);//ContentAssistant.main.GetItem("Wall", position);
//					wall.transform.position = position;
					//wall.transform.parent = slot.transform;
					wall.transform.SetParent(slot.transform,false);
					wall.transform.localPosition = position;
					wall.name = "Wall_" + side;
					if (Utils.SideOffsetY(side) != 0)
						wall.transform.Rotate(0, 0, 90);

					walls.Add(pair);

				}
			}

			yield return 0;
			DebugUtil.Info("Walls loading end");
		}
		#endregion

		#region 生成糖果布局
		// Generation chips
		IEnumerator GenerateChips ()
		{

//			Utils.waitingStatus = "Gathering berries";

			DebugUtil.Info("Gathering Cards");

			int x;
			int y;

			for (x = 0; x < field.width; x++)				
				for (y = 0; y < field.height; y++)
					field.chips[x,y] = LevelProfile.CurrentLevelProfile.GetChip(x,Height-y-1);

			field.FirstChipGeneration();
			yield return 0;

			int id;

			for (x = 0; x < field.width; x++) 
			{
				yield return 0;
				for (y = 0; y < field.height; y++) 
				{
					if (field.blocks[x, y] == 4)
						continue;
					id = field.GetChip(x, y);
					if (id >= 0 && id != 9 && (field.blocks[x,y] == 0 || field.blocks[x,y] == 5)) 
						GetNewSimpleChip(x, y, new Vector3(0, 4, 0), id);
					if (id == 9 && (field.blocks[x,y] == 0))
						GetNewStone(x, y, new Vector3(0, 4, 0));
				}
			}			
		}

		/// <summary>
		/// 随机出一个木块
		/// </summary>
		public void CreateOneBlock()
		{
			while(true)
			{
				int x = Random.Range(0,field.width);
				int y = Random.Range(0,field.height);

				if(field.GetSlot(x,y))
				{
					int id = field.GetChip(x, y);
					if (id >= 0 && id != 9 && (field.blocks[x,y] == 0 || field.blocks[x,y] == 5))
					{
						Slot s = SlotManager.Instance.FindSlot(x, y);
						if(s != null && s.GetChip() && !s.Block && !s.IsGenerator)
						{
							s.GetChip().HideChip(false);

							field.blocks[x,y] = 1;

							GameObject o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(Block.prefabName);
							o.name = "Block_" + x + "x" + y;
							//								o.transform.parent = s.transform;
							o.transform.SetParent(s.transform,false);
							o.transform.position = s.transform.position;
							Block b = o.GetComponent<Block>();
							s.SetBlock(b);
							b.slot = s;
							b.level = field.blocks[x,y];
							b.Initialize();
							break;
						}
					}
				}
			}
		}

		/// <summary>
		/// 随机出一排木块
		/// </summary>
		public void CreateRowBlock()
		{
			while(true)
			{
				int x = Random.Range(0,field.width);
				int y = Random.Range(0,field.height);

				if(field.GetSlot(x,y))
				{
					int blockCount = 3;
					for(int i = 0; i < field.width && blockCount > 0;i++)
					{
						x = i;

						Slot s = SlotManager.Instance.FindSlot(x, y);
						if(s != null && s.GetChip() && !s.Block && !s.IsGenerator)
						{
							s.GetChip().HideChip(false);

							field.blocks[x,y] = 1;

							GameObject o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(Block.prefabName);
							o.name = "Block_" + x + "x" + y;
							//								o.transform.parent = s.transform;
							o.transform.SetParent(s.transform,false);
							o.transform.position = s.transform.position;
							Block b = o.GetComponent<Block>();
							s.SetBlock(b);
							b.slot = s;
							b.level = field.blocks[x,y];
							b.Initialize();

							blockCount--;
						}
					}

					break;
				}
			}
		}

		/// <summary>
		/// 生成两个无法消除木块
		/// </summary>
		public void CreateNotDestroyableBlcok()
		{
			int createCount = 2;

			while(createCount > 0)
			{
				int x = Random.Range(0,field.width);
				int y = Random.Range(0,field.height);

				if(field.GetSlot(x,y))
				{
					Slot s = SlotManager.Instance.FindSlot(x, y);
					if(s == null || s.IsGenerator)
					{
						continue;
					}

					int blockId = field.blocks[x,y];
					if(blockId >= 6 && blockId <= 10)
						//boss
						continue;

					field.blocks[x,y] = 10;

					if(s.GetChip())
					{
						s.GetChip().HideChip(false);
					}
//					else if(s.Block)
//					{
//						s.SetBlock(null);
//						s.gameObject.SetActive(false);
//					}

					s = SlotManager.Instance.FindSlot(x, y);
					GameObject o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(FightDefine.SpecialBlock);
					o.transform.position = s.transform.position;
					o.name = "SpecialBlock_" + x + "x" + y;
					//								o.transform.parent = s.transform;
					o.transform.SetParent(s.transform,false);
					o.transform.position = s.transform.position;
					SpecialBlock boss = o.GetComponent<SpecialBlock>();
					s.SetBlock(boss);
					boss.slot = s;
					boss.Initialize();

					s.gameObject.SetActive(false);

					createCount--;
				}
			}
		}

		// Creating a simple random color chips
		public Card GetNewSimpleChip (int x, int y, Vector3 position){
			return GetNewSimpleChip(x, y, position, SessionControl.Instance.colorMask[Random.Range(0, field.chipCount)]);
		}

		// Creating a simple chip specified color
		public Card GetNewSimpleChip (int x, int y, Vector3 position, int id) 
		{
			//ContentAssistant.main.GetItem("SimpleChip" + Chip.chipTypes[id]);
//			GameObject o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload("SimpleCard" + Card.chipTypes[id]);
//			o.transform.position = position;
//			o.name = "Card_" + Card.chipTypes[id];
//
//			DebugUtil.Debug(FightControl.Instance.EFightStatus + ",{" + x + "x" + y + "}");
			Slot slot = SlotManager.Instance.FindSlot(x, y);
//			Card chipForSlot = slot.Card;
//			if (chipForSlot != null)
//				o.transform.position = chipForSlot.transform.position;
//			
//			Chip chip = o.GetComponent<Chip> ();
//
//
			Card card = CardManager.Instance.GetOneSimpleCard(id);
			slot.SetChip(card);
			//card.transform.SetParent(slot.transform,false);

//			DebugUtil.Debug("aa");
			return card;
		}

		public Card GetNewStone (int x, int y, Vector3 position) 
		{
			//ContentAssistant.main.GetItem ("Stone");
//			GameObject o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload("Stone");
//			o.transform.position = position;
//			o.name = "Stone";
//
			Slot slot = SlotManager.Instance.FindSlot(x, y);
//			Chip chipForSlot = slot.GetChip();
//			if (chipForSlot != null)
//				o.transform.position = chipForSlot.transform.position;
//			
//			Chip chip = o.GetComponent<Chip> ();
//			slot.SetChip(chip);
//			return chip;

			Card card = CardManager.Instance.GetOneSimpleCard(-1);
			slot.SetChip(card);
			card.transform.position = position;
//			card.transform.SetParent(slot.transform);
			return card;
		}
		#endregion

		#region 障碍物
		IEnumerator GenerateBlocks() 
		{
//			Utils.waitingStatus = "Block building";
			GameObject o;
			Slot s;
			Block b;
			Boss boss;
			Weed w;
			Branch brch;


			float bossX1 = 0;
			float bossX2 = 0;
			float bossY1 = 0;
			float bossY2 = 0;

			int blockVal = 0;
			for (int x = 0; x < field.width; x++)					
				for (int y = 0; y < field.height; y++) 
				{
					field.blocks[x,y] = LevelProfile.CurrentLevelProfile.GetBlock(x,Height-y-1);
					if (field.slots[x,y]) 
					{
						blockVal = field.blocks[x,y];
						if (blockVal > 0) 
						{
							if (blockVal <= 3) 
							{
								s = SlotManager.Instance.FindSlot(x, y);
								o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(Block.prefabName);
								o.name = "Block_" + x + "x" + y;
//								o.transform.parent = s.transform;
								o.transform.SetParent(s.transform,false);
								o.transform.position = s.transform.position;
								b = o.GetComponent<Block>();
								s.SetBlock(b);
								b.slot = s;
								b.level = field.blocks[x,y];
								b.Initialize();
							}
							if (blockVal == 4) {
								s = SlotManager.Instance.FindSlot(x, y);
								o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(Weed.prefabName);
								o.transform.position = s.transform.position;
								o.name = "Weed_" + x + "x" + y;
//								o.transform.parent = s.transform;
								o.transform.SetParent(s.transform,false);
								o.transform.position = s.transform.position;
								w = o.GetComponent<Weed>();
								s.SetBlock(w);
								w.slot = s;
								w.Initialize();
							}
							if (blockVal == 5) {
								s = SlotManager.Instance.FindSlot(x, y);
								o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(Branch.prefabName);
								o.transform.position = s.transform.position;
								o.name = "Branch_" + x + "x" + y;
//								o.transform.parent = s.transform;
								o.transform.SetParent(s.transform,false);
								o.transform.position = s.transform.position;
								brch = o.GetComponent<Branch>();
								s.SetBlock(brch);
								brch.slot = s;
								brch.Initialize();
							}

							if (blockVal >= 6 && blockVal <= 9) 
							{
								s = SlotManager.Instance.FindSlot(x, y);
								o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(Boss.prefabName);
								o.transform.position = s.transform.position;
								o.name = "Boss_" + x + "x" + y;
//								o.transform.parent = s.transform;
								o.transform.SetParent(s.transform,false);
								o.transform.position = s.transform.position;
								boss = o.GetComponent<Boss>();
								s.SetBlock(boss);
								boss.slot = s;
								boss.Initialize(blockVal - 6);

								if(blockVal == 6)
								{
									bossX1 = s.transform.localPosition.x;
									bossY1 = s.transform.localPosition.y;
								}
								else if(blockVal == 9)
								{
									bossX2 = s.transform.localPosition.x;
									bossY2 = s.transform.localPosition.y;
								}
							}

							yield return 0;
						}
					}
				}

			//设置boss的位置
//			DebugUtil.Info(bossX1 + "," + bossX2 + "," + bossY1 + "," + bossY2);
			Transform bossPos = _goSlotCreate.transform.Find("BossPos");
			bossPos.localPosition = new Vector3((bossX1 + bossX2) * 0.5f,(bossY2 + bossY1) * 0.5f,0f);
//			bossPos.gameObject.SetActive(true);
			 
		}
		#endregion

		#region 生成特殊卡牌
		// Generation bombs
		IEnumerator GeneratePowerups ()
		{

//			Utils.waitingStatus = "Making bombs";

			int x;
			int y;

			for (x = 0; x < field.width; x++)				
				for (y = 0; y < field.height; y++)
					field.powerUps[x,y] = LevelProfile.CurrentLevelProfile.GetPowerup(x,Height - y - 1);

			SessionControl.PowerUps powerup;

			for (x = 0; x < field.width; x++)				
				for (y = 0; y < field.height; y++) 
				{
					if (field.powerUps[x,y] > 0 && field.slots[x,y]) 
					{
						powerup = SessionControl.powerupsNew.Find(pu => pu.levelEditorID == field.powerUps[x, y]);
						if (powerup != null)
							AddPowerup(x, y, powerup);
						yield return 0;
					}
				}
		}
		public Card AddPowerup(int x, int y, string powerupName) 
		{
			SessionControl.PowerUps powerup = SessionControl.powerupsNew.Find(pu => pu.name == powerupName);
			if (powerup == null)
				return null;

			return AddPowerup(x, y, powerup);
		}

		// Make a bomb in the specified location with the ability to transform simple chips in a bomb
		public Card AddPowerup(int x, int y, SessionControl.PowerUps powerup) {
			SlotForCard slot = SlotManager.Instance.FindSlot(x, y).GetComponent<SlotForCard>();
			Card chip = slot.card;
			int id;
			if (chip)
				id = chip.id;
			else 
				id = Random.Range(0, LevelProfile.CurrentLevelProfile.CardCount);
			
			if (chip)
			{
//				DebugUtil.Debug("destroy card:" + chip.gameObject.name);
				if(chip.move)
					chip.gravity = false;
				
				Destroy (chip.gameObject);
			}

			chip = GetNewBomb(slot.slot.Row, slot.slot.Col, powerup, slot.transform.position, id);
			return chip;
		}

		public Card GetNewBomb(int x, int y, string powerup, Vector3 position, int id) {
			SessionControl.PowerUps p = SessionControl.powerupsNew.Find(pu => pu.name == powerup);
			return GetNewBomb(x,y,p,position,id);
		}

		public Card GetNewBomb(int x, int y,  SessionControl.PowerUps p, Vector3 position, int id) 
		{
			if (p == null)
				return null;

			string prefabKey = string.Format("{0}{1}",p.contentName,(p.color ? Card.chipTypes[id] : ""));
			GameObject o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(prefabKey);
			o.name = p.contentName + (p.color ? Card.chipTypes[id] : "");
			o.transform.position = position;

			Card cardExist = SlotManager.Instance.FindSlot(x, y).GetChip();
				if (cardExist != null)
					o.transform.position = cardExist.transform.position;
			
			Card chip = o.GetComponent<Card>();
			SlotManager.Instance.FindSlot(x, y).SetChip(chip);
			return chip;
		}
		#endregion

		// Crush block function
		public void  BlockCrush (int x, int y, bool radius, bool force = false) 
		{
			BlockInterface b;
			Slot s;
			Card c;
			StoneChip sc;


			if (radius) {
				foreach (Side side in Utils.straightSides) {
					b = null;
					s = null;
					c = null;
					sc = null;

					b = GetBlock(x + Utils.SideOffsetX(side), y + Utils.SideOffsetY(side));
					if (b && b.CanBeCrushedByNearSlot())
						b.BlockCrush(force);

					s = SlotManager.Instance.FindSlot(x + Utils.SideOffsetX(side), y + Utils.SideOffsetY(side));
					if (s) c = s.GetChip();
					if (c) sc = c.GetComponent<StoneChip>();
					if (sc) c.DestroyChip();
				}
			} 

			b = GetBlock(x, y);
			if (b) b.BlockCrush(force);
		}

		// Request of block object
		public BlockInterface GetBlock ( int x ,   int y  )
		{
			if (field != null && field.GetSlot(x, y))
				return SlotManager.Instance.FindSlot(x, y).Block;		
			return null;
		}

		public IEnumerator ChangeNextScene()
		{
			
			if(FightControl.Instance.EFightStatus == E_FightStatus.ChangeBattle)
				yield break;

			E_FightStatus nextScene = E_FightStatus.None;
			if(FightControl.Instance.EFightStatus == E_FightStatus.FightBoss1)
			{
				nextScene = E_FightStatus.FightBoss2;
			}
			else if(FightControl.Instance.EFightStatus == E_FightStatus.FightBoss2)
			{
				nextScene = E_FightStatus.FightBoss3;
			}
			else if(FightControl.Instance.EFightStatus == E_FightStatus.FightBoss3)
			{
				nextScene = E_FightStatus.Finish;
			}
			else
			{
				yield break;
			}

			//放boss死亡动画
			if(nextScene == E_FightStatus.FightBoss2)
			{
				EventDispatcher.TriggerEvent<int>(UIFight.BossKillEvent,0);
				yield return new WaitForSeconds(1.2f);
			}
			else if(nextScene == E_FightStatus.FightBoss3)
			{
				EventDispatcher.TriggerEvent<int>(UIFight.BossKillEvent,1);
				yield return new WaitForSeconds(1.2f);
			}

			yield return StartCoroutine(Utils.WaitFor(SessionControl.Instance.CanIWait, 0.5f));
			//清标志位
			FightControl.Instance.TransHpFlyPos = null;
			ModuleMgr.Instance.Get<ModuleFight>().ClearBossSkill();

//			if(nextScene == E_FightStatus.Finish)
//			{
//				yield break;
//			}

			if(nextScene != E_FightStatus.FightBoss2
				&& nextScene != E_FightStatus.FightBoss3)
			{
				yield break;
			}

			FightControl.Instance.EFightStatus = E_FightStatus.ChangeBattle;

			GameObject goSlotAll = GameObject.Find("SlotAll");
			if(nextScene == E_FightStatus.FightBoss2)
			{
//				EventDispatcher.TriggerEvent<int>(UIFight.BossKillEvent,0);
//				yield return new WaitForSeconds(1.2f);

				_goSlotCreateBoss2.SetActive(true);
				yield return StartCoroutine(MoveSlot(goSlotAll,_vecSlot2 * -1f));
				goSlotAll.transform.localPosition = _vecSlot2 * -1f;

				_goSlotCreateBoss1.SetActive(false);
				FightControl.Instance.EFightStatus = nextScene;
				SessionControl.Instance.eventCount++;

			}
			else if(nextScene == E_FightStatus.FightBoss3)
			{
//				EventDispatcher.TriggerEvent<int>(UIFight.BossKillEvent,1);
//				yield return new WaitForSeconds(1.2f);

				_goSlotCreateBoss3.SetActive(true);
				yield return StartCoroutine(MoveSlot(goSlotAll,Vector3.zero));
				goSlotAll.transform.localPosition = Vector3.zero;

				_goSlotCreateBoss2.SetActive(false);
				FightControl.Instance.EFightStatus = nextScene;
				SessionControl.Instance.eventCount++;
			}
			else// if(nextScene == E_FightStatus.Finish)
			{
				
				yield break;
			}

		}

	}


	// The class information about the playing field and the target level
	public class MatchField 
	{
		public int width;
		public int height;
		public int chipCount;
		public bool[,] slots;
		public int[,] teleport;
		public bool[,] generator;
//		public bool[,] sugarDrop;
		public int[,] chips;
		public int[,] powerUps;
		public int[,] blocks;
		public int[,] jellies;
		public bool[,] wallsH;
		public bool[,] wallsV;

//		public E_FieldTarget target = E_FieldTarget.KillBoss;
		public int targetValue = 0;


		public MatchField (int w, int h)
		{
			width = w;
			height = h;
			slots = new bool [w,h];
			generator = new bool[w, h];
//			sugarDrop = new bool[w, h];
			teleport = new int[w,h];
			chips = new int [w,h];
			powerUps = new int [w,h];
			blocks = new int [w,h];
			jellies = new int [w,h];
			wallsV = new bool [w,h];
			wallsH = new bool [w,h];
		}

		public bool GetSlot (int x, int y){
			if (x >= 0 && x < width && y >= 0 && y < height) return slots[x,y];
			return false;
		}

		public int GetChip (int x, int y){
			if (x >= 0 && x < width && y >= 0 && y < height) return chips[x,y];
			return 0;
		}

		public void  NewRandomChip (int x, int y, bool unmatching){
			if (chips[x,y] == -1) return;

			chips[x,y] = Random.Range(1, chipCount + 1);

			if (unmatching) 
			{
				while (GetChip(x, y) == GetChip(x, y+1)
					|| GetChip(x, y) == GetChip(x+1, y)
					|| GetChip(x, y) == GetChip(x, y-1)
					|| GetChip(x, y) == GetChip(x-1, y))
				{
					if(chipCount < 5)
					{
						if(
							(GetChip(x, y) == GetChip(x, y+1) && GetChip(x, y) == GetChip(x, y+2))
							|| (GetChip(x, y) == GetChip(x+1, y) && GetChip(x, y) == GetChip(x+2, y))
							|| (GetChip(x, y) == GetChip(x, y-1) && GetChip(x, y) == GetChip(x, y-2))
							|| (GetChip(x, y) == GetChip(x-1, y) && GetChip(x, y) == GetChip(x-2, y))
							)
						{
							chips[x,y] = Random.Range(1, chipCount + 1);
						}
						else
						{
							if(
								(GetChip(x,y) == GetChip(x,y+1) && GetChip(x,y) == GetChip(x,y-1))
								|| (GetChip(x,y) == GetChip(x+1,y) && GetChip(x,y) == GetChip(x-1,y))
							)
							{
								chips[x,y] = Random.Range(1, chipCount + 1);
							}
							else
							{
								break;
							}
						}
					}
					else
					{
						chips[x,y] = Random.Range(1, chipCount + 1);
					}
				}
			}
		}

		public void  FirstChipGeneration (){
			int x;
			int y;

			// impose a mask slots
			for (x = 0; x < width; x++) {
				for (y = 0; y < height; y++) {
					if (!slots[x,y] || (blocks[x,y] > 0 && blocks[x,y] != 5))
						chips[x,y] = -1;
					if (blocks[x,y] == 5 && chips[x,y] == -1)
						chips[x,y] = 0;
				}
			}

			// replace random chips on nonrandom
			for (x = 0; x < width; x++)					
				for (y = 0; y < height; y++) 
					if (chips[x,y] == 0 && chips[x,y] != 9)
						NewRandomChip(x, y, true);	

			// nonrandom give chips to the normal (0 to 5)
			for (x = 0; x < width; x++)					
				for (y = 0; y < height; y++) 
					if (chips[x,y] > 0 && chips[x,y] != 9)
						chips[x,y] --;


			// shuffling color
			// create a deck of colors and shuffling its
			int[] a = {0, 1, 2, 3, 4, 5};
			int j;
			for (int i = 5; i > 0; i--) {
				j = Random.Range(0, i);
				a[j] = a[j] + a[i];
				a[i] = a[j] - a[i];
				a[j] = a[j] - a[i];
			}

			SessionControl.Instance.colorMask = a;

			// apply the results to the matrix shuffling chips	
			for (x = 0; x < width; x++)					
				for (y = 0; y < height; y++) 
					if (chips[x,y] >= 0 && chips[x,y] != 9)
						chips[x,y] = a[chips[x,y]];
		}

	}
}

