/*
 * 
 * 文件名(File Name)：             LevelEditor
 *
 * 作者(Author)：                  #AuthorName#
 *
 * 创建时间(CreateTime):           2016/03/16 18:56:41
 *
 */

using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using System.Collections;
using System.Collections.Generic;

namespace Fight
{
	[CustomEditor(typeof(Level))]
	public class LevelEditor : Editor
	{
		LevelProfile profile;
		Level level;
		Rect rect;

		SerializedProperty serializedProperty;

		enum EditMode {Slot, Chip, PowerUp, Jelly, Block, Generator, Wall,PowerUpNew};
		EditMode currentMode = EditMode.Slot;
		AnimBool parametersFade = new AnimBool(true);

		string toolID = "";
		Vector2 teleportID = -Vector2.right;

		/// <summary>
		/// 绘制大小
		/// </summary>
		static int cellSize = 40;
		/// <summary>
		/// 格子间隙
		/// </summary>
		static int slotOffect = 4;

		static Color defaultColor;
		static Color transparentColor = new Color (0, 0, 0, 0f);
		static Color unpressedColor = new Color (0.7f, 0.7f, 0.7f, 1);
		static Color[] chipColor = {new Color(1,0.6f,0.6f,1),
			new Color(0.6f,1,0.6f,1),
			new Color(0.6f,0.8f,1,1),
			new Color(1,1,0.6f,1),
			new Color(1,0.6f,1,1),
			new Color(1,0.8f,0.6f,1)};
		static Color buttonColor = new Color(0.5f,0.5f,0.5f,1);
		static string[] alphabet = {"A", "B", "C", "D", "E", "F"};
		static string[] gravityLabel = { "v", "<", "^", ">" };


		#region 样式
		static GUIStyle mSlotStyle;
		public static GUIStyle slotStyle 
		{
			get 
			{
				if (mSlotStyle == null) 
				{
					mSlotStyle = new GUIStyle (GUI.skin.button);
					mSlotStyle.wordWrap = true;

					mSlotStyle.normal.background = Texture2D.whiteTexture;
					mSlotStyle.focused.background = mSlotStyle.normal.background;
					mSlotStyle.active.background = mSlotStyle.normal.background;

					mSlotStyle.normal.textColor = Color.black;
					mSlotStyle.focused.textColor = mSlotStyle.normal.textColor;
					mSlotStyle.active.textColor = mSlotStyle.normal.textColor;

					mSlotStyle.fontSize = 8;
					mSlotStyle.margin = new RectOffset ();
					mSlotStyle.padding = new RectOffset ();
				}
				return mSlotStyle;
			}
		}

		static GUIStyle mIconStyle;
		static GUIStyle iconStyle 
		{
			get 
			{
				if (mIconStyle == null) 
				{
					mIconStyle = new GUIStyle (GUI.skin.button);
					mIconStyle.wordWrap = true;

					mIconStyle.normal.background = Texture2D.whiteTexture;

					mIconStyle.normal.textColor = Color.white;

					mIconStyle.fontSize = 8;

					mIconStyle.border = new RectOffset ();
					mIconStyle.margin = new RectOffset ();
					mIconStyle.padding = new RectOffset ();
				}
				return mIconStyle;
			}
		}

		#endregion

		/// <summary>
		/// 特殊效果
		/// </summary>
		static Dictionary<int, string> powerupLabel = new Dictionary<int,string>();
		static Dictionary<int, string> powerupLabelNew = new Dictionary<int,string>();

		public LevelEditor() 
		{
			parametersFade.valueChanged.AddListener (Repaint);
		}

		override public bool UseDefaultMargins() 
		{
			return false;
		}

