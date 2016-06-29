require "Common/define"
require "3rd/pblua/auth_pb"
require "3rd/pblua/match_pb"
require "3rd/pblua/msg_pb"
require "protobuf"

Event = require 'events'

UILoginModule = {};
local this = UILoginModule;

--登录数据--
local ServerLoginData;
--构建函数--
function UILoginModule.New()
	logWarn("UILoginModule.New--->>");
	return this;
end

function UILoginModule.GetUserNick()
	--log('GetUserNick!'..ServerLoginData.user.nick);
	return UILoginModule.ServerLoginData.user.nick;
end

function UILoginModule.GetUserLevel()
	return UILoginModule.ServerLoginData.user.lv;
end

function UILoginModule.GetUserExp()
	return UILoginModule.ServerLoginData.user.exp;
end

function UILoginModule.GetUserLvUpExp()
	return UILoginModule.ServerLoginData.user.lvup_exp;
end

function UILoginModule.GetUserMoney()
	return UILoginModule.ServerLoginData.user.money;
end

function UILoginModule.GetUserGMoney()
	return UILoginModule.ServerLoginData.user.g_money;
end

function UILoginModule.GetUserTrouphy()
	return UILoginModule.ServerLoginData.user.trophy;
end

function UILoginModule.GetCards()
	return UILoginModule.ServerLoginData.cards;
end

function UILoginModule.GetLoginResult()
	return UILoginModule.ServerLoginData.result;
end

function UILoginModule.GetLoginisRegister()
	return UILoginModule.ServerLoginData.is_register;
end