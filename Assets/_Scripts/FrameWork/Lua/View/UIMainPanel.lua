local transform;
local gameObject;

UIMainPanel = {};
local this = UIMainPanel;
--启动事件--
function UIMainPanel.Awake(obj)
	gameObject = obj;
	transform = obj.transform;
	UIMainPanel.Gtransform = obj.transform;
	this.InitPanel();
	logWarn("Awake lua--->>"..gameObject.name);
end

--初始化面板--
function UIMainPanel.InitPanel()
	--this.btnOpen = transform:FindChild("BtnLogin").gameObject; --获取按钮"BtnLogin"
	--this.gridParent = transform:FindChild('ScrollView/Grid');
	
	--获取ScrewView游戏物体
	this.go_SV=transform:FindChild("ScrollView_Panel").gameObject; 
	
	--获取ScrollBar游戏物体	
	this.scrollBar=transform:FindChild("ScrollView_Panel/Simple Horizontal Scroll Bar").gameObject;

	--获取Top游戏物体	
	this.go_Top=transform:FindChild("Top_Bottom_Panel/Container_Top").gameObject;
	
	--获取Top_Bottom 一起的TweenAlpha
	this.ta_TopBottom=transform:FindChild("Top_Bottom_Panel").gameObject:GetComponent('TweenAlpha');
		
	--获取底部灰色小块
	this.bottom_Bg_Red=transform:FindChild("Top_Bottom_Panel/Container_Bottom/Sprite_Bg_Red").gameObject;
	
	--获取Grid游戏物体
	this.grid_sv=transform:FindChild("ScrollView_Panel/Grid").gameObject;
	this.mGrid=this.grid_sv:GetComponent('UIGrid');
	this.mCenterOnChild=this.grid_sv:GetComponent('UICenterOnChild');
	
	
	--获取底部五个图片父物体
	this.bottomIcons=transform:FindChild("Top_Bottom_Panel/Container_Bottom/Container_Icons").gameObject; 
	
	--获得底部五个图片tranform数组。通过 数组名[1].gameObject获得游戏物体
	this.BottomSpriteTransformArray=UnityEngine.Component.GetComponentsInChildren(this.bottomIcons.transform,typeof(UnityEngine.Transform)); 
	
	--获取底部五个五个图标中文Label父物体
	this.bottomIconsLabel=transform:FindChild("Top_Bottom_Panel/Container_Bottom/Container_Icons_Chinese").gameObject; 
		
	--获得底部五个图标中文Label数组。
	this.BottomLabelArray=UnityEngine.Component.GetComponentsInChildren(this.bottomIconsLabel.transform,typeof(UnityEngine.Transform)); 
	
	--获取“战斗”按钮
	this.FightButton=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Sprite_FightBtnFrame/FightBtnIn").gameObject; 

	--获取“玩家昵称”UILabel控件
	this.label_playerName=transform:FindChild("Top_Bottom_Panel/Container_Top/Container_Exp/Sprite_Frame/Label_Name"):GetComponent('UILabel'); 
	
	--获取“玩家杯数”UILabel控件
	this.label_playerTrophy=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Container_Trophy/Sprite_CupFrame/Sprite_Cup/Label_Cup"):GetComponent('UILabel'); 

	--获取“玩家杯数” 游戏物体
	this.go_playerTrophy=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Container_Trophy/Sprite_CupFrame/Sprite_Cup").gameObject; 
	
	--获取“玩家等级”UILabel控件
	this.label_playerLevel=transform:FindChild("Top_Bottom_Panel/Container_Top/Container_Exp/Sprite_Frame/Sprite_Lv/Label_Lv"):GetComponent('UILabel');

	--获取“玩家经验”UILabel控件
	this.label_playerExp=transform:FindChild("Top_Bottom_Panel/Container_Top/Container_Exp/Sprite_Frame/Sprite_Exp/Label"):GetComponent('UILabel'); 
	
	--获取“玩家经验”游戏物体
	this.go_playerExp=transform:FindChild("Top_Bottom_Panel/Container_Top/Container_Exp/Sprite_Frame/Sprite_Lv").gameObject; 
	
	--获取“玩家信息”整个框
	this.go_playerInfo=transform:FindChild("Top_Bottom_Panel/Container_Top/Container_Exp/Sprite_Frame").gameObject; 

	--获取“玩家经验”UISprite控件
	this.sprite_playerExpFillAmount=transform:FindChild("Top_Bottom_Panel/Container_Top/Container_Exp/Sprite_Frame/Sprite_Exp"):GetComponent('UISprite'); 

	--获取“玩家金币”UILabel控件
	this.label_playerGold=transform:FindChild("Top_Bottom_Panel/Container_Top/Container_Gold/Sprite_Frame/Label"):GetComponent('UILabel'); 

	--获取“玩家金币” 金币游戏物体
	this.go_playerGold=transform:FindChild("Top_Bottom_Panel/Container_Top/Container_Gold/Sprite_G").gameObject; 

	
	--获取“玩家钻石“UILabel控件
	this.label_playerDiamond=transform:FindChild("Top_Bottom_Panel/Container_Top/Container_Diamond/Sprite_Frame/Label"):GetComponent('UILabel'); 
	
	--获取“战场名称“UILabel控件
	this.label_BattleFieldName=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Sprite_BattleField/Label_Name"):GetComponent('UILabel'); 

	--获取“重新命名按钮“
	this.go_RenamingBtn=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Container_Bubble/Sprite_Bubble-1").gameObject; 

	--获取提醒游戏物体
	this.go_TipWords=transform:FindChild("Top_Bottom_Panel/Panel/Label_TipWords").gameObject;
	
	--获取入场金币消耗
	this.label_BattleCost=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Sprite_FightBtnFrame/FightBtnIn/Sprite_DarkBack/Label_Cost"):GetComponent('UILabel');
	
	--获取战斗场景图片
	this.sprite_BattleField=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Sprite_Castle"):GetComponent('UISprite');
	
	--飞翔的经验
	this.go_Flying_Exp=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Container_Flyings/Sprite_Flying_Exp").gameObject;
	
	--飞翔的金币
	this.go_Flying_Gold=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Container_Flyings/Sprite_Flying_Gold").gameObject;
	
	--飞翔的奖杯
	this.go_Flying_Trophy=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Container_Flyings/Sprite_Flying_Trophy").gameObject;
	
	--飞翔的文字 
	this.go_Label_Flying=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Container_Flyings/Label_Flying").gameObject;
	
	--飞翔起飞点
	this.go_Flying_Start=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Container_Flyings/Flying_Start_Pos").gameObject;
	
	--飞翔金币刷新特效 
	this.go_Eft_Gold=transform:FindChild("Top_Bottom_Panel/Container_Top/Container_Gold/Sprite_Frame/S_Splash_G").gameObject; 

	--飞翔奖杯刷新特效 
	this.go_Eft_Trophy=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Container_Trophy/Sprite_CupFrame/Sprite_Cup/S_Splash_T").gameObject; 
	
	--飞翔经验刷新特效 
	this.go_Eft_Exp=transform:FindChild("Top_Bottom_Panel/Container_Top/Container_Exp/Sprite_Frame/Sprite_Exp/S_Splash_E").gameObject; 
	
	--左上玩家头像
	this.texture_Player_Photo=transform:FindChild("Top_Bottom_Panel/Container_Top/Container_Exp/Sprite_Frame/T_Photo"):GetComponent('UITexture');

	--获取挂在在UIMAINPANEL上的LuaBehaviour脚本
	this.LuaBH=transform:GetComponent('LuaBehaviour');

	--获取“测试按钮“(需要用的时候再激活)
	this.go_TestBtn=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Container_Bubble/Sprite_Bubble-3").gameObject; 

	--新手进度提示 图片和文字
	this.go_NewCount=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/L_NewCount").gameObject; 
	
	--新手进度，图片
	this.sp_NewCount=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/L_NewCount/S_Exp").gameObject:GetComponent('UISprite'); 
	
	--战场名字 整个，带黑框
	this.trans_BattleFieldNameWhole=transform:FindChild("ScrollView_Panel/Grid/Sprite_Fight/Sprite_BattleField"); 
	
end

function UIMainPanel.OnUpdate()
	UIMainCtrl:OnUpdate();
	
end

--单击事件--
function UIMainPanel.OnDestroy()
	logWarn("OnDestroy---->>>");
end



