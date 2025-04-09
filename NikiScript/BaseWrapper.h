#pragma once

#include "DLLExport.h"

#include <stdint.h>

extern "C" {
	/**
	 * @param size includeint null-terminator
	 */
	NIKIAPI char* ns_newString(size_t size);
	NIKIAPI void ns_freeString(char* string);
}