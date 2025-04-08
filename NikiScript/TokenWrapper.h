#pragma once

#include <stdint.h>

#include "DLLExport.h"
#include "Token.h"

typedef ns::TokenType ns_TokenType;
typedef ns::Token ns_Token;

#ifdef __cplusplus
extern "C" {
#endif

// Create/Destroy
NIKIAPI ns_Token* ns_newToken(ns_TokenType type, const char* value);
NIKIAPI void ns_deleteToken(ns_Token* token);

NIKIAPI ns_TokenType ns_getTokenType(ns_Token* pToken);
NIKIAPI void ns_setTokenType(ns_Token* pToken, ns_TokenType type);

// Value Get/Set
NIKIAPI const char* ns_getTokenValue(ns_Token* token);
NIKIAPI void ns_setTokenValue(ns_Token* token, const char* value);

// Reference count
NIKIAPI size_t ns_getTokenReferenceCount(ns_Token* token);

// Get reference at index
NIKIAPI uint64_t ns_getTokenReferenceIndex(ns_Token* token, size_t i);
NIKIAPI const char* ns_getTokenReferenceString(ns_Token* token, size_t i);

// Add a reference
NIKIAPI void ns_addTokenReference(ns_Token* token, uint64_t index, const char* str);

// Clear references
NIKIAPI void ns_clearTokenReferences(ns_Token* token);

#ifdef __cplusplus
}
#endif