#pragma once

#include "DLLExport.h"

#include <stdint.h>

#ifdef __cplusplus
extern "C" {
#endif

// Match the PrintLevel enum
typedef enum ns_PrintLevel {
    PrintLevel_DEFAULT = 0,
    PrintLevel_ECHO,
    PrintLevel_WARNING,
    PrintLevel_ERROR
} ns_PrintLevel;

typedef void(*ns_PrintCallback)(void* pData, ns_PrintLevel level, const char* message);

// ns::setPrintCallback
NIKIAPI void ns_setPrintCallback(void* pData, ns_PrintCallback callback);

// wraps ns::print
NIKIAPI void ns_Print(ns_PrintLevel level, const char* message);

// ns::printUnknownCommand
NIKIAPI void ns_printUnknownCommand(const char* command);

// ns::printLevelToString
NIKIAPI const char* ns_printLevelToString(ns_PrintLevel level);

#ifdef __cplusplus
}
#endif
