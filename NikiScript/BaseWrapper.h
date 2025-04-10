#pragma once

#include <stdint.h>
#include <string>

#include "DLLExport.h"

namespace ns {
	/**
	 * @brief should be used when a string is created in the function scope and not passed to the function as a parameter
	 * @param size not counting null-terminator as it's added automatically
	 */
	char* allocStringToCharString(const std::string& src);
}

extern "C" {
	NIKIAPI void ns_freeCharString(char* string);
}