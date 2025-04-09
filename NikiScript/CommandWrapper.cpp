#include "CommandWrapper.h"

#include "BaseWrapper.h"

#include <vector>
#include <string>
#include <cstring>

ns_Command* ns_newCommand(const char* name, uint8_t minArgs, uint8_t maxArgs, ns_CommandCallback callback, const char* description, const char* _argsDescriptions[]) {
	std::vector<std::string> argsDescriptions{};
	for (uint16_t i = 0; i < maxArgs*2; i += 2) {
		argsDescriptions.push_back(_argsDescriptions[i]);
		argsDescriptions.push_back(_argsDescriptions[i + 1]);
	}

	return new ns::Command(name, minArgs, maxArgs, callback, description, argsDescriptions);
}

void ns_deleteCommand(ns_Command* pCommand) {
	delete pCommand;
}

const char* ns_CommandAllocGetArgumentsNames(ns_Command* pCommand) {
	std::string temp = pCommand->getArgumentsNames();
	char* out = ns::newString(temp.size());
	memcpy(out, temp.c_str(), temp.size());

	return out;
}

// void ns_CommandPrintAsDataTree(ns_Command* pCommand) {}

// void ns_CommandSetName(ns_Command* pCommand, const char* name) {}
// const char* ns_CommandGetName(ns_Command* pCommand) {}

// void ns_CommandSetMinArgs(ns_Command* pCommand, uint8_t minArgs) {}
// uint8_t ns_CommandGetMinArgs(ns_Command* pCommand) {}

// void ns_CommandSetMaxArgs(ns_Command* pCommand, uint8_t maxArgs) {}
// uint8_t ns_CommandGetMaxArgs(ns_Command* pCommand) {}

// void ns_CommandSetCallback(ns_Command* pCommand, ns_CommandCallback callback) {}
// ns_CommandCallback ns_CommandGetCallback(ns_Command* pCommand) {}
	
// void ns_CommandSetDescription(ns_Command* pCommand, const char* description) {}
// const char* ns_CommandGetDescription(ns_Command* pCommand) {}

// void ns_CommandSetArgsDescriptions(ns_Command* pCommand, const char* argsDescriptions[]) {}
// const char* ns_CommandGetArgsDescription(ns_Command* pCommand, unsigned int index) {}

// uint8_t ns_CommandGetArgsDescriptionsSize(ns_Command* pCommand) {}