		public override void OnInspectorGUI () 
		{
			level = (Level) target;

			Undo.RecordObject(level, "Edit Level");
			profile = level.Profile;

			if (profile == null)
			{
				profile = new LevelProfile();
			}

			//nick_yangzj:ToDo 
//			if (SessionAssistant.main == null)
//				SessionAssistant.main = GameObject.FindObjectOfType<SessionAssistant>();

			if (profile.LevelInstanceID == 0 || profile.LevelInstanceID != target.GetInstanceID ()) 
			{
				if (profile.LevelInstanceID != target.GetInstanceID ())
					profile = profile.GetClone();
				profile.LevelInstanceID = target.GetInstanceID ();
			}

			profile.Level = level.transform.GetSiblingIndex() + 1;

			level.name = "Level:" + profile.Level.ToString();// + "," + profile.target + "," + profile.limitation;

			parametersFade.target = GUILayout.Toggle(parametersFade.target, "Level Parameters", EditorStyles.foldout);

			if (EditorGUILayout.BeginFadeGroup (parametersFade.faded)) 
			{

				profile.Width = Mathf.RoundToInt (EditorGUILayout.Slider ("宽度", 1f * profile.Width, 4f, 12f));
				profile.Height = Mathf.RoundToInt (EditorGUILayout.Slider ("高度", 1f * profile.Height, 4f, 12f));
				profile.CardCount = Mathf.RoundToInt (EditorGUILayout.Slider ("带入战斗的卡牌个数（待定）", 1f * profile.CardCount, 3f, 6f));
				profile.StonePortion = Mathf.Round(EditorGUILayout.Slider ("格子是障碍的概率", profile.StonePortion, 0f, 0.7f) * 100) / 100;
//				profile.MoveCount = Mathf.Clamp(EditorGUILayout.IntField("初始步数", profile.MoveCount), 5, 100);
//				profile.SecDuration = Mathf.Max(0, EditorGUILayout.IntField("倒计时(s)", profile.SecDuration));
//				EditorGUILayout.BeginHorizontal ();
//				EditorGUILayout.LabelField ("Score Stars", GUILayout.ExpandWidth(true));
//				profile.firstStarScore = Mathf.Max(EditorGUILayout.IntField (profile.firstStarScore, GUILayout.ExpandWidth(true)), 1);
//				profile.secondStarScore = Mathf.Max(EditorGUILayout.IntField (profile.secondStarScore, GUILayout.ExpandWidth(true)), profile.firstStarScore+1);
//				profile.thirdStarScore = Mathf.Max(EditorGUILayout.IntField (profile.thirdStarScore, GUILayout.ExpandWidth(true)), profile.secondStarScore+1);
//				EditorGUILayout.EndHorizontal ();

//				profile.limitation = (Limitation) EditorGUILayout.EnumPopup ("Limitation", profile.limitation);
//				switch (profile.limitation) {
//					case Limitation.Moves:
//						profile.moveCount = Mathf.Clamp(EditorGUILayout.IntField("Move Count", profile.moveCount), 5, 50);
//						break;
//					case Limitation.Time:
//						profile.duration = Mathf.Max(0, EditorGUILayout.IntField("Session duration", profile.duration));
//						break;
//				}

//				profile.target = (E_FieldTarget) EditorGUILayout.EnumPopup ("Target", profile.target);

				//colorModeFade.target = profile.target == E_FieldTarget.Color;

//				if (EditorGUILayout.BeginFadeGroup (colorModeFade.faded)) {
//					defaultColor = GUI.color;
//					profile.targetColorCount = Mathf.RoundToInt(EditorGUILayout.Slider("Targets Count", profile.targetColorCount, 1, profile.chipCount));
//					for (int i = 0; i < 6; i++) {
//						GUI.color = chipColor[i];
//						if (i < profile.targetColorCount)
//							profile.SetTargetCount(i, Mathf.Clamp(EditorGUILayout.IntField("Color " + alphabet[i].ToString(), profile.GetTargetCount(i)), 1, 999));
//						else 
//							profile.SetTargetCount(i, 0);
//					}
//					GUI.color = defaultColor;
//				}
//				EditorGUILayout.EndFadeGroup ();
//
//				sugarDropFade.target = profile.target == E_FieldTarget.SugarDrop;
//
//				if (EditorGUILayout.BeginFadeGroup(sugarDropFade.faded)) {
//					profile.targetSugarDropsCount = Mathf.RoundToInt(EditorGUILayout.Slider("Sugar Count", profile.targetSugarDropsCount, 1, 20));
//				}
//				EditorGUILayout.EndFadeGroup();
			}

			EditorGUILayout.EndFadeGroup ();


			EditorGUILayout.Space ();
			EditorGUILayout.BeginHorizontal (EditorStyles.toolbar, GUILayout.ExpandWidth(true));

			defaultColor = GUI.color;
			GUI.color = currentMode == EditMode.Slot ? unpressedColor : defaultColor;
			if (GUILayout.Button("卡槽", EditorStyles.toolbarButton, GUILayout.Width(40)))
				currentMode = EditMode.Slot;
			GUI.color = currentMode == EditMode.Chip ? unpressedColor : defaultColor;
			if (GUILayout.Button("卡片", EditorStyles.toolbarButton, GUILayout.Width(40)))
				currentMode = EditMode.Chip;
			GUI.color = currentMode == EditMode.PowerUpNew ? unpressedColor : defaultColor;
			if (GUILayout.Button("特殊卡片(新版)", EditorStyles.toolbarButton, GUILayout.Width(70)))
				currentMode = EditMode.PowerUpNew;
//			if (profile.target == E_FieldTarget.Jelly) {
//				GUI.color = currentMode == EditMode.Jelly ? unpressedColor : defaultColor;
//				if (GUILayout.Button("冰冻", EditorStyles.toolbarButton, GUILayout.Width(50)))
//					currentMode = EditMode.Jelly;
//			}
			GUI.color = currentMode == EditMode.Block ? unpressedColor : defaultColor;
			if (GUILayout.Button("阻碍", EditorStyles.toolbarButton, GUILayout.Width(50)))
				currentMode = EditMode.Block;
			GUI.color = currentMode == EditMode.Wall ? unpressedColor : defaultColor;
			if (GUILayout.Button("墙", EditorStyles.toolbarButton, GUILayout.Width(40)))
				currentMode = EditMode.Wall;
			GUI.color = currentMode == EditMode.PowerUp ? unpressedColor : defaultColor;
			if (GUILayout.Button("特殊卡片", EditorStyles.toolbarButton, GUILayout.Width(70)))
				currentMode = EditMode.PowerUp;
			GUI.color = defaultColor;

			GUILayout.FlexibleSpace ();

			if (GUILayout.Button("重置场景", EditorStyles.toolbarButton, GUILayout.Width(70)))
			{
				profile = new LevelProfile ();
			}

			EditorGUILayout.EndVertical ();

			// Slot modes
			if (currentMode == EditMode.Slot) {
				EditorGUILayout.BeginHorizontal (EditorStyles.toolbar, GUILayout.ExpandWidth(true));

				defaultColor = GUI.color;

				GUI.color = toolID == "Slots" ? unpressedColor : defaultColor;
				if (GUILayout.Button("卡槽", EditorStyles.toolbarButton, GUILayout.Width(40)))
					toolID = "Slots";

				GUI.color = toolID == "Generators" ? unpressedColor : defaultColor;
				if (GUILayout.Button("出生点", EditorStyles.toolbarButton, GUILayout.Width(70)))
					toolID = "Generators";

				GUI.color = toolID == "Teleports" ? unpressedColor : defaultColor;
				if (GUILayout.Button("传送", EditorStyles.toolbarButton, GUILayout.Width(70)))
					toolID = "Teleports";

//				if (profile.target == E_FieldTarget.SugarDrop) {
//					GUI.color = toolID == "Sugar Drop" ? unpressedColor : defaultColor;
//					if (GUILayout.Button("Sugar Drop", EditorStyles.toolbarButton, GUILayout.Width(70)))
//						toolID = "Sugar Drop";
//				}
				GUI.color = toolID == "Gravity" ? unpressedColor : defaultColor;
				if (GUILayout.Button("重力方向", EditorStyles.toolbarButton, GUILayout.Width(70)))
					toolID = "Gravity";

				GUI.color = defaultColor;		
				GUILayout.FlexibleSpace ();

				EditorGUILayout.EndHorizontal ();
			}

//			Debug.LogError(profile.GetSlot(0,0));

			// Slot modes
			if (currentMode == EditMode.PowerUp)
			{
				EditorGUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.ExpandWidth(true));

				defaultColor = GUI.color;

				//特殊效果卡牌放置
//				DebugUtil.Info("powerup count:" + SessionControl.powerups.Count);

				foreach (SessionControl.PowerUps powerup in SessionControl.powerupsNew) 
				{
					if (powerup.levelEditorID > 0) 
					{
						GUI.color = toolID == powerup.levelEditorName ? unpressedColor : defaultColor;
						if (GUILayout.Button(powerup.levelEditorName, EditorStyles.toolbarButton, GUILayout.Width(30)))
							toolID = powerup.levelEditorName;
					}
				}

				GUI.color = defaultColor;
				GUILayout.FlexibleSpace();

				EditorGUILayout.EndHorizontal();
			}

