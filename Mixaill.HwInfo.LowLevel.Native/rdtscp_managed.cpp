//
// Includes
//

#include "rdtscp.h"



//
// Functions
//

namespace Mixaill::HwInfo::LowLevel::Native {
	uint64_t Rdtsc::Timestamp()
	{
		return RdtscUnmanaged::Timestamp();
	}

	uint64_t Rdtsc::Test()
	{
		return RdtscUnmanaged::Test();
	}

	uint64_t Rdtsc::Frequency() {
		return RdtscUnmanaged::Frequency();
	}
}
 