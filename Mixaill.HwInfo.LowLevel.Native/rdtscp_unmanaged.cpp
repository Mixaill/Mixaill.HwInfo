//
// Includes
//

#include <intrin.h>
#include <Windows.h>

#include "rdtscp.h"



//
// Functions
//

namespace Mixaill::HwInfo::LowLevel::Native {

#pragma managed(push, off)

    uint64_t round_smart(uint64_t i, uint64_t nearest)
    {
        if (nearest <= 0 || nearest % 10 != 0) {
            return 0;
        }

        return (i + 5 * nearest / 10) / nearest * nearest;
    }

    uint64_t RdtscUnmanaged::Timestamp(void) {
        unsigned int ui;
        return __rdtscp(&ui);
    }

    uint64_t RdtscUnmanaged::Frequency(void) {
        uint64_t frequency = 0;
        size_t repeats = 100;

        //get QPC freq
        LARGE_INTEGER Frequency{};
        QueryPerformanceFrequency(&Frequency);

        for (size_t i = 0; i < repeats; i++)
        {
            LARGE_INTEGER qpc_ts_1{};
            LARGE_INTEGER qpc_ts_2{};
            LARGE_INTEGER qpc_ts_target{};


            uint64_t rdtsc_start = Timestamp();

            QueryPerformanceCounter(&qpc_ts_1);
            qpc_ts_target.QuadPart = qpc_ts_1.QuadPart + Frequency.QuadPart / 1000;

            uint64_t tsc_1 = Timestamp();

            while (qpc_ts_2.QuadPart < qpc_ts_target.QuadPart) {
                QueryPerformanceCounter(&qpc_ts_2);
            }

            uint64_t rdtsc_stop = Timestamp();
            uint64_t ticks_ms = rdtsc_stop - rdtsc_start;
            if (i == 0 || ticks_ms < frequency)
                frequency = ticks_ms;
        }

        return round_smart(frequency, 100000) * 1000;
    }

    uint64_t RdtscUnmanaged::Test() {
        //warmup
        for (int i = 0; i < 100; i++)
        {
            Timestamp();
        }

        //count
        uint64_t count = 0;
        uint64_t doneTick = Timestamp() + Frequency();

        while (Timestamp() <= doneTick)
        {
            count++;
        }

        return count;
    }
}

#pragma managed(pop)