			if (currentMode == EditMode.PowerUpNew)
			{
				EditorGUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.ExpandWidth(true));

				defaultColor = GUI.color;

				//特殊效果卡牌放置
				//				DebugUtil.Info("powerup count:" + SessionControl.powerups.Count);
				
				foreach (FightNew.PowerUps powerup in FightNew.PowerUps.powerupsNew) 
				{
					if (powerup.levelEditorID > 0) 
					{
						GUI.color = toolID == powerup.levelEditorName ? unpressedColor : defaultColor;
						if (GUILayout.Button(powerup.levelEditorName, EditorStyles.toolbarButton, GUILayout.Width(50)))
							toolID = powerup.levelEditorName;
					}
				}

				GUI.color = defaultColor;
				GUILayout.FlexibleSpace();

				EditorGUILayout.EndHorizontal();
			}

			// Chip modes
			if (currentMode == EditMode.Chip) {
				EditorGUILayout.BeginHorizontal (EditorStyles.toolbar, GUILayout.ExpandWidth(true));

				string  key;
				defaultColor = GUI.color;

				GUI.color = toolID == "Random" ? unpressedColor : defaultColor;
				if (GUILayout.Button("随机", EditorStyles.toolbarButton, GUILayout.Width(50)))
					toolID = "Random";

				for (int i = 0; i < profile.CardCount; i++) {
					key = "卡牌 " + alphabet[i];
					GUI.color = toolID == key ? unpressedColor * chipColor[i] : defaultColor * chipColor[i];
					if (GUILayout.Button(key, EditorStyles.toolbarButton, GUILayout.Width(50)))
						toolID = key;
				}

				GUI.color = toolID == "Stone" ? unpressedColor : defaultColor;
				if (GUILayout.Button("石头", EditorStyles.toolbarButton, GUILayout.Width(50)))
					toolID = "Stone";

				GUI.color = defaultColor;		
				GUILayout.FlexibleSpace ();

				EditorGUILayout.EndHorizontal ();
			}

