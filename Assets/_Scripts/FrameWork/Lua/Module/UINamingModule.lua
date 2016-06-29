require "Common/define"
require "3rd/pblua/auth_pb"
require "3rd/pblua/match_pb"
require "3rd/pblua/msg_pb"
require "protobuf"

Event = require 'events'

UINamingModule = {};
local this = UINamingModule;

--起名数据--
local ServerNamingData;

--构建函数--
function UINamingModule.New()
	logWarn("UINamingModule.New--->>");
	return this;
end
