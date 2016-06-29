require "Common/define"
require "3rd/pblua/auth_pb"
require "3rd/pblua/match_pb"
require "3rd/pblua/msg_pb"
require "protobuf"

Event = require 'events'

UILoginCtrl = {};
local this = UILoginCtrl;

local panel;
local UILoginBH;
local transform;
local gameObject;
local timer = 0;

--��������--
function UILoginCtrl.New()
	logWarn("UILoginCtrl.New--->>");
	return this;
end

function UILoginCtrl.Awake()
	log("UILoginCtrl.Awake--->>");
	uiMgr:ShowUIAndCloseOthers_LUA(MyFrameWork.E_UIType.UILoginPanel,this.OnCreate);
end

--�����¼�--
function UILoginCtrl.OnCreate(obj)
	gameObject = obj;
	transform = obj.transform;
	UILoginBH = transform:GetComponent('MyFrameWork.LuaBehaviour');
	this.InitPanel();	
	loadingMgr:HideLoading();
end

--��ʼ�����--
function UILoginCtrl.InitPanel()
	local script = transform:GetComponent('BaseUI');
	script.mUIStyle = MyFrameWork.E_UIStyle.Main;
	script.mUILayertype = MyFrameWork.E_LayerType.MainUI;
	UILoginBH:AddClick(UILoginPanel.LoginBtn.gameObject,this.OnLoginClick);

	
	Event.AddListener(tostring(msg_pb.SERVER_LOGIN), this.SendLoginCBK);
	UILoginPanel.UIInput.value=gameMgr:GetLastInputID();
	log('UILoginCtrl.InitPanel over');
end

function UILoginCtrl.OnUpdate()
end

--��������¼�--
function UILoginCtrl.OnLoginClick(go)
	networkMgr:SendReConnect();
	local playername=uiMgr:GetInput();
	if	playername=="" then
		uiMgr:ShowMessageBox("用户名输入不能为空", "确定", null);
		log("用户名输入为空");
	else
		this.SendLogin(playername,"0","");	
	end
	log('UILoginCtrl.OnLoginClick()');
end

--�ر��¼�--
function UILoginCtrl.Close()
--[[	uiMgr:ClosePanel(CtrlNames.Prompt);--]]
end

function UILoginCtrl.GetAccount()
    return UILoginCtrl.account;
end

function UILoginCtrl.ReConnect()
	networkMgr:SendConnect();
end

function UILoginCtrl.ReSendLogin(channel_id,token)
    local login = auth_pb.ClientLogin();
    login.account = UILoginCtrl.account;
    login.channel_id=channel_id;
	login.token=token;
    local msg = login:SerializeToString();
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(msg_pb.CLIENT_LOGIN);
    buffer:WriteBuffer(msg);
    networkMgr:SendMessage(buffer);
	log(tostring(msg_pb.CLIENT_LOGIN));
end

function UILoginCtrl.SendLogin(name,channel_id,token)
    local login = auth_pb.ClientLogin();
    login.account = name;
	login.channel_id=channel_id;
	login.token=token;
	UILoginCtrl.account = name;
    local msg = login:SerializeToString();
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(msg_pb.CLIENT_LOGIN);
    buffer:WriteBuffer(msg);
    networkMgr:SendMessage(buffer);
    log(tostring(msg_pb.CLIENT_LOGIN)..":  SendLogin finished ");
end

function UILoginCtrl.SendLoginCBK(buffer)
	local data = buffer:ReadBuffer();
    local msg = auth_pb.ServerLogin();
    msg:ParseFromString(data);
	UILoginModule.ServerLoginData = msg;
	log("UILoginModule.ServerLoginData.result: "..UILoginModule.ServerLoginData.result);
	log("UILoginModule.ServerLoginData.is_register : "..UILoginModule.ServerLoginData.is_register);
	log('StartHeartBeat');
    networkMgr:StartHeartBeat();
	uiMgr:ChangeLevel(1);
	Game.ShowUIMain();
	log(tostring(msg.result)..'SendLoginCBK');
end