			// Block modes
			if (currentMode == EditMode.Block) {
				EditorGUILayout.BeginHorizontal (EditorStyles.toolbar, GUILayout.ExpandWidth(true));

				defaultColor = GUI.color;
				GUI.color = toolID == "Simple Block" ? unpressedColor : defaultColor;
				if (GUILayout.Button("简单块", EditorStyles.toolbarButton, GUILayout.Width(80)))
					toolID = "Simple Block";
				GUI.color = toolID == "Weed" ? unpressedColor : defaultColor;
				if (GUILayout.Button("杂草", EditorStyles.toolbarButton, GUILayout.Width(40)))
					toolID = "Weed";
				GUI.color = toolID == "Branch" ? unpressedColor : defaultColor;
				if (GUILayout.Button("套壳", EditorStyles.toolbarButton, GUILayout.Width(50)))
					toolID = "Branch";
				GUI.color = toolID == "Boss" ? unpressedColor : defaultColor;
				if (GUILayout.Button("Boss", EditorStyles.toolbarButton, GUILayout.Width(50)))
					toolID = "Boss";

				GUI.color = toolID == "XuanWo" ? unpressedColor : defaultColor;
				if (GUILayout.Button("漩涡", EditorStyles.toolbarButton, GUILayout.Width(50)))
					toolID = "XuanWo";
				GUI.color = toolID == "FengChe" ? unpressedColor : defaultColor;
				if (GUILayout.Button("风车", EditorStyles.toolbarButton, GUILayout.Width(50)))
					toolID = "FengChe";
				GUI.color = toolID == "ZhangAiFaSheQi" ? unpressedColor : defaultColor;
				if (GUILayout.Button("障碍发射器", EditorStyles.toolbarButton, GUILayout.Width(80)))
					toolID = "ZhangAiFaSheQi";
				GUI.color = defaultColor;		
				GUILayout.FlexibleSpace ();

				EditorGUILayout.EndHorizontal ();
			}



			EditorGUILayout.BeginVertical (EditorStyles.inspectorDefaultMargins);

			rect = GUILayoutUtility.GetRect(profile.Width * (cellSize + slotOffect), profile.Height * (cellSize + slotOffect));
			rect.x += slotOffect; 
			rect.y += slotOffect;

			EditorGUILayout.BeginHorizontal ();
			DrawModeTools ();
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.EndVertical ();

			//特殊道具
			powerupLabel.Clear();
			foreach (SessionControl.PowerUps powerup in SessionControl.powerupsNew)
			{
				if (powerup.levelEditorID > 0
					&& !powerupLabel.ContainsKey(powerup.levelEditorID))
				{
					powerupLabel.Add(powerup.levelEditorID, powerup.levelEditorName);
				}
			}

			powerupLabelNew.Clear();
			foreach (FightNew.PowerUps powerup in FightNew.PowerUps.powerupsNew)
			{
				if (powerup.levelEditorID > 0
					&& !powerupLabelNew.ContainsKey(powerup.levelEditorID))
				{
					powerupLabelNew.Add(powerup.levelEditorID, powerup.levelEditorName);
				}
			}

			switch (currentMode) 
			{
				case EditMode.Slot: DrawSlot(); break;
				case EditMode.Chip: DrawChip(); break;
				case EditMode.PowerUp: DrawPowerUp(); break;
				case EditMode.PowerUpNew: DrawPowerUpNew(); break;
//				case EditMode.Jelly: DrawJelly(); break;
				case EditMode.Block: DrawBlock(); break;
				case EditMode.Wall: DrawWall(); break;
			}

			if(level != (Level)target)
				Debug.LogError("not eaual");
			
