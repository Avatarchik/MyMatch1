require "Common/define"
require "3rd/pblua/auth_pb"
require "3rd/pblua/match_pb"
require "3rd/pblua/msg_pb"
require "3rd/pblua/home_pb"
require "protobuf"
require "TableCtrl/TableBattleCtrl"
require "TableCtrl/TablePlayerAttrCtrl"

UIMainCtrl = {};
local this = UIMainCtrl;
local panel;
local UIMain;
local transform;
local gameObject;
local timer = 0;
--飞翔倒计时
local flytrophyafter=1;
local flygoldafter=60;
local flyexpafter=120;
local flynewbieexp=false;
--起飞按钮
local isflygold=false;
local isflyexp=false;
local isflytrophy=false;
--刷特效通用
local EftComing=130;
local StartFadingIn=120;
--刷奖杯特效
local TrophyEftComing=EftComing;
local isEftTrophy=false;
local isHideTrophy=false;
--刷金币特效
local GoldEftComing=EftComing;
local isEftGold=false;
local isHideGold=false;
--刷经验特效
local ExpEftComing=EftComing;
local isEftExp=false;
local isHideExp=false;
--等级提升标志位。服务器推送后改变
local isLevelUp=false;
--第一次进入，保存战场名字
local ArenaName;
local isLevelUpForArena=false;

--构建函数--
function UIMainCtrl.New()
	logWarn("UIMainCtrl.New--->>");
	return this;
end

--Awake方法--
function UIMainCtrl.Awake()
	logWarn("UIMainCtrl.Awake--->>");
	loadingMgr:ShowLoading();
	uiMgr:ShowUIAndCloseOthers_LUA(MyFrameWork.E_UIType.UIMainPanel,this.OnCreate);
	
end

--启动事件--
function UIMainCtrl.OnCreate(obj)
	
	gameObject = obj;
	transform = obj.transform;
	UIMain = transform:GetComponent('MyFrameWork.LuaBehaviour');
	UIMainCtrl.UIMain = transform:GetComponent('MyFrameWork.LuaBehaviour');
	--lua代码添加组件
	dragScript_sp2=Util.AddComponent(UIMainPanel.BottomSpriteTransformArray[2].gameObject,'MyFrameWork','LuaDrag');
	UIMainCtrl.InitPanel();	
	--loadingMgr:HideLoading();--等从服务器读取数据后再显示
	gameObject:SetActive(false);
	--让 GameManager 的 标志位 _isServerMatched 变false，确保服务器在战斗中发送匹配成功消息，导致玩家创建完匹配界面后直接进入匹配成功和战场
	utilMgr:DelMatchedInfo(); 
end

