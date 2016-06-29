require "Common/define"
require "Common/functions"
require "3rd/pblua/auth_pb"
require "3rd/pblua/match_pb"
require "3rd/pblua/msg_pb"
require "3rd/pblua/fight_pb"
require "3rd/pblua/sys_pb"
Event = require 'events'


Network = {};
local this = Network;
--this.NoMoreNewbie = 1; --不能省略this，否则在其他Lua脚本引用Network.NoMoreNewbie会为空
local transform;
local gameObject;
local islogging = false;

function Network.Start() 
    --logWarn("Network.Start!!");          
	--Event.AddListener(tostring(msg_pb.TEST_FIGHT), this.SendLoginCBK);
	Event.AddListener(tostring(msg_pb.SERVER_ATTACKED),this.SendAttackedCBK);
	Event.AddListener(tostring(msg_pb.SERVER_FIGHT_RESULT),this.SendFightResultCBK);
    Event.AddListener(tostring(msg_pb.SERVER_RANK_CHANGE),this.SendRankChangeResultCBK);
	Event.AddListener(tostring(msg_pb.SERVER_RANK_UP),this.SendRankUpResultCBK);
    Event.AddListener(tostring(msg_pb.SERVER_HEARTBEAT),this.SendHeartBeatCBK);
	Event.AddListener(tostring(msg_pb.SERVER_FACE),this.SendFacedCBK); --服务器推送对手表情
end

--Socket消息--
function Network.OnSocket(key, data)
    Event.Brocast(tostring(key), data);
end

--当连接建立时--
function Network.OnConnect() 
    logWarn("Game Server connected!!");
end

--异常断线--
function Network.OnException() 
    islogging = false; 
    NetManager:SendConnect();
   	logError("OnException------->>>>");
end

--连接中断，或者被踢掉--
function Network.OnDisconnect() 
    islogging = false; 
    logError("OnDisconnect------->>>>");
end

--卸载网络监听--
function Network.Unload()
    logWarn('Unload Network...');
end

function Network.ClientHeartBeat()
    local heartBeat = sys_pb.ClientHeartbeat();
    local msg = heartBeat:SerializeToString();
    local buffer = ByteBuffer.New();
    buffer:WriteShort(msg_pb.CLIENT_HEARTBEAT);
    buffer:WriteBuffer(msg);
    networkMgr:SendMessage(buffer);
    log(tostring(msg_pb.CLIENT_HEARTBEAT));
end

function Network.SendHeartBeatCBK(buffer)
    log('receive msg SendHeartBeatCBK');
    local data = buffer:ReadBuffer();
    local msg = sys_pb.ServerHeartbeat();
    msg:ParseFromString(data);
    networkMgr:ReceiveHeartBeat();
end

--客户端发动攻击 c#send msg--
function Network.ClientAttack(id,damage,attack_type,skillId,param1,param2)
	
    local attackinfo = fight_pb.ClientAttack();
    attackinfo.boss_id = id;
    attackinfo.harm = damage;
    attackinfo.attack_type = attack_type;
	attackinfo.skillId = skillId;
	attackinfo.param1 = param1;
	attackinfo.param2 = param2;
    local msg = attackinfo:SerializeToString();
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(msg_pb.CLIENT_ATTACK);
    buffer:WriteBuffer(msg);
    networkMgr:SendMessage(buffer);
    log(tostring(msg_pb.CLIENT_ATTACK));
	
end

--服务器回调: 被攻击回调--
function Network.SendAttackedCBK(buffer)
	log('receive msg SendAttackedCBK');
	local data = buffer:ReadBuffer();
    local msg = fight_pb.ServerAttacked();
    msg:ParseFromString(data);
	FightModule.ServerFightData=msg;
	fightDataMgr:OnFightCBK(msg.boss_id,msg.hp,msg.harm,msg.attack_type,msg.skillId,msg.param1,msg.param2);
	--服务器回调后执行的方法
end

--服务器关于战斗结果的回调--
function Network.SendFightResultCBK(buffer)
	log('receive msg SendFightResultCBK');
	local data = buffer:ReadBuffer();
    local msg = fight_pb.ServerFightResult();
    msg:ParseFromString(data);
	FightModule.ServerFightResultData=msg; 
	fightDataMgr:OnFightResultCBK(msg);
	if msg.is_rename == true then
		fightDataMgr.isRename = true;
		--Network.NoMoreNewbie = 2;
		utilMgr:PlayerPrefs_SetString_Lua(UIMainModule.ServerHomeData.user.id.."newbie","noMore");
		else
		fightDataMgr.isRename = false;
	end
	log('Parse msg SendFightResultCBK over');
	--服务器回调后执行的方法
end

--服务器关于排名变化的回调--
function Network.SendRankChangeResultCBK(buffer)
    log('receive msg SendRankChangeResultCBK');
    local data = buffer:ReadBuffer();
    local msg = fight_pb.ServerRankChange();
    msg:ParseFromString(data);
    FightModule.ServerRankChangeData=msg;
    fightDataMgr:OnRankChangeCBK(msg);
    log('Parse msg SendRankChangeResultCBK over');
    --服务器回调后执行的方法
end

--服务器关于排名大幅度提升的回调 SendRankUpResultCBK
function Network.SendRankUpResultCBK(buffer)
    log('receive msg SendRankUpResultCBK');
    local data = buffer:ReadBuffer();
    local msg = fight_pb.ServerRankUp();
    msg:ParseFromString(data);
    FightModule.ServerRankUpData=msg;
    fightDataMgr:OnRankUpCBK(msg);
    log('Parse msg SendRankUpResultCBK over');
    --服务器回调后执行的方法
end

--客户端一键胜利 c#send msg--
function Network.ClientCheat(result)
    local attackinfo = fight_pb.ClientAttack();
    attackinfo.boss_id = id;
    attackinfo.harm = damage;
    local msg = attackinfo:SerializeToString();
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(msg_pb.CLIENT_ATTACK);
    buffer:WriteBuffer(msg);
    networkMgr:SendMessage(buffer);
    log(tostring(msg_pb.CLIENT_ATTACK));
end


--客户端表情 c#send msg--
function Network.ClientFacing(id)
	
    local faceinfo = fight_pb.ClientFace();
    faceinfo.face_id = id;
    local msg = faceinfo:SerializeToString();
    ----------------------------------------------------------------
    local buffer = ByteBuffer.New();
    buffer:WriteShort(msg_pb.CLIENT_FACE);
    buffer:WriteBuffer(msg);
    networkMgr:SendMessage(buffer);
    log(tostring(msg_pb.CLIENT_FACE));
	
end

--服务器回调: 对方发表情--
function Network.SendFacedCBK(buffer)
	log('receive msg SendFacedCBK');
	local data = buffer:ReadBuffer();
    local msg = fight_pb.ServerFace();
    msg:ParseFromString(data);
	FightModule.ServerFaceData=msg;
	fightDataMgr:OnFaceCBK(msg.face_id);
	
	--服务器回调后执行的方法
end