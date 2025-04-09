#pragma once

#include "DLLExport.h"
#include "Command.h"

typedef ns::CommandCallback ns_CommandCallback;
typedef ns::Command ns_Command;

extern "C" {
	// struct Command {
	// 	std::string name;
	// 	uint8_t minArgs = 0, maxArgs = 0;
	// 	CommandCallback callback = nullptr;
	// 	std::string description;

	// 	/**
	// 	 * @note odd = name
	// 	 * @note even = description
	// 	 */
	// 	std::vector<std::string> argsDescriptions{};

	NIKIAPI ns_Command* ns_newCommand(const char* name, uint8_t minArgs, uint8_t maxArgs, ns_CommandCallback callback, const char* description, const char* argsDescriptions[]);
	NIKIAPI void ns_deleteCommand(ns_Command* pCommand);

	NIKIAPI char* ns_CommandAllocGetArgumentsNames(ns_Command* pCommand);

	NIKIAPI void ns_CommandPrintAsDataTree(ns_Command* pCommand);

	NIKIAPI void ns_CommandSetName(ns_Command* pCommand, const char* name);
	NIKIAPI char* ns_CommandAllocGetName(ns_Command* pCommand);

	NIKIAPI void ns_CommandSetMinArgs(ns_Command* pCommand, uint8_t minArgs);
	NIKIAPI uint8_t ns_CommandGetMinArgs(ns_Command* pCommand);

	NIKIAPI void ns_CommandSetMaxArgs(ns_Command* pCommand, uint8_t maxArgs);
	NIKIAPI uint8_t ns_CommandGetMaxArgs(ns_Command* pCommand);

	NIKIAPI void ns_CommandSetCallback(ns_Command* pCommand, ns_CommandCallback callback);
	NIKIAPI ns_CommandCallback ns_CommandGetCallback(ns_Command* pCommand);
	
	NIKIAPI void ns_CommandSetDescription(ns_Command* pCommand, const char* description);
	NIKIAPI char* ns_CommandAllocGetDescription(ns_Command* pCommand);

	NIKIAPI void ns_CommandSetArgsDescriptions(ns_Command* pCommand, const char* argsDescriptions[]);
	NIKIAPI char* ns_CommandAllocGetArgDescription(ns_Command* pCommand, uint16_t index);

	NIKIAPI uint16_t ns_CommandGetArgsDescriptionsSize(ns_Command* pCommand);
}