--初始化面板：按钮绑定，服务器回调--
function UIMainCtrl.InitPanel()
	--参数初始化--
	local script = transform:GetComponent('BaseUI');
	script.mUIStyle = MyFrameWork.E_UIStyle.Main;
	script.mUILayertype = MyFrameWork.E_LayerType.MainUI;
	--按钮事件绑定--
	UIMain:AddClick(UIMainPanel.BottomSpriteTransformArray[1].gameObject,this.OnItemClick_Sp1);
	UIMain:AddClick(UIMainPanel.BottomSpriteTransformArray[2].gameObject,this.OnItemClick_Sp2);
	UIMain:AddClick(UIMainPanel.BottomSpriteTransformArray[3].gameObject,this.OnItemClick_Sp3);
	UIMain:AddClick(UIMainPanel.BottomSpriteTransformArray[4].gameObject,this.OnItemClick_Sp4);
	UIMain:AddClick(UIMainPanel.BottomSpriteTransformArray[5].gameObject,this.OnItemClick_Sp5);
	UIMain:AddClick(UIMainPanel.FightButton,this.SendMatch);--点击“战斗”按钮
	UIMain:AddClick(UIMainPanel.go_RenamingBtn,Game.ShowUINaming);--点击“重新起名”按钮，显示起名PANEL
	UIMain:AddClick(UIMainPanel.sprite_BattleField.gameObject,Game.ShowArena); --测试，竞技场升级
	UIMain:AddClick(UIMainPanel.go_playerInfo,this.OnplayerInfo);
	UIMain:AddClick(UIMainPanel.go_TestBtn,this.OnTestBtn);
	--协议回调绑定--
	Event.AddListener(tostring(msg_pb.SERVER_MATCH), this.SendMatchCBK);
	Event.AddListener(tostring(msg_pb.SERVER_MATCHED), this.SendMatchedCBK);
	Event.AddListener(tostring(msg_pb.SERVER_CANCEL_MATCH), this.SendCancelMatchCBK);
	Event.AddListener(tostring(msg_pb.SERVER_HOME), this.SendGetPlayerInfoCBK);
	Event.AddListener(tostring(msg_pb.SYS_MSG), this.SysMsgCBK);
	Event.AddListener(tostring(msg_pb.SERVER_PLAYER_LVUP), this.SendLvUpCBK);
        
        log('发送消息获取玩家信息');
	UIMainCtrl.SendGetPlayerInfo();--向服务器申请玩家信息，服务器回调后显示
        log('获取ScrollView组件');
	sv_ScrollView=UIMainPanel.go_SV.transform:GetComponent('UIScrollView');--获取ScrollView组件
        log('获取ScrollBar组件');
	sb_ScrollBar=UIMainPanel.scrollBar:GetComponent('UIScrollBar');	--获取ScrollBar组件
        log('获取transform组件');
	trans_ScrollView=UIMainPanel.go_SV.transform;--获取transform组件
        log('获取UICenterOnChild脚本');
	script_center=UIMainPanel.grid_sv:GetComponent('UICenterOnChild');--获取UICenterOnChild脚本
        log('底部图标的y轴值');
	origin_Y=UIMainPanel.BottomSpriteTransformArray[1].localPosition.y;--底部图标的y轴值
        log('在C#中进行滑动窗口的事件注册');
	utilMgr:RegisterDelegation();--在C#中进行滑动窗口的事件注册
        log('初始值3号图标变大');
	UIMainCtrl.Enlarge(UIMainPanel.BottomSpriteTransformArray[3]);--初始值3号图标变大
        log('只显示3号的中文');
	UIMainCtrl.ShowChinesePageLabel(3);--只显示3号的中文
        log('发送消息获取排行榜信息');
	UIMainCtrl.ClientAllPlayerRank();--加载排行榜
	
	--初始化Rank子UI--
	UIRankPanel.InitPanel();
	UIRankCtrl.InitPanel();
	
	log('UIMainCtrl.InitPanel over');
end

--每帧调用--
function UIMainCtrl.OnUpdate()
	UIRankCtrl:OnUpdate();
	
	--飞奖杯
	if	isflytrophy==true	then
		UIMainCtrl.OnFlyTrophyisTrue();
	end
	
	--飞金币
	if	isflygold==true	then
		UIMainCtrl.OnFlyGoldisTrue();
	end
	
	--飞经验
	if	isflyexp==true	then
		UIMainCtrl.OnFlyExpisTrue();

	end
	
	--飞奖杯后刷数字特效
	if	isEftTrophy==true	then
		UIMainCtrl.OnEftTrophyTrue();
	end
	
	--飞金币后刷数字特效
	if	isEftGold==true	then
		UIMainCtrl.OnEftGoldTrue();
	end
	
	--飞经验后刷数字特效
	if	isEftExp==true	then
		UIMainCtrl.OnEftExpTrue();
	end

	local pageNum=UIMainCtrl.GetPageNum();
        if pageNum == 5 then 
            utilMgr:UpdateTouchMove();
        end 
end


------------------------------------------------------------------
--切页组：滑动/点下方图标切换页面相关方法--
------------------------------------------------------------------

--点击下方五个图标分别执行的方法--
function UIMainCtrl.OnItemClick_Sp1(go)
	sb_ScrollBar.value=0.02; --故意偏移一点
	script_center:Recenter();
	UIMainCtrl.IconChange_HideTop();	
	
end

function UIMainCtrl.OnItemClick_Sp2(go)
	sb_ScrollBar.value=0.27 --中心在0.2508 
	script_center:Recenter();
	UIMainCtrl.IconChange_HideTop();	
end

function UIMainCtrl.OnItemClick_Sp3(go)
	sb_ScrollBar.value=0.49 --中心在0.5
	script_center:Recenter();
	UIMainCtrl.IconChange_HideTop();
end

function UIMainCtrl.OnItemClick_Sp4(go)
	sb_ScrollBar.value=0.7292 --中心在0.7492
	script_center:Recenter();
	UIMainCtrl.IconChange_HideTop();
end

function UIMainCtrl.OnItemClick_Sp5(go)
	sb_ScrollBar.value=0.9784 --中心在0.9984
	script_center:Recenter();
	UIMainCtrl.IconChange_HideTop();
	--UIRankCtrl.SendRankAllPlayer();--问服务器重新读取数据，并排序
	--UIRankCtrl.ResetRankList(); --根据服务器传来的数据，重新排序制作排行榜
	--log(UIRankCtrl.phase);
