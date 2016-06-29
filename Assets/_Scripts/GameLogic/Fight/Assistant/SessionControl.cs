/*
 * 
 * 文件名(File Name)：             SessionControl
 *
 * 作者(Author)：                  #AuthorName#
 *
 * 创建时间(CreateTime):           2016/03/29 15:57:18
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;
using System.Linq;

namespace Fight
{
	public class SessionControl : DDOLSingleton<SessionControl> 
	{
		public bool isPlaying = false;

		/// <summary>
		/// 正在下落的卡牌个数
		/// </summary>
		public int gravity = 0;

		/// <summary>
		/// Number of current matching process
		/// </summary>
		public int matching = 0;

		/// <summary>
		/// 剩余步数
		/// </summary>
		public int movesCount;

		/// <summary>
		/// 剩余时间
		/// </summary>
		public float timeLeft;

		/// <summary>
		/// 正在播放的动画数量
		/// </summary>
		public int animate = 0; // Number of current animation

		/// <summary>
		/// 每次成功交换后加1
		/// </summary>
		public int swapEvent; 

		public int lastMovementId;

		/// <summary>
		/// 事件计数器
		/// </summary>
		public int eventCount;

		public int[] colorMask = new int[6]; // Mask of random colors: color number - colorID


		/// <summary>
		/// 是否超过限制条件（时间／步数）
		/// </summary>
		public bool outOfLimit = false;

//		/// <summary>
//		/// 是否完成目标
//		/// </summary>
//		public bool reachedTheTarget = false;

		/// <summary>
		/// 限制检测协程是否运行完毕
		/// </summary>
		bool limitationRoutineIsOver = false;

//		/// <summary>
//		/// 目标检测协程是否运行完毕
//		/// </summary>
//		bool targetRoutineIsOver = false;

		public SessionControl()
		{
			InitMixData();
			InitPowerUpData();
			InitCombinationData();
		}

		// Reset variables
		public void Reset() 
		{
			animate = 0;
			gravity = 0;
			matching = 0;
			//
			//		main.stars = 0;
			//
			successMoveCount = 0;
			eventCount = 0;
			lastMovementId = 0;
			swapEvent = 0;
			//		main.score = 0;
			//		main.firstChipGeneration = true;
			//
			isPlaying = false;
			movesCount = FightControl.Instance.ModuleFight.Moves;// LevelProfile.CurrentLevelProfile.MoveCount;
			timeLeft = FightControl.Instance.ModuleFight.TimeSec;//LevelProfile.CurrentLevelProfile.SecDuration;

			//		main.timeLeft = LevelProfile.main.duration;
			//		main.countOfEachTargetCount = new int[] { 0, 0, 0, 0, 0, 0};
			//		main.creatingSugarTask = 0;
			//
			//
//			reachedTheTarget = false;
			outOfLimit = false;
			//

//			targetRoutineIsOver = false;
			limitationRoutineIsOver = false;
			//
			AnimationControl.Instance.Iteraction = true;
		}

		// Conditions to start animation
		public bool CanIAnimate ()
		{
			return isPlaying && gravity == 0 && matching == 0;
		}


		#region Mix
		public List<Mix> mixes = new List<Mix>();

		private void InitMixData()
		{
			mixes.Add(new Mix("CrossBomb","SimpleBomb","CrossSimpleMix"));
			mixes.Add(new Mix("CrossBomb","CrossBomb","CrossMix"));
			mixes.Add(new Mix("VLineBomb","SimpleBomb","CrossSimpleMix"));
			mixes.Add(new Mix("HLineBomb","SimpleBomb","CrossSimpleMix"));

			mixes.Add(new Mix("VLineBomb","VLineBomb","LineMix"));
			mixes.Add(new Mix("HLineBomb","HLineBomb","LineMix"));
			mixes.Add(new Mix("HLineBomb","VLineBomb","LineMix"));

			mixes.Add(new Mix("SimpleBomb","SimpleBomb","SimpleMix"));

			mixes.Add(new Mix("ColorBomb","SimpleBomb","ColorMix"));
			mixes.Add(new Mix("ColorBomb","CrossBomb","ColorMix"));
			mixes.Add(new Mix("ColorBomb","VLineBomb","ColorMix"));
			mixes.Add(new Mix("ColorBomb","HLineBomb","ColorMix"));
			mixes.Add(new Mix("ColorBomb","ColorBomb","ColorMix"));
			mixes.Add(new Mix("ColorBomb","Ladybird","ColorMix"));
			mixes.Add(new Mix("ColorBomb","RainbowHeart","ColorMix"));

			mixes.Add(new Mix("RainbowHeart","SimpleBomb","RainbowMix"));
			mixes.Add(new Mix("RainbowHeart","CrossBomb","RainbowMix"));
			mixes.Add(new Mix("RainbowHeart","VLineBomb","RainbowMix"));
			mixes.Add(new Mix("RainbowHeart","HLineBomb","RainbowMix"));
			mixes.Add(new Mix("RainbowHeart","Ladybird","RainbowMix"));
			mixes.Add(new Mix("RainbowHeart","RainbowHeart","RainbowMix"));

			mixes.Add(new Mix("UltraColorBomb","SimpleChip","UltraColorMix"));
			mixes.Add(new Mix("UltraColorBomb","SimpleBomb","UltraColorMix"));
			mixes.Add(new Mix("UltraColorBomb","CrossBomb","UltraColorMix"));
			mixes.Add(new Mix("UltraColorBomb","VLineBomb","UltraColorMix"));
			mixes.Add(new Mix("UltraColorBomb","HLineBomb","UltraColorMix"));
			mixes.Add(new Mix("UltraColorBomb","Ladybird","UltraColorMix"));

			mixes.Add(new Mix("RainbowHeart","UltraColorBomb","RainbowMix"));
			mixes.Add(new Mix("UltraColorBomb","UltraColorBomb","UltraColorMix"));

			mixes.Add(new Mix("Ladybird","Ladybird","LadybirdsMix"));
			mixes.Add(new Mix("Ladybird","SimpleBomb","LadybirdsMix"));
			mixes.Add(new Mix("Ladybird","CrossBomb","LadybirdsMix"));
			mixes.Add(new Mix("Ladybird","VLineBomb","LadybirdsMix"));
			mixes.Add(new Mix("Ladybird","HLineBomb","LadybirdsMix"));

			mixes.Add(new Mix("CrossBomb","VLineBomb","CrossMix"));
			mixes.Add(new Mix("CrossBomb","HLineBomb","CrossMix"));
			mixes.Add(new Mix("CrossBomb","UltraColorBomb","CrossMix"));
		}

		[System.Serializable]
		public class Mix 
		{
			public Pair pair;// = new Pair("", "");

			public string function;

			public bool Compare(string _a, string _b) {
				return pair == new Pair(_a, _b);
			}

			public static bool ContainsThisMix(string _a, string _b) {
				return Instance.mixes.Find(x => x.Compare(_a, _b)) != null;
			}

			public static Mix FindMix(string _a, string _b) {
				return Instance.mixes.Find((x) => x.Compare(_a, _b));
			}

			public Mix(string a,string b,string func)
			{
				pair = new Pair(a,b);
				function = func;
			}
		}

//		public void MixChips(Chip a, Chip b) 
//		{
//			Mix mix = Mix.FindMix(a.chipType, b.chipType);
//			if (mix == null)
//				return;
//
//			Chip target = null;
//			Chip secondary = null;
//			if (a.chipType == mix.pair.a) {
//				target = a;
//				secondary = b;
//			}
//			if (b.chipType == mix.pair.a) {
//				target = b;
//				secondary = a;
//			}
//
//
//			if (target == null) {
//				Debug.LogError("It can't be mixed, because there is no target chip");
//				return;
//			}
//			b.parentSlot.SetChip(target);
//			secondary.HideChip(false);
//			target.SendMessage(mix.function, secondary);
//		}
		#endregion

		#region 特殊卡牌
		//特殊效果
		private static List<PowerUps> _powerups = null;
		public static List<PowerUps> powerupsNew
		{
			get
			{
				if(_powerups == null)
				{
					_powerups = new List<PowerUps>();
				}

				if(_powerups.Count <= 0)
				{
					InitPowerUpData();
				}

				return _powerups;
			}
		}

		private static void InitPowerUpData()
		{
			if(_powerups == null)	
				_powerups = new List<PowerUps>();

			if(_powerups.Count > 0)
			{
				return;
			}
			
			//_powerups.Add(new PowerUps("SimpleBomb","SimpleBomb",true,2,"SB"));
			_powerups.Add(new PowerUps("CrossBomb","CrossBomb",true,1,"XB"));
			//_powerups.Add(new PowerUps("ColorBomb","ColorBomb",true,3,"CB"));
			//_powerups.Add(new PowerUps("RainbowHeart","RainbowHeart",false,4,"RH"));
			_powerups.Add(new PowerUps("HLineBomb","HLineBomb",true,6,"hB"));
			_powerups.Add(new PowerUps("VLineBomb","VLineBomb",true,7,"vB"));
			_powerups.Add(new PowerUps("UltraColorBomb","UltraColorBomb",false,8,"UC"));
		}

		[System.Serializable]
		public class PowerUps 
		{
			public string name = "";
			public string contentName = "";
			public bool color = true;
			public int levelEditorID = 0;
			public string levelEditorName = "";

			public PowerUps(string _name,string _contentName,bool _color,int _levelEditorID,string _levelEditorName)
			{
				name = _name;
				contentName = _contentName;
				color = _color;
				levelEditorID = _levelEditorID;
				levelEditorName = _levelEditorName;
			}
		}
		#endregion

		#region 合并
		public static List<Combinations> combinations = new List<Combinations>();
		public static void InitCombinationData()
		{
			combinations.Add(new Combinations(1,"UltraColorBomb",true,false,5));
			combinations.Add(new Combinations(2,"UltraColorBomb",false,true,5));

			combinations.Add(new Combinations(3,"ColorBomb",true,false,5));
			combinations.Add(new Combinations(4,"ColorBomb",false,false,5));

			combinations.Add(new Combinations(5,"SimpleBomb",true,false,4));
			combinations.Add(new Combinations(6,"SimpleBomb",false,true,4));

//			return;

//			combinations.Add(new Combinations(1,"UltraColorBomb",false,true,5));
//			combinations.Add(new Combinations(2,"UltraColorBomb",true,false,5));
			combinations.Add(new Combinations(3,"CrossBomb",true,true,5));
//			combinations.Add(new Combinations(4,"CrossBomb",false,true,5));
			combinations.Add(new Combinations(5,"VLineBomb",true,false,4));
			combinations.Add(new Combinations(6,"HLineBomb",false,true,4));
				

			combinations.Sort((SessionControl.Combinations a, SessionControl.Combinations b) => 
				{
					if (a.priority < b.priority)
						return -1;
					if (a.priority > b.priority)
						return 1;
					return 0;
				});
		}


		[System.Serializable]
		public class Combinations 
		{
			public int priority = 0;
			public string powerup;
			public bool horizontal = true;
			public bool vertical = true;
			public bool square = false;
			public int minCount = 4;
			public string tag = "";

			public Combinations(int _priority,string _powerup,bool _horizontal,bool _vertical,int _minCount)
			{
				priority = _priority;
				powerup = _powerup;
				horizontal = _horizontal;
				vertical = _vertical;
				minCount = _minCount;
			}

		}
		#endregion

		public int GetMovementID ()
		{
			lastMovementId ++;
			return lastMovementId;
		}

		public void MixChips(Card a, Card b) 
		{
			Mix mix = Mix.FindMix(a.chipType, b.chipType);
			if (mix == null)
				return;
			
			Card target = null;
			Card secondary = null;
			if (a.chipType == mix.pair.a) {
				target = a;
				secondary = b;
			}
			if (b.chipType == mix.pair.a) {
				target = b;
				secondary = a;
			}


			if (target == null) {
				DebugUtil.Error("It can't be mixed, because there is no target chip");
				return;
			}

			b.parentSlot.SetChip(target);
			secondary.HideChip(false);
			//todo: nickyangzj
			target.SendMessage(mix.function, secondary);
		}

		// Class with information of solution
		public class Solution {
			//   T
			//   T
			// LLXRR  X - center of solution
			//   B
			//   B

			public int count; // count of chip combination (count = T + L + R + B + X)
			public int potential; // potential of solution
			public int id; // ID of chip color
			public List<Card> chips = new List<Card>();

			// center of solution
			public int x;
			public int y;

			public bool v; // is this solution is vertical?  (v = L + R + X >= 3)
			public bool h; // is this solution is horizontal? (h = T + B + X >= 3)
			public bool q;
			//public int posV; // number on right chips (posV = R)
			//public int negV; // number on left chips (negV = L)
			//public int posH; // number on top chips (posH = T)
			//public int negH; // number on bottom chips (negH = B)
		}

		// Event counter
		public void EventCounter ()
		{
			eventCount++;
		}

		/// <summary>
		/// 有效
		/// </summary>
		public int successMoveCount;
		// Event counter
		public void SuccessMoveCounter ()
		{
			successMoveCount++;
		}



		// Starting a new game session
		public void StartSession() 
		{
			StopAllCoroutines (); // Ending of all current coroutines

			isPlaying = true;

			//block数量，存粹显示用
			//			blockCountTotal = GameObject.FindObjectsOfType<Block>().Length;

			//			switch (limitationType) 
			//			{ // Start corresponding coroutine depending on the limiation mode
			//				case Limitation.Moves: StartCoroutine(MovesLimitation()); break;
			//				case Limitation.Time: StartCoroutine(TimeLimitation());break;
			//			}

//			StartCoroutine(MovesLimitation());
//			StartCoroutine(TimeLimitation());

//			//开启目标检测协程
//			StartCoroutine(TargetSession(() => {
//				return true;
//			}));




			StartCoroutine (BaseSession()); // Base routine of game session
			StartCoroutine (ShowingHintRoutine()); // Coroutine display hints
			StartCoroutine (ShuffleRoutine()); // Coroutine of mixing chips at the lack moves
			StartCoroutine (FindingSolutionsRoutine()); // Coroutine of finding a solution and destruction of existing combinations of chips
			StartCoroutine (IllnessRoutine()); // Coroutine of Weeds logic

//			GameCamera.main.ShowField();
			//			UIAssistant.main.ShowPage("Field");
		}


		#region Limitation Modes Logic	

//		// Game session with limited time
//		IEnumerator TimeLimitation() 
//		{
//			outOfLimit = false;
//
//			// Waiting until the rules of the game are carried out
//			while (timeLeft > 0 && !targetRoutineIsOver) 
//			{
//				if (Time.timeScale == 1)
//					timeLeft -= 1f;
//				
//				timeLeft = Mathf.Max(timeLeft, 0);
//				if (timeLeft <= 5)
//					AudioAssistant.Shot("TimeWarrning");
//				yield return new WaitForSeconds(1f);
//
//				if (timeLeft <= 0) {
//					do
//						yield return StartCoroutine(Utils.WaitFor(CanIWait, 1f));
//					while (FindObjectsOfType<Chip>().ToList().Find(x => x.destroying) != null);
//					if (!reachedTheTarget) {
//						UIAssistant.main.ShowPage("NoMoreMoves");
//						AudioAssistant.Shot("NoMoreMoves");
//						wait = true;
//						// Pending the decision of the player - lose or purchase additional time
//						while (wait)
//							yield return new WaitForSeconds(0.5f);
//
//					}
//				}
//			}
//
//			yield return StartCoroutine(Utils.WaitFor(CanIWait, 1f));
//
//			if (timeLeft <= 0) outOfLimit = true;
//
//			limitationRoutineIsOver = true;
//		}

		// Game session with limited count of moves
		IEnumerator MovesLimitation() 
		{
			outOfLimit = false;

			// Waiting until the rules of the game are carried out
			while (movesCount > 0) 
			{
				yield return new WaitForSeconds(1f);
				if (movesCount <= 0) 
				{
					//					//等爆炸完
					//					do
					//						yield return StartCoroutine(Utils.WaitFor(CanIWait, 1f));
					//					while (FindObjectsOfType<Chip>().ToList().Find(x => x.destroying) != null);
					//
					//
					//					//没有完成目标弹购买
					//					if (!reachedTheTarget) {
					//						UIAssistant.main.ShowPage("NoMoreMoves");
					//						AudioAssistant.Shot("NoMoreMoves");
					//						wait = true;
					//						// Pending the decision of the player - lose or purchase additional time
					//						while (wait)
					//							yield return new WaitForSeconds(0.5f);
					//
					//					}
				}
			}

			yield return StartCoroutine(Utils.WaitFor(CanIWait, 1f));

			outOfLimit = true;
			limitationRoutineIsOver = true;
		}

		#endregion

//		IEnumerator TargetSession(System.Func<bool> func) 
//		{
//			reachedTheTarget = false;
//			while (!outOfLimit && !func.Invoke()) 
//			{
//				yield return new WaitForSeconds(0.33f);
//				//				if (GetResource() == 0 && score >= LevelProfile.CurrentLevelProfile.firstStarScore && func.Invoke())
//				//					reachedTheTarget = true;
//			}
//
//			//			if (score >= LevelProfile.CurrentLevelProfile.firstStarScore && func.Invoke())
//			//				reachedTheTarget = true;
//
//			targetRoutineIsOver = true;
//		}

		// Conditions to start falling chips
		public bool CanIGravity ()
		{
			return isPlaying && ((animate == 0 && matching == 0) || gravity > 0) && FightControl.Instance.IsFighting;
		}

		// Conditions for waiting player's actions
		public bool CanIWait ()
		{
			return isPlaying && animate == 0 && matching == 0 && gravity == 0 && FightControl.Instance.IsFighting;
		}


		IEnumerator BaseSession () 
		{
			yield return 0;
			while (!limitationRoutineIsOver) 
			{
				yield return 0;
			}

//			// Checking the condition of losing
//			if (!reachedTheTarget) {
//				yield return StartCoroutine(GameCamera.main.HideFieldRoutine());
//				FieldAssistant.main.RemoveField();
//				ShowLosePopup();
//				yield break;
//			}

			//不能移动
			AnimationControl.Instance.Iteraction = false;

			yield return new WaitForSeconds(0.2f);
//			UIAssistant.main.ShowPage("TargetIsReached");
//			AudioAssistant.Shot("TargetIsReached");
//			yield return StartCoroutine(Utils.WaitFor(() => CPanel.uiAnimation == 0, 0.4f));

//			UIAssistant.main.ShowPage("Field");

			// Conversion of the remaining moves into bombs and activating them
//			yield return StartCoroutine(BurnLastMovesToPowerups());

//			yield return StartCoroutine(Utils.WaitFor(CanIWait, 1f));

			// Ending the session, showing win popup
//			yield return StartCoroutine(GameCamera.main.HideFieldRoutine());
//			FieldAssistant.Instance.RemoveField();

//			StartCoroutine(YouWin());
		}

		#region 提示
		// Coroutine of showing hints
		IEnumerator ShowingHintRoutine () 
		{
			yield return 0;

			int hintOrder = 0;
			float delay = 5;

			yield return new WaitForSeconds(1f);

			while (!limitationRoutineIsOver) 
			{
				while (!isPlaying || !FightControl.Instance.IsFighting)
					yield return 0;
				
				yield return StartCoroutine(Utils.WaitFor(CanIWait, delay));
				if (eventCount > hintOrder) 
				{
					hintOrder = eventCount;
					ShowHint();
				}
			}
		}

		// Showing random hint
		void  ShowHint ()
		{
			if (!isPlaying || !FightControl.Instance.IsFighting) return;
			List<Move> moves = FindMoves();

//			foreach (Move move in moves) 
//			{
//				Debug.DrawLine(Slot.GetSlot(move.fromX, move.fromY).transform.position, Slot.GetSlot(move.toX, move.toY).transform.position, Color.red, 10);
//			}

			if (moves.Count == 0) return;

			//yangzj：找最优解
//			Move bestMove = moves[ Random.Range(0, moves.Count) ];
			Move bestMove = null;
			int potential = 0;
			for(int i = 0; i < moves.Count;i++)
			{
				if(moves[i].solution != null && moves[i].solution.potential > potential)
					bestMove = moves[i];
			}

			if (bestMove.solution == null) return;

			foreach (Card chip in bestMove.solution.chips)
			{
				chip.Flashing(eventCount);
			}
		}

		public List<Move> FindMoves ()
		{
			List<Move> moves = new List<Move>();
			if (!FieldAssistant.Instance.gameObject.activeSelf) return moves;
			if (LevelProfile.CurrentLevelProfile == null) return moves;

			int x;
			int y;
			int width = LevelProfile.CurrentLevelProfile.Width;
			int height = LevelProfile.CurrentLevelProfile.Height;
			Move move;
			Solution solution;
			int potential;
			SlotForCard slot;
			string chipTypeA = "";
			string chipTypeB = "";

			// horizontal
			for (x = 0; x < width - 1; x++)
				for (y = 0; y < height; y++) {
					if (!FieldAssistant.Instance.field.slots[x,y]) continue;
					if (!FieldAssistant.Instance.field.slots[x+1,y]) continue;
					if (FieldAssistant.Instance.field.blocks[x,y] > 0) continue;
					if (FieldAssistant.Instance.field.blocks[x+1,y] > 0) continue;
					if (FieldAssistant.Instance.field.chips[x,y] == FieldAssistant.Instance.field.chips[x+1,y]) continue;
					if (FieldAssistant.Instance.field.chips[x,y] == -1 && FieldAssistant.Instance.field.chips[x+1,y] == -1) continue;
					if (FieldAssistant.Instance.field.wallsV[x,y]) continue;
					move = new Move();
					move.fromX = x;
					move.fromY = y;
					move.toX = x + 1;
					move.toY = y;
					AnalizSwap(move);

					Solution solutionA = null;
					Solution solutionB = null;

					slot = SlotManager.Instance.FindSlot(move.fromX, move.fromY).GetComponent<SlotForCard>();
					chipTypeA = slot.card == null ? "SimpleChip" : slot.card.chipType;

					potential = 0;

					solution = slot.MatchAnaliz();
					if (solution != null) {
						solutionA = solution;
						potential = solution.potential;
					}

//					solution = slot.MatchSquareAnaliz();
//					if (solution != null && potential < solution.potential) {
//						solutionA = solution;
//						potential = solution.potential;
//					}

					move.potencial += potential;

					slot = SlotManager.Instance.FindSlot(move.toX, move.toY).GetComponent<SlotForCard>();
					chipTypeB = slot.card == null ? "SimpleChip" : slot.card.chipType;

					potential = 0;
					solution = slot.MatchAnaliz();
					if (solution != null) {
						solutionB = solution;
						potential = solution.potential;
					}

//					solution = slot.MatchSquareAnaliz();
//					if (solution != null && potential < solution.potential) {
//						solutionB = solution;
//						potential = solution.potential;
//					}

					move.potencial += potential;

					if (solutionA != null && solutionB != null)
						move.solution = solutionA.potential >= solutionB.potential ? solutionA : solutionB;
					else
						move.solution = solutionA != null ? solutionA : solutionB;

					AnalizSwap(move);
					if (Mix.ContainsThisMix(chipTypeA, chipTypeB))
						move.potencial += 100;
					if (move.potencial > 0 || (chipTypeA != "SimpleChip" &&  chipTypeB != "SimpleChip")) 
						moves.Add(move);		
				}

			// vertical
			for (x = 0; x < width; x++)
				for (y = 0; y < height - 1; y++) {
					if (!FieldAssistant.Instance.field.slots[x,y]) continue;
					if (!FieldAssistant.Instance.field.slots[x,y+1]) continue;
					if (FieldAssistant.Instance.field.blocks[x,y] > 0) continue;
					if (FieldAssistant.Instance.field.blocks[x,y+1] > 0) continue;
					if (FieldAssistant.Instance.field.chips[x,y] == FieldAssistant.Instance.field.chips[x,y+1]) continue;
					if (FieldAssistant.Instance.field.chips[x,y] == -1 && FieldAssistant.Instance.field.chips[x,y+1] == -1) continue;
					if (FieldAssistant.Instance.field.wallsH[x,y]) continue;
					move = new Move();
					move.fromX = x;
					move.fromY = y;
					move.toX = x;
					move.toY = y + 1;

					AnalizSwap(move);

					Solution solutionA = null;
					Solution solutionB = null;

					slot = SlotManager.Instance.FindSlot(move.fromX, move.fromY).GetComponent<SlotForCard>();
					chipTypeA = slot.card == null ? "SimpleChip" : slot.card.chipType;

					potential = 0;

					solution = slot.MatchAnaliz();
					if (solution != null) {
						solutionA = solution;
						potential = solution.potential;
					}

//					solution = slot.MatchSquareAnaliz();
//					if (solution != null && potential < solution.potential) {
//						solutionA = solution;
//						potential = solution.potential;
//					}

					move.potencial += potential;

					slot = SlotManager.Instance.FindSlot(move.toX, move.toY).GetComponent<SlotForCard>();
					chipTypeB = slot.card == null ? "SimpleChip" : slot.card.chipType;

					potential = 0;
					solution = slot.MatchAnaliz();
					if (solution != null) {
						solutionB = solution;
						potential = solution.potential;
					}

//					solution = slot.MatchSquareAnaliz();
//					if (solution != null && potential < solution.potential) {
//						solutionB = solution;
//						potential = solution.potential;
//					}

					move.potencial += potential;

					if (solutionA != null && solutionB != null)
						move.solution = solutionA.potential >= solutionB.potential ? solutionA : solutionB;
					else
						move.solution = solutionA != null ? solutionA : solutionB;

					AnalizSwap(move);
					if (Mix.ContainsThisMix(chipTypeA, chipTypeB))
						move.potencial += 100;
					if (move.potencial > 0 || (chipTypeA != "SimpleChip" &&  chipTypeB != "SimpleChip")) 
						moves.Add(move);		
				}

			return moves;
		}

		// change places 2 chips in accordance with the move for the analysis of the potential of this move
		void  AnalizSwap (Move move)
		{
			SlotForCard slot;
			Card fChip = GameObject.Find(string.Format(Slot.SlotKeyFormat,move.fromX,move.fromY)).GetComponent<Slot>().GetChip();
			Card tChip = GameObject.Find(string.Format(Slot.SlotKeyFormat,move.toX,move.toY)).GetComponent<Slot>().GetChip();
			if (!fChip || !tChip) return;
			slot = tChip.parentSlot;
			fChip.parentSlot.SetChip(tChip);
			slot.SetChip(fChip);
		}

		// Class with information of move
		public class Move 
		{
			//
			// A -> B
			//

			// position of start chip (A)
			public int fromX;
			public int fromY;
			// position of target chip (B)
			public int toX;
			public int toY;

			public Solution solution; // solution of this move
			public int potencial; // potential of this move
		}
		#endregion


		#region 消除
		private int _conitnueMathchCount = 0;
		// Coroutine of searching solutions and the destruction of existing combinations
		IEnumerator FindingSolutionsRoutine () 
		{
			yield return 0;

			List<Solution> solutions;
			int id = 0;

			while (true) 
			{
				if (isPlaying && FightControl.Instance.IsFighting) 
				{
					yield return StartCoroutine(Utils.WaitFor(() => lastMovementId > id, 0.2f));

					if(isPlaying && FightControl.Instance.IsFighting)
					{
						id = lastMovementId;
						solutions = FindSolutions();
						if (solutions.Count > 0) 
						{
							MatchSolutions(solutions);
						} 
						else
						{
							Debug.Log("<color=orange>消除次数:</color>" + _conitnueMathchCount);
							_conitnueMathchCount = 0;
							yield return StartCoroutine(Utils.WaitFor(CanIMatch, 0.1f));
						}
					}
					else
					{
						yield return 0;
					}
				} else
					yield return 0;
			}
		}

		// Function of searching possible solutions
		List<Solution> FindSolutions() 
		{
			List<Solution> solutions = new List<Solution> ();
			Solution zsolution;
			foreach(SlotForCard slot in GameObject.FindObjectsOfType<SlotForCard>()) 
			{
				zsolution = slot.MatchAnaliz();
				if (zsolution != null) solutions.Add(zsolution);
//				zsolution = slot.MatchSquareAnaliz();
//				if (zsolution != null) solutions.Add(zsolution);
			}
			return solutions;
		}


		/// <summary>
		/// 消除卡牌
		/// </summary>
		/// <param name="solutions">Solutions.</param>
		void MatchSolutions(List<Solution> solutions) 
		{
			if (!isPlaying || !FightControl.Instance.IsFighting) return;

			solutions.Sort(delegate(Solution x, Solution y) 
				{
				if (x.potential == y.potential)
					return 0;
				else if (x.potential > y.potential)
					return -1;
				else
					return 1;
			});

			int width = FieldAssistant.Instance.field.width;
			int height = FieldAssistant.Instance.field.height;

			bool[,] mask = new bool[width,height];
			//string key;
			Slot slot;

			//把卡牌标记为true
			for (int x = 0; x < width; x++) 
			{
				for (int y = 0; y < height; y++) 
				{
					mask[x, y] = false;

					slot = SlotManager.Instance.FindSlot(x,y);
					if (slot != null && slot.GetChip()) 
					{
						mask[x, y] = true;
					}
				}
			}

			List<Solution> final_solutions = new List<Solution>();

			bool breaker;
			Solution s = null;
			Card c = null;
			for(int solutionIndex = 0;solutionIndex < solutions.Count;solutionIndex++)
			{
				s = solutions[solutionIndex];

				//筛选solution
				breaker = false;
				for(int cardIndex = 0;cardIndex < s.chips.Count;cardIndex++)
//				foreach (Chip c in s.chips) 
				{
					c = s.chips[cardIndex];

					if (!mask[c.parentSlot.slot.Row, c.parentSlot.slot.Col]) 
					{
						breaker = true;
						break;
					}
				}

				if (breaker)
					continue;

				//最终的解决方案
				final_solutions.Add(s);

				//锁定
				//foreach (Chip c in s.chips)
				for(int cardIndex = 0;cardIndex < s.chips.Count;cardIndex++)
				{
					c = s.chips[cardIndex];

					mask[c.parentSlot.slot.Row, c.parentSlot.slot.Col] = false;
				}
			}

			Solution solution = null;
			Card chip;
			//foreach (Solution solution in final_solutions) 
			//{
			for(int solutionIndex = 0;solutionIndex < final_solutions.Count;solutionIndex++)
			{
				solution = final_solutions[solutionIndex];

				EventCounter();

//				Jelly jelly;
				int puID = -1;

//				foreach(Card chip in solution.chips) 
//				{
				for(int cardIndex = 0; cardIndex < solution.chips.Count;cardIndex++)
				{
					chip = solution.chips[cardIndex];

					if (chip.id == solution.id || chip.id == 10) 
					{
						if (!chip.parentSlot)
							continue;

						slot = chip.parentSlot.slot;

						if (!chip.IsMatcheble())
							break;

						if (chip.movementID > puID)
							puID = chip.movementID;
						
//						chip.SetScore(Mathf.Pow(2, solution.count - 3) / solution.count);
						if (!slot.Block)
							FieldAssistant.Instance.BlockCrush(slot.Row, slot.Col, true);
						
						chip.DestroyChip();
//						jelly = slot.GetJelly();
//						if (jelly)
//							jelly.JellyCrush();
					}
				}

				solution.chips.Sort(delegate(Card a, Card b) 
					{
					return a.movementID > b.movementID ? -1 : a.movementID == b.movementID ? 0 : 1;
				});

				breaker = false;
				foreach (Combinations combination in combinations) 
				{
					if (combination.square && !solution.q)
						continue;
					
					if (!combination.square) 
					{
						if ((combination.horizontal && !solution.h)
							|| (!combination.horizontal && solution.h))
							continue;
						
						if ((combination.vertical && !solution.v)
							|| (!combination.vertical && solution.v))
							continue;
						
						if (combination.minCount > solution.count)
							continue;
					}

//					// For additional logic
//					switch (combination.tag) 
//					{
//						case "Tag1":
//							Debug.Log("Tag 1 combination..."); break;
//						case "Tag2":
//							Debug.Log("Tag 2 combination..."); break;
//						case "Tag3":
//							Debug.Log("Tag 3 combination..."); break;
//					}
//
					foreach (Card ch in solution.chips)
						if (ch.chipType == "SimpleChip") 
						{
							FieldAssistant.Instance.GetNewBomb(ch.parentSlot.slot.Row, ch.parentSlot.slot.Col, combination.powerup, ch.parentSlot.slot.transform.position, solution.id);
							breaker = true;
							break;
						}
							
					if (breaker)
						break;
				}
			}

			_conitnueMathchCount += final_solutions.Count;
		}

		// Conditions to start matching
		public bool CanIMatch ()
		{
			return isPlaying && animate == 0 && gravity == 0 && FightControl.Instance.IsFighting;
		}
		#endregion

		#region 自动洗牌
		// Coroutine of call mixing chips in the absence of moves
		IEnumerator ShuffleRoutine () 
		{
			yield return 0;

			int shuffleOrder = 0;
			float delay = 1;
			while (true) 
			{
				yield return StartCoroutine(Utils.WaitFor(CanIWait, delay));

				List<Card> chips = new List<Card>(FindObjectsOfType<Card>());

				if (eventCount > shuffleOrder && chips.Find(x => x.destroying) == null) {
					shuffleOrder = eventCount;
					yield return StartCoroutine(Shuffle(false));
				}
			}
		}

		private GameObject _goSlotCreate;
		public IEnumerator Shuffle (bool f) 
		{
			bool force = f;

			List<Move> moves = FindMoves();
			if (moves.Count > 0 && !force)
				yield break;
			if (!isPlaying || !FightControl.Instance.IsFighting)
				yield break;

			isPlaying = false;

			_goSlotCreate = FieldAssistant.Instance._goSlotCreate;

			List<Slot> slots = new List<Slot>(SlotManager.Instance.DicSlotNow.Values);

			Dictionary<Slot, Vector3> positions = new Dictionary<Slot, Vector3> ();
			for (int index = 0;index < slots.Count;index++)
			{
				positions.Add(slots[index], slots[index].transform.position);
			}

			float t = 0;
			while (t < 1) {
				t += Time.unscaledDeltaTime * 3;
				_goSlotCreate.transform.localScale = Vector3.one * Mathf.Lerp(1, 0.6f, EasingFunctions.easeInOutQuad(t));
				_goSlotCreate.transform.eulerAngles = Vector3.forward * Mathf.Lerp(0, Mathf.Sin(Time.unscaledTime * 40) * 3, EasingFunctions.easeInOutQuad(t));

				yield return 0;
			}


			if (f || moves.Count == 0) {
				f = false;
				RawShuffle(slots);
			}

			moves = FindMoves();
			List<Solution> solutions = FindSolutions ();

			int itrn = 0;
			int targetID;
			if(solutions.Count > 0 || moves.Count == 0)
			{
				while (solutions.Count > 0 || moves.Count == 0) {
					if (itrn > 100) {
						//ShowLosePopup();
						_goSlotCreate.transform.localScale = Vector3.one;
						_goSlotCreate.transform.eulerAngles = Vector3.zero;
						FightControl.Instance.ShowMsg(FightDefine.E_NoteMsgType.NoShuffle);
						yield break;
					}
					if (solutions.Count > 0) {
						for (int s = 0; s < solutions.Count; s++) {
							targetID = Random.Range(0, slots.Count - 1);
							if (slots[targetID].GetChip() && slots[targetID].GetChip().chipType != "SugarChip" && slots[targetID].GetChip().id != solutions[s].id)
								AnimationControl.Instance.SwapTwoItemNow(solutions[s].chips[Random.Range(0, solutions[s].chips.Count - 1)], slots[targetID].GetChip());
						}
					} else 
						RawShuffle(slots);

					moves = FindMoves();
					solutions = FindSolutions();
					itrn++;
					_goSlotCreate.transform.eulerAngles = Vector3.forward * Mathf.Sin(Time.unscaledTime * 40) * 3;

					yield return 0;
				}
			}
//			else
//			{
//				_goSlotCreate.transform.eulerAngles = Vector3.forward * Mathf.Sin(Time.unscaledTime * 40) * 3;
//				yield return 0;
//			}

			t = 0;
//			AudioAssistant.Shot("Shuffle");
			while (t < 1) {
				t += Time.unscaledDeltaTime * 3;
				_goSlotCreate.transform.localScale = Vector3.one * Mathf.Lerp(0.6f, 1, EasingFunctions.easeInOutQuad(t));
				_goSlotCreate.transform.eulerAngles = Vector3.forward * Mathf.Lerp(Mathf.Sin(Time.unscaledTime * 40) * 3, 0, EasingFunctions.easeInOutQuad(t));
				yield return 0;
			}

			_goSlotCreate.transform.localScale = Vector3.one;
			_goSlotCreate.transform.eulerAngles = Vector3.zero;

			isPlaying = true;
		}

		void RawShuffle(List<Slot> slots) 
		{
			EventCounter();
			int targetID;
			for (int j = 0; j < slots.Count; j++) {
				targetID = Random.Range(0, j - 1);
				if (!slots[j].GetChip() || !slots[targetID].GetChip())
					continue;
				if (slots[j].GetChip().chipType == "SugarChip" || slots[targetID].GetChip().chipType == "SugarChip")
					continue;
				AnimationControl.Instance.SwapTwoItemNow(slots[j].GetChip(), slots[targetID].GetChip());
			}
		}
		#endregion

		#region 杂草
		// Weeds logic
		IEnumerator IllnessRoutine () 
		{
			Weed.lastWeedCrush = swapEvent;
			Weed.seed = 0;

			int last_swapEvent = swapEvent;

			yield return new WaitForSeconds(1f);

			while (Weed.WeedCount > 0) 
			{
				yield return StartCoroutine(Utils.WaitFor(() => swapEvent > last_swapEvent && Weed.all != null && Weed.all.Count > 0, 0.1f));
				last_swapEvent = swapEvent;
				yield return StartCoroutine(Utils.WaitFor(CanIWait, 0.1f));
				if (Weed.lastWeedCrush < swapEvent) {
					Weed.seed = 1;//swapEvent - Weed.lastWeedCrush;
					DebugUtil.Debug("Weed:" + Weed.seed);
					Weed.lastWeedCrush = swapEvent;
				}
				Weed.Grow();
			}
		}
		#endregion



	}


}
