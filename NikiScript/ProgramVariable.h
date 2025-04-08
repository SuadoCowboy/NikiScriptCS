#pragma once

#include <string>
#include <sstream>
#include <unordered_map>

namespace ns {
	struct ProgramVariable;
	struct Context;

	typedef std::string(*GetProgramVariableValue)(Context& ctx, ProgramVariable* pVar);
	typedef void(*SetProgramVariableValue)(Context& ctx, ProgramVariable* pVar, const std::string& str);
	
	struct Context;

	struct ProgramVariable {
		void* pValue = nullptr;
		std::string description;

		GetProgramVariableValue get = nullptr;
		SetProgramVariableValue set = nullptr;

		ProgramVariable();
		ProgramVariable(void* pVariable, const std::string& description, const GetProgramVariableValue& get, const SetProgramVariableValue& set);
	};

	typedef std::unordered_map<std::string, ProgramVariable> ProgramVariables;

	std::string getString(Context&, ProgramVariable* pVar);
	void setString(Context&, ProgramVariable* pVar, const std::string& str);

	template<typename T>
	std::string getNumber(Context&, ProgramVariable* pVar) {
		return std::to_string(*static_cast<T*>(pVar->pValue));
	}

	/**
	 * @brief uses std::stoul so any number below that can be used
	 * @tparam T number type
	 * @param ctx
	 * @param pVar 
	 * @param str
	 */
	template<typename T>
	void setUnsigned(Context&, ProgramVariable* pVar, const std::string& str)
	#if BUILD_SHARED == 1 && BUILD_EXPORT == 1
	;
	#else
	{
		try {
			*static_cast<T*>(pVar->pValue) = (T)std::stoul(str);
		} catch (...) {}
	}
	#endif

	void setUnsignedLongLong(Context&, ProgramVariable* pVar, const std::string& str);

	void setFloat(Context&, ProgramVariable* pVar, const std::string& str);
	void setDouble(Context&, ProgramVariable* pVar, const std::string& str);
	void setLongDouble(Context&, ProgramVariable* pVar, const std::string& str);

	void setChar(Context&, ProgramVariable* pVar, const std::string& str);
	void setShort(Context&, ProgramVariable* pVar, const std::string& str);
	void setInteger(Context&, ProgramVariable* pVar, const std::string& str);
	void setLong(Context&, ProgramVariable* pVar, const std::string& str);
	void setLongLong(Context&, ProgramVariable* pVar, const std::string& str);
}