end

--根据PageNum决定下方五图标缩放，上方是否显示
function UIMainCtrl.IconChange_HideTop()
	soundMgr:PlaySoundEff("Music/Swipe");
	local pageNum=UIMainCtrl.GetPageNum();
	UIMainCtrl.TweenBack();
	UIMainCtrl.Enlarge(UIMainPanel.BottomSpriteTransformArray[pageNum]);
	UIMainCtrl.HideTopBottom(pageNum);
	UIMainCtrl.ShowChinesePageLabel(pageNum);
end

--让底部五个图标都变回原来大小
function UIMainCtrl.TweenBack()	
	 for i = 1,UIMainPanel.BottomSpriteTransformArray.Length-1 do		
			UIMainPanel.BottomSpriteTransformArray[i].localScale=Vector3(1,1,1);
			UIMainPanel.BottomSpriteTransformArray[i].localPosition=Vector3(UIMainPanel.BottomSpriteTransformArray[i].localPosition.x,origin_Y,0);
		end
end

--让选中的图标变大20%，位置提上15, 
function UIMainCtrl.Enlarge(go)
	
	go.transform.localScale=Vector3(1.2,1.2,1);
	go.transform.localPosition=Vector3(go.transform.localPosition.x,origin_Y+15,0);
    UIMainPanel.bottom_Bg_Red.transform.localPosition=Vector3(go.transform.localPosition.x,UIMainPanel.bottom_Bg_Red.transform.localPosition.y,0);
end
	
--根据scrollview的数字，决定top和bottom是否隐藏
function UIMainCtrl.HideTopBottom(num)
	if	num>= 4 then
		UIMainPanel.go_Top:SetActive(false);
	else
		UIMainPanel.go_Top:SetActive(true);
	end
end

--根据scrollview的数字，显示当下页面的中文，隐藏其他的
function UIMainCtrl.ShowChinesePageLabel(num)
	for i = 1,UIMainPanel.BottomLabelArray.Length-1 do		
		UIMainPanel.BottomLabelArray[i].gameObject:SetActive(false);
	end
	UIMainPanel.BottomLabelArray[num].gameObject:SetActive(true);
end

--获取当前scrollview的页面数字
function UIMainCtrl.GetPageNum()	
	return 1+UIMainPanel.mGrid:GetIndex(UIMainPanel.mCenterOnChild.centeredObject.transform);
end

------------------------------------------------------------------
--服务器组：与服务器相关方法--
------------------------------------------------------------------

--进入主页面，向服务器申请全玩家排名列表
function UIMainCtrl.ClientAllPlayerRank()
	UIRankCtrl.SendRankAllPlayer();		
end

--点击“出战”，发送消息给服务器，等回调，回调在c#脚本uimain中
function UIMainCtrl.SendMatch()
	log('UIMainCtrl.SendMatch');
    local match = match_pb.ClientMatch();
    local msg = match:SerializeToString();
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(msg_pb.CLIENT_MATCH);
    buffer:WriteBuffer(msg);
    networkMgr:SendMessage(buffer);
end

--点击出战，进入对战排队的回调
function UIMainCtrl.SendMatchCBK(buffer)
	log('receive msg SendMatchCBK');
	local data = buffer:ReadBuffer();
    local msg = match_pb.ServerMatch();
    msg:ParseFromString(data);
	UIMainModule.ServerMatchData = msg;
	
	if	tostring(msg.result)=="0" then --有金币,进入排队队列
		utilMgr:OpenMatchingPanel();--打开匹配界面
		soundMgr:PlaySoundEff("Music/ClickFight");--播放声音
		UIMainPanel.ta_TopBottom: PlayForward(); --主界面上下bar两个渐隐
		
		if	FightModule.ServerFightResultData==nil	then --第一次进入点战斗，不修改
			
		else	--非第一次进入，把上盘战斗结果改为平局（102），防止断线重连后飞金币
			log('----------------'..FightModule.ServerFightResultData.result)
			FightModule.ServerFightResultData.result=102;
			log('----------------'..FightModule.ServerFightResultData.result)
		end
	else
		UIMainCtrl.MessageTip("金币不足，请休息一下再战哦");
	end
	
	log('Parse msg SendMatchCBK over');
end