			level.Profile = profile; 
		}

			
		#region 每个页面的reset按钮
		/// <summary>
		/// 绘制每个页面的reset按钮
		/// </summary>
		void DrawModeTools ()
		{
			switch (currentMode) {
				case EditMode.Slot:
					if (GUILayout.Button("Reset", GUILayout.Width(70))) 
						ResetSlots();		
					break;
				case EditMode.Chip:
					if (GUILayout.Button("Clear", GUILayout.Width(50))) 
						SetAllChips(-1);
					if (GUILayout.Button("设置所有按钮随机", GUILayout.Width(120))) 
						SetAllChips(0);
					break;
				case EditMode.PowerUp:
					if (GUILayout.Button("Clear", GUILayout.Width(50))) 
						PowerUpClear();
					break;
				case EditMode.Jelly:	
					if (GUILayout.Button("Clear", GUILayout.Width(50))) 
						JellyClear();
					break;
				case EditMode.Block:
					if (GUILayout.Button("Clear", GUILayout.Width(50))) 
						BlockClear();
					break;
				case EditMode.Wall:
					if (GUILayout.Button("Clear", GUILayout.Width(50))) 
						WallClear();
					break;	
			}
		}

		void ResetSlots () 
		{
			if (toolID == "Slots")
				for (int x = 0; x < LevelProfile.MaxSize; x++)
					for (int y = 0; y < LevelProfile.MaxSize; y++)
						profile.SetSlot(x, y, true);

			if (toolID == "Generators")
				for (int x = 0; x < LevelProfile.MaxSize; x++)
					for (int y = 0; y < LevelProfile.MaxSize; y++)
						profile.SetGenerator(x, y, y == 0);

			if (toolID == "Teleports")
				for (int x = 0; x < LevelProfile.MaxSize; x++)
					for (int y = 0; y < LevelProfile.MaxSize; y++)
						profile.SetTeleport(x, y, 0);

//			if (toolID == "Sugar Drop")
//				for (int x = 0; x < 12; x++)
//					for (int y = 0; y < 12; y++)
//						profile.SetSugarDrop(x, y, y == profile.height - 1);
		}

		void SetAllChips (int c) 
		{
			for (int x = 0; x < LevelProfile.MaxSize; x++)
				for (int y = 0; y < LevelProfile.MaxSize; y++)
					profile.SetChip(x, y, c);
		}

		void PowerUpClear ()
		{
			for (int x = 0; x < LevelProfile.MaxSize; x++)
				for (int y = 0; y < LevelProfile.MaxSize; y++)
					profile.SetPowerup(x, y, 0);
		}

		void JellyClear ()
		{
			for (int x = 0; x < LevelProfile.MaxSize; x++)
				for (int y = 0; y < LevelProfile.MaxSize; y++)
					profile.SetJelly(x, y, 0);
		}

		void BlockClear ()
		{
			for (int x = 0; x < LevelProfile.MaxSize; x++)
				for (int y = 0; y < LevelProfile.MaxSize; y++)
					profile.SetBlock(x, y, 0);
		}

		void WallClear ()
		{
			for (int x = 0; x < LevelProfile.MaxSize; x++)
				for (int y = 0; y < LevelProfile.MaxSize; y++) {
					profile.SetWallH (x, y, false);
					profile.SetWallV (x, y, false);
				}
		}
		#endregion

		#region 绘制卡槽
		void DrawSlot () 
		{
			for (int x = 0; x < profile.Width; x++) 
			{
				for (int y = 0; y < profile.Height; y++) 
				{
					if (teleportID != -Vector2.right) 
					{
						if (DrawSlotButtonTeleport(x, y, rect, profile)) {
							if (x == teleportID.x && y == teleportID.y)
								profile.SetTeleport(Mathf.CeilToInt(teleportID.x), Mathf.CeilToInt(teleportID.y), 0);
							else
								profile.SetTeleport(Mathf.CeilToInt(teleportID.x), Mathf.CeilToInt(teleportID.y), y * 12 + x + 1);
							teleportID = -Vector2.right;
						}
						continue;
					}


					if (DrawSlotButton(x, y, rect, profile)) 
					{
						switch (toolID) {
							case "Slots": 
								profile.SetSlot(x, y, !profile.GetSlot(x,y));
								break;
							case "Generators": 
								profile.SetGenerator(x, y, !profile.GetGenerator(x,y));
								break;
							case "Teleports": 
								teleportID = new Vector2(x, y);
								break;
							case "Sugar Drop":
								//profile.SetSugarDrop(x, y, !profile.GetSugarDrop(x, y));
								break;
							case "Gravity":
								profile.SetGravity(x, y, Mathf.CeilToInt(Mathf.Repeat( profile.GetGravity(x, y) + 1, 4)));
								break;
						}
					}
				}
			}
			DrawWallPreview (rect, profile);
		}

		bool DrawSlotButton (int x, int y, Rect r, LevelProfile lp) {
			defaultColor = GUI.backgroundColor;
			Color color = Color.white;
			string label = "";
			bool btn = false;
			int block = lp.GetBlock (x, y);
			int jelly = lp.GetJelly (x, y);
			int chip = lp.GetChip (x, y);

			if (!lp.GetSlot(x, y)) 
				color *= 0;
			else 
			{
				if (block == 0) 
				{
					if (chip == 9) 
					{
						color *= buttonColor;
						lp.SetPowerup(x, y, 0);
					} 
					else if (chip > 0) 
					{
						if (chip > lp.CardCount)
							lp.SetChip(x, y, -1);
						
						color *= chipColor[chip - 1];
					}
				}

				if (block == 5) {
					if (chip > 0) {
						if (chip > lp.CardCount)
							lp.SetChip(x, y, -1);
						color *= chipColor[chip - 1];
					}
				}
				if (block == 0 && chip == -1 && lp.GetPowerup(x, y) == 0) {
					color *= unpressedColor;
				}
				if (block == 0 && lp.GetPowerup(x, y) > 0) {
					label += (label.Length == 0 ? "" : "\n");
					label += powerupLabelNew[lp.GetPowerup(x, y)];
				}

				if (block > 0 && block <= 3)
					label += (label.Length == 0 ? "" : "\n") + "B:" + block.ToString();
				if (block == 4)
					label += (label.Length == 0 ? "" : "\n") + "Weed";
				if (block == 5)
					label += (label.Length == 0 ? "" : "\n") + "Brch";

				if (block >= 6 && block <= 9)
					label += (label.Length == 0 ? "" : "\n") + "Boss" + (block - 5).ToString();
//				if (jelly > 0 && lp.Target == E_FieldTarget.Jelly) {
//					label += (label.Length == 0 ? "" : "\n");
//					switch (jelly) {
//						case 1: label += "JS"; break;
//						case 2: label += "JT"; break;
//					}
//				}

				if (block >= 10 && block < 20)
					label += (label.Length == 0 ? "" : "\n") + "漩涡" + (block - 9).ToString();

				if (block >= 20 && block < 30)
					label += (label.Length == 0 ? "" : "\n") + "风车";

				if (block >= 30 && block < 40)
					label += (label.Length == 0 ? "" : "\n") + "发射器" + (block - 29).ToString();
			}

			GUI.backgroundColor = color;
			btn = GUI.Button(new Rect(r.xMin + x * (cellSize + slotOffect), r.yMin + y * (cellSize + slotOffect), cellSize, cellSize), label, slotStyle);

			float cursor = -2;

			if (lp.GetSlot(x, y) && lp.GetGenerator (x, y)) {
				GUI.backgroundColor = Color.black;
				GUI.Box(new Rect(r.xMin + x * (cellSize + slotOffect) + cursor, r.yMin + y * (cellSize + slotOffect) - 2, 10, 10), "G", iconStyle);
				cursor += 10 + 2;
			}

//			if (lp.target == E_FieldTarget.SugarDrop && lp.GetSlot(x, y) && lp.GetSugarDrop(x, y)) {
//				GUI.backgroundColor = Color.black;
//				GUI.Box(new Rect(r.xMin + x * (cellSize + slotOffect) + cursor, r.yMin + y * (cellSize + slotOffect) - 2, 10, 10), "S", iconStyle);
//				cursor += 10 + 2;
//			}

			if (lp.GetSlot(x, y)) {
				GUI.backgroundColor = Color.black;
				GUI.Box(new Rect(r.xMin + x * (cellSize + slotOffect) + cursor, r.yMin + y * (cellSize + slotOffect) - 2, 10, 10), gravityLabel[profile.GetGravity(x, y)], iconStyle);
				cursor += 10 + 2;
			}

			if (lp.GetSlot(x, y) && lp.GetTeleport (x, y) > 0) {
				GUI.backgroundColor = Color.black;
				GUI.Box(new Rect(r.xMin + x * (cellSize + slotOffect) + cursor, r.yMin + y * (cellSize + slotOffect) - 2, cellSize - 12, 10), "T:" + lp.GetTeleport (x, y).ToString(), iconStyle);
			}

			if (lp.GetSlot (x, y)) {
				GUI.backgroundColor = transparentColor;
				GUI.Box (new Rect (r.xMin + x * (cellSize + slotOffect), r.yMin + y * (cellSize + slotOffect) + cellSize - 10, 20, 10), (y * 12 + x + 1).ToString (), slotStyle);
			}

			GUI.backgroundColor = defaultColor;
			return btn;
		}

		bool DrawSlotButtonTeleport (int x, int y, Rect r, LevelProfile lp) {
			if (!lp.GetSlot(x, y)) return false;

			defaultColor = GUI.backgroundColor;
			Color color = Color.cyan;
			if (teleportID.x == x && teleportID.y == y) color = Color.magenta;
			if (lp.GetTeleport(Mathf.FloorToInt(teleportID.x), Mathf.FloorToInt(teleportID.y)) == 12 * y + x + 1) color = Color.yellow;
			string label = "";

			bool btn = false;

			GUI.backgroundColor = color;
			btn = GUI.Button (new Rect (r.xMin + x * (cellSize + slotOffect), r.yMin + y * (cellSize + slotOffect), cellSize, cellSize), label, slotStyle);

			if (lp.GetSlot(x, y) && lp.GetGenerator (x, y)) {
				GUI.backgroundColor = Color.black;
				GUI.Box(new Rect(r.xMin + x * (cellSize + slotOffect) - 2, r.yMin + y * (cellSize + slotOffect) - 2, 10, 10), "G", iconStyle);
			}
			if (lp.GetSlot(x, y) && lp.GetTeleport (x, y) > 0) {
				GUI.backgroundColor = Color.black;
				GUI.Box(new Rect(r.xMin + x * (cellSize + slotOffect) + 10, r.yMin + y * (cellSize + slotOffect) - 2, cellSize - 12, 10), "T:" + lp.GetTeleport (x, y).ToString(), iconStyle);
			}

			if (lp.GetSlot (x, y)) {
				GUI.backgroundColor = transparentColor;
				GUI.Box (new Rect (r.xMin + x * (cellSize + slotOffect), r.yMin + y * (cellSize + slotOffect) + cellSize - 10, 20, 10), (y * 12 + x + 1).ToString (), slotStyle);
			}

			GUI.backgroundColor = defaultColor;

			return btn;
		}

		public void DrawSlotPreview (Rect r, LevelProfile lp) {
			int x;
			int y;
			GUI.enabled = false;
			for (x = 0; x < lp.Width; x++)
				for (y = 0; y < lp.Height; y++)
					if (lp.GetSlot(x, y))
						DrawSlotButton(x, y, r, lp);
			GUI.enabled = true;
		}
		#endregion

		#region 绘制墙
		public static void DrawWallPreview (Rect r, LevelProfile lp) {
			int x;
			int y;
			GUI.enabled = false;
			for (x = 0; x < lp.Width-1; x++)
				for (y = 0; y < lp.Height; y++)
					if (lp.GetWallV(x,y) && lp.GetSlot(x,y) && lp.GetSlot(x+1,y))
						DrawWallButton(x, y, "V", r, lp);
			for (x = 0; x < lp.Width; x++)
				for (y = 0; y < lp.Height-1; y++)
					if (lp.GetWallH(x,y) && lp.GetSlot(x,y) && lp.GetSlot(x,y+1))
						DrawWallButton(x, y, "H", r, lp);
			GUI.enabled = true;
		}

		static bool DrawWallButton (int x, int y, string t, Rect r, LevelProfile lp)
		{
			bool btn = false;
			if (t == "H") btn = lp.GetWallH(x,y);
			if (t == "V") btn = lp.GetWallV(x,y);

			defaultColor = GUI.color;
			Color color = defaultColor;

			if (btn)
				color *= Color.red;
			GUI.color = color;

			if (t == "V") btn = GUI.Button(new Rect(r.xMin + (x + 1) * (cellSize + slotOffect) - 4 - slotOffect / 2,
				r.yMin + y * (cellSize + slotOffect) - 10 + 20, 8, 20), "", slotStyle);
			if (t == "H") btn = GUI.Button(new Rect(r.xMin + x * (cellSize + slotOffect) - 10 + 20,
				r.yMin + (y + 1) * (cellSize + slotOffect) - 4 - slotOffect / 2, 20, 8), "", slotStyle);
			GUI.color = defaultColor;
			return btn;
		}

		void DrawWall () {
			int x;
			int y;
			DrawSlotPreview (rect, profile);
			for (x = 0; x < profile.Width-1; x++)
				for (y = 0; y < profile.Height; y++)
					if (profile.GetSlot(x,y) && profile.GetSlot(x+1,y))
					if (DrawWallButton(x, y, "V", rect, profile))
						profile.SetWallV(x, y, !profile.GetWallV(x, y));
			for (x = 0; x < profile.Width; x++)
				for (y = 0; y < profile.Height-1; y++)
					if (profile.GetSlot(x,y) && profile.GetSlot(x,y+1))
					if (DrawWallButton(x, y, "H", rect, profile))
						profile.SetWallH(x, y, !profile.GetWallH(x, y));
		}
		#endregion

		#region 绘制卡牌
		void DrawChip () {
			for (int x = 0; x < profile.Width; x++) {
				for (int y = 0; y < profile.Height; y++) {
					if (DrawSlotButton(x, y, rect, profile)) {
						switch (toolID) {
							case "Random": 
								if (profile.GetChip(x, y) != 0)
									profile.SetChip(x, y, 0);
								else
									profile.SetChip(x, y, -1);
								break;
							case "卡牌 A": 
								if (profile.GetChip(x, y) != 1)
									profile.SetChip(x, y, 1);
								else
									profile.SetChip(x, y, -1);
								break;
							case "卡牌 B": 
								if (profile.GetChip(x, y) != 2)
									profile.SetChip(x, y, 2);
								else
									profile.SetChip(x, y, -1);
								break;
							case "卡牌 C": 
								if (profile.GetChip(x, y) != 3)
									profile.SetChip(x, y, 3);
								else
									profile.SetChip(x, y, -1);
								break;
							case "卡牌 D": 
								if (profile.GetChip(x, y) != 4)
									profile.SetChip(x, y, 4);
								else
									profile.SetChip(x, y, -1);
								break;
							case "卡牌 E": 
								if (profile.GetChip(x, y) != 5)
									profile.SetChip(x, y, 5);
								else
									profile.SetChip(x, y, -1);
								break;
							case "卡牌 F": 
								if (profile.GetChip(x, y) != 6)
									profile.SetChip(x, y, 6);
								else
									profile.SetChip(x, y, -1);
								break;
							case "Stone": 
								if (profile.GetChip(x, y) != 9)
									profile.SetChip(x, y, 9);
								else
									profile.SetChip(x, y, -1);
								break;
						}
					}
				}
			}
			DrawWallPreview (rect, profile);
		}
		#endregion

		#region 绘制障碍
		void DrawBlock() 
		{
			for (int x = 0; x < profile.Width; x++) 
			{
				for (int y = 0; y < profile.Height; y++) 
				{
					if (DrawSlotButton(x, y, rect, profile)) 
					{
						switch (toolID) 
						{
							case "Simple Block":
								profile.SetBlock(x, y, profile.GetBlock(x, y) + 1);
								if (profile.GetBlock(x, y) > 3)
									profile.SetBlock(x, y, 0);
								break;
							case "Weed":
								if (profile.GetBlock(x, y) != 4)
									profile.SetBlock(x, y, 4);
								else 
									profile.SetBlock(x, y, 0);
								break;
							case "Branch": 
								if (profile.GetBlock(x, y) != 5)
									profile.SetBlock(x, y, 5);
								else
									profile.SetBlock(x, y, 0);
								break;
							case "Boss":
								profile.SetBlock(x, y, Mathf.Max(profile.GetBlock(x, y) + 1,6));
								if (profile.GetBlock(x, y) > 9)
									profile.SetBlock(x, y, 0);
								break;

								//10~19
							case "XuanWo":
								profile.SetBlock(x, y, Mathf.Max(profile.GetBlock(x, y) + 1,10));
								if (profile.GetBlock(x, y) > 19)
									profile.SetBlock(x, y, 0);
								break;
								//20
							case "FengChe":
								profile.SetBlock(x, y, Mathf.Max(profile.GetBlock(x, y) + 1,20));
								if (profile.GetBlock(x, y) > 20)
									profile.SetBlock(x, y, 0);
								break;
								//30~39
							case "ZhangAiFaSheQi":
								profile.SetBlock(x, y, Mathf.Max(profile.GetBlock(x, y) + 1,30));
								if (profile.GetBlock(x, y) > 39)
									profile.SetBlock(x, y, 0);
								break;
						}
					}
				}
			}
			DrawWallPreview (rect, profile);
		}
		#endregion


		#region 特殊卡牌
		void DrawPowerUp () 
		{
			for (int x = 0; x < profile.Width; x++)
			{
				for (int y = 0; y < profile.Height; y++)
				{
					if (DrawSlotButton(x, y, rect, profile)) 
					{
						if (profile.GetPowerup(x, y) == 0) 
						{
							SessionControl.PowerUps powerup = SessionControl.powerupsNew.Find(pu => pu.levelEditorName == toolID);
							if (powerup != null)
								profile.SetPowerup(x, y, powerup.levelEditorID);
						} else
							profile.SetPowerup(x, y, 0);
					}
				}
			}

			DrawWallPreview (rect, profile);
		}
		void DrawPowerUpNew () 
		{
			for (int x = 0; x < profile.Width; x++)
			{
				for (int y = 0; y < profile.Height; y++)
				{
					if (DrawSlotButton(x, y, rect, profile)) 
					{
						if (profile.GetPowerup(x, y) == 0) 
						{
							FightNew.PowerUps powerup = FightNew.PowerUps.powerupsNew.Find(pu => pu.levelEditorName == toolID);
							if (powerup != null)
								profile.SetPowerup(x, y, powerup.levelEditorID);
						} else
							profile.SetPowerup(x, y, 0);
					}
				}
			}

			DrawWallPreview (rect, profile);
		}
		#endregion
	}
}
