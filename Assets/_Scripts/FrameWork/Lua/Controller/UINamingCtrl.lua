require "Common/define"
require "3rd/pblua/auth_pb"
require "3rd/pblua/match_pb"
require "3rd/pblua/msg_pb"
require "3rd/pblua/rename_pb"
require "protobuf"

Event = require 'events'

UINamingCtrl = {};
local this = UINamingCtrl;

local panel;
local UINamingBH;
local transform;
local gameObject;
local timer = 0;

--构建函数--
function UINamingCtrl.New()
	logWarn("UINamingCtrl.New--->>");
	return this;
end

function UINamingCtrl.Awake()
	log("UINamingCtrl.Awake--->>");
	uiMgr:ShowUI_LUA(MyFrameWork.E_UIType.UINamingPanel,this.OnCreate,false,false);
end

--启动事件--
function UINamingCtrl.OnCreate(obj)
	gameObject = obj;
	transform = obj.transform;
	UINamingBH = transform:GetComponent('MyFrameWork.LuaBehaviour');
	this.InitPanel();	
	loadingMgr:HideLoading();
end

--初始化面板--
function UINamingCtrl.InitPanel()
	local script = transform:GetComponent('BaseUI');
	script.mUIStyle = MyFrameWork.E_UIStyle.Main;
	script.mUILayertype = MyFrameWork.E_LayerType.MainUI;
	UINamingBH:AddClick(UINamingPanel.NamingBtn.gameObject,this.OnNamingClick);
	UINamingBH:AddClick(UINamingPanel.go_CloseBtn,this.OnCloseClick);
	Event.AddListener(tostring(msg_pb.SERVER_RENAME), this.SendNamingCBK);
	--UINamingPanel.label_Input.text=UIMainModule.ServerHomeData.user.nick; --更换表面值，点进去变空
	UINamingPanel.input_Input.value=UIMainModule.ServerHomeData.user.nick; --更换实际值，点进去就是这个值
	log('UINamingCtrl.InitPanel over');
end

function UINamingCtrl.OnUpdate()
end

--滚动项单击事件--
function UINamingCtrl.OnNamingClick(go)
	
	local playername=UINamingPanel.input_Input.value;
	if playername=="" then
		UINamingPanel.go_HintLabel:GetComponent('UILabel').text=tostring("昵称不能为空，请输入昵称");
		UINamingPanel.go_Hint:SetActive(false);
		UINamingPanel.go_Hint:GetComponent('TweenAlpha'):ResetToBeginning();
		UINamingPanel.go_Hint:GetComponent('TweenAlpha').enabled=true;
		UINamingPanel.go_Hint:GetComponent('TweenPosition'):ResetToBeginning();
		UINamingPanel.go_Hint:GetComponent('TweenPosition').enabled=true;
		UINamingPanel.go_Hint:SetActive(true);
		log('Not Sending NickName');
		else
		this.SendNaming(playername);
	end
	
	
	log('UINamingCtrl.OnUpdate()');
end

--关闭事件--
function UINamingCtrl.Close()
--[[	uiMgr:ClosePanel(CtrlNames.Prompt);--]]
end

function UINamingCtrl.SendNaming(name)
    local namelogin = rename_pb.ClientRename();
    namelogin.nick = name;
    local msg = namelogin:SerializeToString();
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(msg_pb.CLIENT_RENAME);
    buffer:WriteBuffer(msg);
    networkMgr:SendMessage(buffer);
	log(tostring(msg_pb.CLIENT_RENAME));
	log('SendNaming');
end

function UINamingCtrl.SendNamingCBK(buffer)
	local data = buffer:ReadBuffer();
    local msg = rename_pb.ServerRename();
    msg:ParseFromString(data);
	UINamingModule.ServerNamingData = msg;
	log('Resetname:'..msg.nick);
	
	UIMainModule.ServerHomeData.user.nick = msg.nick --把新昵称存起来
	UIMainPanel.label_playerName.text = UIMainModule.ServerHomeData.user.nick;--赋值给主页面名字

	if	UIMainModule.ArenaUpLater==true	then	--如果重命名和玩家升级同时发生，先重命名，再竞技场升级
		UIMainModule.ArenaUpLater=false;
		Game.ShowArena();
		log("showing arena by ReNaming................")
	end
	
	--隐藏UINaming
	UINamingPanel.gameObject:SetActive(false);
	log('SendNamingCBK');
end

--点关闭按钮
function UINamingCtrl.OnCloseClick(go)
	if	UIMainModule.ArenaUpLater==true	then	--如果重命名和玩家升级同时发生，先重命名，再竞技场升级
		UIMainModule.ArenaUpLater=false;
		Game.ShowArena();
		log("showing arena by closing UINaming................")
	end
	UINamingPanel.gameObject:SetActive(false);
	log('UINamingCtrl.OnCloseClick');
end