--匹配成功，找到对手的回调
function UIMainCtrl.SendMatchedCBK(buffer)
	log('receive msg SendMatchedCBK');
	local data = buffer:ReadBuffer();
    local msg = match_pb.ServerMatched();
    msg:ParseFromString(data);
	UIMainModule.ServerMatchedData=msg;
	log('stage no.:'..table.getn(msg.stages));--服务器传来的stages关卡数
	log('boss id:'..tostring(UIMainModule.ServerMatchedData.stages[1].boss_id));
	fightDataMgr:SetData();
	log('Parse msg SendMatchedCBK over');
	--把对手信息添加过来： 名字，奖杯 ，已经改在UIMATCHING中完成
	gameMgr:OnFindOpponentCBK();
end

--向服务器请求撤销匹配
function UIMainCtrl.SendCancelMatch()
	log('UIMainCtrl.SendCancelMatch');
    local match = match_pb.ClientCancelMatch();
    local msg = match:SerializeToString();
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(msg_pb.CLIENT_CANCEL_MATCH);
    buffer:WriteBuffer(msg);
    networkMgr:SendMessage(buffer);
end

--撤销匹配成功的回调，回到主界面
function UIMainCtrl.SendCancelMatchCBK(buffer)
	log('receive msg SendCancelMatchCBK');
	local data = buffer:ReadBuffer();
    local msg = match_pb.ServerCancelMatch();
    msg:ParseFromString(data);
	UIMainModule.ServerCancelMatchData=msg;
	log('Parse msg SendCancelMatchCBK over');
	utilMgr:CloseMatchingPanel();
	UIMainPanel.ta_TopBottom: PlayReverse();
	--匹配画面到屏幕中
	--UIMainPanel.go_UIMatchingPanel.transform.localPosition = Vector3(0,1500,0);
	--开启下部遮挡板（这块遮挡板不能常开）
	--UIMainPanel.uisprite_UIMatchingPanel_LowerScreen.enabled=false;
end

--向服务器申请读取用户信息 协议号  CLIENT_HOME = 1007
function UIMainCtrl.SendGetPlayerInfo()
    log('UIMainCtrl.SendGetPlayerInfo');
    local home = home_pb.ClientHome();
    local msg = home:SerializeToString();
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(msg_pb.CLIENT_HOME);
    buffer:WriteBuffer(msg);
    networkMgr:SendMessage(buffer);
    log('如果服务器不回GetPlayerInfoCBK消息将造成卡loading！！！！');
end

--服务器回调：读取用户信息 SERVER_HOME = 1008
function UIMainCtrl.SendGetPlayerInfoCBK(buffer)
	log('receive msg SendGetPlayerInfoCBK');
	local data = buffer:ReadBuffer();
    local msg = home_pb.ServerHome();
    msg:ParseFromString(data);
	UIMainModule.ServerHomeData = msg;
	UIMainCtrl.ShowPlayerInfo();--已经获得玩家信息，进行显示
	gameObject:SetActive(true);
        log('服务器回复GetPlayerInfoCBK隐藏loading面板');
	loadingMgr:HideLoading();--loading界面消失
	utilMgr:ReadPhoto(UIMainModule.ServerHomeData.user.id);  --读取照片
	if	isLevelUp==true	then	--玩家等级提升，服务器回调后变true
		Game.ShowUIPlayerLvUp();
		isLevelUp=false;
	end
	
	if	FightModule.ServerFightResultData~=nil	then	--有比赛结果，并非第一次进入
		UIMainCtrl.FlyingItems();--飞金币经验奖杯
	else
		--仅在第一次进游戏时执行的方法
		UIMainCtrl.ForSDK(msg.user.lv,msg.user.id,msg.user.nick);--连接SDK
		--utilMgr:ShowUICardPanel();--打开CardPanel，位置和层级让UICARDPANEL自带的脚本UICARD自己调整
		log('not ShowUICardPanel();..................................');
	end
	
	log('Parse msg SendGetPlayerInfoCBK over');
	--log('--------☆☆☆☆☆☆☆☆☆-------------');
	
end

--服务器调用消息提示框,显示消息
function UIMainCtrl.SysMsgCBK(buffer)
	log('receive msg SysMsgCBK');
	local data = buffer:ReadBuffer();
    local msg = sys_pb.ServerMsg();
    msg:ParseFromString(data);
	UIMainModule.SysMsgData=msg;
	UIMainCtrl.MessageTip(msg.msg)
end

--玩家升级服务器回调，让isLevelUp=true
function UIMainCtrl.SendLvUpCBK()
	isLevelUp=true;
	isLevelUpForArena=true;

end

------------------------------------------------------------------
--飞翔方法组：和飞翔有关都放这里--
------------------------------------------------------------------



