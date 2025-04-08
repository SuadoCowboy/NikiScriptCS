#pragma once

#include <unordered_map>
#include <string>

#include "DLLExport.h"
#include "Command.h"

namespace ns {
	struct Context;

	struct NIKIAPI CommandHandler {
		std::unordered_map<std::string, Command> commands;

		Command* get(const std::string& name);
		/**
		 * @brief adds command to commands unordered_map
		 * 
		 * @param command name of the will be mapped in the commands variable
		 * @return true if command name is okay
		 * @return false if command with this name already exists
		 */
		bool add(const Command& command);

		void remove(const std::string& name, Context& ctx);
	};
}