require "Common/define"
require "3rd/pblua/auth_pb"
require "3rd/pblua/match_pb"
require "3rd/pblua/msg_pb"
require "3rd/pblua/sys_pb"
require "protobuf"

Event = require 'events'

UIMainModule = {};
local this = UIMainModule;

--����������--
local ServerMatchData;

local ServerMatchedData;

local ServerCancelMatchData;

local ServerHomeData;

local SysMsgData;

local ArenaUpLater;

--��������--
function UIMainModule.New()
	logWarn("UIMainModule.New--->>");
	return this;
end

function UIMainModule.GetOpponentID()
	return UIMainModule.ServerMatchData.result;
end

--����ƥ�������Ϣ ServerMatchData--
function UIMainModule.GetMatchResult()
	return UIMainModule.ServerMatchData.result;
end

--匹配成功后的对手信息 ServerMatchedData--
function UIMainModule.GetOpponentNick()
	--log('GetOpponentNick!'..ServerMatchedData.opponent.nick);
	return UIMainModule.ServerMatchedData.opponent.nick;
end

function UIMainModule.GetOpponentTrophy()
	return UIMainModule.ServerMatchedData.opponent.trophy;
end

function UIMainModule.GetOpponentCity()
	log("opponent.city................."..UIMainModule.ServerMatchedData.opponent.city);
	return UIMainModule.ServerMatchedData.opponent.city;
end

function UIMainModule.GetOpponentFlag()
	log('opponent.country...............'..UIMainModule.ServerMatchedData.opponent.country);
	return UIMainModule.ServerMatchedData.opponent.country;
end

function UIMainModule.GetOpponentPhoto()
	log('opponent.id................'..UIMainModule.ServerMatchedData.opponent.id);
	return UIMainModule.ServerMatchedData.opponent.id;
end



function UIMainModule.GetStages()
	return UIMainModule.ServerMatchedData.stages;
end

--����ƥ��ɹ���Ϣ ServerCancelMatchData--
function UIMainModule.GetCancelMatchResult()
	return UIMainModule.ServerCancelMatchData.result;
end

--��ȡ�û��ǳƣ�����C#����
function UIMainModule.GetUserNick()
	return UIMainModule.ServerHomeData.user.nick;
end

--��ȡ�û�������������C#����
function UIMainModule.GetUserTrophy()
	return UIMainModule.ServerHomeData.user.trophy;
end

--获取玩家自身信息-国旗
function UIMainModule.GetUserFlag()
	--log("GetUserFlag.................."..UIMainModule.ServerHomeData.user.country);
	return UIMainModule.ServerHomeData.user.country;
end

--获取玩家自身信息-城市
function UIMainModule.GetUserCity()
	return UIMainModule.ServerHomeData.user.city;
end

--获取玩家自身信息-照片
function UIMainModule.GetUserPhoto()
	return UIMainModule.ServerHomeData.user.id;
end