--先飞奖杯，再飞金币，再飞经验
function UIMainCtrl.FlyingItems()	
	if	FightModule.ServerFightResultData~=nil	then	--非第一次进游戏
		if	FightModule.ServerFightResultData.award.trophy>0	then	--上盘获胜
			isflytrophy=true;
			isflygold=true;
			isflyexp=true;
		else  --"=0":新手战, "<0"上盘输了
			isflyexp=true;
		end
	else	--第一次进游戏，按键就飞，测试用
		isflytrophy=true;
		isflygold=true;
		isflyexp=true;
	end
end	

--isflytrophy=true时执行的方法
function UIMainCtrl.OnFlyTrophyisTrue()	
	flytrophyafter=flytrophyafter-1;
		if	flytrophyafter<=0	then
			UIMainCtrl.Flying("trophy",UIMainPanel.go_Flying_Trophy,UIMainPanel.go_Flying_Start,UIMainPanel.go_playerTrophy,5);
			if	FightModule.ServerFightResultData~=nil	then	--非第一次进入
				UIMainCtrl.MessageFlying("奖杯增加："..FightModule.ServerFightResultData.award.trophy);
			else		--第一次进入
				UIMainCtrl.MessageFlying("奖杯增加："..888);
			end
			flytrophyafter=1;
			isflytrophy=false;
			--启动定时刷特效
			isEftTrophy=true;
			isHideTrophy=false;
		end
end	


--isflygold=true时执行的方法
function UIMainCtrl.OnFlyGoldisTrue()	
	flygoldafter=flygoldafter-1;
		if	flygoldafter<=0	then
			UIMainCtrl.Flying("gold",UIMainPanel.go_Flying_Gold,UIMainPanel.go_Flying_Start,UIMainPanel.go_playerGold,5);
			if	FightModule.ServerFightResultData~=nil	then
				UIMainCtrl.MessageFlying("金币增加："..FightModule.ServerFightResultData.award.g_money);
			else
				UIMainCtrl.MessageFlying("金币增加："..888);
			end
			flygoldafter=60;
			isflygold=false;
			--启动定时刷特效
			isEftGold=true;
			isHideGold=false;
		end
end	

--isflyexp=true时执行的方法
function UIMainCtrl.OnFlyExpisTrue()
	if	FightModule.ServerFightResultData~=nil	then	--非第一次
		if	FightModule.ServerFightResultData.award.trophy<=0	then	--上盘为新手战或失败
			if	flynewbieexp==false	then	--进一次
				flyexpafter=10;
				flynewbieexp=true;
			end
			
		end	
	end

	flyexpafter=flyexpafter-1;
	
	if	flyexpafter<=0	then
		if	FightModule.ServerFightResultData.award.trophy<0	then	--上盘失利，飞1颗星
			UIMainCtrl.Flying("exp",UIMainPanel.go_Flying_Exp,UIMainPanel.go_Flying_Start,UIMainPanel.go_playerExp,1);
		else	--上盘获胜 trophy>0 或者 新手获胜 trophy==0 ，飞5颗星
			UIMainCtrl.Flying("exp",UIMainPanel.go_Flying_Exp,UIMainPanel.go_Flying_Start,UIMainPanel.go_playerExp,5);
		end
		
		if	FightModule.ServerFightResultData~=nil	then
			UIMainCtrl.MessageFlying("经验增加："..FightModule.ServerFightResultData.award.exp);
		else
			UIMainCtrl.MessageFlying("经验增加："..888);
		end
		flyexpafter=120;
		isflyexp=false;
		--启动定时刷特效
		isEftExp=true;
		isHideExp=false;
		--只飞经验的等待时间
		flynewbieexp=false;
	end
end	

--isEftTrophy=true时执行的方法
function UIMainCtrl.OnEftTrophyTrue()
	TrophyEftComing=TrophyEftComing-1;
		
	if	TrophyEftComing<=EftComing-StartFadingIn	then
		
		if	isHideTrophy==false	then	--执行一次
			UIMainPanel.label_playerTrophy:GetComponent('TweenAlpha').enabled=true; --文字开始隐藏
			isHideTrophy=true;
		end
	end
	
	if	TrophyEftComing<=0	then
		TrophyEftComing=EftComing;
		isEftTrophy=false;
		UIMainPanel.go_Eft_Trophy:SetActive(true);
		UIMainPanel.label_playerTrophy.text=UIMainModule.ServerHomeData.user.trophy;
	end
end
	
