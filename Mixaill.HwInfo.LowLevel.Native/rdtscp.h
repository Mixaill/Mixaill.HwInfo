#pragma once

#include <cstdint>

using namespace System;

namespace Mixaill::HwInfo::LowLevel::Native {
	public ref class Rdtsc
	{
	public:
		static uint64_t Timestamp();
		static uint64_t Test();
		static uint64_t Frequency();
	};

	class RdtscUnmanaged
	{
	public:
		RdtscUnmanaged() = delete;
		~RdtscUnmanaged() = delete;

		[[nodiscard]] static uint64_t Timestamp();
		static uint64_t Test();
		static uint64_t Frequency();
	};
}
