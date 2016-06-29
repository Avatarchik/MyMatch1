require "Common/define"
require "3rd/pblua/auth_pb"
require "3rd/pblua/match_pb"
require "3rd/pblua/msg_pb"
require "protobuf"

Event = require 'events'

UIRankModule = {};
local this = UIRankModule;

--��������--
function UIRankModule.New()
	logWarn("UIRankModule.New--->>");
	return this;
end