--isEftGold=true时执行的方法
function UIMainCtrl.OnEftGoldTrue()
	GoldEftComing=GoldEftComing-1;	
	if	GoldEftComing<=EftComing-StartFadingIn	then  --启动隐藏再出现文字的tween
		
		if	isHideGold==false	then	--执行一次
			UIMainPanel.label_playerGold:GetComponent('TweenAlpha').enabled=true; --文字开始隐藏
			isHideGold=true;	
		end
	end
	
	if	GoldEftComing<=0	then		--启动刷特效
		GoldEftComing=EftComing;	--恢复倒计时的值
		isEftGold=false;			--恢复标志位
		UIMainPanel.go_Eft_Gold:SetActive(true);	--特效启动
		UIMainPanel.label_playerGold.text=UIMainModule.ServerHomeData.user.g_money;		--金币值更新
	end
end

--isEftExp=true时执行的方法
function UIMainCtrl.OnEftExpTrue()
	ExpEftComing=ExpEftComing-1;	
	if	ExpEftComing<=EftComing-StartFadingIn	then  --启动隐藏再出现文字的tween
		
		if	isHideExp==false	then	--执行一次
			UIMainPanel.label_playerExp:GetComponent('TweenAlpha').enabled=true; --文字开始隐藏
			isHideExp=true;	
		end
	end
	
	if	ExpEftComing<=0	then		--启动刷特效
		ExpEftComing=EftComing;	--恢复倒计时的值
		isEftExp=false;			--恢复标志位
		UIMainPanel.go_Eft_Exp:SetActive(true);	--特效启动
		UIMainPanel.label_playerExp.text=UIMainModule.ServerHomeData.user.exp.."/"..UIMainModule.ServerHomeData.user.lvup_exp;		--经验值更新
		UIMainPanel.sprite_playerExpFillAmount.fillAmount = UIMainModule.ServerHomeData.user.exp / UIMainModule.ServerHomeData.user.lvup_exp; --经验图片
		if	UIMainModule.ServerHomeData.user.exp < FightModule.ServerFightResultData.award.exp	then 
			UIMainPanel.label_playerLevel.text=UIMainModule.ServerHomeData.user.lv; --等级
			UIMainPanel.label_playerLevel.transform:GetComponent('TweenScale').enabled=true;
		end
		
		
	end
end

--飞翔的经验-文字
function UIMainCtrl.MessageFlying(content)
	UIMainPanel.go_Label_Flying:SetActive(false);
	UIMainPanel.go_Label_Flying:GetComponent('TweenAlpha'):ResetToBeginning();
	UIMainPanel.go_Label_Flying:GetComponent('TweenPosition'):ResetToBeginning();
	UIMainPanel.go_Label_Flying:GetComponent('TweenAlpha').enabled=true;
	UIMainPanel.go_Label_Flying:GetComponent('TweenPosition').enabled=true;
	UIMainPanel.go_Label_Flying:GetComponent('UILabel').text=content;
	UIMainPanel.go_Label_Flying:SetActive(true);
end

--飞翔的方法连接，Lua->C#
function UIMainCtrl.Flying(str,go,flyfrom,flyto,num)
	utilMgr:Flying(str,go,flyfrom,flyto,num);
end

------------------------------------------------------------------
--其他方法组--
------------------------------------------------------------------

