/*
 * 
 * 文件名(File Name)：             Level
 *
 * 作者(Author)：                  #AuthorName#
 *
 * 创建时间(CreateTime):           2016/03/16 16:54:47
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace Fight
{
	public class CPanel
	{
		public static int uiAnimation = 0;
	}

	public class Level : MonoBehaviour 
	{
		public static Dictionary<int, LevelProfile> AllLevels = new Dictionary<int, LevelProfile>();
		/// <summary>
		/// 配置信息
		/// </summary>
		public LevelProfile Profile;

		void Awake() 
		{
//			Debug.LogError("awake level");
			Profile.Level = transform.GetSiblingIndex() + 1;

			AllLevels.Add(Profile.Level, Profile);

			if (!Application.isEditor)
				Destroy(gameObject);
		}

//		void Start()
//		{
//			LoadLevel(1);
//		}

		public static void LoadLevel() 
		{
			if (CPanel.uiAnimation > 0)
				return;


			FightNew.ModuleFight moduleFight = FightNew.FightMgr.Instance.ModuleFight;
			if(moduleFight == null || moduleFight.LevelId == null || moduleFight.LevelId.Length <= 0)
				return;
			
			LevelProfile.CurrentLevelProfileAll = new LevelProfile[moduleFight.LevelId.Length];
			FieldAssistant.Instance.fieldAll = new MatchField[moduleFight.LevelId.Length];

			for(int i = 0; i < moduleFight.LevelId.Length;i++)
			{
				if (!AllLevels.ContainsKey(moduleFight.LevelId[i]))
				{
					DebugUtil.Error("没有配置的场景：" + moduleFight.LevelId[i]);
					return;
				}
				LevelProfile.CurrentLevelProfileAll[i] = AllLevels[moduleFight.LevelId[i]];
			}

			FightNew.FightMgr.Instance.StartLevel(LevelProfile.CurrentLevelProfileAll[0].Level);
			//加载关卡
//			FieldAssistant.Instance.StartLevel();
		}

		public static void LoadTestLevel(int levelId) 
		{
			if (CPanel.uiAnimation > 0)
				return;


//			ModuleFight moduleFight = FightControl.Instance.ModuleFight;
//			if(moduleFight == null || moduleFight.LevelId == null || moduleFight.LevelId.Length <= 0)
//				return;

			LevelProfile.CurrentLevelProfileAll = new LevelProfile[3]{AllLevels[levelId], AllLevels[levelId], AllLevels[levelId]};
			FieldAssistant.Instance.fieldAll = new MatchField[3];


			if (!AllLevels.ContainsKey(levelId))
			{
				DebugUtil.Error("没有配置的场景：" + levelId);
				return;
			}
			LevelProfile.CurrentLevelProfileAll[0] = AllLevels[levelId];


			//			if (ProfileAssistant.main.local_profile["live"] > 0)
			//				UIAssistant.main.ShowPage("LevelSelectedPopup");
			//			else
			//				UIAssistant.main.ShowPage("NotEnoughLives");

			//加载关卡
			FieldAssistant.Instance.StartLevel();
		}
	}

	[System.Serializable]
	/// <summary>
	/// 关卡配置信息
	/// </summary>
	public class LevelProfile 
	{
		public static LevelProfile NewCurrentLevelProfile;

		public static LevelProfile[] CurrentLevelProfileAll{set;get;}
		/// <summary>
		/// 当前配置信息
		/// </summary>
		public static LevelProfile CurrentLevelProfile
		{
			get
			{
				if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss1
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss1)
				{
					return CurrentLevelProfileAll[0];
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss2
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss2)
				{
					return CurrentLevelProfileAll[1];
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss3
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss3)
				{
					return CurrentLevelProfileAll[2];
				}
				else
				{
					DebugUtil.Error("get slot fail");
					return null;
				}
			}
		}
		/// <summary>
		/// 最大宽高
		/// </summary>
		public const int MaxSize = 12;
		/// <summary>
		/// 场景Unity唯一标示ID
		/// </summary>
		public int LevelInstanceID = 0;
		/// <summary>
		/// 场景编号
		/// </summary>
		public int Level = 0;

		// field size
		/// <summary>
		/// 宽度
		/// </summary>
		public int Width = 9;
		/// <summary>
		/// 高度
		/// </summary>
		public int Height = 9;
		/// <summary>
		/// 场景中的卡牌数量
		/// </summary>
		public int CardCount = 6;
//		public int targetColorCount = 30; // Count of target color in Color mode
//		public int targetSugarDropsCount = 0; // Count of sugar chips in SugaDrop mode
//		public int firstStarScore = 100; // number of score points needed to get a first stars
//		public int secondStarScore = 200; // number of score points needed to get a second stars
//		public int thirdStarScore = 300; // number of score points needed to get a third stars

		/// <summary>
		/// 石头随机出现比例
		/// </summary>
		public float StonePortion = 0f;
//		/// <summary>
//		/// 游戏目标
//		/// </summary>
//		public E_FieldTarget Target = E_FieldTarget.KillBoss;

		// Target score in Score mode = firstStarScore;
		// Count of jellies in Jelly mode colculate automaticly via jellyData array;
		// Count of blocks in Blocks mode colculate automaticly via blockData array;
		// Count of remaining chips in Color mode takes from "countOfEachTargetCount" array, where value is count, index is color ID ;

		//public Limitation limitation = Limitation.Moves;
//		// Session duration in time limitation mode = duration value (sec);
//		// Count of moves in moves limimtation mode = moveCount value (sec);
//		public int MoveCount = 20; // Count of moves in TargetScore and JellyCrush
//		public int SecDuration = 180;
//
//		public int[] countOfEachTargetCount = new int[6]; // Array of counts of each color matches. Color ID is index.
//
//		public void SetTargetCount(int index, int target) {
//			countOfEachTargetCount[index] = target;
//		}
//		public int GetTargetCount(int index) {
//			return countOfEachTargetCount[index];
//		}

		public LevelProfile() 
		{
//			Debug.LogError("aaaa:" + LevelInstanceID);

			//设置插槽
			for (int x = 0; x < MaxSize; x++)
				for (int y = 0; y < MaxSize; y++)
					SetSlot(x, y, true);
			
			for (int x = 0; x < MaxSize; x++)
				SetGenerator(x, 0, true);
			
//			for (int x = 0; x < MaxSize; x++)
//				SetSugarDrop(x, MaxSize - 1, true);
		}

		// Slot
		public bool[] _slot = new bool[MaxSize * MaxSize];
		public bool GetSlot(int x, int y) 
		{
			return _slot[y * MaxSize + x];
		}
		/// <summary>
		/// true说明是卡槽
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="v">If set to <c>true</c> v.</param>
		public void SetSlot(int x, int y, bool v) 
		{
			//Debug.LogError("x:" + x + "y:" + y + "v:" + v);


			_slot[y * MaxSize + x] = v;
		}

		// Gravity
		public int[] _gravity = new int[MaxSize * MaxSize];
		public int GetGravity(int x, int y) {
			return _gravity[y * MaxSize + x];
		}
		public void SetGravity(int x, int y, int v) {
			_gravity[y * MaxSize + x] = v;
		}

		// Generators
		public bool[] _generator = new bool[MaxSize * MaxSize];
		public bool GetGenerator(int x, int y) {
			return _generator[y * MaxSize + x];
		}
		public void SetGenerator(int x, int y, bool v) {
			_generator[y * MaxSize + x] = v;
		}

		// Teleports
		public int[] _teleport = new int[MaxSize * MaxSize];
		public int GetTeleport(int x, int y) {
			return _teleport[y * MaxSize + x];
		}

		/// <summary>
		/// 0说明没有传送
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="v">V.</param>
		public void SetTeleport(int x, int y, int v) {
			_teleport[y * MaxSize + x] = v;
		}

//		// Sugar Drop slots
//		private bool[] _sugarDrop = new bool[MaxSize * MaxSize];
//		public bool GetSugarDrop(int x, int y) {
//			return _sugarDrop[y * MaxSize + x];
//		}
//		public void SetSugarDrop(int x, int y, bool v) {
//			_sugarDrop[y * MaxSize + x] = v;
//		}

		// Chip
		public int[] _chip = new int[MaxSize * MaxSize];
		/// <summary>
		/// －1表明空白，0表明随机
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="v">V.</param>
		public void SetChip(int x, int y, int v) {
			_chip[y * MaxSize + x] = v;
		}


		public int GetChip(int x, int y) 
		{
			return _chip[y * MaxSize + x];
		}



		// Jelly
		public int[] _jelly = new int[MaxSize * MaxSize];
		public int GetJelly(int x, int y) {
			return _jelly[y * MaxSize + x];
		}
		public void SetJelly(int x, int y, int v) {
			_jelly[y * MaxSize + x] = v;
		}

		// Block
		public int[] _block = new int[MaxSize * MaxSize];
		public int GetBlock(int x, int y) {
			return _block[y * MaxSize + x];
		}
		public void SetBlock(int x, int y, int v) {
			_block[y * MaxSize + x] = v;
		}

		// Powerup
		public int[] _powerup = new int[MaxSize * MaxSize];
		public int GetPowerup(int x, int y) {
			return _powerup[y * MaxSize + x];
		}
		public void SetPowerup(int x, int y, int v) {
			_powerup[y * MaxSize + x] = v;
		}

		// Wall
		public bool[] _wallV = new bool[MaxSize * MaxSize];
		public bool[] _wallH = new bool[MaxSize * MaxSize];
		public bool GetWallV(int x, int y) {
			return _wallV[y * MaxSize + x];
		}
		public bool GetWallH(int x, int y) {
			return _wallH[y * MaxSize + x];
		}
		public void SetWallV(int x, int y, bool v) {
			_wallV[y * MaxSize + x] = v;
		}
		public void SetWallH(int x, int y, bool v) {
			_wallH[y * MaxSize + x] = v;
		}

		public LevelProfile GetClone() 
		{
			LevelProfile clone = new LevelProfile();
			clone.Level = Level;

			clone.Width = Width;
			clone.Height = Height;
			clone.CardCount = CardCount;
//			clone.targetSugarDropsCount = targetSugarDropsCount;
//			clone.countOfEachTargetCount = countOfEachTargetCount;
//			clone.targetColorCount = targetColorCount;

//			clone.firstStarScore = firstStarScore;
//			clone.secondStarScore = secondStarScore;
//			clone.thirdStarScore = thirdStarScore;

//			clone.Target = Target;
//			clone.limitation = limitation;

//			clone.SecDuration = SecDuration;
//			clone.MoveCount = MoveCount;

			clone._slot = _slot;
			clone._gravity = _gravity;
			clone._generator = _generator;
			clone._teleport = _teleport;
//			clone._sugarDrop = _sugarDrop;
			clone._chip = _chip;
			clone._jelly = _jelly;
			clone._block = _block;
			clone._powerup = _powerup;
			clone._wallV = _wallV;
			clone._wallH = _wallH;
			clone.StonePortion = StonePortion;

			return clone;
		}
	}
}
