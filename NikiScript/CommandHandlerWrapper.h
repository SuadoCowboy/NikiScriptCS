#pragma once

#include "DLLExport.h"
#include "CommandHandler.h"

#include "CommandWrapper.h"

extern "C" {
	// std::unordered_map<std::string, ns_Command> commands;
	NIKIAPI const char* ns_CommandHandlerGetCommandsNames();

	NIKIAPI ns_Command* ns_CommandHandlerGetCommand(const char* name);
	NIKIAPI bool ns_CommandHandlerAdd(const ns_Command& command);
	NIKIAPI void ns_CommandHandlerRemove(const std::string& name, struct ns_Context& ctx);
}