--显示玩家信息：昵称，等级，金币，钻石，卡牌, 战场等
function UIMainCtrl.ShowPlayerInfo()
	
	if	FightModule.ServerFightResultData~=nil	then	--并非第一次进入
	log('ServerFightResultData.result='..FightModule.ServerFightResultData.result);
		if	UIMainModule.ServerHomeData.user.exp < FightModule.ServerFightResultData.award.exp	then --玩家升级
			UIMainPanel.label_playerLevel.text=UIMainModule.ServerHomeData.user.lv-1; --显示升级前的等级
			--显示升级前的经验和所需升级经验
			UIMainPanel.label_playerExp.text= TablePlayerAttrCtrl.GetCardSeatAddition(0,0,true)-(FightModule.ServerFightResultData.award.exp-UIMainModule.ServerHomeData.user.exp).."/"..TablePlayerAttrCtrl.GetCardSeatAddition(0,0,true);
			UIMainPanel.sprite_playerExpFillAmount.fillAmount = (TablePlayerAttrCtrl.GetCardSeatAddition(0,0,true)-(FightModule.ServerFightResultData.award.exp-UIMainModule.ServerHomeData.user.exp)) / TablePlayerAttrCtrl.GetCardSeatAddition(0,0,true);  
		else --玩家没升级
			UIMainPanel.label_playerLevel.text=UIMainModule.ServerHomeData.user.lv; --目前等级
			--比赛之前的经验
			UIMainPanel.label_playerExp.text=UIMainModule.ServerHomeData.user.exp-FightModule.ServerFightResultData.award.exp.."/"..UIMainModule.ServerHomeData.user.lvup_exp; --经验
			UIMainPanel.sprite_playerExpFillAmount.fillAmount = (UIMainModule.ServerHomeData.user.exp-FightModule.ServerFightResultData.award.exp) / UIMainModule.ServerHomeData.user.lvup_exp; --经验条图片
		end
		
		if	FightModule.ServerFightResultData.result==101	then	--上盘获胜
			--显示比赛之前的奖杯和金币，因为要飞
			UIMainPanel.label_playerTrophy.text=UIMainModule.ServerHomeData.user.trophy-FightModule.ServerFightResultData.award.trophy; --奖杯
			UIMainPanel.label_playerGold.text=UIMainModule.ServerHomeData.user.g_money-FightModule.ServerFightResultData.award.g_money; --金币
			log('----------------------------------非第一次进入，上盘获胜，飞，显示比赛之前的奖杯金币经验')
		else 	--上盘输了，1.奖杯，金币直接最新的
			UIMainPanel.label_playerTrophy.text=UIMainModule.ServerHomeData.user.trophy; --奖杯
			UIMainPanel.label_playerGold.text=UIMainModule.ServerHomeData.user.g_money; --金币
			log('----------------------------------非第一次进入，上盘没胜，只飞经验，金币，奖杯直接显示最新值')
		end
		UIMainCtrl.CheckArenaUp();
	else
		UIMainPanel.label_playerTrophy.text=UIMainModule.ServerHomeData.user.trophy; --奖杯		
		UIMainPanel.label_playerGold.text=UIMainModule.ServerHomeData.user.g_money; --金币
		UIMainPanel.label_playerExp.text=UIMainModule.ServerHomeData.user.exp.."/"..UIMainModule.ServerHomeData.user.lvup_exp; --经验
		UIMainPanel.sprite_playerExpFillAmount.fillAmount = UIMainModule.ServerHomeData.user.exp / UIMainModule.ServerHomeData.user.lvup_exp; --经验图片
		UIMainPanel.label_playerLevel.text=UIMainModule.ServerHomeData.user.lv; --等级
		ArenaName=	TableBattleCtrl.GetBattleField(UIMainModule.ServerHomeData.user.trophy);
		log('----------------------------------第一次进入，不飞，直接显示最新值')
		
	end
	--不管第几次进，胜利与否都不改变
	UIMainPanel.label_playerName.text=UIMainModule.ServerHomeData.user.nick; --昵称
	
	UIMainPanel.label_playerDiamond.text=UIMainModule.ServerHomeData.user.money; --钻石	
	
	--log("......................... Player_Photo");
	--根据玩家杯数，显示战场名字
	UIMainPanel.label_BattleFieldName.text = TableBattleCtrl.GetBattleField(UIMainModule.ServerHomeData.user.trophy);
	 
	--根据玩家杯数，显示入场金额
	UIMainPanel.label_BattleCost.text = TableBattleCtrl.GetGoldCost(UIMainModule.ServerHomeData.user.trophy);
	
	--主界面的战斗场景图
	--UIMainPanel.sprite_BattleField.spriteName= 'plan-10';--方法有效
	--UIMainPanel.sprite_BattleField.color=Color.green; --方法有效
	--local co=Color(0,255,0);
	--UIMainPanel.sprite_BattleField.color=UnityEngine.Color(1,1,255); --无效，但不报错，但会阻碍本方法内，后面的方法

	--新手盘数提示
	if	UIMainModule.ServerHomeData.user.trophy==0  and TableBattleCtrl.GetIndexByTrophy(0)==1  then
		UIMainPanel.go_NewCount:SetActive(true);
		UIMainPanel.trans_BattleFieldNameWhole.localPosition=Vector3(0,116,0);
		UIMainPanel.go_NewCount:GetComponent('UILabel').text=UIMainModule.ServerHomeData.user.exp/10 .."/".."5";
		UIMainPanel.sp_NewCount.fillAmount=UIMainModule.ServerHomeData.user.exp/50 ;
	else
		UIMainPanel.go_NewCount:SetActive(false);
		UIMainPanel.trans_BattleFieldNameWhole.localPosition=Vector3(0,64,0);
	end
	
end

