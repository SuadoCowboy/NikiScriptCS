#include "BaseWrapper.h"

char* ns_newString(size_t size) {
	char* string = new char[size];
	string[size] = '\0';
	return string;
}

void ns_freeString(char* string) {
	if (string != nullptr) {
		delete[] string;
		string = nullptr;
	}
}