--显示提醒条，1秒后向上淡出--content-显示内容， “要写的内容”
function UIMainCtrl.MessageTip(content)
	UIMainPanel.go_TipWords:SetActive(false);
	UIMainPanel.go_TipWords:GetComponent('TweenAlpha'):ResetToBeginning();
	UIMainPanel.go_TipWords:GetComponent('TweenPosition'):ResetToBeginning();
	UIMainPanel.go_TipWords:GetComponent('TweenAlpha').enabled=true;
	UIMainPanel.go_TipWords:GetComponent('TweenPosition').enabled=true;
	UIMainPanel.go_TipWords:GetComponent('UILabel').text=content;
	UIMainPanel.go_TipWords:SetActive(true);
	--测试：读策划表
	--TableBattleCtrl.GetName("5");

end

--从服务器读取玩家信息后，给SDK使用
function UIMainCtrl.ForSDK(int_lv,str_id,str_nickName)
	utilMgr:SendGameInfoToSdk(int_lv,str_id,str_nickName);
end

--显示鼠标位置
function UIMainCtrl.ShowMousePos()
	utilMgr:ShowMousePos();
end

--点击左上个人头像，显示卡座加成
function UIMainCtrl.OnplayerInfo()
	Game.ShowUIPlayerLvUp();
end

--检测竞技场是否升级，如果升级，修改标志位 
function UIMainCtrl.CheckArenaUp()
	if	ArenaName~=TableBattleCtrl.GetBattleField(UIMainModule.ServerHomeData.user.trophy)	then	--如果竞技场升级
		ArenaName=TableBattleCtrl.GetBattleField(UIMainModule.ServerHomeData.user.trophy);
		log('.............Arena Changed');
		if	isLevelUpForArena==true	then	--如果同时玩家升级，修改标志位ArenaUpLater，玩家升级后再升战场
			UIMainModule.ArenaUpLater=true;
			isLevelUpForArena=false;
			log('........ player lv up......showArenaUp later');
		else
			if	FightModule.ServerFightResultData.is_rename == true	then	--重命名界面出现，稍后再做竞技场升级
				UIMainModule.ArenaUpLater=true;
				log('.........renaming.............showArenaUp later');
			else
				Game.ShowArena();
				log('.........no player up.............showArenaUp');
			end
		end
	end
end




------------------------------------------------------------------
--未使用，或已弃用方法--
------------------------------------------------------------------

--（已弃用，每次进主画面都会向服务器请求最新数据）战斗结束后，将战斗所获加到Module的ServerLoginData数据中
function UIMainCtrl.Recalculate_Gold_Trophy_After_Fight()	
	--log('...................................');
	--log('UILoginModule.ServerLoginData.user.trophy:'..UILoginModule.ServerLoginData.user.trophy);
	--log('FightModule.ServerFightResultData.award.trophy'..FightModule.ServerFightResultData.award.trophy);
	UIMainModule.ServerHomeData.user.trophy=UIMainModule.ServerHomeData.user.trophy + FightModule.ServerFightResultData.award.trophy; --奖杯
	if UIMainModule.ServerHomeData.user.trophy < 0 then
		UIMainModule.ServerHomeData.user.trophy = 0;
	end
	UIMainModule.ServerHomeData.user.g_money=UIMainModule.ServerHomeData.user.g_money + FightModule.ServerFightResultData.award.g_money; --金币
	--log('Recalculate_Gold_Trophy_After_Fight DES tROPHY:'..tostring(UILoginModule.ServerLoginData.user.trophy));
	--log('Recalculate_Gold_Trophy_After_Fight CHA tROPHY:'..tostring(FightModule.ServerFightResultData.award.trophy));
end

--关闭事件--
function UIMainCtrl.Close()
--[[	uiMgr:ClosePanel(CtrlNames.Prompt);--]]
end

--拖拽结束
function UIMainCtrl.OnDragEnd()
	log("OnDragEnd");
end

--(效果不好，弃用)TweenAlpha和Scale反复播放
function UIMainCtrl.Tween_Again_N_Again(go) 
	go:SetActive(false);
	go:GetComponent('TweenAlpha'):ResetToBeginning();
	go:GetComponent('TweenScale'):ResetToBeginning();
	go:GetComponent('TweenAlpha').enabled=true;
	--go:GetComponent('TweenScale').enabled=true;
	go:SetActive(true);
end

--测试用
function UIMainCtrl.OnTestBtn()
	
	log('..................'..UIMainModule.ServerHomeData.user.id.."newbie:"..utilMgr: PlayerPrefs_GetString_Lua(UIMainModule.ServerHomeData.user.id.."newbie","yesnewbie"));
end